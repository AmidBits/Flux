namespace Flux
{
  public static partial class StreamEm
  {
    /// <summary>Returns a <see cref="System.Byte[]"/> from the stream. The array will always be the size of actually read bytes.</summary>
    public static byte[] ReadByteArray(this System.IO.Stream source, int size)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (size < 0) throw new System.ArgumentOutOfRangeException(nameof(size));

      var buffer = new byte[size];
      System.Array.Resize(ref buffer, source.Read(buffer, 0, size));
      return buffer;
    }
  }
}
