namespace Flux.Midi
{
  public sealed class NoteOffMidiMessage
  {
    private int m_channel;
    public int Channel { get => m_channel; set => m_channel = value >= 0 && value <= 15 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    private int m_note;
    public int Note { get => m_note; set => m_note = value >= 0 && value <= 127 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    private int m_velocity;
    public int Velocity { get => m_velocity; set => m_velocity = value >= 0 && value <= 127 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    public NoteOffMidiMessage(int channel, int note, int velocity)
    {
      Channel = channel;
      Note = note;
      Velocity = velocity;
    }
  }

  /// <summary>This note library enables conversions to and from MIDI note numbers and other relative data points, e.g. pitch notations and frequencies.</summary>
  /// <seealso cref="https://www.midi.org/specifications/item/table-1-summary-of-midi-message"/>
  /// <seealso cref="https://tttapa.github.io/Arduino/MIDI/Chap01-MIDI-Protocol.html"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/MIDI_tuning_standard"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/>
}