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

    string ToValueString(QuantifiableValueStringOptions options);

    /// <summary>The value of the quantity.</summary>
    TValue Value { get; }
  }

  public readonly record struct QuantifiableValueStringOptions
  {
    public readonly static QuantifiableValueStringOptions Default;

    public QuantifiableValueStringOptions(string? format, System.IFormatProvider? formatProvider)
    {
      Format = format;
      FormatProvider = formatProvider;
    }
    public QuantifiableValueStringOptions(string? format)
    {
      Format = format;
    }

    /// <summary>
    /// <para>The culture info. The default is <see cref="System.Globalization.CultureInfo.InvariantCulture"/>.</para>
    /// </summary>
    public System.Globalization.CultureInfo? CultureInfo { get; init; } = System.Globalization.CultureInfo.InvariantCulture;

    /// <summary>
    /// <para>The format for the value. The default is <see cref="null"/>.</para>
    /// </summary>
    public string? Format { get; init; }

    /// <summary>
    /// <para>The format provider. The default is <see cref="null"/>.</para>
    /// </summary>
    public System.IFormatProvider? FormatProvider { get; init; }

    /// <summary>
    /// <para>Whether to prefer Unicode symbols, where and when available. The default is true.</para>
    /// </summary>
    public bool PreferUnicode { get; init; } = true;

    /// <summary>
    /// <para>Whether to use the full actual name of the enum value, rather than symbols or shorter (e.g. acronym) variants. The default is false.</para>
    /// </summary>
    public bool UseFullName { get; init; }
  }
}
