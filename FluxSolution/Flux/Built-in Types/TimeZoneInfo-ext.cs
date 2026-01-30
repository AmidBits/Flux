namespace Flux
{
  public static partial class TimeZoneInfoExtensions
  {
    [System.Text.RegularExpressions.GeneratedRegex(@"[^\p{Lu}]")]
    private static partial System.Text.RegularExpressions.Regex RegexTimeZoneInfoCustomAbbreviation();

    extension(System.TimeZoneInfo source)
    {
      #region GetCustomAbbreviation

      /// <summary>This is simple straight forward abbreviator for time zones. Since there is no real standard for abbreviations, or rather all abbreviations are localized, this is not a reliable source of information.</summary>
      public string GetCustomAbbreviation(bool? isDaylightSavingTime = null)
      {
        var name = (source.SupportsDaylightSavingTime && (isDaylightSavingTime ?? System.DateTime.Now.IsDaylightSavingTime())) ? source.DaylightName : source.StandardName;

        name = RegexTimeZoneInfoCustomAbbreviation().Replace(name, string.Empty);

        return name;
      }

      #endregion
    }
  }
}
