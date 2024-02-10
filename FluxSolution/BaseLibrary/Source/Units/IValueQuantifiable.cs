namespace Flux
{
  /// <summary>An interface representing a quantifiable value.</summary>
  /// <typeparam name="TValue">The value type.</typeparam>
  public interface IValueQuantifiable<TValue>
    where TValue : struct, System.IEquatable<TValue>
  {
    /// <summary>Create a string representing a standard, typical, or common format of the quantity.</summary>
    /// <param name="format">The format for the unit value.</param>
    /// <param name="preferUnicode">Whether to prefer Unicode symbols, where and when available. This typically result in reduced length of the returning string, and also less support for some of those symbols, e.g. fonts.</param>
    /// <param name="useFullName">Whether use the full actual name of the enum, rather than symbols or shorter (e.g. acronym) variants.</param>
    /// <returns>A string with the value and any symbols representing the quantity.</returns>
    //string ToValueString(string? format, bool preferUnicode, bool useFullName, System.Globalization.CultureInfo? culture);

    string ToValueString(Units.TextOptions options);

    /// <summary>The value of the quantity.</summary>
    TValue Value { get; }
  }
}
