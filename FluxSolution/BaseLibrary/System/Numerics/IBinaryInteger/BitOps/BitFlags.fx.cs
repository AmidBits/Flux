namespace Flux
{
  public static partial class BitOps
  {
    public static bool BitFlagCarryLsb<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => !TValue.IsZero(value & TValue.One);

    public static bool BitFlagCarryMsb<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => !TValue.IsZero(value & (TValue.One << (value.GetBitCount() - 1)));
  }
}
