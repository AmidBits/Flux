namespace Flux.Units
{
  /// <summary>
  /// <para>This is for quantities and units in general. This interface enables non-standardized or otherwise not covered units.</para>
  /// </summary>
  /// <typeparam name="TValue">The type of value.</typeparam>
  /// <typeparam name="TUnit">The type of unit enum.</typeparam>
  /// <remarks>
  /// <para>If use of <see cref="System.IConvertible"/> is desirable, use the return value from <see cref="GetUnitValue(TUnit)"/> for such functionality.</para>
  /// </remarks>
  public interface IUnitValueQuantifiable<TValue, TUnit>
    : IValueQuantifiable<TValue>
    where TValue : System.IEquatable<TValue>//, System.IComparable<TValue> // System.Numerics.INumber<TValue> // 
    where TUnit : System.Enum
  {
    /// <summary>
    /// <para>Convert <paramref name="value"/> in unit <paramref name="from"/> to the unit <paramref name="to"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    static abstract TValue ConvertUnit(TValue value, TUnit from, TUnit to);

    /// <summary>
    /// <para>Gets the value of the quantity for the specified <paramref name="unit"/>.</para>
    /// </summary>
    /// <param name="unit">The unit to represent.</param>
    /// <returns>The value of the quantity in the specified <paramref name="unit"/>.</returns>
    TValue GetUnitValue(TUnit unit);

    /// <summary>
    /// <para>Creates a string with the name of the quantity for the <paramref name="unit"/>, in the <paramref name="format"/> using the <paramref name="formatProvider"/> and whether to use symbols or <paramref name="fullName"/>.</para>
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="format"></param>
    /// <param name="formatProvider"></param>
    /// <param name="fullName"></param>
    /// <returns></returns>
    string ToUnitString(TUnit unit, string? format, System.IFormatProvider? formatProvider, UnicodeSpacing spacing, bool fullName);
  }
}
