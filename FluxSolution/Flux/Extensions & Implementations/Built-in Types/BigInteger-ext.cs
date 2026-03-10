namespace Flux
{
  public static partial class BigIntegerExtensions
  {
    extension(System.Numerics.BigInteger)
    {
      #region BinarySearchRootN

      public static System.Numerics.BigInteger BinarySearchRootN(System.Numerics.BigInteger value, int n)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(value);
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);

        if (value == 0 || value == 1 || n == 1)
          return value;

        var low = System.Numerics.BigInteger.One;
        var high = value;
        var result = System.Numerics.BigInteger.One;

        while (low <= high)
        {
          var mid = (low + high) / 2;
          var midPow = System.Numerics.BigInteger.Pow(mid, n);

          if (midPow == value)
            return mid;
          else if (midPow < value)
          {
            result = mid;
            low = mid + 1;
          }
          else
          {
            high = mid - 1;
          }
        }

        return result;
      }

      #endregion

      #region FitSmallestIntegerType

      //public static object FitSmallestIntegerType(System.Numerics.BigInteger value)
      //{
      //  if (value >= sbyte.MinValue && value <= sbyte.MaxValue) return (sbyte)value;
      //  else if (value >= byte.MinValue && value <= byte.MaxValue) return (byte)value;
      //  else if (value >= short.MinValue && value <= short.MaxValue) return (short)value;
      //  else if (value >= ushort.MinValue && value <= ushort.MaxValue) return (ushort)value;
      //  else if (value >= int.MinValue && value <= int.MaxValue) return (int)value;
      //  else if (value >= uint.MinValue && value <= uint.MaxValue) return (uint)value;
      //  else if (value >= long.MinValue && value <= long.MaxValue) return (long)value;
      //  else if (value >= ulong.MinValue && value <= ulong.MaxValue) return (ulong)value;
      //  else if (value >= Int128.MinValue && value <= Int128.MaxValue) return (Int128)value;
      //  else if (value >= UInt128.MinValue && value <= UInt128.MaxValue) return (UInt128)value;
      //  else return value;
      //}

      #endregion

      #region IsPrimeProbabilistic (Miller-Rabin probabilistic primality test)

      /// <summary>
      /// <para>This implementation uses a Miller-Rabin probabilistic algorithm.</para>
      /// </summary>
      /// <param name="n"></param>
      /// <param name="k">Log(bit-length, 1.17) yields an approximately 15 iterations @ 10 bits, 30 @ 100, 44 @ 1000, 59 @ 10000, and can be lowered for a higher iteration (k) count. The lower the base, the higher the count.</param>
      /// <returns></returns>
      public static bool IsPrimeProbabilistic(System.Numerics.BigInteger n, int k)
        => MillerRabinProbabilisticIsPrime(n, k);

      /// <summary>
      /// <para>Probabilistic Miller–Rabin primality test with parallel rounds.</para>
      /// </summary>
      /// <param name="n"></param>
      /// <param name="k">Log(bit-length, 1.17) yields an approximately 15 iterations @ 10 bits, 30 @ 100, 44 @ 1000, 59 @ 10000, and can be lowered for a higher iteration (k) count. The lower the base, the higher the count.</param>
      /// <returns></returns>
      private static bool MillerRabinProbabilisticIsPrime(System.Numerics.BigInteger n, int k)
      {
        if (n <= 3) return n == 2 || n == 3;
        if ((n % 2).IsZero) return false;

        // Write n-1 as d*2^r
        var d = n - 1;
        while (d % 2 == 0) d /= 2;

        var isPrime = true;
        var lockObj = new object();

        System.Threading.Tasks.Parallel.For(0, k, (i, state) =>
        {
          if (!isPrime) { state.Stop(); return; }

          System.Numerics.BigInteger a; // Random base in [2, n-2]

          lock (lockObj) { a = System.Random.Shared.NextNumber(2, n - 2); }

          if (!MillerRabinProbabilisticTest(d, n, a))
          {
            lock (lockObj) { isPrime = false; }

            state.Stop();
          }
        });

        return isPrime;
      }

      /// <summary>
      /// <para>Miller–Rabin test for a single base</para>
      /// </summary>
      /// <param name="d"></param>
      /// <param name="n"></param>
      /// <param name="a"></param>
      /// <returns></returns>
      private static bool MillerRabinProbabilisticTest(System.Numerics.BigInteger d, System.Numerics.BigInteger n, System.Numerics.BigInteger a)
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

      #endregion

      #region NewtonRaphsonRootN

      public static System.Numerics.BigInteger NewtonRaphsonRootN(System.Numerics.BigInteger value, int n)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(value);
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);

        if (value == 0 || value == 1 || n == 1)
          return value;

        var guess = System.Numerics.BigInteger.One << (int)(System.Numerics.BigInteger.Log(value) / n); // Initial guess: 2^(log2(x)/n)

        while (true)
        {
          var previousGuess = guess;

          var t = System.Numerics.BigInteger.Pow(guess, n - 1);

          if (t == 0)
            throw new System.ArithmeticException(); // Avoid division by zero (shouldn't happen for valid inputs).

          guess = ((n - 1) * guess + value / t) / n; // Newton iteration.

          if (System.Numerics.BigInteger.Abs(guess - previousGuess) <= 1) // Adjust to ensure r^n <= x
          {
            while (System.Numerics.BigInteger.Pow(guess + 1, n) <= value)
              guess++;

            while (System.Numerics.BigInteger.Pow(guess, n) > value)
              guess--;

            return guess;
          }
        }
      }

      #endregion
    }

    extension(System.Numerics.BigInteger source)
    {
      /// <summary>Returns either the built-in byte array, or if a zero byte padding is present, a byte array excluding the zero byte is returned.</summary>
      public byte[] ToByteArrayEx(out int length)
      {
        var byteArray = source.ToByteArray();

        length = byteArray.Length - 1;

        if (length > 0 && byteArray[length] == 0)
          System.Array.Resize(ref byteArray, length);
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
  }
}
