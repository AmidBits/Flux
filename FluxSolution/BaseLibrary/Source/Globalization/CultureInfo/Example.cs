namespace Flux
{
  public static partial class CultureInfo
  {
    /// <summary>Indicates whether the name in the string can be considered of slavo/germanic origin.</summary>
    public static bool Example(this System.Globalization.CultureInfo source)
    {
      return source.TwoLetterISOLanguageName switch
      {
        "en" => true,
        _ => false,
      };
    }
  }
}
