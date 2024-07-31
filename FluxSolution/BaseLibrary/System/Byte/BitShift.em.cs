namespace Flux
{
  public static partial class Fx
  {
    public static bool BitFlagCarryLsb<TSelf>(this TSelf source) => !TSelf.IsZero(source & TSelf.One);
    public static bool BitFlagCarryMsb<TSelf>(this TSelf source) => !TSelf.IsZero(source & TSelf.RotateRight(TSelf.One, 1));

    public static byte BitShiftLeft<TSelf>(this TSelf source, bool lsb) => (source << 1) | (lsb ? 1 : 0);
    public static byte BitShiftRight<TSelf>(this TSelf source, bool msb) => (msb ? TSelf.RotateRight(TSelf.One, 1) : 0) | (source >> 1);

    /// <summary>Returns a sequence bit-shifted left by count bits, by extending the array with the necessary number of bytes.</summary>
    public static System.Collections.Generic.IEnumerable<byte> BitShiftLeft(this System.Collections.Generic.IEnumerable<byte> source, int count)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var effectiveShift = (count % 8);
      var inverseShift = (8 - effectiveShift);

      byte previousByte = 0;

      foreach (var currentByte in source)
      {
        yield return (byte)((previousByte << effectiveShift) | (currentByte >> inverseShift));

        previousByte = currentByte;
      }

      yield return (byte)(previousByte << effectiveShift);

      for (var i = count / 8; i > 0; i--)
      {
        yield return 0;
      }
    }
    /// <summary>Returns a sequence bit-shifted right by count bits, by extending the array with the necessary number of bytes.</summary>
    public static System.Collections.Generic.IEnumerable<byte> BitShiftRight(this System.Collections.Generic.IEnumerable<byte> source, int count)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      for (var i = count / 8; i > 0; i--)
      {
        yield return 0;
      }

      var effectiveShift = (count % 8);
      var inverseShift = (8 - effectiveShift);

      byte previousByte = 0;

      foreach (var currentByte in source)
      {
        yield return (byte)((previousByte << inverseShift) | (currentByte >> effectiveShift));

        previousByte = currentByte;
      }

      yield return (byte)(previousByte << inverseShift);
    }
  }
}
