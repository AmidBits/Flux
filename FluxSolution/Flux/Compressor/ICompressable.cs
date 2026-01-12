namespace Flux
{
  public interface ICompressable
  {
    /// <summary>Compress an <paramref name="input"/> stream into an <paramref name="output"/> stream.</summary>
    void Compress(System.IO.Stream input, System.IO.Stream output);

    /// <summary>
    /// <para>Attempts to compress <paramref name="input"/> to a new byte array.</para>
    /// </summary>
    /// <param name="input">The bytes to compress.</param>
    /// <returns></returns>
    byte[] Compress(byte[] input)
    {
      using var inputStream = new System.IO.MemoryStream(input);
      using var outputStream = new System.IO.MemoryStream();

      Compress(inputStream, outputStream);

      outputStream.Flush();

      return outputStream.ToArray();
    }

    /// <summary>
    /// <para>Attempts to compress <paramref name="input"/> to a new byte array as out parameter <paramref name="output"/>.</para>
    /// </summary>
    /// <param name="input">The bytes to compress.</param>
    /// <param name="output">The compressed bytes.</param>
    /// <returns></returns>
    bool TryCompress(byte[] input, out byte[] output)
    {
      try
      {
        output = Compress(input);
        return true;
      }
      catch { }

      output = default!;
      return false;
    }

    byte[] Compress(string input, System.Text.Encoding encoding)
    {
      var bytes = encoding.GetBytes(input);

      return Compress(bytes);
    }
  }
}
