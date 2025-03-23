//namespace Flux.Formatting
//{
//  public abstract class AFormatter
//    : System.ICustomFormatter, System.IFormatProvider
//  {
//    // ICustomFormatter
//    public virtual string Format(string? format, object? arg, System.IFormatProvider? formatProvider)
//      => HandleOtherFormats(format, arg);

//    // IFormatProvider
//    public virtual object? GetFormat(System.Type? formatType)
//      => formatType == typeof(System.ICustomFormatter) ? this : null;

//    /// <summary></summary>
//    protected static string HandleOtherFormats(string? format, object? arg)
//      => ((arg as System.IFormattable)?.ToString(format, System.Globalization.CultureInfo.CurrentCulture)) ?? arg?.ToString() ?? string.Empty;
//  }
//}
