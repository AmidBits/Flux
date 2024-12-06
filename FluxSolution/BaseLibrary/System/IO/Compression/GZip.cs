namespace Flux.Compression
{
  /// <summary>GZip compression algorithm.</summary>
  public class GZip
    : ICompressable
  {
    public void Compress(System.IO.Stream input, System.IO.Stream output)
    {
      System.ArgumentNullException.ThrowIfNull(input);
      System.ArgumentNullException.ThrowIfNull(output);

      using var compressor = new System.IO.Compression.GZipStream(output, System.IO.Compression.CompressionMode.Compress);
      input.CopyTo(compressor);

      output.Flush();
    }

    public void Decompress(System.IO.Stream input, System.IO.Stream output)
    {
      System.ArgumentNullException.ThrowIfNull(input);
      System.ArgumentNullException.ThrowIfNull(output);

      using var decompressor = new System.IO.Compression.GZipStream(input, System.IO.Compression.CompressionMode.Decompress);
      decompressor.CopyTo(output);

      output.Flush();
    }
  }
}
