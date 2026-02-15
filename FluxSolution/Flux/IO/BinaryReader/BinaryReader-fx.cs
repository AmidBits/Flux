namespace Flux
{
  public static partial class Streams
  {
    public static System.Numerics.BigInteger ReadBigInteger(this System.IO.BinaryReader source, int numberOfBytes, bool reverseBytes)
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
