namespace Flux.Midi.Protocol
{
  public sealed class Helper
  {
    public static byte Ensure14BitValueHigh(int value)
      => value < 0 || value > 0x3FFF ? throw new System.ArgumentOutOfRangeException(nameof(value)) : (byte)((value >> 7) & 0x7F);
    public static byte Ensure14BitValueLow(int value)
      => value < 0 || value > 0x3FFF ? throw new System.ArgumentOutOfRangeException(nameof(value)) : (byte)(value & 0x7F);
    public static byte Ensure7BitValue(int value)
      => value < 0 || value > 0x7F ? throw new System.ArgumentOutOfRangeException(nameof(value)) : (byte)value;
    public static byte Ensure6BitValue(int value)
      => value < 0 || value > 0x3F ? throw new System.ArgumentOutOfRangeException(nameof(value)) : (byte)value;
    public static byte Ensure5BitValue(int value)
      => value < 0 || value > 0x1F ? throw new System.ArgumentOutOfRangeException(nameof(value)) : (byte)value;
    public static byte Ensure4BitValue(int value)
      => value < 0 || value > 0xF ? throw new System.ArgumentOutOfRangeException(nameof(value)) : (byte)value;
    public static byte Ensure3BitValue(int value)
      => value < 0 || value > 0x7 ? throw new System.ArgumentOutOfRangeException(nameof(value)) : (byte)value;
    public static byte Ensure2BitValue(int value)
      => value < 0 || value > 0x3 ? throw new System.ArgumentOutOfRangeException(nameof(value)) : (byte)value;
  }
}
