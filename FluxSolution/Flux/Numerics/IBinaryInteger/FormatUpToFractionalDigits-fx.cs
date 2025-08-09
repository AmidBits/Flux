namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>
    /// <para>Returns a string format for a dynamic <paramref name="value"/> of fractional digits.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static string FormatUpToFractionalDigits<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => value is < 1 or > 339
      ? throw new System.ArgumentOutOfRangeException(nameof(value))
      : "0." + new string('#', int.CreateChecked(value));

    public static System.Globalization.NumberFormatInfo GetSiNumberFormatInfo(Flux.Unicode.UnicodeSpacing unicodeSpacing = Flux.Unicode.UnicodeSpacing.ThinSpace)
    {
      var nfi = (System.Globalization.NumberFormatInfo)System.Globalization.CultureInfo.InvariantCulture.NumberFormat.Clone();
      nfi.NumberGroupSeparator = unicodeSpacing.ToSpacingString();
      return nfi;
    }
  }
}
