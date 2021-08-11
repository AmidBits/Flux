namespace Flux.Compression
{
  public static class Deflate
  {
    /// <summary>Compress the source stream to the specified stream using deflate.</summary>
    public static void Compress(System.IO.Stream input, System.IO.Stream output)
    {
      if (input is null) throw new System.ArgumentNullException(nameof(input));
      using var compressor = new System.IO.Compression.DeflateStream(output, System.IO.Compression.CompressionMode.Compress, true);
      input.CopyTo(compressor);
    }
    /// <summary>Decompress the source stream to the specified stream using deflate.</summary>
    public static void Decompress(System.IO.Stream input, System.IO.Stream output)
    {
      using var decompressor = new System.IO.Compression.DeflateStream(input, System.IO.Compression.CompressionMode.Decompress);
      decompressor.CopyTo(output);
    }

    /// <summary>Attempts to compress the source byte array to new byte array using deflate.</summary>
    public static bool TryCompress(byte[] data, out byte[] result)
    {
      try
      {
        using var input = new System.IO.MemoryStream(data);
        using var output = new System.IO.MemoryStream();
        Compress(input, output);
        result = output.ToArray();
        return true;
      }
      catch { }

      result = default!;
      return false;
    }
    /// <summary>Attempts to compress the source string to the out string (base64) using deflate.</summary>
    public static bool TryCompress(string data, out string result)
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
    public static bool TryDecompress(byte[] data, out byte[] result)
    {
      try
      {
        using var input = new System.IO.MemoryStream(data);
        using var output = new System.IO.MemoryStream();
        Decompress(input, output);
        result = output.ToArray();
        return true;
      }
      catch { }

      result = default!;
      return false;
    }
    /// <summary>Attempts to decompress the source string (base64) to the out string using deflate.</summary>
    public static bool TryDecompress(string data, out string result)
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

  public static class Gzip
  {
    /// <summary>Compress the source stream to the specified stream using gzip.</summary>
    public static void Compress(System.IO.Stream input, System.IO.Stream output)
    {
      if (input is null) throw new System.ArgumentNullException(nameof(input));
      using var compressor = new System.IO.Compression.GZipStream(output, System.IO.Compression.CompressionMode.Compress);
      input.CopyTo(compressor);
    }
    /// <summary>Decompress the source stream to the specified stream using gzip.</summary>
    public static void Decompress(System.IO.Stream input, System.IO.Stream output)
    {
      using var decompressor = new System.IO.Compression.GZipStream(input, System.IO.Compression.CompressionMode.Decompress);
      decompressor.CopyTo(output);
    }

    /// <summary>Attempts to compress the source byte array to new byte array using gzip.</summary>
    public static bool TryCompress(byte[] data, out byte[] result)
    {
      try
      {
        using var input = new System.IO.MemoryStream(data);
        using var output = new System.IO.MemoryStream();
        Compress(input, output);
        result = output.ToArray();
        return true;
      }
      catch { }

      result = default!;
      return false;
    }
    /// <summary>Attempts to compress the source string to the out string (base64) using gzip.</summary>
    public static bool TryCompress(string data, out string result)
    {
      try
      {
        if (TryCompress(System.Text.Encoding.UTF8.GetBytes(data), out var bytes))
        {
          result = System.Convert.ToBase64String(bytes);
          return true;
        }
      }
      catch { }

      result = default!;
      return false;
    }

    /// <summary>Attempts to decompress the source byte array to new byte array using gzip.</summary>
    public static bool TryDecompress(byte[] data, out byte[] result)
    {
      try
      {
        using var input = new System.IO.MemoryStream(data);
        using var output = new System.IO.MemoryStream();
        Decompress(input, output);
        result = output.ToArray();
        return true;
      }
      catch { }

      result = default!;
      return false;
    }
    /// <summary>Attempts to decompress the source string (base64) to the out string using gzip.</summary>
    public static bool TryDecompress(string data, out string result)
    {
      try
      {
        if (TryDecompress(System.Convert.FromBase64String(data), out var bytes))
        {
          result = System.Text.Encoding.UTF8.GetString(bytes);
          return false;
        }
      }
      catch { }

      result = default!;
      return false;
    }
  }
}
