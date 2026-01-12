namespace Flux.Compressor
{
  public sealed class Deflate
    : ICompressor
  {
    public static ICompressor Default { get; } = new Deflate();
    public static ICompressor Max { get; } = new Deflate() { ZLibCompressionOptions = new System.IO.Compression.ZLibCompressionOptions() { CompressionLevel = 9 } };

    private System.IO.Compression.ZLibCompressionOptions m_zLibCompressionOptions = new System.IO.Compression.ZLibCompressionOptions();

    /// <summary>
    /// <para>Compression options for the Brotli algoritm.</para>
    /// </summary>
    public System.IO.Compression.ZLibCompressionOptions ZLibCompressionOptions { get => m_zLibCompressionOptions; set => m_zLibCompressionOptions = value ?? new System.IO.Compression.ZLibCompressionOptions(); }

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <remarks>
    /// <para>Leaves the stream open.</para>
    /// </remarks>
    /// <param name="input"></param>
    /// <param name="output"></param>
    public void Compress(System.IO.Stream input, System.IO.Stream output)
    {
      System.ArgumentNullException.ThrowIfNull(input);
      System.ArgumentNullException.ThrowIfNull(output);

      using var compressor = new System.IO.Compression.DeflateStream(output, m_zLibCompressionOptions, true);
      input.CopyTo(compressor);
    }

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <remarks>
    /// <para>Leaves the stream open.</para>
    /// <param name="input"></param>
    /// <param name="output"></param>
    public void Decompress(System.IO.Stream input, System.IO.Stream output)
    {
      System.ArgumentNullException.ThrowIfNull(input);
      System.ArgumentNullException.ThrowIfNull(output);

      using var decompressor = new System.IO.Compression.DeflateStream(input, System.IO.Compression.CompressionMode.Decompress, true);
      decompressor.CopyTo(output);
    }
  }
}
