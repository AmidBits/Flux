namespace Flux.Music.Midi
{
  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/MIDI_beat_clock"/>
  /// <seealso cref="https://www.midi.org/specifications/item/table-1-summary-of-midi-message"/>
  public static class Timing
  {
    public const int PulsesPerQuarterNote = 24;

    public static double ClocksPerMinute(double bpm) => bpm * PulsesPerQuarterNote;

    public static double MicrosecondsPerClock(double bpm) => 60000000.0 / ClocksPerMinute(bpm);
  }
}
