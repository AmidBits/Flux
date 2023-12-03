namespace Flux
{
  public static partial class Em
  {
    public static System.Collections.Generic.Dictionary<TUnit, string> ToStringOfAllUnits<TType, TUnit>(this IUnitQuantifiable<TType, TUnit> source, string? format = null, bool preferUnicode = true, bool useFullName = false)
      where TType : struct, System.IEquatable<TType>
      where TUnit : notnull, System.Enum
    {
      var d = new System.Collections.Generic.Dictionary<TUnit, string>();

      foreach (TUnit unit in System.Enum.GetValues(typeof(TUnit)))
        d.Add(unit, source.ToUnitString(unit, format, preferUnicode, useFullName));

      return d;
    }
  }

  /// <summary>An interface representing a qunatifiable value that can be converted to other units.</summary>
  /// <typeparam name="TType">The value type.</typeparam>
  /// <typeparam name="TUnit">The unit enum.</typeparam>
  public interface IUnitQuantifiable<TType, TUnit>
    : IQuantifiable<TType>
    where TType : struct, System.IEquatable<TType>
    where TUnit : notnull, System.Enum
  {
    /// <summary>Create a string of the quantity, suffixed with the unit symbol, in the specified unit.</summary>
    /// <param name="unit">The unit to represent.</param>
    /// <param name="format">The format for the unit value.</param>
    /// <param name="preferUnicode">Whether to prefer Unicode symbols, where and when available. This typically result in reduced length of the returning string, and also less support for some of those symbols, e.g. fonts.</param>
    /// <param name="useFullName">Whether use the full actual name of the enum, rather than symbols or shorter (e.g. acronym) variants.</param>
    /// <returns>A string with the quantity and symbol based on the specified unit.</returns>
    string ToUnitString(TUnit unit, string? format, bool preferUnicode, bool useFullName/*, System.Globalization.CultureInfo? culture*/);

    /// <summary>Create the value of the quantity in the specified unit.</summary>
    /// <param name="unit">The unit to represent.</param>
    /// <returns>The quantity based on the specified unit.</returns>
    TType ToUnitValue(TUnit unit);
  }
}
