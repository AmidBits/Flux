using System.Linq;

namespace Flux.FormatProviders
{
  /// <summary>A formatter for System.TimeSpan, that can also be used for long (ticks), int (seconds) and System.Numerics.BigInteger (milliseconds).</summary>
  /// <example>
  /// System.Console.WriteLine(string.Format(new Flux.IFormatProvider.TsFormatter() { Abbreviated = true }, "{0:TS}", timeValue));
  /// </example>
  public class TimeSpanFormatter 
    : FormatProvider
  {
    private const string csD = "d";
    private const string csDay = "day";
    private const string csH = "h";
    private const string csHour = "hour";
    private const string csMin = "min";
    private const string csMinute = "minute";
    private const string csS = "s";
    private const string csSecond = "second";
    private const string csMs = "ms";
    private const string csMillisecond = "millisecond";

    public const string FormatIdentifier = @"TS";

    public bool Abbreviated { get; set; }

    public override string Format(string? format, object? arg, System.IFormatProvider? formatProvider)
    {
      if (string.IsNullOrEmpty(format) && !(format?.StartsWith(FormatIdentifier, System.StringComparison.OrdinalIgnoreCase) ?? false))
      {
        return HandleOtherFormats(format, arg);
      }

      System.TimeSpan ts;

      switch (arg)
      {
        case System.Decimal f128 when (int)System.Math.Floor(f128) is var i32:
          ts = new System.TimeSpan(0, 0, 0, i32, (int)((f128 - i32) * 1000));
          break;
        case System.Double f64 when (int)System.Math.Floor(f64) is var i32:
          ts = new System.TimeSpan(0, 0, 0, i32, (int)((f64 - i32) * 1000));
          break;
        case System.TimeSpan timeSpan:
          ts = timeSpan;
          break;
        case System.Int32 seconds:
          ts = new System.TimeSpan(0, 0, seconds);
          break;
        case System.Int64 ticks:
          ts = new System.TimeSpan(ticks);
          break;
        case System.Single f32 when (int)System.Math.Floor(f32) is var i32:
          ts = new System.TimeSpan(0, 0, 0, i32, (int)((f32 - i32) * 1000));
          break;
        case System.Numerics.BigInteger nanoSeconds:
          ts = new System.TimeSpan((long)((double)nanoSeconds / System.Diagnostics.Stopwatch.Frequency));
          break;
        default:
          return HandleOtherFormats(format, arg);
      }

      var text = new System.Collections.Generic.List<string>(5);

      if (ts.Days > 0)
        text.Add(ts.Days.ToString(System.Globalization.CultureInfo.InvariantCulture) + (Abbreviated ? csD : csDay) + (ts.Days == 1 ? string.Empty : @"s"));
      if (ts.Hours > 0)
        text.Add(ts.Hours.ToString(System.Globalization.CultureInfo.InvariantCulture) + (Abbreviated ? csH : csHour) + (ts.Hours == 1 ? string.Empty : @"s"));
      if (ts.Minutes > 0)
        text.Add(ts.Minutes.ToString(System.Globalization.CultureInfo.InvariantCulture) + (Abbreviated ? csMin : csMinute) + (ts.Minutes == 1 ? string.Empty : @"s"));
      if (ts.Seconds > 0)
        text.Add(ts.Seconds.ToString(System.Globalization.CultureInfo.InvariantCulture) + (Abbreviated ? csS : csSecond) + (ts.Seconds == 1 ? string.Empty : @"s"));
      if (ts.Milliseconds > 0)
        text.Add(ts.Milliseconds.ToString(System.Globalization.CultureInfo.InvariantCulture) + (Abbreviated ? csMs : csMillisecond) + (ts.Milliseconds == 1 ? string.Empty : @"s"));

      return string.Join(@", ", text.SkipLast(1)) + @" and " + text.Last();
    }
  }
}
