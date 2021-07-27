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

    public MidiTimeCode(MidiTimeCodeType rate, int hour, int minute, int second, int frame)
    {
      m_rate = rate;
      m_hour = hour >= 0 && hour <= 23 ? (byte)hour : throw new System.ArgumentOutOfRangeException(nameof(hour));
      m_minute = minute >= 0 && minute <= 59 ? (byte)minute : throw new System.ArgumentOutOfRangeException(nameof(minute));
      m_second = second >= 0 && second <= 59 ? (byte)second : throw new System.ArgumentOutOfRangeException(nameof(second));
      m_frame = frame >= 0 && frame < (int)rate ? (byte)frame : throw new System.ArgumentOutOfRangeException(nameof(frame));
    }

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

    public static byte[] MakeFullFrame(int rate, int hour, int minute, int second, int frame)
      => new byte[] { (int)SystemCommonStatus.StartOfExclusive, 0x7F, 0x7F, 0x01, 0x01, (byte)((rate >= 0 && rate <= 3 ? rate << 5 : throw new System.ArgumentOutOfRangeException(nameof(rate))) | (hour >= 0 && hour <= 23 ? hour : throw new System.ArgumentOutOfRangeException(nameof(hour)))), (byte)(minute >= 0 && minute <= 59 ? minute : throw new System.ArgumentOutOfRangeException(nameof(minute))), (byte)(second >= 0 && second <= 59 ? second : throw new System.ArgumentOutOfRangeException(nameof(second))), (byte)(frame >= 0 && frame <= 29 ? frame : throw new System.ArgumentOutOfRangeException(nameof(frame))), (byte)SystemCommonStatus.EndOfExclusive };

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
