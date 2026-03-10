namespace Flux
{
  public static partial class BinaryReaderExtensions
  {
    extension(System.IO.BinaryReader source)
    {
      public System.Numerics.BigInteger ReadBigInteger(int numberOfBytes, bool reverseBytes)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        System.Span<byte> bytes = stackalloc byte[numberOfBytes];
        source.Read(bytes);
        if (reverseBytes)
          bytes.Reverse();
        return new System.Numerics.BigInteger(bytes);
      }
    }
  }
}
