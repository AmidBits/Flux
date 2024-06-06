namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Retrieves the MIDI time code rate.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static double GetTimeCodeRate(this Music.Midi.MidiTimeCodeType source)
      => source switch
      {
        Music.Midi.MidiTimeCodeType.TwentyFour => 24,
        Music.Midi.MidiTimeCodeType.TwentyFive => 25,
        Music.Midi.MidiTimeCodeType.TwentyNineNinetySeven => 29.97,
        Music.Midi.MidiTimeCodeType.Thirty => 30,
        _ => throw new System.ArgumentOutOfRangeException(nameof(source)),
      };
  }

  namespace Music.Midi
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