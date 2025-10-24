namespace Flux.Transcipher.Asymmetric
{
  public sealed class EcDsaCng
    : IAsymmetricTranscipherable
  {
    //public static IAsymmetricTranscipherable Default { get; } = new EcDsaCng();

    public byte[] Decrypt(byte[] sourceBytes, byte[] ecDsaPrivateKey) => throw new System.NotImplementedException();

    public byte[] Encrypt(byte[] sourceBytes, byte[] ecDsaPublicKey) => throw new System.NotImplementedException();
  }
}
