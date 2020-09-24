namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Yields a sequence of byte buffers (arrays) from the stream.</summary>
    static public System.Collections.Generic.IEnumerable<byte[]> GetByteBuffers(this System.IO.Stream source, int bufferSize)
    {
      if (source is null) throw (new System.ArgumentNullException(nameof(source)));
      else if (bufferSize <= 0) throw new System.ArgumentOutOfRangeException(nameof(bufferSize));

      while (new byte[bufferSize] is var buffer)
      {
        System.Array.Resize(ref buffer, source.Read(buffer, 0, buffer.Length));

        if (buffer.Length > 0) yield return buffer;
        else break;
      }
    }
  }
}
