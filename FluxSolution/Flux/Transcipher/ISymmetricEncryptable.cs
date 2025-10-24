namespace Flux
{
  public interface ISymmetricEncryptable
  {
    /// <summary>Encrypt an <paramref name="input"/> stream into an <paramref name="output"/> stream.</summary>
    void Encrypt(System.IO.Stream input, byte[] key, byte[] salt, System.IO.Stream output) => throw new System.NotImplementedException();

    /// <summary>
    /// <para>Encrypt <paramref name="sourceBytes"/> to a new byte array.</para>
    /// </summary>
    /// <param name="sourceBytes">The bytes to compress.</param>
    /// <returns></returns>
    byte[] Encrypt(byte[] sourceBytes, byte[] key, byte[] salt)
    {
      using var inputStream = new System.IO.MemoryStream(sourceBytes);
      using var outputStream = new System.IO.MemoryStream();

      Encrypt(inputStream, key, salt, outputStream);

      return outputStream.ToArray();
    }

    /// <summary>
    /// <para>Attempts to encrypt <paramref name="sourceBytes"/> to a new byte array as out parameter <paramref name="resultBytes"/>.</para>
    /// </summary>
    /// <param name="sourceBytes">The bytes to compress.</param>
    /// <param name="resultBytes">The compressed bytes.</param>
    /// <returns></returns>
    bool TryEncrypt(byte[] sourceBytes, byte[] key, byte[] salt, out byte[] resultBytes)
    {
      try
      {
        resultBytes = Encrypt(sourceBytes, key, salt);
        return true;
      }
      catch { }

      resultBytes = default!;
      return false;
    }
  }
}
