namespace Flux.Units
{
  /// <summary>
  /// <para>This is for quantities and units that follow the SI (International System of Units). This interface enables the International System of Units (SI) to be represented.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/International_System_of_Units"/></para>
  /// </summary>
  /// <typeparam name="TValue"></typeparam>
  /// <typeparam name="TUnit"></typeparam>
  /// <remarks>
  /// <para>If use of <see cref="System.IConvertible"/> is desirable, use the return value from <see cref="GetSiUnitValue(MetricPrefix)"/> as a parameter for such functionality.</para>
  /// <para>No <see cref="ISiUnitValueQuantifiable{TValue, TUnit}"/>.GetSiPrefixName() exists. There are only enum labels, no modifiers, e.g. plural, etc. Try <see cref="MetricPrefix"/>.GetUnitName() instead.</para>
  /// </remarks>
  public interface ISiUnitValueQuantifiable<TValue, TUnit>
    : IUnitValueQuantifiable<TValue, TUnit>
    where TValue : System.IEquatable<TValue>//, System.IComparable<TValue> // System.Numerics.INumber<TValue> // System.Numerics.INumber<TValue>
    where TUnit : System.Enum
  {
    /// <summary>
    /// <para>Gets the value of the quantity for the specified <paramref name="prefix"/>.</para>
    /// </summary>
    /// <param name="prefix">The prefix to project.</param>
    /// <returns></returns>
    TValue GetSiUnitValue(MetricPrefix prefix/*, TUnit unit*/);

    /// <summary>
    /// <para>Creates a new string of the SI quantity for the <paramref name="prefix"/> and whether to use symbols or <paramref name="fullName"/>.</para>
    /// </summary>
    /// <param name="prefix"></param>
    /// <param name="format"></param>
    /// <param name="formatProvider"></param>
    /// <returns></returns>
    string ToSiUnitString(MetricPrefix prefix/*, TUnit unit*/, string? format, System.IFormatProvider? formatProvider);
  }
}
