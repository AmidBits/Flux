namespace Flux
{
  public interface ISymmetricEncryptable
  {
    /// <summary>Encrypt an <paramref name="input"/> stream into an <paramref name="output"/> stream.</summary>
    void Encrypt(System.IO.Stream input, byte[] key, byte[] iv, System.IO.Stream output) => throw new System.NotImplementedException();

    /// <summary>
    /// <para>Encrypt <paramref name="input"/> to a new byte array.</para>
    /// </summary>
    /// <param name="input">The bytes to compress.</param>
    /// <returns></returns>
    byte[] Encrypt(byte[] input, byte[] key, byte[] iv)
    {
      using var inputStream = new System.IO.MemoryStream(input);
      using var outputStream = new System.IO.MemoryStream();

      Encrypt(inputStream, key, iv, outputStream);

      outputStream.Flush();

      return outputStream.ToArray();
    }

    /// <summary>
    /// <para>Attempts to encrypt <paramref name="input"/> to a new byte array as out parameter <paramref name="output"/>.</para>
    /// </summary>
    /// <param name="input">The bytes to compress.</param>
    /// <param name="output">The compressed bytes.</param>
    /// <returns></returns>
    bool TryEncrypt(byte[] input, byte[] key, byte[] iv, out byte[] output)
    {
      try
      {
        output = Encrypt(input, key, iv);
        return true;
      }
      catch { }

      output = default!;
      return false;
    }
  }
}
