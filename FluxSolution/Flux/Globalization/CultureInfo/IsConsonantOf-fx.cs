namespace Flux
{
  public static partial class CultureInfos
  {
    public static System.ReadOnlySpan<char> GetConsonantsOf(this System.Globalization.CultureInfo source)
    {
      source ??= System.Globalization.CultureInfo.CurrentCulture;

      return source.TwoLetterISOLanguageName switch
      {
        "en" => "bcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ",
        "se" => "bcdfghjklmnpqrstvwxzBCDFGHJKLMNPQRSTVWXZ",
        _ => throw new System.NotImplementedException(nameof(source))
      };
    }

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="culture">If null, then <see cref="System.Globalization.CultureInfo.CurrentCulture"/></param>
    /// <returns></returns>
    public static bool IsConsonantOf(this char source, System.Globalization.CultureInfo? culture = null)
      => char.IsLetter(source) && MemoryExtensions.Contains(GetConsonantsOf(culture!), source);

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="culture">If null, then <see cref="System.Globalization.CultureInfo.CurrentCulture"/></param>
    /// <returns></returns>
    public static bool IsConsonantOf(this System.Text.Rune source, System.Globalization.CultureInfo? culture = null)
      => System.Text.Rune.IsLetter(source) && MemoryExtensions.Contains(GetConsonantsOf(culture!), (char)source.Value);
  }
}
