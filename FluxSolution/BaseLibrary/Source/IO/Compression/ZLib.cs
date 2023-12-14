namespace Flux.Compression
{
  public class ZLib
    : ICompression
  {
    /// <summary>Compress the source stream to the specified stream using gzip.</summary>
    public void Compress(System.IO.Stream input, System.IO.Stream output)
    {
      System.ArgumentNullException.ThrowIfNull(input);
      System.ArgumentNullException.ThrowIfNull(output);

      using var compressor = new System.IO.Compression.ZLibStream(output, System.IO.Compression.CompressionMode.Compress);
      input.CopyTo(compressor);

      input.Flush();
      output.Flush();
    }
    /// <summary>Decompress the source stream to the specified stream using gzip.</summary>
    public void Decompress(System.IO.Stream input, System.IO.Stream output)
    {
      System.ArgumentNullException.ThrowIfNull(input);
      System.ArgumentNullException.ThrowIfNull(output);

      using var decompressor = new System.IO.Compression.ZLibStream(input, System.IO.Compression.CompressionMode.Decompress);
      decompressor.CopyTo(output);

      input.Flush();
      output.Flush();
    }
  }
}
