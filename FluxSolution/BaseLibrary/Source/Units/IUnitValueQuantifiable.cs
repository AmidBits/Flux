namespace Flux
{
  #region Extension methods
  public static partial class Em
  {
    public static System.Collections.Generic.Dictionary<TUnit, string> ToStringOfAllUnits<TType, TUnit>(this IUnitValueQuantifiable<TType, TUnit> source, QuantifiableValueStringOptions options)
      where TType : struct, System.IEquatable<TType>
      where TUnit : notnull, System.Enum
    {
      var d = new System.Collections.Generic.Dictionary<TUnit, string>();

      foreach (TUnit unit in System.Enum.GetValues(typeof(TUnit)))
        d.Add(unit, source.ToUnitValueString(unit, options));

      return d;
    }
  }
  #endregion // Extension methods

  /// <summary>An interface representing a quantifiable value that can be converted to other units.</summary>
  /// <typeparam name="TValue">The value type.</typeparam>
  /// <typeparam name="TUnit">The unit enum.</typeparam>
  public interface IUnitValueQuantifiable<TValue, TUnit>
    : IValueQuantifiable<TValue>
    where TValue : struct, System.IEquatable<TValue>
    where TUnit : notnull, System.Enum
  {
    /// <summary>Create a string of the quantity, suffixed with the unit symbol, in the specified unit.</summary>
    /// <param name="unit">The unit to represent.</param>
    /// <param name="options">The options.</param>
    /// <param name="preferUnicode">Whether to prefer Unicode symbols, where and when available. This typically result in reduced length of the returning string, and also less support for some of those symbols, e.g. fonts.</param>
    /// <param name="useFullName">Whether use the full actual name of the enum, rather than symbols or shorter (e.g. acronym) variants.</param>
    /// <returns>A string with the value and any symbols representing the quantity in the specified <typeparamref name="TUnit"/>.</returns>
    //string ToUnitValueString(TUnit unit, string? format, bool preferUnicode, bool useFullName, System.Globalization.CultureInfo? culture);

    string ToUnitValueString(TUnit unit, QuantifiableValueStringOptions options);

    /// <summary>Create the value of the quantity in the specified unit.</summary>
    /// <param name="unit">The unit to represent.</param>
    /// <returns>The value of the quantity based on the specified <typeparamref name="TUnit"/>.</returns>
    TValue GetUnitValue(TUnit? unit);
  }
}
