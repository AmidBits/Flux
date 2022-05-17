namespace Flux
{
  /// <summary>An interface representing a qunatifiable value that can be converted to other units.</summary>
  /// <typeparam name="TType">The value type.</typeparam>
  /// <typeparam name="TUnit">The unit enum.</typeparam>
  public interface IUnitQuantifiable<TType, TUnit>
    : IQuantifiable<TType>
    where TType : struct//, System.IComparable<TType>, System.IEquatable<TType>
    where TUnit : System.Enum
  {
    /// <summary>Create a string of the quantity, suffixed with the unit symbol, in the specified unit.</summary>
    /// <param name="unit">The unit to represent.</param>
    /// <param name="format">The format for the unit value.</param>
    /// <param name="useFullName">Optionally use the full name, rather than symbols or shorter (e.g. acronym) variants.</param>
    /// <param name="preferUnicode">Optionally prefer Unicode characters, where and when available.</param>
    /// <returns></returns>
    string ToUnitString(TUnit unit, string? format, bool useFullName, bool preferUnicode);

    /// <summary>Create the value of the quantity in the specified unit.</summary>
    /// <param name="unit">The unit to represent.</param>
    /// <returns></returns>
    TType ToUnitValue(TUnit unit);
  }
}
