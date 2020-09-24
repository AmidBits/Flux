namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Returns the source as a string formatted for use in a file name using "yyyyMMdd HHmmss fffffff".</summary>
    public static string ToStringFileNameFriendly(this System.DateTime source)
      => source.ToString("yyyyMMdd HHmmss fffffff", System.Globalization.CultureInfo.CurrentCulture);

    /// <summary>Returns the System.DateTime as a string using the full ISO 8601 format: "yyyy-MM-ddTHH:mm:ss.fffffff".</summary>
    public static string ToStringISO8601Full(this System.DateTime source)
      => source.ToString("yyyy-MM-ddTHH:mm:ss.fffffff", System.Globalization.CultureInfo.CurrentCulture);
    /// <summary>Returns the System.DateTime date part only as a string using the '' ISO 8601 format: "yyyy-MM-dd".</summary>
    public static string ToStringISO8601FullDateOnly(this System.DateTime source)
      => source.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
    /// <summary>Returns the System.DateTime time part only as a string using the 'full time' ISO 8601 format: "HH:mm:ss.fffffff".</summary>
    public static string ToStringISO8601FullTimeOnly(this System.DateTime source)
      => source.ToString("HH:mm:ss.fffffff", System.Globalization.CultureInfo.CurrentCulture);
    /// <summary>Returns the System.DateTime as a string using the optimized ISO 8601 format: "yyyy-MM-ddTHH:mm[:ss[.fffffff]]".</summary>
    public static string ToStringISO8601Optimized(this System.DateTime source)
      => $"{source.ToStringISO8601FullDateOnly()}T{source.ToStringISO8601OptimizedTimeOnly()}";
    /// <summary>Returns the System.DateTime time part only as a string using an 'optimized time' ISO 8601 format: "HH:mm[:ss[.fffffff]]".</summary>
    public static string ToStringISO8601OptimizedTimeOnly(this System.DateTime source)
      => source.Millisecond > 0 ? source.ToString("HH:mm:ss.fffffff", System.Globalization.CultureInfo.CurrentCulture) : source.Second > 0 ? source.ToString("HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture) : source.ToString("HH:mm", System.Globalization.CultureInfo.CurrentCulture);

    /// <summary>Creates a string optimized (in the sense of being parsed by the most SQL Server datetime types) for SQL Server transfers.</summary>
    public static string ToStringSqlServerOptimized(this System.DateTime source)
      => source.Millisecond >= 1000 ? source.ToString(@"yyyy-MM-dd HH:mm:ss.fffffff", System.Globalization.CultureInfo.CurrentCulture) : source.Millisecond >= 1 ? source.ToString(@"yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.CurrentCulture) : source.ToString(@"yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
  }
}
