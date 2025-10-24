namespace Flux
{
  public interface ICompressable
  {
    /// <summary>Compress an <paramref name="input"/> stream into an <paramref name="output"/> stream.</summary>
    void Compress(System.IO.Stream input, System.IO.Stream output) => throw new System.NotImplementedException();

    /// <summary>
    /// <para>Attempts to compress <paramref name="sourceBytes"/> to a new byte array.</para>
    /// </summary>
    /// <param name="sourceBytes">The bytes to compress.</param>
    /// <returns></returns>
    byte[] Compress(byte[] sourceBytes)
    {
      using var inputStream = new System.IO.MemoryStream(sourceBytes);
      using var outputStream = new System.IO.MemoryStream();

      Compress(inputStream, outputStream);

      return outputStream.ToArray();
    }

    /// <summary>
    /// <para>Attempts to compress <paramref name="sourceBytes"/> to a new byte array as out parameter <paramref name="resultBytes"/>.</para>
    /// </summary>
    /// <param name="sourceBytes">The bytes to compress.</param>
    /// <param name="resultBytes">The compressed bytes.</param>
    /// <returns></returns>
    bool TryCompress(byte[] sourceBytes, out byte[] resultBytes)
    {
      try
      {
        resultBytes = Compress(sourceBytes);
        return true;
      }
      catch { }

      resultBytes = default!;
      return false;
    }
  }
}
