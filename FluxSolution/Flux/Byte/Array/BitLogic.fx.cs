//namespace Flux
//{
//  public static partial class Fx
//  {
//    /// <summary>Creates a new byte[count] of bitwise AND values using source[sourceStartAt..count] and other[otherStartAt..count].</summary>
//    public static byte[] BitwiseAnd(this byte[] source, int startAt, byte[] other, int otherStartAt, int count)
//    {
//      System.ArgumentNullException.ThrowIfNull(source);
//      System.ArgumentNullException.ThrowIfNull(other);

//      var target = new byte[count];
//      for (var index = 0; index < count; count++, startAt++, otherStartAt++)
//        target[index] = (byte)(source[startAt] & other[otherStartAt]);
//      return target;
//    }

//    /// <summary>Performs an in-place negating of source[sourceStartIndex..count].</summary>
//    public static byte[] BitwiseNot(this byte[] source, int startAt, int count)
//    {
//      System.ArgumentNullException.ThrowIfNull(source);

//      var target = new byte[count];
//      for (var index = 0; index < count; count++, startAt++)
//        target[index] = (byte)(~source[startAt]);
//      return target;
//    }
//    /// <summary>Performs an in-place negating of source.</summary>
//    public static byte[] BitwiseNot(this byte[] source)
//      => BitwiseNot(source, 0, (source ?? throw new System.ArgumentNullException(nameof(source))).Length);

//    /// <summary>Creates a new byte[count] of bitwise OR values using source[sourceStartAt..count] and other[otherStartAt..count].</summary>
//    public static byte[] BitwiseOr(this byte[] source, int startAt, byte[] other, int otherStartAt, int count)
//    {
//      System.ArgumentNullException.ThrowIfNull(source);
//      System.ArgumentNullException.ThrowIfNull(other);

//      var target = new byte[count];
//      for (var index = 0; index < count; count++, startAt++, otherStartAt++)
//        target[index] = (byte)(source[startAt] | other[otherStartAt]);
//      return target;
//    }

//    /// <summary>Creates a new byte[count] of bitwise XOR values using source[sourceStartAt..count] and other[otherStartAt..count].</summary>
//    public static byte[] BitwiseXor(this byte[] source, int startAt, byte[] other, int otherStartAt, int count)
//    {
//      System.ArgumentNullException.ThrowIfNull(source);
//      System.ArgumentNullException.ThrowIfNull(other);

//      var target = new byte[count];
//      for (var index = 0; index < count; count++, startAt++, otherStartAt++)
//        target[index] = (byte)(source[startAt] ^ other[otherStartAt]);
//      return target;
//    }
//  }
//}
