namespace Flux
{
  public static partial class GenericMath
  {
    public static TInteger CreateRandom<TInteger>()
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      var bytes = new byte[TInteger.Zero.GetByteCount()];

      System.Security.Cryptography.RandomNumberGenerator.Fill(bytes);

      return TInteger.ReadLittleEndian(bytes, typeof(TInteger).IsUnsignedNumericType());
    }
  }
}
