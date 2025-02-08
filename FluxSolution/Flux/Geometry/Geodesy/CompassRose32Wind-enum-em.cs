namespace Flux
{
  public static partial class Em
  {
    /// <summary>Returns an extrapolated Azimuth, i.e. a value in the range [0, 359] (every 11.25°, starting at 0), from a thirty-two value compass point [0, 31]. Each principal wind is 11.25° from its two neighbours.</summary>
    public static Geometry.Geodesy.Azimuth GetAzimuth(this Geometry.Geodesy.CompassRose32Wind source) => new(11.25 * (int)source);

    public static System.Collections.Generic.List<string> ToWords(this Geometry.Geodesy.CompassRose32Wind source)
    {
      var list = new System.Collections.Generic.List<string>();

      var s = source.ToString();

      var words = System.Enum.GetNames<WordsOfTheCompass>();

      for (var index = 0; index < s.Length; index++)
      {
        var characterAsString = s[index].ToString();

        foreach (var word in words)
          if (word.StartsWith(characterAsString, StringComparison.CurrentCultureIgnoreCase))
          {
            list.Add(word);

            break;
          }
      }

      return list;
    }
  }
}
