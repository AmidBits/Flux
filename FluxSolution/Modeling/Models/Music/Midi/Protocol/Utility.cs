namespace Flux.Music.Midi.Protocol
{
  public sealed class Utility
  {
    public static byte Ensure14BitHiByte(int value, string? paramName = null)
      => value >= 0 && value <= 0x3FFF ? (byte)((value >> 7) & 0x7F) : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value));
    public static byte Ensure14BitLoByte(int value, string? paramName = null)
      => value >= 0 && value <= 0x3FFF ? (byte)(value & 0x7F) : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value));
    public static byte Ensure7BitByte(int value, string? paramName = null)
      => value >= 0 && value <= 0x7F ? (byte)value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value));
    public static byte Ensure6BitByte(int value, string? paramName = null)
      => value >= 0 && value <= 0x3F ? (byte)value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value));
    public static byte Ensure5BitByte(int value, string? paramName = null)
      => value >= 0 && value <= 0x1F ? (byte)value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value));
    public static byte Ensure4BitByte(int value, string? paramName = null)
      => value >= 0 && value <= 0xF ? (byte)value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value));
    public static byte Ensure3BitByte(int value, string? paramName = null)
      => value >= 0 && value <= 0x7 ? (byte)value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value));
    public static byte Ensure2BitByte(int value, string? paramName = null)
      => value >= 0 && value <= 0x3 ? (byte)value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value));
  }
}
