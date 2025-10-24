namespace Flux
{
  public interface ISymmetricDecryptable
  {
    /// <summary>Decrypt an <paramref name="input"/> stream into an <paramref name="output"/> stream.</summary>
    void Decrypt(System.IO.Stream input, byte[] key, byte[] salt, System.IO.Stream output) => throw new System.NotImplementedException();

    /// <summary>
    /// <para>Attempts to encrypt <paramref name="sourceBytes"/> to a new byte array.</para>
    /// </summary>
    /// <param name="sourceBytes">The bytes to decompress.</param>
    /// <returns></returns>
    byte[] Decrypt(byte[] sourceBytes, byte[] key, byte[] salt)
    {
      using var inputStream = new System.IO.MemoryStream(sourceBytes);
      using var outputStream = new System.IO.MemoryStream();

      Decrypt(inputStream, key, salt, outputStream);

      return outputStream.ToArray();
    }

    /// <summary>
    /// <para>Attempts to encrypt <paramref name="sourceBytes"/> to a new byte array as out parameter <paramref name="resultBytes"/>.</para>
    /// </summary>
    /// <param name="sourceBytes">The bytes to decompress.</param>
    /// <param name="resultBytes">The decompressed bytes.</param>
    /// <returns></returns>
    bool TryDecrypt(byte[] sourceBytes, byte[] key, byte[] salt, out byte[] resultBytes)
    {
      try
      {
        resultBytes = Decrypt(sourceBytes, key, salt);
        return true;
      }
      catch { }

      resultBytes = default!;
      return false;
    }
  }
}
