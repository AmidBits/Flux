namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static double GetTimeCodeRate(this Midi.MidiTimeCodeType source)
    {
      switch (source)
      {
        case Midi.MidiTimeCodeType.TwentyFour:
          return 24;
        case Midi.MidiTimeCodeType.TwentyFive:
          return 25;
        case Midi.MidiTimeCodeType.TwentyNineNinetySeven:
          return 29.97;
        case Midi.MidiTimeCodeType.Thirty:
          return 30;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(source));
      }
    }
  }

  namespace Midi
  {
    public enum MidiTimeCodeType
      : byte
    {
      TwentyFour = 24,
      TwentyFive = 25,
      TwentyNineNinetySeven = 29, // This is really the 29.97, hence the name. 
      Thirty = 30
    }
  }
}