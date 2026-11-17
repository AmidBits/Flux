namespace Flux
{
  public static partial class DecimalFloatingPointIeee754
  {
    extension<TDecimalN>(TDecimalN)
      where TDecimalN : System.Numerics.IDecimalFloatingPointIeee754<TDecimalN>
    {
      public static (int Sign, int Exponent, System.Int128 Coefficient) AnalyzeDecimal(TDecimalN value)
      {
        if (TDecimalN.IsNaN(value)) throw new System.NotSupportedException();

        if (TDecimalN.IsInfinity(value)) throw new System.NotFiniteNumberException();

        var exponent = TDecimalN.ILogB(TDecimalN.GetQuantum(value));

        var scaled = TDecimalN.ScaleB(value, -exponent);

        var coefficient = System.Int128.CreateChecked(TDecimalN.Abs(scaled));

        return (TDecimalN.IsNegative(value) ? -1 : 1, exponent, coefficient);
      }
    }
  }
}