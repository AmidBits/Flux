namespace Flux
{
  public static partial class TextReaderEm
  {
    /// <summary>Returns a <see cref="System.Byte[]"/> from the stream. The array will always be the size of actually read bytes.</summary>
    public static char[] ReadCharArray(this System.IO.TextReader source, int size)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (size < 0) throw new System.ArgumentOutOfRangeException(nameof(size));

      var buffer = new char[size];
      var actual = source.Read(buffer, 0, size);
      if (actual < size)
        System.Array.Resize(ref buffer, actual);
      return buffer;
    }
  }
}
