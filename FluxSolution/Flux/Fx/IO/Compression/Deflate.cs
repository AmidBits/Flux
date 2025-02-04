namespace Flux.Compression
{
  /// <summary>Deflate compression algorithm.</summary>
  public class Deflate
    : ICompressable
  {
    public void Compress(System.IO.Stream input, System.IO.Stream output)
    {
      System.ArgumentNullException.ThrowIfNull(input);
      System.ArgumentNullException.ThrowIfNull(output);

      using var compressor = new System.IO.Compression.DeflateStream(output, System.IO.Compression.CompressionMode.Compress, true);
      input.CopyTo(compressor);

      output.Flush();
    }

    public void Decompress(System.IO.Stream input, System.IO.Stream output)
    {
      System.ArgumentNullException.ThrowIfNull(input);
      System.ArgumentNullException.ThrowIfNull(output);

      using var decompressor = new System.IO.Compression.DeflateStream(input, System.IO.Compression.CompressionMode.Decompress);
      decompressor.CopyTo(output);

      output.Flush();
    }
  }
}
