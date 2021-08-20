namespace Flux.Midi
{
  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/MIDI_timecode"/>
  public class MidiTimeCode
  {
    private MidiTimeCodeType m_rate;
    private byte m_hour;
    private byte m_minute;
    private byte m_second;
    private byte m_frame;

    public MidiTimeCode(MidiTimeCodeType rate, int hour, int minute, int second, int frame)
    {
      m_rate = rate;
      m_hour = hour >= 0 && hour <= 23 ? (byte)hour : throw new System.ArgumentOutOfRangeException(nameof(hour));
      m_minute = minute >= 0 && minute <= 59 ? (byte)minute : throw new System.ArgumentOutOfRangeException(nameof(minute));
      m_second = second >= 0 && second <= 59 ? (byte)second : throw new System.ArgumentOutOfRangeException(nameof(second));
      m_frame = frame >= 0 && frame < (int)rate ? (byte)frame : throw new System.ArgumentOutOfRangeException(nameof(frame));
    }

    public MidiTimeCodeType Rate
      => m_rate;
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
      ? throw new System.ArithmeticException($"The maximum value for the hour part is 23.")
      : new MidiTimeCode(m_rate, reminder, m_minute, m_second, m_frame);
    public MidiTimeCode AddMinutes(int value)
      => new MidiTimeCode(m_rate, m_hour + System.Math.DivRem(m_minute + value, 60, out var reminder), reminder, m_second, m_frame);
    public MidiTimeCode AddSeconds(int value)
      => new MidiTimeCode(m_rate, m_hour, m_minute + System.Math.DivRem(m_second + value, 60, out var reminder), reminder, m_frame);
    public MidiTimeCode AddFrames(int value)
      => new MidiTimeCode(m_rate, m_hour, m_minute, m_second + System.Math.DivRem(m_frame + value, (int)m_rate, out var reminder), reminder);

    public byte[] ToFullFrameBytes()
      => Protocol.CommonMessage.MtcFullFrame((int)m_rate, m_hour, m_minute, m_second, m_frame);
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
        7 => Protocol.CommonMessage.MtcQuarterFrame(0x7, (int)m_rate | m_hour >> 4),
        _ => throw new System.ArgumentOutOfRangeException(nameof(index)),
      };
  }
}
