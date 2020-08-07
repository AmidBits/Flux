namespace Flux
{
  public static partial class XtensionsByteArray
  {
    /// <summary>Performs an in-place left rotation of all bits in the array.</summary>
    public static byte[] BitRotateLeft(this byte[] source)
    {
      if (BitShiftLeft(source)) source[source.Length - 1] |= 0x01;

      return source;
    }
    /// <summary>Performs an in-place right rotation of all bits in the array.</summary>
    public static byte[] BitRotateRight(this byte[] source)
    {
      if (BitShiftRight(source)) source[0] |= 0x80;

      return source;
    }
  }
}
