namespace Flux
{
  #region Extension methods

  public static partial class Em
  {
    public static System.Collections.Generic.Dictionary<TUnit, string> ToStringsOfAllUnits<TValue, TUnit>(this Quantities.IUnitValueQuantifiable<TValue, TUnit> source, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool useFullName = false)
      where TValue : System.Numerics.INumber<TValue> // struct, System.IEquatable<TValue>, System.IComparable<TValue>
      where TUnit : System.Enum
    {
      var d = new System.Collections.Generic.Dictionary<TUnit, string>();

      foreach (TUnit unit in System.Enum.GetValues(typeof(TUnit)))
        d.Add(unit, source.ToUnitValueSymbolString(unit, format, formatProvider, unitSpacing, preferUnicode));

      return d;
    }
  }

  #endregion // Extension methods

  namespace Quantities
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
    where TValue : System.Numerics.INumber<TValue> // System.IEquatable<TValue>, System.IComparable<TValue>
    where TUnit : System.Enum
    {
      /// <summary>
      /// <para>Gets the name for the specified <paramref name="unit"/> and whether to <paramref name="preferPlural"/>.</para>
      /// </summary>
      /// <param name="unit"></param>
      /// <param name="preferPlural">Whether to use plural of the name, if applicable.</param>
      /// <returns>The name for the <paramref name="unit"/>.</returns>
      string GetUnitName(TUnit unit, bool preferPlural);

      /// <summary>
      /// <para>Gets the symbol for the specified <paramref name="unit"/> and whether to <paramref name="preferUnicode"/>.</para>
      /// </summary>
      /// <param name="unit">The unit to represent.</param>
      /// <param name="preferUnicode">Whether to prefer Unicode symbols, where and when available. This typically result in reduced length of the returning string, and also less support for some of those symbols, e.g. fonts.</param>
      /// <returns>The symbol for the <paramref name="unit"/>.</returns>
      string GetUnitSymbol(TUnit unit, bool preferUnicode);

      /// <summary>
      /// <para>Gets the value of the quantity in the specified <paramref name="unit"/>.</para>
      /// </summary>
      /// <param name="unit">The unit to represent.</param>
      /// <returns>The value of the quantity based on the specified <paramref name="unit"/>.</returns>
      TValue GetUnitValue(TUnit unit);

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="unit"></param>
      /// <param name="format"></param>
      /// <param name="formatProvider"></param>
      /// <param name="unitSpacing"></param>
      /// <param name="preferPlural"></param>
      /// <returns></returns>
      string ToUnitValueNameString(TUnit unit, string? format, System.IFormatProvider? formatProvider, UnicodeSpacing unitSpacing, bool preferPlural);

      /// <summary>
      /// <para>Creates an quantity string for the <paramref name="unit"/>, in the <paramref name="format"/> using the <paramref name="formatProvider"/>, <paramref name="unitSpacing"/> and whether to <paramref name="preferUnicode"/>.</para>
      /// </summary>
      /// <param name="unit">The unit to represent.</param>
      /// <param name="format">The format.</param>
      /// <param name="formatProvider">The format provider.</param>
      /// <param name="unitSpacing">The Unicode spacing to apply.</param>
      /// <param name="preferUnicode">Whether to prefer Unicode symbols, where and when available. This typically result in reduced length of the returning string, and also less support for some of those symbols, e.g. fonts.</param>
      /// <returns>A string with the value and any symbols representing the quantity in the specified <typeparamref name="TUnit"/>.</returns>
      string ToUnitValueSymbolString(TUnit unit, string? format, System.IFormatProvider? formatProvider, UnicodeSpacing unitSpacing, bool preferUnicode);
    }
  }
}
