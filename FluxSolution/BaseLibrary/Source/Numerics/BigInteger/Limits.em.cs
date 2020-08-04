namespace Flux
{
  public static partial class XtensionsNumerics
  {
    public static System.Numerics.BigInteger DecimalMaxValue
      => (System.Numerics.BigInteger)decimal.MaxValue;
    public static System.Numerics.BigInteger DecimalMinValue
      => (System.Numerics.BigInteger)decimal.MinValue;

    public static System.Numerics.BigInteger DoubleMaxValue
      => (System.Numerics.BigInteger)double.MaxValue;
    public static System.Numerics.BigInteger DoubleMinValue
      => (System.Numerics.BigInteger)double.MinValue;

    public static System.Numerics.BigInteger SingleMaxValue
      => (System.Numerics.BigInteger)float.MaxValue;
    public static System.Numerics.BigInteger SingleMinValue
      => (System.Numerics.BigInteger)float.MinValue;
  }
}
