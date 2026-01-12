namespace Flux.Compressor
{
  public sealed class Brotli
    : ICompressor
  {
    public static ICompressor Default { get; } = new Brotli();
    public static ICompressor Max { get; } = new Brotli() { BrotliCompressionOptions = new System.IO.Compression.BrotliCompressionOptions() { Quality = 11 } };

    private System.IO.Compression.BrotliCompressionOptions m_brotliCompressionOptions = new System.IO.Compression.BrotliCompressionOptions();

    /// <summary>
    /// <para>Compression options for the Brotli algoritm.</para>
    /// </summary>
    public System.IO.Compression.BrotliCompressionOptions BrotliCompressionOptions { get => m_brotliCompressionOptions; set => m_brotliCompressionOptions = value ?? new System.IO.Compression.BrotliCompressionOptions(); }

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

      using var compressor = new System.IO.Compression.BrotliStream(output, m_brotliCompressionOptions, true);
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

      using var decompressor = new System.IO.Compression.BrotliStream(input, System.IO.Compression.CompressionMode.Decompress, true);
      decompressor.CopyTo(output);
    }
  }
}

/*
  var sourceString = "Man is distinguished, not only by his reason, but by this singular passion from other animals, which is a lust of the mind, that by a perseverance of delight in the continued and indefatigable generation of knowledge, exceeds the short vehemence of any carnal pleasure."; //"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
  var sourceLength = sourceString.Length;

  var uncompressedBytes = Flux.Transcode.Utf8.Default.DecodeString(sourceString);

  var compressedBytes = Flux.Transflex.Brotli.Default.Compress(uncompressedBytes);

  var decompressedBytes = Flux.Transflex.Brotli.Default.Decompress(compressedBytes);

  var targetString = Flux.Transcode.Utf8.Default.EncodeString(decompressedBytes);
  var targetLength = targetString.Length;
 */