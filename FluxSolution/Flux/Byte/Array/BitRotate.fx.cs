//namespace Flux
//{
//  public static partial class Fx
//  {
//    /// <summary>Performs an in-place bit rotate <paramref name="count"/> left on all bytes in the <paramref name="source"/> array.</summary>
//    public static void BitRotateLeft(this byte[] source, int count)
//    {
//      var carryBits = BitShiftLeft(source, count);

//      source[0] |= carryBits;
//    }
//    /// <summary>Performs an in-place bit rotate <paramref name="count"/> right on all bytes in the <paramref name="source"/> array.</summary>
//    public static void BitRotateRight(this byte[] source, int count)
//    {
//      var carryBits = BitShiftRight(source, count);

//      source[^1] |= carryBits;
//    }
//  }
//}
