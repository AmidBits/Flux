namespace Flux
{
  public static partial class IBinaryIntegers
  {
    public static System.Globalization.NumberFormatInfo GetSiNumberFormatInfo(UnicodeSpacing unicodeSpacing = UnicodeSpacing.ThinSpace)
    {
      var nfi = (System.Globalization.NumberFormatInfo)System.Globalization.CultureInfo.InvariantCulture.NumberFormat.Clone();
      nfi.NumberGroupSeparator = unicodeSpacing.ToSpacingString();
      return nfi;
    }
  }
}
