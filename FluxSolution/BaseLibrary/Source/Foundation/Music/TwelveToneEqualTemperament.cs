using System.Linq;

namespace Flux.Music
{
  public static class TwelveToneEqualTemperament
  {
    public static System.Collections.Generic.IEnumerable<double> GetFrequencyRatios()
      => System.Enum.GetValues(typeof(TwelveToneEqualTemperamentName)).Cast<int>().Select(i => Semitone.ConvertSemitoneToFrequencyRatio(i));

    public static System.Collections.Generic.IEnumerable<int> GetCents()
      => System.Enum.GetValues(typeof(TwelveToneEqualTemperamentName)).Cast<int>().Select(i => Semitone.ConvertSemitoneToCent(i));
  }
}
