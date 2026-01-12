namespace Flux.Cipher.Asymmetric
{
  public sealed class DsaCng
    : IAsymmetricCipher
  {
    //public static IAsymmetricTranscipherable Default { get; } = new DsaCng();

    public byte[] Decrypt(byte[] sourceBytes, byte[] dsaPrivateKey) => throw new System.NotImplementedException();

    public byte[] Encrypt(byte[] sourceBytes, byte[] dsaPublicKey) => throw new System.NotImplementedException();
  }
}
