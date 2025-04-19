namespace Flux
{
  public static partial class GenericMath

  {
    public static Numerics.BigRational ToBigRational<TNumber>(this TNumber value, int maxApproximationIterations = 101)
      where TNumber : System.Numerics.IFloatingPoint<TNumber>
    {
      if (TNumber.IsZero(value)) return Numerics.BigRational.Zero;
      if (TNumber.IsInteger(value)) return new Numerics.BigRational(System.Numerics.BigInteger.CreateChecked(value));

      var Am = (Item1: System.Numerics.BigInteger.Zero, Item2: System.Numerics.BigInteger.One);
      var Bm = (Item1: System.Numerics.BigInteger.One, Item2: System.Numerics.BigInteger.Zero);

      System.Numerics.BigInteger A;
      System.Numerics.BigInteger B;

      var a = System.Numerics.BigInteger.Zero;
      var b = System.Numerics.BigInteger.Zero;

      if (value > TNumber.One)
      {
        var xW = TNumber.Truncate(value);

        var ar = ToBigRational(value - xW, maxApproximationIterations);

        return ar + System.Numerics.BigInteger.CreateChecked(xW);
      }

      for (var counter = 0; counter < maxApproximationIterations && !TNumber.IsZero(value); counter++)
      {
        var r = TNumber.One / value;
        var rR = TNumber.Round(r);

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
