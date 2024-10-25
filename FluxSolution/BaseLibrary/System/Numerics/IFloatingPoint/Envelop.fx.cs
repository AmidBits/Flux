namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Equivalent to the opposite effect of the Truncate() functionality.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <remarks>Like truncate, envelop is a symmetric biased around 0 type rounding.</remarks>
    public static TValue Envelop<TValue>(this TValue value)
        where TValue : System.Numerics.IFloatingPoint<TValue>
        => TValue.IsNegative(value) ? TValue.Floor(value) : TValue.Ceiling(value);
  }
}
