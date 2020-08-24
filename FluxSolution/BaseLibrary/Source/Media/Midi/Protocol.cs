using System.Linq;

namespace Flux.Media.Midi
{
  /// <summary>This note library enables conversions to and from MIDI note numbers and other relative data points, e.g. pitch notations and frequencies.</summary>
  /// <see cref="https://www.midi.org/specifications/item/table-1-summary-of-midi-message"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/MIDI_tuning_standard"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/>
  public static class Protocol
  {
    public enum SystemRealTimeMessage
    {
      /// <summary>Sent 24 times per quarter note when synchronization is required.</summary>
      TimingClock = 0b11111000,
      /// <summary>Undefined (reserved).</summary>
      //Undefined = 0b11111001,
      /// <summary>Start the current sequence playing. (This message will be followed with Timing Clocks).</summary>
      Start = 0b11111010,
      /// <summary>Continue at the point the sequence was Stopped.</summary>
      Continue = 0b11111011,
      /// <summary>Stop the current sequence.</summary>
      Stop = 0b11111100,
      /// <summary>Undefined (reserved).</summary>
      //Undefined = 0b11111101,
      /// <summary>This message is intended to be sent repeatedly to tell the receiver that a connection is alive. Use of this message is optional. When initially received, the receiver will expect to receive another Active Sensing message each 300ms (max), and if it does not then it will assume that the connection has been terminated. At termination, the receiver will turn off all voices and return to normal (non- active sensing) operation.</summary>
      ActiveSensing = 0b11111110,
      /// <summary>Reset all receivers in the system to power-up status. This should be used sparingly, preferably under manual control. In particular, it should not be sent on power-up.</summary>
      Reset = 0b11111111
    }

    public static byte TimingClock => 0b11111000;
    public static byte Start => 0b11111010;
    public static byte Continue => 0b11111011;
    public static byte Stop => 0b11111100;
    public static byte ActiveSensing => 0b11111110;
    public static byte Reset => 0b11111111;
  }
}
