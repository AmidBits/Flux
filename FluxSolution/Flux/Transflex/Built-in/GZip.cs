namespace Flux.Transflex
{
  public sealed class GZip
    : ITransflexable
  {
    public static ITransflexable Default { get; } = new GZip();
    public static ITransflexable Max { get; } = new GZip() { ZLibCompressionOptions = new System.IO.Compression.ZLibCompressionOptions() { CompressionLevel = 9 } };

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

      using var compressor = new System.IO.Compression.GZipStream(output, m_zLibCompressionOptions, true);
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

      using var decompressor = new System.IO.Compression.GZipStream(input, System.IO.Compression.CompressionMode.Decompress, true);
      decompressor.CopyTo(output);
    }
  }
}
