namespace Flux
{
  #region Extension methods

  public static partial class Em
  {
    public static System.Collections.Generic.Dictionary<TUnit, TValue> GetUnitValueAll<TValue, TUnit>(this IUnitValueQuantifiable<TValue, TUnit> source)
      where TValue : System.IEquatable<TValue>
      where TUnit : System.Enum
    {
      var d = new System.Collections.Generic.Dictionary<TUnit, TValue>();
      foreach (TUnit unit in System.Enum.GetValues(typeof(TUnit)))
        d.Add(unit, source.GetUnitValue(unit));
      return d;
    }

    public static System.Collections.Generic.Dictionary<TUnit, string> ToUnitStringAll<TValue, TUnit>(this IUnitValueQuantifiable<TValue, TUnit> source, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool useFullName = false)
      where TValue : System.IEquatable<TValue>
      where TUnit : System.Enum
    {
      var d = new System.Collections.Generic.Dictionary<TUnit, string>();
      foreach (TUnit unit in System.Enum.GetValues(typeof(TUnit)))
        d.Add(unit, source.ToUnitString(unit, format, formatProvider, useFullName));
      return d;
    }
  }

  #endregion // Extension methods

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
    ///// <summary>
    ///// <para>Convert the <paramref name="value"/> from <paramref name="unit"/> to base (storage) unit.</para>
    ///// </summary>
    ///// <param name="unit"></param>
    ///// <param name="value"></param>
    ///// <returns></returns>
    //static abstract TValue ConvertFromUnit(TUnit unit, TValue value);

    ///// <summary>
    ///// <para>Convert the <paramref name="value"/> from base (storage) unit to <paramref name="unit"/>.</para>
    ///// </summary>
    ///// <param name="unit"></param>
    ///// <param name="value"></param>
    ///// <returns></returns>
    //static abstract TValue ConvertToUnit(TUnit unit, TValue value);

    /// <summary>
    /// <para>Convert <paramref name="value"/> in unit <paramref name="from"/> to the unit <paramref name="to"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    static abstract TValue ConvertUnit(TValue value, TUnit from, TUnit to);

    /// <summary>
    /// <para>Gets the <paramref name="unit"/> factor, which is used to convert a <typeparamref name="TValue"/> between the different <typeparamref name="TUnit"/>s.</para>
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    /// <remarks>Please note that some units do not have simple conversion factors, e.g. Celsius to Fahrenheit, but algorithms or formulas.</remarks>
    static abstract TValue GetUnitFactor(TUnit unit);

    /// <summary>
    /// <para>Gets the name representing the specified <paramref name="unit"/> and whether to <paramref name="preferPlural"/>.</para>
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="preferPlural">Whether to use plural of the name, if applicable.</param>
    /// <returns>The name for the <paramref name="unit"/>.</returns>
    static virtual string GetUnitName(TUnit unit, bool preferPlural) => unit.ToString().ConvertUnitNameToPlural(preferPlural);

    /// <summary>
    /// <para>Gets the symbol representing the specified <paramref name="unit"/> and whether to <paramref name="preferUnicode"/>.</para>
    /// </summary>
    /// <param name="unit">The unit to represent.</param>
    /// <param name="preferUnicode">Whether to prefer Unicode symbols, where and when available. This typically result in reduced length of the returning string, and also less support for some of those symbols, e.g. fonts.</param>
    /// <returns>The symbol for the <paramref name="unit"/>.</returns>
    static abstract string GetUnitSymbol(TUnit unit, bool preferUnicode);

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
    string ToUnitString(TUnit unit, string? format, System.IFormatProvider? formatProvider, bool fullName);
  }
}
