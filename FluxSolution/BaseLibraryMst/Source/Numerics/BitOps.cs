﻿#if NET7_0_OR_GREATER
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Numerics
{
  [TestClass]
  public class BitOps
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
    public void ClearBitIndex()
    {
      Assert.AreEqual(0x0U, 0x4U.ClearBitIndex(2));
    }

    [TestMethod]
    public void FlipBitIndex()
    {
      Assert.AreEqual(0x2U, 0x6U.ClearBitIndex(2));
    }

    [TestMethod]
    public void GetBitIndex()
    {
      Assert.AreEqual(true, 0x6U.GetBitIndex(2));
    }

    [TestMethod]
    public void SetBitIndex()
    {
      Assert.AreEqual(0x4U, 0b00000000U.SetBitIndex(2));
    }

    [TestMethod]
    public void BitMaskClear()
    {
      Assert.AreEqual(0b0101, 0b1111.BitMaskClear(0b1010));
    }

    [TestMethod]
    public void BitMaskFillLeft()
    {
      var templateBitMask = 0b110;
      var templateBitLength = 3;

      var expected = 0b1011_0110_1101_1011_0110_1101_1011_0110U;
      var actual = unchecked((uint)templateBitMask.BitMaskFillLeft(templateBitLength));

      // Debug:
      var e = expected.ToBinaryString();
      var a = actual.ToBinaryString();

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void BitMaskFillRight()
    {
      var templateBitMask = 0b110;
      var templateBitLength = 3;

      var expected = 0b1101_1011_0110_1101_1011_0110_1101_1011U;
      var actual = unchecked((uint)templateBitMask.BitMaskFillRight(templateBitLength));

      // Debug:
      var e = expected.ToBinaryString();
      var a = actual.ToBinaryString();

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void BitMaskFlip()
    {
      Assert.AreEqual(0b0101, 0b1111.BitMaskFlip(0b1010));
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
    public void BitMaskSet()
    {
      Assert.AreEqual(0b1010, 0b0000.BitMaskSet(0b1010));
    }

    [TestMethod]
    public void GetBitCount()
    {
      Assert.AreEqual(32, 88.GetBitCount());
    }

    [TestMethod]
    public void GetBitLengthEx()
    {
      Assert.AreEqual(7, 88.GetBitLengthEx());
      Assert.AreEqual(32, (-88).GetBitLengthEx());
    }

    [TestMethod]
    public void GetShortestBitLength()
    {
      Assert.AreEqual(7, 88.GetShortestBitLength());
      Assert.AreEqual(8, (-88).GetShortestBitLength());
    }

    [TestMethod]
    public void GetByteCount()
    {
      Assert.AreEqual(4, 88.GetByteCount());
    }

    [TestMethod]
    public void GetPopCount()
    {
      Assert.AreEqual(4, 0xF0.GetPopCount());
    }

    [TestMethod]
    public void IntegerLog2Ceiling()
    {
      Assert.AreEqual(8, 215.IntegerLog2Ceiling());
    }

    [TestMethod]
    public void IntegerLog2Floor()
    {
      Assert.AreEqual(7, 215.IntegerLog2Floor());
    }

    [TestMethod]
    public void IntegerLog2()
    {
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
      var (towardsZero, awayFromZero) = 88.PowOf2(false);

      Assert.AreEqual(64, towardsZero);
      Assert.AreEqual(128, awayFromZero);
    }

    [TestMethod]
    public void PowOf2WithRounding()
    {
      var rounded = 88.PowOf2(false, RoundingMode.HalfAwayFromZero, out var towardsZero, out var awayFromZero);

      Assert.AreEqual(64, rounded);

      Assert.AreEqual(64, towardsZero);
      Assert.AreEqual(128, awayFromZero);
    }

    [TestMethod]
    public void PowOf2AwayFromZeroProperWithRounding()
    {
      var actual = 88.ToBigInteger().PowOf2(true, RoundingMode.AwayFromZero, out var _, out var _);

      Assert.AreEqual(128.ToBigInteger(), actual);
    }

    [TestMethod]
    public void PowOf2AwayFromZeroWithRounding()
    {
      var actual = 88.ToBigInteger().PowOf2(false, RoundingMode.AwayFromZero, out var _, out var _);

      Assert.AreEqual(128.ToBigInteger(), actual);
    }

    [TestMethod]
    public void PowOf2TowardZeroProperWithRounding()
    {
      var actual = 88.ToBigInteger().PowOf2(true, RoundingMode.TowardsZero, out var _, out var _);

      Assert.AreEqual(64.ToBigInteger(), actual);
    }

    [TestMethod]
    public void PowOf2TowardZeroWithRounding()
    {
      var actual = 88.ToBigInteger().PowOf2(false, RoundingMode.TowardsZero, out var _, out var _);

      Assert.AreEqual(64.ToBigInteger(), actual);
    }

    [TestMethod]
    public void ReverseBits()
    {
      // Somehow BigInteger must differ between .NET version 6 and 7. 

      Assert.AreEqual(0x00010000.ToBigInteger(), Flux.BitOps.ReverseBits(0x00008000.ToBigInteger())); // This works on .NET 7, but not on .NET 6.
      Assert.AreEqual(0x10000000.ToBigInteger(), Flux.BitOps.ReverseBits(0x00000008.ToBigInteger())); // This works on .NET 6, but not on .NET 7.

      Assert.AreEqual(unchecked((int)0xFFFFFFFE).ToBigInteger(), 0x7FFFFFFF.ToBigInteger().ReverseBits());
    }

    [TestMethod]
    public void ReverseBytes()
    {
      Assert.AreEqual(0x00010000, Flux.BitOps.ReverseBytes(0x00000100));
    }

    [TestMethod]
    public void ClearLeastSignificant1Bit()
    {
      Assert.AreEqual(0b1010000, 0b1011000.ClearLeastSignificant1Bit());
    }

    [TestMethod]
    public void ClearMostSignificant1Bit()
    {
      Assert.AreEqual(0b11000, 0b1011000.ClearMostSignificant1Bit());
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
    public void GetLeadingZeroCount()
    {
      Assert.AreEqual(25, 88.GetLeadingZeroCount());
    }

    [TestMethod]
    public void GetTrailingZeroCount()
    {
      Assert.AreEqual(3, 88.ToBigInteger().GetTrailingZeroCount());
    }
  }
}
#endif