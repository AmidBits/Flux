namespace Flux
{
  public interface ISymmetricDecryptable
  {
    /// <summary>Decrypt an <paramref name="input"/> stream into an <paramref name="output"/> stream.</summary>
    void Decrypt(System.IO.Stream input, byte[] key, byte[] iv, System.IO.Stream output) => throw new System.NotImplementedException();

    /// <summary>
    /// <para>Attempts to encrypt <paramref name="input"/> to a new byte array.</para>
    /// </summary>
    /// <param name="input">The bytes to decompress.</param>
    /// <returns></returns>
    byte[] Decrypt(byte[] input, byte[] key, byte[] iv)
    {
      using var inputStream = new System.IO.MemoryStream(input);
      using var outputStream = new System.IO.MemoryStream();

      Decrypt(inputStream, key, iv, outputStream);

      outputStream.Flush();

      return outputStream.ToArray();
    }

    /// <summary>
    /// <para>Attempts to encrypt <paramref name="input"/> to a new byte array as out parameter <paramref name="output"/>.</para>
    /// </summary>
    /// <param name="input">The bytes to decompress.</param>
    /// <param name="output">The decompressed bytes.</param>
    /// <returns></returns>
    bool TryDecrypt(byte[] input, byte[] key, byte[] iv, out byte[] output)
    {
      try
      {
        output = Decrypt(input, key, iv);
        return true;
      }
      catch { }

      output = default!;
      return false;
    }
  }
}
