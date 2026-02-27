namespace Flux
{
  public static partial class NumberFormatInfoExtensions
  {
    extension(System.Globalization.NumberFormatInfo)
    {
      public static System.Globalization.NumberFormatInfo GetSiNumberFormatInfo(UnicodeSpacing unicodeSpacing = UnicodeSpacing.Space)
      {
        var nfi = (System.Globalization.NumberFormatInfo)System.Globalization.CultureInfo.InvariantCulture.NumberFormat.Clone();
        nfi.NumberGroupSeparator = unicodeSpacing.ToSpacingString();
        return nfi;
      }
    }
  }
}
