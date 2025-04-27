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
    static TUnit SiBasePrefix { get; } = default!;

    static TUnit SiUnprefixedUnit { get; } = default!;

    /// <summary>
    /// <para>Gets the name of the <paramref name="prefix"/> with the <typeparamref name="TUnit"/> and whether to <paramref name="preferPlural"/>.</para>
    /// </summary>
    /// <param name="prefix"></param>
    /// <param name="preferPlural"></param>
    /// <returns></returns>
    abstract static string GetSiUnitName(MetricPrefix prefix, /*TUnit unit,*/ bool preferPlural);

    /// <summary>
    /// <para>Gets the symbol of the <paramref name="prefix"/> with the <typeparamref name="TUnit"/> and whether to <paramref name="preferUnicode"/>.</para>
    /// </summary>
    /// <param name="prefix">The prefix to project.</param>
    /// <param name="preferUnicode"></param>
    /// <returns></returns>
    abstract static string GetSiUnitSymbol(MetricPrefix prefix, /*TUnit unit,*/ bool preferUnicode);

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
    /// <param name="fullName"></param>
    /// <returns></returns>
    //string ToSiUnitString(MetricPrefix prefix/*, TUnit unit*/, bool fullName);
    string ToSiUnitString(MetricPrefix prefix, string? format, System.IFormatProvider? formatProvider, bool fullName);
  }
}
