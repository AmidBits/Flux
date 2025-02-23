namespace Flux
{
  public static partial class Fx
  {
    public static System.Numerics.BigInteger ReadBigInteger(this System.IO.BinaryReader source, int numberOfBytes, bool reverseBytes)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var bytes = new byte[numberOfBytes];

      source.Read(bytes, 0, numberOfBytes);

      if (reverseBytes)
        System.Array.Reverse(bytes);

      return new System.Numerics.BigInteger(bytes);
    }
  }
}
