namespace Flux
{
  public static partial class CultureInfo
  {
    /// <summary>Indicates whether the name in the string can be considered of slavo/germanic origin.</summary>
    public static bool Example(this System.Globalization.CultureInfo source)
    {
      switch(source.TwoLetterISOLanguageName)
      {
        default:
          return false;
      }
    }
  }
}
