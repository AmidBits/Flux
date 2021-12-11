namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static double GetFrequencyRatios(this Music.TwelveToneEqualTemperament source)
      => Semitone.ConvertSemitoneToFrequencyRatio((int)source);

    public static int GetCents(this Music.TwelveToneEqualTemperament source)
      => Semitone.ConvertSemitoneToCent((int)source);
  }

  namespace Music
  {
    public enum TwelveToneEqualTemperament
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
  }
}
