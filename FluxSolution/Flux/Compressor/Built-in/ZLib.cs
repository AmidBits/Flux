namespace Flux.Compressor
{
  public sealed class ZLib
    : ICompressor
  {
    public static ICompressor Default { get; } = new ZLib();
    public static ICompressor Max { get; } = new ZLib() { ZLibCompressionOptions = new System.IO.Compression.ZLibCompressionOptions() { CompressionLevel = 9 } };

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

      using var compressor = new System.IO.Compression.ZLibStream(output, m_zLibCompressionOptions, true);
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

      using var decompressor = new System.IO.Compression.ZLibStream(input, System.IO.Compression.CompressionMode.Decompress, true);
      decompressor.CopyTo(output);
    }
  }
}

//  /// <summary>Deflate compression algorithm.</summary>
//  public sealed class Compression
//    : System.IFormattable, ITransflexable
//  {
//    public static ITransflexable BrotliDefault { get; } = new Compression(CompressionAlgorithm.Brotli);
//    public static ITransflexable DeflateDefault { get; } = new Compression(CompressionAlgorithm.Deflate);
//    public static ITransflexable GZipDefault { get; } = new Compression(CompressionAlgorithm.GZip);
//    public static ITransflexable ZLibDefault { get; } = new Compression(CompressionAlgorithm.ZLib);

//    public static ITransflexable BrotliMax { get; } = new Compression(CompressionAlgorithm.Brotli) { BrotliCompressionOptions = new System.IO.Compression.BrotliCompressionOptions() { Quality = 11 } };
//    public static ITransflexable DeflateMax { get; } = new Compression(CompressionAlgorithm.Deflate) { ZLibCompressionOptions = new System.IO.Compression.ZLibCompressionOptions() { CompressionLevel = 9 } };
//    public static ITransflexable GZipMax { get; } = new Compression(CompressionAlgorithm.GZip) { ZLibCompressionOptions = new System.IO.Compression.ZLibCompressionOptions() { CompressionLevel = 9 } };
//    public static ITransflexable ZLibMax { get; } = new Compression(CompressionAlgorithm.ZLib) { ZLibCompressionOptions = new System.IO.Compression.ZLibCompressionOptions() { CompressionLevel = 9 } };

//    private System.IO.Compression.BrotliCompressionOptions m_brotliOptions = new System.IO.Compression.BrotliCompressionOptions();
//    private System.IO.Compression.ZLibCompressionOptions m_zLibOptions = new System.IO.Compression.ZLibCompressionOptions();

//    private CompressionAlgorithm m_algorithm;

//    /// <summary>
//    /// 
//    /// </summary>
//    /// <param name="compressionAlgorithm"></param>
//    /// <param name="compressionLevel">Only applies if <c><paramref name="compressionAlgorithm"/> = <see cref="CompressionAlgorithm.Brotli"/></c>.</param>
//    public Compression(CompressionAlgorithm compressionAlgorithm) => m_algorithm = compressionAlgorithm;

//    /// <summary>
//    /// <para>Compression options for the Brotli algoritm.</para>
//    /// </summary>
//    public System.IO.Compression.BrotliCompressionOptions BrotliCompressionOptions { get => m_brotliOptions; set => m_brotliOptions = value ?? new System.IO.Compression.BrotliCompressionOptions(); }

//    /// <summary>
//    /// <para>Compression options for the Deflate, GZip and ZLib algoritms.</para>
//    /// </summary>
//    public System.IO.Compression.ZLibCompressionOptions ZLibCompressionOptions { get => m_zLibOptions; set => m_zLibOptions = value ?? new System.IO.Compression.ZLibCompressionOptions(); }

//    /// <summary>
//    /// <para></para>
//    /// </summary>
//    /// <remarks>
//    /// <para>Leaves the stream open.</para>
//    /// </remarks>
//    /// <param name="input"></param>
//    /// <param name="output"></param>
//    public void Compress(System.IO.Stream input, System.IO.Stream output)
//    {
//      switch (m_algorithm)
//      {
//        case CompressionAlgorithm.Brotli:
//          {
//            using var compressor = new System.IO.Compression.BrotliStream(output, m_brotliOptions, true);
//            input.CopyTo(compressor);
//          }
//          break;
//        case CompressionAlgorithm.Deflate:
//          {
//            using var compressor = new System.IO.Compression.DeflateStream(output, m_zLibOptions, true);
//            input.CopyTo(compressor);
//          }
//          break;
//        case CompressionAlgorithm.GZip:
//          {
//            using var compressor = new System.IO.Compression.GZipStream(output, m_zLibOptions, true);
//            input.CopyTo(compressor);
//          }
//          break;
//        case CompressionAlgorithm.ZLib:
//          {
//            using var compressor = new System.IO.Compression.ZLibStream(output, m_zLibOptions, true);
//            input.CopyTo(compressor);
//          }
//          break;
//        default:
//          throw new System.NotImplementedException();
//      }
//    }

//    /// <summary>
//    /// <para></para>
//    /// </summary>
//    /// <remarks>
//    /// <para>Leaves the stream open.</para>
//    /// <param name="input"></param>
//    /// <param name="output"></param>
//    public void Decompress(System.IO.Stream input, System.IO.Stream output)
//    {
//      System.ArgumentNullException.ThrowIfNull(input);
//      System.ArgumentNullException.ThrowIfNull(output);

//      switch (m_algorithm)
//      {
//        case CompressionAlgorithm.Brotli:
//          {
//            using var decompressor = new System.IO.Compression.BrotliStream(input, System.IO.Compression.CompressionMode.Decompress, true);
//            decompressor.CopyTo(output);
//          }
//          break;
//        case CompressionAlgorithm.Deflate:
//          {
//            using var decompressor = new System.IO.Compression.DeflateStream(input, System.IO.Compression.CompressionMode.Decompress, true);
//            decompressor.CopyTo(output);
//          }
//          break;
//        case CompressionAlgorithm.GZip:
//          {
//            using var decompressor = new System.IO.Compression.GZipStream(input, System.IO.Compression.CompressionMode.Decompress, true);
//            decompressor.CopyTo(output);
//          }
//          break;
//        case CompressionAlgorithm.ZLib:
//          {
//            using var decompressor = new System.IO.Compression.ZLibStream(input, System.IO.Compression.CompressionMode.Decompress, true);
//            decompressor.CopyTo(output);
//          }
//          break;
//        default:
//          throw new System.ArgumentOutOfRangeException();
//      }
//    }

//    public string ToString(string? format, IFormatProvider? formatProvider)
//    {
//      switch (m_algorithm)
//      {
//        case CompressionAlgorithm.Brotli:
//          return $"{m_algorithm.ToString()}, Quality = {m_brotliOptions.Quality}";
//        case CompressionAlgorithm.Deflate:
//        case CompressionAlgorithm.GZip:
//        case CompressionAlgorithm.ZLib:
//          return $"{m_algorithm.ToString()}, Level = {m_zLibOptions.CompressionLevel}, Strategy: {m_zLibOptions.CompressionStrategy}";
//        default:
//          throw new System.NotImplementedException();
//      }
//    }

//    public override string ToString() => ToString(null, null);
//  }
//}
