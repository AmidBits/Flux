namespace Flux.Midi
{
  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/MIDI_timecode"/>
  public struct MidiTimeCode
    : System.IComparable<MidiTimeCode>, System.IEquatable<MidiTimeCode>
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

    [System.Diagnostics.Contracts.Pure] public MidiTimeCodeType Rate => m_rateType;
    [System.Diagnostics.Contracts.Pure] public byte Hour => m_hour;
    [System.Diagnostics.Contracts.Pure] public byte Minute => m_minute;
    [System.Diagnostics.Contracts.Pure] public byte Second => m_second;
    [System.Diagnostics.Contracts.Pure] public byte Frame => m_frame;

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

    [System.Diagnostics.Contracts.Pure]
    public byte[] ToFullFrameBytes()
      => Protocol.SystemCommon.MtcFullFrame((int)m_rateType, m_hour, m_minute, m_second, m_frame);
    [System.Diagnostics.Contracts.Pure]
    public byte[] ToQuarterFrameBytes(int index)
      => index switch
      {
        0 => Protocol.SystemCommon.MtcQuarterFrame(0x0, m_frame & 0xF),
        1 => Protocol.SystemCommon.MtcQuarterFrame(0x1, m_frame >> 4),
        2 => Protocol.SystemCommon.MtcQuarterFrame(0x2, m_second & 0xF),
        3 => Protocol.SystemCommon.MtcQuarterFrame(0x3, m_second >> 4),
        4 => Protocol.SystemCommon.MtcQuarterFrame(0x4, m_minute & 0xF),
        5 => Protocol.SystemCommon.MtcQuarterFrame(0x5, m_minute >> 4),
        6 => Protocol.SystemCommon.MtcQuarterFrame(0x6, m_hour & 0xF),
        7 => Protocol.SystemCommon.MtcQuarterFrame(0x7, (int)m_rateType | m_hour >> 4),
        _ => throw new System.ArgumentOutOfRangeException(nameof(index)),
      };

    /// <summary>Converts the MTC to a <see cref="System.TimeSpan"/> where the milliseconds represents the frame. The frame rate type is unknown in the new TimeSpan.</summary>
    [System.Diagnostics.Contracts.Pure]
    public System.TimeSpan ToTimeSpan()
      => new(0, m_hour, m_minute, m_second, m_frame);

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static bool operator ==(MidiTimeCode a, MidiTimeCode b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(MidiTimeCode a, MidiTimeCode b) => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    [System.Diagnostics.Contracts.Pure]
    public int CompareTo(MidiTimeCode other)
      => m_hour > other.m_hour ? 1
      : m_hour < other.m_hour ? -1
      : m_minute > other.m_minute ? 1
      : m_minute < other.m_minute ? -1
      : m_second > other.m_second ? 1
      : m_second < other.m_second ? -1
      : m_frame > other.m_frame ? 1
      : m_frame < other.m_frame ? -1
      : m_rateType > other.m_rateType ? -1 // Higher frame rate, means each value is worth less.
      : m_rateType < other.m_rateType ? 1 // Lower frame rate, means each value is worth more.
      : 0;

    // IEquatable
    [System.Diagnostics.Contracts.Pure]
    public bool Equals(MidiTimeCode other)
      => m_hour == other.m_hour && m_minute == other.m_minute && m_second == other.m_second && m_frame == other.m_frame && m_rateType == other.m_rateType;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is MidiTimeCode o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_hour, m_minute, m_second, m_frame, m_rateType);
    /// <summary>Creates a string containing the scientific pitch notation of the specified MIDI note.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/>
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ {m_hour}:{m_minute}:{m_second}:{m_frame} ({m_rateType}) }}";
    #endregion Object overrides
  }
}
