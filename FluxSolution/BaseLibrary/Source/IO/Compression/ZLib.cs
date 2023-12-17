namespace Flux.Compression
{
  /// <summary>ZLib compression algorithm.</summary>
  public class ZLib
    : ICompression
  {
    public void Compress(System.IO.Stream input, System.IO.Stream output)
    {
      System.ArgumentNullException.ThrowIfNull(input);
      System.ArgumentNullException.ThrowIfNull(output);

      using var compressor = new System.IO.Compression.ZLibStream(output, System.IO.Compression.CompressionMode.Compress);
      input.CopyTo(compressor);

      output.Flush();
    }

    public void Decompress(System.IO.Stream input, System.IO.Stream output)
    {
      System.ArgumentNullException.ThrowIfNull(input);
      System.ArgumentNullException.ThrowIfNull(output);

      using var decompressor = new System.IO.Compression.ZLibStream(input, System.IO.Compression.CompressionMode.Decompress);
      decompressor.CopyTo(output);

      output.Flush();
    }
  }
}
