namespace Flux.Midi
{
  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/MIDI_timecode"/>
  public struct MidiTimeCode
    : /*System.IComparable<MidiTimeCode>,*/ System.IEquatable<MidiTimeCode>
  {
    private readonly MidiTimeCodeType m_rateType;
    private readonly byte m_hour;
    private readonly byte m_minute;
    private readonly byte m_second;
    private readonly byte m_frame;

    public MidiTimeCode(MidiTimeCodeType rate, int hour, int minute, int second, int frame)
    {
      m_rateType = rate;
      m_hour = hour >= 0 && hour <= 23 ? (byte)hour : throw new System.ArgumentOutOfRangeException(nameof(hour));
      m_minute = minute >= 0 && minute <= 59 ? (byte)minute : throw new System.ArgumentOutOfRangeException(nameof(minute));
      m_second = second >= 0 && second <= 59 ? (byte)second : throw new System.ArgumentOutOfRangeException(nameof(second));
      m_frame = frame >= 0 && frame < (int)rate ? (byte)frame : throw new System.ArgumentOutOfRangeException(nameof(frame));
    }

    public MidiTimeCodeType Rate
      => m_rateType;
    public byte Hour
      => m_hour;
    public byte Minute
      => m_minute;
    public byte Second
      => m_second;
    public byte Frame
      => m_frame;

    public MidiTimeCode AddHours(int value)
      => System.Math.DivRem(m_hour + value, 24, out var reminder) is var quotient && quotient > 0
      ? throw new System.ArgumentOutOfRangeException(nameof(value), $"The maximum value for the hour part is 23.")
      : new MidiTimeCode(m_rateType, reminder, m_minute, m_second, m_frame);
    public MidiTimeCode AddMinutes(int value)
      => new(m_rateType, m_hour + System.Math.DivRem(m_minute + value, 60, out var reminder), reminder, m_second, m_frame);
    public MidiTimeCode AddSeconds(int value)
      => new(m_rateType, m_hour, m_minute + System.Math.DivRem(m_second + value, 60, out var reminder), reminder, m_frame);
    public MidiTimeCode AddFrames(int value)
      => new(m_rateType, m_hour, m_minute, m_second + System.Math.DivRem(m_frame + value, (int)m_rateType, out var reminder), reminder);

    public byte[] ToFullFrameBytes()
      => Protocol.CommonMessage.MtcFullFrame((int)m_rateType, m_hour, m_minute, m_second, m_frame);
    public byte[] ToQuarterFrameBytes(int index)
      => index switch
      {
        0 => Protocol.CommonMessage.MtcQuarterFrame(0x0, m_frame & 0xF),
        1 => Protocol.CommonMessage.MtcQuarterFrame(0x1, m_frame >> 4),
        2 => Protocol.CommonMessage.MtcQuarterFrame(0x2, m_second & 0xF),
        3 => Protocol.CommonMessage.MtcQuarterFrame(0x3, m_second >> 4),
        4 => Protocol.CommonMessage.MtcQuarterFrame(0x4, m_minute & 0xF),
        5 => Protocol.CommonMessage.MtcQuarterFrame(0x5, m_minute >> 4),
        6 => Protocol.CommonMessage.MtcQuarterFrame(0x6, m_hour & 0xF),
        7 => Protocol.CommonMessage.MtcQuarterFrame(0x7, (int)m_rateType | m_hour >> 4),
        _ => throw new System.ArgumentOutOfRangeException(nameof(index)),
      };

    #region Implemented interfaces
    // IComparable
    [System.Obsolete("NOT OBSOLETE, but needs evaluation of m_frame AND m_rateType, then remove this message.")]
    public int CompareTo(MidiTimeCode other)
      => throw new System.Exception("Needs evaluation of m_frame AND m_rateType"); // m_hour > other.m_hour ? 1 : m_hour < other.m_hour ? -1 : m_minute > other.m_minute ? 1 : m_minute < other.m_minute ? -1 : m_second > other.m_second ? 1 : m_second < other.m_second ? -1 : m_second > other.m_second ? 1 : m_second < other.m_second ? -1 : 0;

    // IEquatable
    public bool Equals(MidiTimeCode other)
      => m_hour == other.m_hour && m_minute == other.m_minute && m_second == other.m_second && m_frame == other.m_frame && m_rateType == other.m_rateType;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is MidiTimeCode o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_hour, m_minute, m_second, m_frame, m_rateType);
    /// <summary>Creates a string containing the scientific pitch notation of the specified MIDI note.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/>
    public override string ToString()
      => $"{GetType().Name} {{ {m_hour}:{m_minute}:{m_second}:{m_frame} ({m_rateType}) }}";
    #endregion Object overrides
  }
}
