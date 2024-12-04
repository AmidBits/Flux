namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Equivalent to the opposite effect of the Truncate() functionality, i.e. instead of truncating the fraction and essentially executing a round-toward-zero, envelop the fraction and execute a round-away-from-zero.</para>
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
