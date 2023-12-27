namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Returns a string in SQL Server parsable format, with dynamic precision based on zero values for fractional seconds.</summary>
    public static string ToStringSql(this System.DateTime source)
      => source.Millisecond >= 1000 ? source.ToStringSqlYyyyMmDdHhMmSsFffffff() : source.Millisecond >= 1 ? source.ToStringSqlYyyyMmDdHhMmSsFff() : source.ToStringSqlYyyyMmDdHhMmSs();
    /// <summary>Returns a string in SQL Server parsable format, precision includes seconds.</summary>
    public static string ToStringSqlYyyyMmDdHhMmSs(this System.DateTime source)
      => source.ToString(@"yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
    /// <summary>Returns a string in SQL Server parsable format, precision includes milliseconds.</summary>
    public static string ToStringSqlYyyyMmDdHhMmSsFff(this System.DateTime source)
      => source.ToString(@"yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
    /// <summary>Returns a string in SQL Server parsable format, precision includes ticks (one more digit than microseconds).</summary>
    public static string ToStringSqlYyyyMmDdHhMmSsFffffff(this System.DateTime source)
      => source.ToString(@"yyyy-MM-dd HH:mm:ss.fffffff", System.Globalization.CultureInfo.InvariantCulture);
  }
}
