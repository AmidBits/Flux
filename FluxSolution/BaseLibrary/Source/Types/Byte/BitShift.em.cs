namespace Flux
{
  public static partial class SystemByteEm
  {
    /// <summary>Returns a sequence bit-shifted left by count bits, by extending the array with the necessary number of bytes.</summary>
    public static System.Collections.Generic.IEnumerable<byte> BitShiftLeft(this System.Collections.Generic.IEnumerable<byte> source, int count)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

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
      if (source is null) throw new System.ArgumentNullException(nameof(source));

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
