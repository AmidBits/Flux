namespace Flux
{
  public static partial class Em
  {
    public static System.Globalization.NumberFormatInfo GetSiNumberFormatInfo<TValue>(this Units.IValueQuantifiable<TValue> source, Unicode.UnicodeSpacing numberGroupSeparator = Unicode.UnicodeSpacing.ThinSpace)
      where TValue : struct, System.Numerics.INumber<TValue>
    {
      var nfi = (System.Globalization.NumberFormatInfo)System.Globalization.CultureInfo.InvariantCulture.NumberFormat.Clone();
      nfi.CurrencyGroupSeparator = numberGroupSeparator.ToSpacingString();
      nfi.NumberGroupSeparator = numberGroupSeparator.ToSpacingString();
      nfi.PercentGroupSeparator = numberGroupSeparator.ToSpacingString();
      return nfi;
    }

    public static string ToSiFormattedString<TValue>(this TValue source, string? format = null, System.IFormatProvider? formatProvider = null, Unicode.UnicodeSpacing numberGroupSeparator = Unicode.UnicodeSpacing.ThinSpace)
      where TValue : struct, System.Numerics.INumber<TValue>
      => source.ToString(format, formatProvider);
    //{
    //var ci = System.Globalization.CultureInfo.CreateSpecificCulture("sv-SE");
    //ci.NumberFormat.NumberGroupSeparator = numberGroupSeparator.ToSpacedString();
    //var nfi = (System.Globalization.NumberFormatInfo)System.Globalization.CultureInfo.InvariantCulture.NumberFormat.Clone();
    //nfi.CurrencyGroupSeparator = numberGroupSeparator.ToSpacedString();
    //nfi.NumberGroupSeparator = numberGroupSeparator.ToSpacedString();
    //nfi.PercentGroupSeparator = numberGroupSeparator.ToSpacedString();
    //return string.Format(ci, "{0:N17}", source);
    //return source.ToString(format, formatProvider);
    //return source.ToString("N40", nfi);
    //return source.ToString(33.FormatUpToFractionalDigits(), nfi);
    //}

    public static System.Collections.Generic.Dictionary<(Units.MetricPrefix, TUnit), string> ToStringsOfSiPrefixes<TValue, TUnit>(this Units.ISiUnitValueQuantifiable<TValue, TUnit> source, bool preferUnicode = false, Unicode.UnicodeSpacing unitSpacing = Unicode.UnicodeSpacing.Space, bool fullNames = false)
      where TValue : struct, System.Numerics.INumber<TValue>
      where TUnit : System.Enum
    {
      var d = new System.Collections.Generic.Dictionary<(Units.MetricPrefix, TUnit), string>();

      foreach (Units.MetricPrefix mp in System.Enum.GetValues<Units.MetricPrefix>().OrderDescending())
        d.Add((mp, default(TUnit)!), source.ToSiUnitString(mp, null, null, fullNames));

      return d;
    }
  }
}
