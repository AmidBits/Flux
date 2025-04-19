namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Equivalent to the opposite effect of the Truncate() functionality, i.e. instead of truncating the fraction and essentially executing a round-toward-zero, envelop the fraction and essentially execute a round-away-from-zero.</para>
    /// <para>It can also be seen as a companion function to truncate(). Unlike truncate() which calls floor() for positive numbers and ceiling() for negative; envelope() calls ceiling() for positive numbers and floor() for negative.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <remarks>Like truncate, envelop is a symmetric biased around 0 type rounding.</remarks>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static TNumber Envelop<TNumber>(this TNumber value)
        where TNumber : System.Numerics.IFloatingPoint<TNumber>
        => TNumber.IsNegative(value) ? TNumber.Floor(value) : TNumber.Ceiling(value);
  }
}
