namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static double GetTimeCodeRate(this Midi.MidiTimeCodeType source)
      => source switch
      {
        Midi.MidiTimeCodeType.TwentyFour => 24,
        Midi.MidiTimeCodeType.TwentyFive => 25,
        Midi.MidiTimeCodeType.TwentyNineNinetySeven => 29.97,
        Midi.MidiTimeCodeType.Thirty => 30,
        _ => throw new System.ArgumentOutOfRangeException(nameof(source)),
      };
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