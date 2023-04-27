namespace Flux
{
#if NET7_0_OR_GREATER
  public class SplitFactorial<TSelf>
    : IFactorialComputable<TSelf>
    where TSelf : System.Numerics.IBinaryInteger<TSelf>
#else
  public class SplitFactorial
    : IFactorialComputable<System.Numerics.BigInteger>
#endif
  {
#if NET7_0_OR_GREATER

    public TSelf ComputeFactorial(TSelf source)
    {
      if (TSelf.IsNegative(source))
        return -ComputeFactorial(TSelf.Abs(source));

      if (source <= TSelf.One)
        return TSelf.One;

      var p = TSelf.One;
      var r = TSelf.One;
      var currentN = TSelf.One;

      TSelf h = TSelf.Zero, shift = TSelf.Zero, high = TSelf.One;
      var log2n = source.ILog2(); // System.Nu merics.BigInteger.Log(n);

      while (h != source)
      {
        shift += h;
        h = source >> log2n--;
        TSelf len = high;
        high = (h - TSelf.One) | TSelf.One;
        len = (high - len).Divide(2);

        if (len > TSelf.Zero)
        {
          p *= Product(len);
          r *= p;
        }
      }

      return r << int.CreateChecked(shift);

      TSelf Product(TSelf n)
        => (n >> 1) is var m && (m == TSelf.Zero) ? (currentN += TSelf.One + TSelf.One) : (n == (TSelf.One << 1)) ? (currentN += TSelf.One + TSelf.One) * (currentN += TSelf.One + TSelf.One) : Product(n - m) * Product(m);
    }

#else

    public System.Numerics.BigInteger ComputeFactorial(System.Numerics.BigInteger source)
    {
      if (source < System.Numerics.BigInteger.Zero)
        return -ComputeFactorial(-source);

      if (source <= System.Numerics.BigInteger.One)
        return 1;

      var p = System.Numerics.BigInteger.One;
      var r = System.Numerics.BigInteger.One;
      var currentN = System.Numerics.BigInteger.One;

      System.Numerics.BigInteger h = System.Numerics.BigInteger.Zero, shift = System.Numerics.BigInteger.Zero, high = System.Numerics.BigInteger.One;
      var log2n = source.ILog2(); // System.Nu merics.BigInteger.Log(n);

      while (h != source)
      {
        shift += h;
        h = source >> log2n--;
        System.Numerics.BigInteger len = high;
        high = (h - System.Numerics.BigInteger.One) | System.Numerics.BigInteger.One;
        len = (high - len) / 2;

        if (len > System.Numerics.BigInteger.Zero)
        {
          p *= Product(len);
          r *= p;
        }
      }

      return r << (int)shift;

      System.Numerics.BigInteger Product(System.Numerics.BigInteger n)
        => (n >> 1) is var m && (m == System.Numerics.BigInteger.Zero) ? (currentN += 2) : (n == (System.Numerics.BigInteger.One << 1)) ? (currentN += 2) * (currentN += 2) : Product(n - m) * Product(m);
    }

#endif
  }
}
