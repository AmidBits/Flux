namespace Flux.Midi.Protocol
{
  public sealed class SystemCommon
  {
    public enum Status
    {
      /// <summary>This message type allows manufacturers to create their own messages (such as bulk dumps, patch parameters, and other non-spec data) and provides a mechanism for creating additional MIDI Specification messages. The Manufacturer's ID code (assigned by MMA or AMEI) is either 1 byte (0iiiiiii) or 3 bytes (0iiiiiii 0iiiiiii 0iiiiiii). Two of the 1 Byte IDs are reserved for extensions called Universal Exclusive Messages, which are not manufacturer-specific. If a device recognizes the ID code as its own (or as a supported Universal message) it will listen to the rest of the message (0ddddddd). Otherwise, the message will be ignored. (Note: Only Real-Time messages may be interleaved with a System Exclusive.)</summary>
      StartOfExclusive = 0xF0,
      /// <summary></summary>
      MidiTimeCodeQuarterFrame = 0xF1,
      /// <summary></summary>
      SongPositionPointer = 0xF2,
      /// <summary></summary>
      SongSelect = 0xF3,
      /// <summary>Undefined (reserved).</summary>
      UndefinedF4 = 0xF4,
      /// <summary>Undefined (reserved).</summary>
      UndefinedF5 = 0xF5,
      /// <summary>Upon receiving a Tune Request, all analog synthesizers should tune their oscillators.</summary>
      TuneRequest = 0xF6,
      /// <summary>Used to terminate a System Exclusive dump (see above).</summary>
      EndOfExclusive = 0xF7
    }

    public static byte[] MtcFullFrame(int rate, int hour, int minute, int second, int frame)
      => new byte[] { (int)Status.StartOfExclusive, 0x7F, 0x7F, 0x01, 0x01, (byte)((rate >= 0 && rate <= 3 ? rate << 5 : throw new System.ArgumentOutOfRangeException(nameof(rate))) | (hour >= 0 && hour <= 23 ? hour : throw new System.ArgumentOutOfRangeException(nameof(hour)))), (byte)(minute >= 0 && minute <= 59 ? minute : throw new System.ArgumentOutOfRangeException(nameof(minute))), (byte)(second >= 0 && second <= 59 ? second : throw new System.ArgumentOutOfRangeException(nameof(second))), (byte)(frame >= 0 && frame <= 29 ? frame : throw new System.ArgumentOutOfRangeException(nameof(frame))), (byte)Status.EndOfExclusive };
    public static byte[] MtcQuarterFrame(int type, int value)
      => new byte[] { (byte)Status.MidiTimeCodeQuarterFrame, (byte)((Utility.Ensure3BitByte(type) << 4) | Utility.Ensure4BitByte(value)) };
    public static byte[] MtcQuarterFrame0(int frameNumber)
      => MtcQuarterFrame(0x0, Utility.Ensure5BitByte(frameNumber) & 0xF);
    public static byte[] MtcQuarterFrame1(int frameNumber)
      => MtcQuarterFrame(0x1, Utility.Ensure5BitByte(frameNumber) >> 4);
    public static byte[] MtcQuarterFrame2(int second)
      => MtcQuarterFrame(0x2, Utility.Ensure6BitByte(second) & 0xF);
    public static byte[] MtcQuarterFrame3(int second)
      => MtcQuarterFrame(0x3, Utility.Ensure6BitByte(second) >> 4);
    public static byte[] MtcQuarterFrame4(int minute)
      => MtcQuarterFrame(0x4, Utility.Ensure6BitByte(minute) & 0xF);
    public static byte[] MtcQuarterFrame5(int minute)
      => MtcQuarterFrame(0x5, Utility.Ensure6BitByte(minute) >> 4);
    public static byte[] MtcQuarterFrame6(int hour)
      => MtcQuarterFrame(0x6, Utility.Ensure5BitByte(hour) & 0xF);
    public static byte[] MtcQuarterFrame7(int hour, int rate)
      => MtcQuarterFrame(0x7, Utility.Ensure2BitByte(rate) | Utility.Ensure5BitByte(hour) >> 4);
    public static byte[] SongPositionPointer(int songPositionInMidiBeats)
       => new byte[] { (byte)Status.SongPositionPointer, Utility.Ensure14BitLoByte(songPositionInMidiBeats), Utility.Ensure14BitHiByte(songPositionInMidiBeats) };
    public static byte[] SongSelect(int songNumber)
      => new byte[] { (byte)Status.SongSelect, Utility.Ensure7BitByte(songNumber) };
    public static byte[] TuneRequest()
      => new byte[] { (byte)Status.TuneRequest };
  }
}
