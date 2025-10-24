namespace Flux
{
  public interface IDecompressable
  {
    /// <summary>Decompress an <paramref name="input"/> stream into an <paramref name="output"/> stream.</summary>
    void Decompress(System.IO.Stream input, System.IO.Stream output) => throw new System.NotImplementedException();

    /// <summary>
    /// <summary>Attempts to decompress <paramref name="sourceBytes"/> to a new byte array.</summary>
    /// </summary>
    /// <param name="sourceBytes">The bytes to decompress.</param>
    /// <returns></returns>
    byte[] Decompress(byte[] sourceBytes)
    {
      using var inputStream = new System.IO.MemoryStream(sourceBytes);
      using var outputStream = new System.IO.MemoryStream();

      Decompress(inputStream, outputStream);

      return outputStream.ToArray();
    }

    /// <summary>
    /// <summary>Attempts to decompress <paramref name="sourceBytes"/> to a new byte array as out parameter <paramref name="resultBytes"/>.</summary>
    /// </summary>
    /// <param name="sourceBytes">The bytes to decompress.</param>
    /// <param name="resultBytes">The decompressed bytes.</param>
    /// <returns></returns>
    bool TryDecompress(byte[] sourceBytes, out byte[] resultBytes)
    {
      try
      {
        resultBytes = Decompress(sourceBytes);
        return true;
      }
      catch { }

      resultBytes = default!;
      return false;
    }
  }
}
