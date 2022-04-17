namespace Flux
{
  public interface IUnitQuantifiable<TType, TUnit>
    : IQuantifiable<TType>
    where TUnit : System.Enum
  {
    /// <summary>The unit value of the quantity.</summary>
    new TType Value { get; }

    /// <summary>Create a string representing the quantity in the specified unit.</summary>
    /// <param name="unit">The unit to represent.</param>
    /// <param name="format">Pptionally format the value.</param>
    /// <param name="useFullName">Optionally use the full name, rather than symbols or shorter (e.g. acronym) variants.</param>
    /// <param name="preferUnicode">Optionally prefer Unicode characters where available.</param>
    /// <returns></returns>
    string ToUnitString(TUnit unit, string? format, bool useFullName, bool preferUnicode);

    /// <summary>Create a value representing the value in the specified unit.</summary>
    /// <param name="unit">The unit to convert to.</param>
    TType ToUnitValue(TUnit unit);
  }
}
