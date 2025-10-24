namespace Flux
{
  public interface IAsymmetricEncryptable
  {
    /// <summary>
    /// <para>Attempts to encrypt <paramref name="sourceBytes"/> to a new byte array as out parameter <paramref name="resultBytes"/>.</para>
    /// </summary>
    /// <param name="sourceBytes">The bytes to compress.</param>
    /// <param name="resultBytes">The compressed bytes.</param>
    /// <returns></returns>
    byte[] Encrypt(byte[] sourceBytes, byte[] rsaPublicKey);
  }
}
