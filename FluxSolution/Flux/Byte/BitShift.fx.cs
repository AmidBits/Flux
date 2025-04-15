//namespace Flux
//{
//  public static partial class Bytes
//  {
//    public static (TSelf target, TSelf overflow) BitShiftLeftWithCarry<TSelf>(this TSelf source, int count)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//      => (source << count, source >> (source.GetBitCount() - count));

//    public static (TSelf target, TSelf overflow) BitShiftRightWithCarry<TSelf>(this TSelf source, int count)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//      => (source >>> count, (source & TSelf.CreateChecked(count).CreateBitMaskLsbFromBitLength(0)) << (source.GetBitCount() - count));

//    //public static bool BitFlagCarryLsb<TSelf>(this TSelf source)
//    //  where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    //  => !TSelf.IsZero(source & TSelf.One);

//    //public static bool BitFlagCarryMsb<TSelf>(this TSelf source)
//    //  where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    //  => !TSelf.IsZero(source & (TSelf.One << (source.GetBitCount() - 1)));

//    //public static TSelf BitShiftLeftWithCarry<TSelf>(this TSelf source, bool lsb)
//    //  where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    //  => (source << 1) | (lsb ? TSelf.One : TSelf.Zero);

//    //public static TSelf BitShiftRightWithCarry<TSelf>(this TSelf source, bool msb)
//    //  where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    //  => (msb ? TSelf.RotateRight(TSelf.One, 1) : TSelf.Zero) | (source >> 1);

//    ///// <summary>Returns a sequence bit-shifted left by count bits, by extending the array with the necessary number of bytes.</summary>
//    //public static System.Collections.Generic.IEnumerable<byte> BitShiftLeft(this System.Collections.Generic.IEnumerable<byte> source, int count)
//    //{
//    //  System.ArgumentNullException.ThrowIfNull(source);

//    //  var effectiveShift = (count % 8);
//    //  var inverseShift = (8 - effectiveShift);

//    //  byte previousByte = 0;

//    //  foreach (var currentByte in source)
//    //  {
//    //    yield return (byte)((previousByte << effectiveShift) | (currentByte >> inverseShift));

//    //    previousByte = currentByte;
//    //  }

//    //  yield return (byte)(previousByte << effectiveShift);

//    //  for (var i = count / 8; i > 0; i--)
//    //  {
//    //    yield return 0;
//    //  }
//    //}

//    ///// <summary>Returns a sequence bit-shifted right by count bits, by extending the array with the necessary number of bytes.</summary>
//    //public static System.Collections.Generic.IEnumerable<byte> BitShiftRight(this System.Collections.Generic.IEnumerable<byte> source, int count)
//    //{
//    //  System.ArgumentNullException.ThrowIfNull(source);

//    //  for (var i = count / 8; i > 0; i--)
//    //  {
//    //    yield return 0;
//    //  }

//    //  var effectiveShift = (count % 8);
//    //  var inverseShift = (8 - effectiveShift);

//    //  byte previousByte = 0;

//    //  foreach (var currentByte in source)
//    //  {
//    //    yield return (byte)((previousByte << inverseShift) | (currentByte >> effectiveShift));

//    //    previousByte = currentByte;
//    //  }

//    //  yield return (byte)(previousByte << inverseShift);
//    //}
//  }
//}
