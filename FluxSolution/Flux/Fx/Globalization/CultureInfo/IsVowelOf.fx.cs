namespace Flux
{
  public static partial class Fx
  {
    public static System.ReadOnlySpan<char> GetVowelsOf(this System.Globalization.CultureInfo source)
    {
      source ??= System.Globalization.CultureInfo.CurrentCulture;

      return source.TwoLetterISOLanguageName switch
      {
        "en" => "aeiouyAEIOUY",
        "se" => "aeiouy'\u00E5\u00E4\u00F6AEIOUY\u00C5\u00C4\u00D6", // The additional characters are å, ä, ö and Å, Ä, Ö.
        _ => throw new System.NotImplementedException(nameof(source))
      };
    }

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="culture">If null, then <see cref="System.Globalization.CultureInfo.CurrentCulture"/></param>
    /// <returns></returns>
    public static bool IsVowelOf(this char source, System.Globalization.CultureInfo? culture = null)
      => char.IsLetter(source) && MemoryExtensions.Contains(GetVowelsOf(culture!), source);

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="culture">If null, then <see cref="System.Globalization.CultureInfo.CurrentCulture"/></param>
    /// <returns></returns>
    public static bool IsVowelOf(this System.Text.Rune source, System.Globalization.CultureInfo? culture = null)
      => System.Text.Rune.IsLetter(source) && MemoryExtensions.Contains(GetVowelsOf(culture!), (char)source.Value);
  }
}
