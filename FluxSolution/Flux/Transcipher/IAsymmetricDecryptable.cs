namespace Flux
{
  public interface IAsymmetricDecryptable
  {
    /// <summary>
    /// <para>Attempts to encrypt <paramref name="sourceBytes"/> to a new byte array as out parameter <paramref name="resultBytes"/>.</para>
    /// </summary>
    /// <param name="sourceBytes">The bytes to decompress.</param>
    /// <param name="resultBytes">The decompressed bytes.</param>
    /// <returns></returns>
    byte[] Decrypt(byte[] sourceBytes, byte[] rsaPrivateKey);
  }
}
