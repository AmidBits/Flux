namespace Flux
{
  public interface IAsymmetricDecryptable
  {
    /// <summary>
    /// <para>Attempts to decrypt a byte array to a new byte array using an <paramref name="rsaPrivateKey"/>.</para>
    /// </summary>
    /// <param name="input">The bytes to decompress.</param>
    /// <param name="resultBytes">The decompressed bytes.</param>
    /// <returns></returns>
    byte[] Decrypt(byte[] input, byte[] rsaPrivateKey);
  }
}
