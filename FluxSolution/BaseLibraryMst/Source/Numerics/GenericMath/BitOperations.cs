#if NET7_0_OR_GREATER
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericMath
{
  [TestClass]
  public class BitOperations
  {
    [TestMethod]
    public void BitFoldLeft()
    {
      Assert.AreEqual(0xFFFFFFF8U, 0b00000000_10011000U.BitFoldLeft());
    }

    [TestMethod]
    public void BitFoldRight()
    {
      Assert.AreEqual(0x0000007FU, 0b00000000_01011000U.BitFoldRight());
    }

    [TestMethod]
    public void GetBitLength()
    {
      Assert.AreEqual(7, 88.GetBitLength());
      Assert.AreEqual(32, (-88).GetBitLength());
    }

    [TestMethod]
    public void GetShortestBitLength()
    {
      Assert.AreEqual(7, 88.GetShortestBitLength());
      Assert.AreEqual(8, (-88).GetShortestBitLength());
    }

    [TestMethod]
    public void BitMaskLeft()
    {
      Assert.AreEqual(-1, 32.BitMaskLeft());
    }

    [TestMethod]
    public void BitMaskRight()
    {
      Assert.AreEqual(127, 7.BitMaskRight());
    }

    [TestMethod]
    public void GetIntegerLog2()
    {
      Assert.AreEqual(7, 215.GetIntegerLog2Floor());
      Assert.AreEqual((7, 8), 215.IntegerLog2());
    }

    [TestMethod]
    public void IsPowOf2()
    {
      Assert.AreEqual(false, 88.IsPow2());
      Assert.AreEqual(true, 64.IsPow2());
    }

    [TestMethod]
    public void PowOf2()
    {
      var rounded = 88.PowOf2(false, RoundingMode.HalfAwayFromZero, out var towardsZero, out var awayFromZero);

      Assert.AreEqual(64, rounded);

      Assert.AreEqual(64, towardsZero);
      Assert.AreEqual(128, awayFromZero);
    }

    [TestMethod]
    public void RoundToPowOf2AwayFromZero()
    {
      var actual = 88.ToBigInteger().PowOf2(false, RoundingMode.AwayFromZero, out var _, out var _);

      Assert.AreEqual(128.ToBigInteger(), actual);
    }

    [TestMethod]
    public void RoundToPowOf2AwayFromZeroProper()
    {
      var actual = 88.ToBigInteger().PowOf2(true, RoundingMode.AwayFromZero, out var _, out var _);

      Assert.AreEqual(128.ToBigInteger(), actual);
    }

    [TestMethod]
    public void RoundToPowOf2TowardZero()
    {
      var actual = 88.ToBigInteger().PowOf2(false, RoundingMode.TowardsZero, out var _, out var _);

      Assert.AreEqual(64.ToBigInteger(), actual);
    }

    [TestMethod]
    public void RoundToPowOf2TowardZeroProper()
    {
      var actual = 88.ToBigInteger().PowOf2(true, RoundingMode.TowardsZero, out var _, out var _);

      Assert.AreEqual(64.ToBigInteger(), actual);
    }

    [TestMethod]
    public void LeastSignificant1Bit()
    {
      Assert.AreEqual(8, 88.LeastSignificant1Bit());
    }

    [TestMethod]
    public void MostSignificant1Bit()
    {
      Assert.AreEqual(64, 88.MostSignificant1Bit());
    }

    [TestMethod]
    public void StripLeastSignificant1Bit()
    {
      Assert.AreEqual(80, 88.StripLeastSignificant1Bit());
    }

    [TestMethod]
    public void GetLeadingZeroCount()
    {
      Assert.AreEqual(25, 88.GetLeadingZeroCount());
    }

    [TestMethod]
    public void GetTrailingZeroCount()
    {
      Assert.AreEqual(3, 88.ToBigInteger().GetTrailingZeroCount());
    }

    [TestMethod]
    public void GetBitCount()
    {
      Assert.AreEqual(32, 88.GetBitCount());
    }

    [TestMethod]
    public void GetByteCount()
    {
      Assert.AreEqual(4, 88.GetByteCount());
    }

    [TestMethod]
    public void GetMaxDigitCount()
    {
      Assert.AreEqual(4, Bits.GetMaxDigitCount(10, 10, false));
      Assert.AreEqual(3, Bits.GetMaxDigitCount(10, 10, true));
    }

    [TestMethod]
    public void GetPopCount()
    {
      Assert.AreEqual(4, 0xF0.GetPopCount());
    }

    [TestMethod]
    public void ReverseBits()
    {
      // Somehow BigInteger must differ between .NET version 6 and 7. 

      Assert.AreEqual(0x00010000, Flux.Bits.ReverseBits(0x00008000.ToBigInteger())); // This works on .NET 7, but not on .NET 6.
      Assert.AreEqual(0x10000000, Flux.Bits.ReverseBits(0x00000008.ToBigInteger())); // This works on .NET 6, but not on .NET 7.

      Assert.AreEqual(unchecked((int)0xFFFFFFFE).ToBigInteger(), 0x7FFFFFFF.ToBigInteger().ReverseBits());
    }

    [TestMethod]
    public void ReverseBytes()
    {
      Assert.AreEqual(0x00010000, Flux.Bits.ReverseBytes(0x00000100));
    }

    //public static void Test()
    //{

    //  //var bi = unchecked((int)0x800081c3); // {-1014956031} (v7 & v6)
    //  //var bi = 0x800081c3; // {-1014956031} (v7 & v6)
    //  //var bi = 0x3c180008;//.ToBigInteger(); // {268441660} (v6)
    //  var bi = 0x03060c18;

    //  var ry1 = bi.ReverseBytes();
    //  var ry2 = ry1.ReverseBytes();
    //  var ry3 = ry2.ReverseBytes();

    //  var ri1 = bi.ReverseBits();
    //  var ri2 = ri1.ReverseBits();
    //  var ri3 = ri2.ReverseBits();
    //}

  }
}
#endif
