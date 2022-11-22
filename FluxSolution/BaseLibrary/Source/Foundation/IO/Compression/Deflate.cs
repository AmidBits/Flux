namespace Flux.Compression
{
  public class Deflate
    : ICompression
  {
    /// <summary>Compress the source stream to the specified stream using deflate.</summary>
    public void Compress(Stream input, Stream output)
    {
      if (input is null) throw new ArgumentNullException(nameof(input));
      using var compressor = new System.IO.Compression.DeflateStream(output, System.IO.Compression.CompressionMode.Compress, true);
      input.CopyTo(compressor);
    }
    /// <summary>Decompress the source stream to the specified stream using deflate.</summary>
    public void Decompress(Stream input, Stream output)
    {
      using var decompressor = new System.IO.Compression.DeflateStream(input, System.IO.Compression.CompressionMode.Decompress);
      decompressor.CopyTo(output);
    }
  }
}
