namespace Flux.Midi.Protocol
{
  public sealed class CommonMessage
  {
    public static byte[] MtcFullFrame(int rate, int hour, int minute, int second, int frame)
      => new byte[] { (int)CommonStatus.StartOfExclusive, 0x7F, 0x7F, 0x01, 0x01, (byte)((rate >= 0 && rate <= 3 ? rate << 5 : throw new System.ArgumentOutOfRangeException(nameof(rate))) | (hour >= 0 && hour <= 23 ? hour : throw new System.ArgumentOutOfRangeException(nameof(hour)))), (byte)(minute >= 0 && minute <= 59 ? minute : throw new System.ArgumentOutOfRangeException(nameof(minute))), (byte)(second >= 0 && second <= 59 ? second : throw new System.ArgumentOutOfRangeException(nameof(second))), (byte)(frame >= 0 && frame <= 29 ? frame : throw new System.ArgumentOutOfRangeException(nameof(frame))), (byte)CommonStatus.EndOfExclusive };
    public static byte[] MtcQuarterFrame(int type, int value)
      => new byte[] { (byte)CommonStatus.MidiTimeCodeQuarterFrame, (byte)((Helper.Ensure3BitValue(type) << 4) | Helper.Ensure4BitValue(value)) };
    public static byte[] MtcQuarterFrame0(int frameNumber)
      => MtcQuarterFrame(0x0, Helper.Ensure5BitValue(frameNumber) & 0xF);
    public static byte[] MtcQuarterFrame1(int frameNumber)
      => MtcQuarterFrame(0x1, Helper.Ensure5BitValue(frameNumber) >> 4);
    public static byte[] MtcQuarterFrame2(int second)
      => MtcQuarterFrame(0x2, Helper.Ensure6BitValue(second) & 0xF);
    public static byte[] MtcQuarterFrame3(int second)
      => MtcQuarterFrame(0x3, Helper.Ensure6BitValue(second) >> 4);
    public static byte[] MtcQuarterFrame4(int minute)
      => MtcQuarterFrame(0x4, Helper.Ensure6BitValue(minute) & 0xF);
    public static byte[] MtcQuarterFrame5(int minute)
      => MtcQuarterFrame(0x5, Helper.Ensure6BitValue(minute) >> 4);
    public static byte[] MtcQuarterFrame6(int hour)
      => MtcQuarterFrame(0x6, Helper.Ensure5BitValue(hour) & 0xF);
    public static byte[] MtcQuarterFrame7(int hour, int rate)
      => MtcQuarterFrame(0x7, Helper.Ensure2BitValue(rate) | Helper.Ensure5BitValue(hour) >> 4);
    public static byte[] SongPositionPointer(int songPositionInMidiBeats)
       => new byte[] { (byte)CommonStatus.SongPositionPointer, Helper.Ensure14BitValueLow(songPositionInMidiBeats), Helper.Ensure14BitValueHigh(songPositionInMidiBeats) };
    public static byte[] SongSelect(int songNumber)
      => new byte[] { (byte)CommonStatus.SongSelect, Helper.Ensure7BitValue(songNumber) };
    public static byte[] TuneRequest()
      => new byte[] { (byte)CommonStatus.TuneRequest };
  }
}
