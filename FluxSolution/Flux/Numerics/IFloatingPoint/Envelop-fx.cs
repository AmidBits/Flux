namespace Flux
{
  public static partial class FloatingPoint
  {
    /// <summary>
    /// <para>Equivalent to the opposite effect of the Truncate() functionality, i.e. instead of truncating the fraction and essentially executing a round-toward-zero, envelop the fraction and essentially execute a round-away-from-zero.</para>
    /// <para>It can also be seen as a companion function to truncate(). Unlike truncate() which calls floor() for positive numbers and ceiling() for negative; envelope() calls ceiling() for positive numbers and floor() for negative.</para>
    /// </summary>
    /// <typeparam name="TFloat"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <remarks>Like truncate, envelop is a symmetric biased around 0 type rounding.</remarks>
    public static TFloat Envelop<TFloat>(this TFloat value)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>
        => TFloat.IsNegative(value) ? TFloat.Floor(value) : TFloat.Ceiling(value);
  }
}
