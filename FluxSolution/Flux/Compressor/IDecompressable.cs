namespace Flux
{
  public interface IDecompressable
  {
    /// <summary>Decompress an <paramref name="input"/> stream into an <paramref name="output"/> stream.</summary>
    void Decompress(System.IO.Stream input, System.IO.Stream output);

    /// <summary>
    /// <summary>Attempts to decompress <paramref name="input"/> to a new byte array.</summary>
    /// </summary>
    /// <param name="input">The bytes to decompress.</param>
    /// <returns></returns>
    byte[] Decompress(byte[] input)
    {
      using var inputStream = new System.IO.MemoryStream(input);
      using var outputStream = new System.IO.MemoryStream();

      Decompress(inputStream, outputStream);

      outputStream.Flush();

      return outputStream.ToArray();
    }

    /// <summary>
    /// <summary>Attempts to decompress <paramref name="input"/> to a new byte array as out parameter <paramref name="output"/>.</summary>
    /// </summary>
    /// <param name="input">The bytes to decompress.</param>
    /// <param name="output">The decompressed bytes.</param>
    /// <returns></returns>
    bool TryDecompress(byte[] input, out byte[] output)
    {
      try
      {
        output = Decompress(input);
        return true;
      }
      catch { }

      output = default!;
      return false;
    }

    string Decompress(byte[] input, System.Text.Encoding encoding)
    {
      var bytes = Decompress(input);

      return encoding.GetString(bytes);
    }
  }
}
