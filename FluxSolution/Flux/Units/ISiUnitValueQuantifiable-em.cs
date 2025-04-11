namespace Flux
{
  public static partial class Em
  {
    public static string ToSiFormattedString<TValue>(this TValue source, System.Globalization.CultureInfo? cultureInfo = null)
      where TValue : System.Numerics.INumber<TValue>
      => $"{source}";
    //{
    //  cultureInfo ??= System.Globalization.CultureInfo.CurrentCulture;

    //  var nfi = (System.Globalization.NumberFormatInfo)cultureInfo.NumberFormat.Clone();
    //  nfi.NumberGroupSeparator = UnicodeSpacing.ThinSpace.ToSpacingString();

    //  return source.ToString("#,0.#", nfi);
    //}

    public static System.Collections.Generic.Dictionary<(Units.MetricPrefix, TUnit), string> ToStringsOfSiPrefixes<TValue, TUnit>(this Units.ISiUnitValueQuantifiable<TValue, TUnit> source, bool preferUnicode = false, Unicode.UnicodeSpacing unitSpacing = Unicode.UnicodeSpacing.Space, bool fullNames = false)
      where TValue : struct, System.Numerics.INumber<TValue>
      where TUnit : System.Enum
    {
      var d = new System.Collections.Generic.Dictionary<(Units.MetricPrefix, TUnit), string>();

      foreach (Units.MetricPrefix mp in System.Enum.GetValues<Units.MetricPrefix>().OrderDescending())
        d.Add((mp, default(TUnit)!), source.ToSiUnitString(mp, fullNames));

      return d;
    }
  }
}
