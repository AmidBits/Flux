namespace Flux
{
  public static partial class TimeZoneInfoExtensionMethods
  {
    /// <summary>This is simple straight forward abbreviator for time zones. Since there is no real standard for abbreviations, or rather all abbreviations are localized, this is not a reliable source of information.</summary>
    public static string GetCustomAbbreviation(this System.TimeZoneInfo source, bool? isDaylightSavingTime = null)
    {
      var name = (source.SupportsDaylightSavingTime && (isDaylightSavingTime ?? System.DateTime.Now.IsDaylightSavingTime())) ? source.DaylightName : source.StandardName;

      name = System.Text.RegularExpressions.Regex.Replace(name, @"[^\p{Lu}]", string.Empty);

      return name;
    }
  }
}
