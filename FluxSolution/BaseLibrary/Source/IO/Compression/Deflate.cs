namespace Flux.Compression
{
  public class Deflate
    : ICompression
  {
    /// <summary>Compress the source stream to the specified stream using deflate.</summary>
    public void Compress(System.IO.Stream input, System.IO.Stream output)
    {
      System.ArgumentNullException.ThrowIfNull(input);
      System.ArgumentNullException.ThrowIfNull(output);

      using var compressor = new System.IO.Compression.DeflateStream(output, System.IO.Compression.CompressionMode.Compress, true);
      input.CopyTo(compressor);

      input.Flush();
      output.Flush();
    }
    /// <summary>Decompress the source stream to the specified stream using deflate.</summary>
    public void Decompress(System.IO.Stream input, System.IO.Stream output)
    {
      System.ArgumentNullException.ThrowIfNull(input);
      System.ArgumentNullException.ThrowIfNull(output);

      using var decompressor = new System.IO.Compression.DeflateStream(input, System.IO.Compression.CompressionMode.Decompress);
      decompressor.CopyTo(output);

      input.Flush();
      output.Flush();
    }
  }
}
