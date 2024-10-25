//namespace Flux
//{
//  public static partial class Fx
//  {
//    public static TValue ConvertSiValue<TValue>(this TValue value, MetricPrefix fromPrefix, MetricPrefix toPrefix)
//      where TValue : System.Numerics.IFloatingPoint<TValue>
//    {
//      if (fromPrefix != MetricPrefix.Unprefixed)
//        value *= TValue.CreateChecked(fromPrefix.GetPrefixValue());

//      if (toPrefix != MetricPrefix.Unprefixed)
//        value /= TValue.CreateChecked(toPrefix.GetPrefixValue());

//      return value;
//    }
//  }
//}
