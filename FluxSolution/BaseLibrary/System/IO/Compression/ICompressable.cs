namespace Flux.Compression
{
  /// <summary>Compression interface.</summary>
  public interface ICompressable
  {
    /// <summary>Compress the <paramref name="input"/> stream to the <paramref name="output"/> stream.</summary>
    abstract void Compress(System.IO.Stream input, System.IO.Stream output);
    /// <summary>Decompress the <paramref name="input"/> stream to the <paramref name="output"/> stream.</summary>
    abstract void Decompress(System.IO.Stream input, System.IO.Stream output);

    /// <summary>
    /// <para>Attempts to compress the <paramref name="inputBytes"/> to the <paramref name="outputBytes"/>.</para>
    /// </summary>
    /// <param name="inputBytes">The bytes to compress.</param>
    /// <param name="outputBytes">The compressed bytes.</param>
    /// <returns></returns>
    bool TryCompress(byte[] inputBytes, out byte[] outputBytes)
    {
      try
      {
        using var inputStream = new System.IO.MemoryStream(inputBytes);
        using var outputStream = new System.IO.MemoryStream();

        Compress(inputStream, outputStream);

        outputBytes = outputStream.ToArray();
        return true;
      }
      catch { }

      outputBytes = default!;
      return false;
    }

    /// <summary>
    /// <para>Attempts to compress <paramref name="inputChars"/> to <paramref name="outputChars"/> (encoded as Base64).</para>
    /// </summary>
    /// <param name="inputChars">The characters to compress.</param>
    /// <param name="outputChars">The compressed characters (encoded as Base64).</param>
    /// <returns></returns>
    bool TryCompress(System.ReadOnlySpan<char> inputChars, out string outputChars)
    {
      try
      {
        var data = new byte[System.Text.Encoding.UTF8.GetByteCount(inputChars)];

        System.Text.Encoding.UTF8.GetBytes(inputChars, data);

        if (TryCompress(data, out var compressedBytes))
        {
          outputChars = System.Convert.ToBase64String(compressedBytes);
          return true;
        }
      }
      catch { }

      outputChars = default!;
      return false;
    }

    /// <summary>
    /// <summary>Attempts to decompress the <paramref name="inputBytes"/> to <paramref name="outputBytes"/>.</summary>
    /// </summary>
    /// <param name="inputBytes">The bytes to decompress.</param>
    /// <param name="outputBytes">The decompressed bytes.</param>
    /// <returns></returns>
    bool TryDecompress(byte[] inputBytes, out byte[] outputBytes)
    {
      try
      {
        using var inputStream = new System.IO.MemoryStream(inputBytes);
        using var outputStream = new System.IO.MemoryStream();

        Decompress(inputStream, outputStream);

        outputBytes = outputStream.ToArray();
        return true;
      }
      catch { }

      outputBytes = default!;
      return false;
    }

    /// <summary>
    /// <para>Attempts to decompress <paramref name="inputChars"/> (encoded as Base64) to <paramref name="outputChars"/>.</para>
    /// </summary>
    /// <param name="inputChars">The characters to decompress (encoded as Base64).</param>
    /// <param name="outputChars">The decompressed characters.</param>
    /// <returns></returns>
    bool TryDecompress(string inputChars, out string outputChars)
    {
      try
      {
        var compressedBytes = System.Convert.FromBase64String(inputChars);

        if (TryDecompress(compressedBytes, out var decompressedBytes))
        {
          outputChars = System.Text.Encoding.UTF8.GetString(decompressedBytes);
          return true;
        }
      }
      catch { }

      outputChars = default!;
      return false;
    }
  }
}
