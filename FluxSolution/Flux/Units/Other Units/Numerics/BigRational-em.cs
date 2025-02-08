namespace Flux
{
  public static partial class Em
  {
    public static Units.BigRational ToBigRational<TSelf>(this TSelf value, int maxApproximationIterations = 101)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      if (TSelf.IsZero(value)) return Units.BigRational.Zero;
      if (TSelf.IsInteger(value)) return new Units.BigRational(System.Numerics.BigInteger.CreateChecked(value));

      var Am = (Item1: System.Numerics.BigInteger.Zero, Item2: System.Numerics.BigInteger.One);
      var Bm = (Item1: System.Numerics.BigInteger.One, Item2: System.Numerics.BigInteger.Zero);

      System.Numerics.BigInteger A;
      System.Numerics.BigInteger B;

      var a = System.Numerics.BigInteger.Zero;
      var b = System.Numerics.BigInteger.Zero;

      if (value > TSelf.One)
      {
        var xW = TSelf.Truncate(value);

        var ar = ToBigRational(value - xW, maxApproximationIterations);

        return ar + System.Numerics.BigInteger.CreateChecked(xW);
      }

      for (var counter = 0; counter < maxApproximationIterations && !TSelf.IsZero(value); counter++)
      {
        var r = TSelf.One / value;
        var rR = TSelf.Round(r);

        var rT = System.Numerics.BigInteger.CreateChecked(rR);

        A = Am.Item2 + rT * Am.Item1;
        B = Bm.Item2 + rT * Bm.Item1;

        if (double.IsInfinity(double.CreateChecked(A)) || double.IsInfinity(double.CreateChecked(B)))
          break;

        a = A;
        b = B;

        Am = (A, Am.Item1);
        Bm = (B, Bm.Item1);

        value = r - rR;
      }

      return new(a, b);
    }
  }
}
