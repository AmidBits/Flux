namespace Flux
{
  public static partial class FloatingPoint

  {
    public static Numerics.BigRational ToBigRational<TFloat>(this TFloat value, int maxApproximationIterations = 101)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      if (TFloat.IsZero(value)) return Numerics.BigRational.Zero;
      if (TFloat.IsInteger(value)) return new Numerics.BigRational(System.Numerics.BigInteger.CreateChecked(value));

      var Am = (Item1: System.Numerics.BigInteger.Zero, Item2: System.Numerics.BigInteger.One);
      var Bm = (Item1: System.Numerics.BigInteger.One, Item2: System.Numerics.BigInteger.Zero);

      System.Numerics.BigInteger A;
      System.Numerics.BigInteger B;

      var a = System.Numerics.BigInteger.Zero;
      var b = System.Numerics.BigInteger.Zero;

      if (value > TFloat.One)
      {
        var xW = TFloat.Truncate(value);

        var ar = ToBigRational(value - xW, maxApproximationIterations);

        return ar + System.Numerics.BigInteger.CreateChecked(xW);
      }

      for (var counter = 0; counter < maxApproximationIterations && !TFloat.IsZero(value); counter++)
      {
        var r = TFloat.One / value;
        var rR = TFloat.Round(r);

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
