namespace Flux.Media.Midi
{
  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/MIDI_timecode"/>
  public class MidiTimeCode
  {
    private MidiTimeCodeRate m_rate;
    public MidiTimeCodeRate Rate
    {
      get => m_rate;
      set => m_rate = value;
    }

    private int m_hour;
    public int Hour
    {
      get => m_hour;
      set => m_hour = value >= 0 && value <= 23 ? value : throw new System.ArgumentOutOfRangeException(nameof(value));
    }

    private int m_minute;
    public int Minute
    {
      get => m_minute;
      set => m_minute = value >= 0 && value <= 59 ? value : throw new System.ArgumentOutOfRangeException(nameof(value));
    }

    private int m_second;
    public int Second
    {
      get => m_second;
      set => m_second = value >= 0 && value <= 59 ? value : throw new System.ArgumentOutOfRangeException(nameof(value));
    }

    private int m_frame;
    public int Frame
    {
      get => m_frame;
      set => m_frame = value >= 0 && value < (int)m_rate ? value : throw new System.ArgumentOutOfRangeException(nameof(value));
    }

    public MidiTimeCode(MidiTimeCodeRate rate, int hour, int minute, int second, int frame)
    {
      m_rate = rate;
      m_hour = hour >= 0 && hour <= 23 ? hour : throw new System.ArgumentOutOfRangeException(nameof(hour));
      m_minute = minute >= 0 && minute <= 59 ? minute : throw new System.ArgumentOutOfRangeException(nameof(minute));
      m_second = second >= 0 && second <= 59 ? second : throw new System.ArgumentOutOfRangeException(nameof(second));
      m_frame = frame >= 0 && frame < (int)rate ? frame : throw new System.ArgumentOutOfRangeException(nameof(frame));
    }

    public MidiTimeCode AddHours(int value)
    {
      var quotient = System.Math.DivRem(m_hour + value, 24, out var reminder);

      if (quotient > 0) throw new System.ArithmeticException($"The maximum value for the hour part is 23.");

      return new MidiTimeCode(m_rate, reminder, m_minute, m_second, m_frame);
    }
    public MidiTimeCode AddMinutes(int value)
    {
      var quotient = System.Math.DivRem(m_minute + value, 60, out var reminder);

      return new MidiTimeCode(m_rate, m_hour + quotient, reminder, m_second, m_frame);
    }
    public MidiTimeCode AddSeconds(int value)
    {
      var quotient = System.Math.DivRem(m_second + value, 60, out var reminder);

      return new MidiTimeCode(m_rate, m_hour, m_minute + quotient, reminder, m_frame);
    }
    public MidiTimeCode AddFrames(int value)
    {
      var quotient = System.Math.DivRem(m_frame + value, (int)m_rate, out var reminder);

      return new MidiTimeCode(m_rate, m_hour, m_minute, m_second + quotient, reminder);
    }

    public byte[] ToFullFrameBytes()
      => Protocol.MtcFullFrame((int)m_rate, m_hour, m_minute, m_second, m_frame);
    public byte[] ToQuarterFrameBytes(int piece)
      => piece switch
      {
        0 => Protocol.MtcQuarterFrame(0x0, m_frame & 0xF),
        1 => Protocol.MtcQuarterFrame(0x1, m_frame >> 4),
        2 => Protocol.MtcQuarterFrame(0x2, m_second & 0xF),
        3 => Protocol.MtcQuarterFrame(0x3, m_second >> 4),
        4 => Protocol.MtcQuarterFrame(0x4, m_minute & 0xF),
        5 => Protocol.MtcQuarterFrame(0x5, m_minute >> 4),
        6 => Protocol.MtcQuarterFrame(0x6, m_hour & 0xF),
        7 => Protocol.MtcQuarterFrame(0x7, (int)m_rate | m_hour >> 4),
        _ => throw new System.ArgumentOutOfRangeException(nameof(piece)),
      };
  }
}
