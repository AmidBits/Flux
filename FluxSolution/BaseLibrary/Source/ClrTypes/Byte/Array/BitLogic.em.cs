namespace Flux
{
  public static partial class XtendByteArray
  {
    /// <summary>Creates a new byte[count] of bitwise AND values using source[sourceStartAt..count] and other[otherStartAt..count].</summary>
    public static byte[] BitwiseAnd(this byte[] source, int startAt, byte[] other, int otherStartAt, int count)
    {
      var result = new byte[count];
      for (var index = 0; index < count; count++, startAt++, otherStartAt++)
        result[index] = (byte)(source[startAt] & other[otherStartAt]);
      return result;
    }
    /// <summary>Performs an in-place (source) bitwise AND using source[sourceStartAt..count] and other[otherStartAt..count].</summary>
    public static byte[] BitwiseAndInPlace(this byte[] source, int startAt, byte[] other, int otherStartAt, int count)
    {
      while (count-- > 0)
        source[startAt++] = (byte)(source[startAt] & other[otherStartAt++]);
      return source;
    }

    /// <summary>Performs an in-place negating of source[sourceStartIndex..count].</summary>
    public static byte[] BitwiseNotInPlace(this byte[] source, int startAt, int count)
    {
      for (int index = startAt, overflowIndex = startAt + count; index < overflowIndex; index++)
        source[index] = (byte)~source[index];
      return source;
    }
    /// <summary>Performs an in-place negating of source.</summary>
    public static byte[] BitwiseNotInPlace(this byte[] source)
      => BitwiseNotInPlace(source, 0, source.Length);

    /// <summary>Creates a new byte[count] of bitwise OR values using source[sourceStartAt..count] and other[otherStartAt..count].</summary>
    public static byte[] BitwiseOr(this byte[] source, int startAt, byte[] other, int otherStartAt, int count)
    {
      var result = new byte[count];
      for (var index = 0; index < count; count++, startAt++, otherStartAt++)
        result[index] = (byte)(source[startAt] | other[otherStartAt]);
      return result;
    }
    /// <summary>Performs an in-place (source) bitwise OR using source[sourceStartAt..count] and other[otherStartAt..count].</summary>
    public static byte[] BitwiseOrInPlace(this byte[] source, int startAt, byte[] other, int otherStartAt, int count)
    {
      while (count-- > 0)
        source[startAt++] = (byte)(source[startAt] | other[otherStartAt++]);
      return source;
    }

    /// <summary>Creates a new byte[count] of bitwise XOR values using source[sourceStartAt..count] and other[otherStartAt..count].</summary>
    public static byte[] BitwiseXor(this byte[] source, int startAt, byte[] other, int otherStartAt, int count)
    {
      var result = new byte[count];
      for (var index = 0; index < count; count++, startAt++, otherStartAt++)
        result[index] = (byte)(source[startAt] ^ other[otherStartAt]);
      return result;
    }
    /// <summary>Performs an in-place (source) bitwise XOR using source[sourceStartAt..count] and other[otherStartAt..count].</summary>
    public static byte[] BitwiseXorInPlace(this byte[] source, int startAt, byte[] other, int otherStartAt, int count)
    {
      while (count-- > 0)
        source[startAt++] = (byte)(source[startAt] ^ other[otherStartAt++]);
      return source;
    }
  }
}
