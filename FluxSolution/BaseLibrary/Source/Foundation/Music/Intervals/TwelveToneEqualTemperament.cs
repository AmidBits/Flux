using System.Linq;

namespace Flux.Music.Interval
{
  public static class TwelveToneEqualTemperament
  {
    public enum Name
    {
      Unison = 0,
      MinorSecond = 1,
      MajorSecond = 2,
      MinorThird = 3,
      MajorThird = 4,
      PerfectFourth = 5,
      Tritone = 6,
      PerfectFifth = 7,
      MinorSixth = 8,
      MajorSixth = 9,
      MinorSeventh = 10,
      MajorSeventh = 11,
      Octave = 12
    }

    public static System.Collections.Generic.IEnumerable<double> GetFrequencyRatios()
      => System.Enum.GetValues(typeof(Name)).Cast<int>().Select(i => System.Math.Pow(2.0, i / 12.0));

    public static System.Collections.Generic.IEnumerable<int> GetCents()
      => System.Enum.GetValues(typeof(Name)).Cast<int>().Select(i => i * 100);
  }
}
