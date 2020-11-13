namespace Flux.IFormatProvider
{
  public abstract class FormatProvider
    : System.IFormatProvider, System.ICustomFormatter
  {
    public virtual string Format(string? format, object? arg, System.IFormatProvider? formatProvider)
      => HandleOtherFormats(format, arg);

    public virtual object? GetFormat(System.Type? formatType)
      => formatType == typeof(System.ICustomFormatter) ? this : null!;

    protected static string HandleOtherFormats(string? format, object? arg)
      => arg is not null && arg is System.IFormattable ? ((System.IFormattable)arg).ToString(format, System.Globalization.CultureInfo.CurrentCulture) : arg != null ? arg.ToString() ?? string.Empty : string.Empty;
  }
}
