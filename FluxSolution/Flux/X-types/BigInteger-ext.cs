namespace Flux
{
  public static partial class BigIntegerExtensions
  {
    extension(System.Numerics.BigInteger)
    {
      public static bool IsProbablePrime(System.Numerics.BigInteger value)
      {
        var log = System.Numerics.BigInteger.Log(value.GetBitLength(), 1.17);

        return IsProbablePrime(value, int.CreateChecked(log));
      }

      /// <summary>
      /// <para>Computes the square-root of a value.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns>The square-root of the value.</returns>
      public static System.Numerics.BigInteger Sqrt(System.Numerics.BigInteger value)
        => RootN(value, 2);

      /// <summary>
      /// <para>Computes the nth-root of a value.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="n"></param>
      /// <returns>The nth-root of the value.</returns>
      public static System.Numerics.BigInteger RootN(System.Numerics.BigInteger value, int n)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(value);
        System.ArgumentOutOfRangeException.ThrowIfLessThan(n, 2);

        if (value <= System.Numerics.BigInteger.One) // 0 = 0, 1 = 1.
          return value;

        return RootN(value, n, 101);
      }
    }

    extension(System.Numerics.BigInteger source)
    {
      /// <summary>Returns either the built-in byte array, or if a zero byte padding is present, a byte array excluding the zero byte is returned.</summary>
      public byte[] ToByteArrayEx(out int length)
      {
        var byteArray = source.ToByteArray();

        length = byteArray.Length - 1;

        if (length > 0 && byteArray[length] == 0)
          Array.Resize(ref byteArray, length);
        else
          length++;

        return byteArray;
      }

      /// <summary>This is essentially the same as the native ToByteArray (which is even called from this extension method) with the addition of the most significant byte index and its value as out parameters.</summary>
      public byte[] ToByteArrayEx(out int msbIndex, out byte msbValue)
      {
        var byteArray = source.ToByteArray();

        msbIndex = byteArray.Length - 1;
        msbValue = byteArray[msbIndex];

        if (msbIndex > 0 && msbValue == 0)
          msbValue = byteArray[--msbIndex];

        return byteArray;
      }
    }

    #region Primality test (Miller Rabin)

    /// <summary>
    /// <para>Miller–Rabin test for a single base</para>
    /// </summary>
    /// <param name="d"></param>
    /// <param name="n"></param>
    /// <param name="a"></param>
    /// <returns></returns>
    private static bool MillerRabinTest(System.Numerics.BigInteger d, System.Numerics.BigInteger n, System.Numerics.BigInteger a)
    {
      var x = System.Numerics.BigInteger.ModPow(a, d, n);

      if (x == 1 || x == n - 1) return true;

      while (d != n - 1)
      {
        x = (x * x) % n;
        d *= 2;

        if (x == 1) return false;
        if (x == n - 1) return true;
      }

      return false;
    }

    /// <summary>
    /// <para>Probabilistic Miller–Rabin with parallel rounds.</para>
    /// </summary>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    private static bool IsProbablePrime(System.Numerics.BigInteger n, int k)
    {
      if (n < 2) return false;
      if (n == 2 || n == 3) return true;
      if (n % 2 == 0) return false;

      // Write n-1 as d*2^r
      var d = n - 1;
      while (d % 2 == 0) d /= 2;

      var isPrime = true;
      var lockObj = new object();

      System.Threading.Tasks.Parallel.For(0, k, (i, state) =>
      {
        if (!isPrime) { state.Stop(); return; }

        // Random base in [2, n-2]
        System.Numerics.BigInteger a;

        lock (lockObj) { a = System.Random.Shared.NextBigInteger(2, n - 2); }

        if (!MillerRabinTest(d, n, a))
        {
          lock (lockObj) { isPrime = false; }

          state.Stop();
        }
      });

      return isPrime;
    }


    #endregion

    #region RootN helpers

    private static System.Numerics.BigInteger RootN(System.Numerics.BigInteger value, int nth, int iterationThreshold)
    {
      checked
      {
        var oldValue = System.Numerics.BigInteger.Zero;
        var newValue = RoughRootN(value, nth);

        for (var i = 0; System.Numerics.BigInteger.Abs(newValue - oldValue) >= 1 && i < iterationThreshold; i++) // I limited iterations to 100, but you may want way less
        {
          oldValue = newValue;
          newValue = ((nth - 1) * oldValue + (value / System.Numerics.BigInteger.Pow(oldValue, nth - 1))) / nth;
        }

        return newValue;
      }
    }

    private static System.Numerics.BigInteger RoughRootN(System.Numerics.BigInteger value, int nth)
    {
      var bytes = value.ToByteArray();

      checked
      {
        var bits = (bytes.Length - 1) * 8;  // Bit count in all but MSB.

        bits += bytes[^1].GetBitLength(); // Add bit length of MSB.

        var rootBits = bits / nth + 1;   // Bit count in the root.

        var rootBytes = rootBits / 8 + 1;   // Byte count in the root.

        var rootMod8 = rootBits % 8;

        var padByte = (rootMod8 == 7) ? 1 : 0; // If high bit (MSb) is set, add blank byte for BigInteger to avoid a negative result.

        var rootArray = new byte[rootBytes + padByte];

        rootArray[rootBytes - 1] = (byte)(1 << rootMod8); // Set the MSB (excluding blank byte if present).

        return new System.Numerics.BigInteger(rootArray);
      }
    }

    #endregion
  }
}
