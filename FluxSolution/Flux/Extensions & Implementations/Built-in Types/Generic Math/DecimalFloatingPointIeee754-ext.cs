namespace Flux
{
  public static partial class DecimalFloatingPointIeee754
  {
#if NET11_0_OR_GREATER

    extension<TDecimalN>(TDecimalN)
      where TDecimalN : System.Numerics.IDecimalFloatingPointIeee754<TDecimalN>
    {
    }

#endif
  }
}