namespace Flux
{
  public static partial class Fx
  {
    public static TValue ConvertSiValue<TValue>(this TValue value, Quantities.MetricPrefix fromPrefix, Quantities.MetricPrefix toPrefix)
      where TValue : System.Numerics.IFloatingPoint<TValue>
    {
      if (fromPrefix != Quantities.MetricPrefix.Unprefixed)
        value *= TValue.CreateChecked(fromPrefix.GetPrefixValue());

      if (toPrefix != Quantities.MetricPrefix.Unprefixed)
        value /= TValue.CreateChecked(toPrefix.GetPrefixValue());

      return value;
    }
  }
}
