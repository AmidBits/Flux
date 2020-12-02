namespace Flux
{
  public static partial class BinaryReaderEm
  {
    public static System.Numerics.BigInteger ReadBigInteger(this System.IO.BinaryReader source, int numberOfBytes, bool reverseBytes)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var bytes = new byte[numberOfBytes];

      source.Read(bytes, 0, numberOfBytes);

      if (reverseBytes)
      {
        var halfLength = bytes.Length / 2;

        for (var index = 0; index < halfLength; index++)
        {
          var swap = bytes[index];
          bytes[index] = bytes[bytes.Length - 1 - index];
          bytes[bytes.Length - 1 - index] = swap;
        }
      }

      return new System.Numerics.BigInteger(bytes);
    }
  }
}
