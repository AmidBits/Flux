namespace FluxNet.Numerics
{
  public static partial class Number
  {
    /// <summary>
    /// <para>Determines whether the <paramref name="value"/> is unsigned.</para>
    /// <para><seealso href="https://stackoverflow.com/a/13609383/3178666"/></para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsUnsignedNumber<TNumber>(this TNumber value)
      where TNumber : System.Numerics.INumber<TNumber>
      => typeof(TNumber) is var type
      && (
        type.IsUnsignedNumericType()
        // Or if 0.CompareTo(FIELD:MinValue) <= 0, i.e. zero is equal-or-less-than the number.
        || TNumber.Zero.CompareTo(type.GetField("MinValue")?.GetValue(null) ?? TNumber.One) <= 0
      );
  }
}
