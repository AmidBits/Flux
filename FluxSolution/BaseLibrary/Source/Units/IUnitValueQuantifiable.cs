namespace Flux
{
  #region Extension methods

  public static partial class Em
  {
    public static System.Collections.Generic.Dictionary<TUnit, string> ToStringsOfAllUnits<TValue, TUnit>(this IUnitValueQuantifiable<TValue, TUnit> source, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool useFullName = false)
      where TValue : struct, System.IEquatable<TValue>
      where TUnit : System.Enum
    {
      var d = new System.Collections.Generic.Dictionary<TUnit, string>();

      foreach (TUnit unit in System.Enum.GetValues(typeof(TUnit)))
        d.Add(unit, source.ToUnitValueString(unit, format, formatProvider, preferUnicode, unitSpacing, useFullName));

      return d;
    }
  }

  #endregion // Extension methods

  /// <summary>An interface representing a quantifiable value that can be converted to other units.</summary>
  /// <typeparam name="TValue">The type of value.</typeparam>
  /// <typeparam name="TUnit">The type of unit enum.</typeparam>
  /// <remarks>
  /// <para>If use of <see cref="System.IConvertible"/> is desirable, use <see cref="IUnitValueQuantifiable{TValue, TUnit}.GetUnitValue(TUnit)"/> return value as a parameter for such functionality.</para>
  /// </remarks>
  public interface IUnitValueQuantifiable<TValue, TUnit>
    : IValueQuantifiable<TValue>
    where TValue : struct, System.IEquatable<TValue>
    where TUnit : System.Enum
  {
    TUnit MetricUnprefixedUnit { get => default(TUnit)!; } // By default we use the System.Enum default which is zero (0  ).

    /// <summary>Gets the value of the quantity in the specified unit.</summary>
    /// <param name="unit">The unit to represent.</param>
    /// <returns>The value of the quantity based on the specified <typeparamref name="TUnit"/>.</returns>
    TValue GetUnitValue(TUnit unit);

    /// <summary>Create a string of the quantity, suffixed with the unit symbol, in the specified unit.</summary>
    /// <param name="unit">The unit to represent.</param>
    /// <param name="format">The format.</param>
    /// <param name="formatProvider">The format provider.</param>
    /// <param name="preferUnicode">Whether to prefer Unicode symbols, where and when available. This typically result in reduced length of the returning string, and also less support for some of those symbols, e.g. fonts.</param>
    /// <param name="unitSpacing">The Unicode spacing to apply.</param>
    /// <param name="useFullName">Whether use the full actual name of the enum, rather than symbols or shorter (e.g. acronym) variants.</param>
    /// <returns>A string with the value and any symbols representing the quantity in the specified <typeparamref name="TUnit"/>.</returns>
    string ToUnitValueString(TUnit unit, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool useFullName = false);
  }
}
