namespace Flux.Music.Midi.Sequencer
{
  public sealed class MeterEvent
  {
    private byte m_beatsPerBar;
    /// <summary>Indicates how many beats constitute a bar. E.g. 3 for three note values per beats.</summary>
    public byte BeatsPerBar { get => m_beatsPerBar; set => m_beatsPerBar = value; }

    private byte m_noteValuePerBeat;
    /// <summary>Indicates the note value that represents one beat (the beat unit). E.g. 4 means quarter note per beat.</summary>
    public byte NoteValuePerBeat { get => m_noteValuePerBeat; set => m_noteValuePerBeat = value; }

    public MeterEvent(byte beatsPerBar, byte noteValuePerBeat)
    {
      m_beatsPerBar = beatsPerBar;
      m_noteValuePerBeat = noteValuePerBeat;
    }
    public MeterEvent()
      : this(4, 4)
    {
    }
  }

  // http://www.petesqbsite.com/sections/express/issue20/midifilespart3.html
  public sealed class TempoEvent
  {
    public const double MicrosecondsPerMinute = 60000000.0;
    public const double MillisecondsPerMinute = 60000.0;

    private double m_beatsPerMinute;
    /// <summary>Beats per minute, or BPM for short, defines the speed of music.</summary>
    public double BeatsPerMinute { get => m_beatsPerMinute; set => m_beatsPerMinute = value.WrapAround(40.0, 280.0); }

    public double MicroSecondsPerBeat(double ratioOfQuarterNote = 1.0) => MicrosecondsPerMinute / (m_beatsPerMinute / ratioOfQuarterNote);

    private double m_pulsesPerQuarterNote;
    /// <summary>Pulses per quarter note is the resolution of a quarter note. This is also known as PPQN.</summary>
    public double PulsesPerQuarterNote { get => m_pulsesPerQuarterNote; set => m_pulsesPerQuarterNote = value; }

    public TempoEvent(double beatsPerMinute, double pulsesPerQuarterNote)
    {
      m_beatsPerMinute = beatsPerMinute;
      m_pulsesPerQuarterNote = pulsesPerQuarterNote;
    }
    public TempoEvent()
      : this(120, 480)
    {
    }
  }
}
