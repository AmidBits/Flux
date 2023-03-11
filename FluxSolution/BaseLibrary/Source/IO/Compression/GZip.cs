namespace Flux.Compression
{
  public class GZip
    : ICompression
  {
    /// <summary>Compress the source stream to the specified stream using gzip.</summary>
    public void Compress(System.IO.Stream input, System.IO.Stream output)
    {
      if (input is null) throw new ArgumentNullException(nameof(input));
      using var compressor = new System.IO.Compression.GZipStream(output, System.IO.Compression.CompressionMode.Compress);
      input.CopyTo(compressor);
    }
    /// <summary>Decompress the source stream to the specified stream using gzip.</summary>
    public void Decompress(System.IO.Stream input, System.IO.Stream output)
    {
      using var decompressor = new System.IO.Compression.GZipStream(input, System.IO.Compression.CompressionMode.Decompress);
      decompressor.CopyTo(output);
    }
  }
}
