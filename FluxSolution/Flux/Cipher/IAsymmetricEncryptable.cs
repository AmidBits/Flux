namespace Flux
{
  public interface IAsymmetricEncryptable
  {
    /// <summary>
    /// <para>Attempts to encrypt a byte array to a new byte array using an <paramref name="rsaPublicKey"/>.</para>
    /// </summary>
    /// <param name="input">The bytes to compress.</param>
    /// <param name="rsaPublicKey">The compressed bytes.</param>
    /// <returns></returns>
    byte[] Encrypt(byte[] input, byte[] rsaPublicKey);
  }
}
