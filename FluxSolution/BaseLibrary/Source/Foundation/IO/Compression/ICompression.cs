namespace Flux.Compression
{
  public interface ICompression
  {
    /// <summary>Compress the source stream to the specified stream using deflate.</summary>
    abstract void Compress(Stream input, Stream output);
    /// <summary>Decompress the source stream to the specified stream using deflate.</summary>
    abstract void Decompress(Stream input, Stream output);

    /// <summary>Attempts to compress the source byte array to new byte array using deflate.</summary>
    bool TryCompress(byte[] data, out byte[] result)
    {
      try
      {
        using var input = new MemoryStream(data);
        using var output = new MemoryStream();
        Compress(input, output);
        result = output.ToArray();
        return true;
      }
      catch { }

      result = default!;
      return false;
    }
    /// <summary>Attempts to compress the source string to the out string (base64) using deflate.</summary>
    bool TryCompress(string data, out string result)
    {
      if (TryCompress(System.Text.Encoding.UTF8.GetBytes(data), out var bytes))
      {
        result = System.Convert.ToBase64String(bytes);
        return true;
      }
      result = default!;
      return false;
    }

    /// <summary>Attempts to decompress the source byte array to new byte array using deflate.</summary>
    bool TryDecompress(byte[] data, out byte[] result)
    {
      try
      {
        using var input = new MemoryStream(data);
        using var output = new MemoryStream();
        Decompress(input, output);
        result = output.ToArray();
        return true;
      }
      catch { }

      result = default!;
      return false;
    }
    /// <summary>Attempts to decompress the source string (base64) to the out string using deflate.</summary>
    bool TryDecompress(string data, out string result)
    {
      if (TryDecompress(System.Convert.FromBase64String(data), out var bytes))
      {
        result = System.Text.Encoding.UTF8.GetString(bytes);
        return true;
      }

      result = default!;
      return false;
    }
  }
}