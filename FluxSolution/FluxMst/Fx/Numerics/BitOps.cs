#if NET7_0_OR_GREATER
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Numerics
{
  [TestClass]
  public class BitOps
  {
    #region BitFlags

    [TestMethod]
    public void BitFlagCarryLsb()
    {
      Assert.AreEqual(true, 0xFFFFFFFFU.BitFlagCarryLsb());
      Assert.AreEqual(false, 0.BitFlagCarryLsb());
    }

    [TestMethod]
    public void BitFlagCarryMsb()
    {
      Assert.AreEqual(true, 0xFFFFFFFFU.BitFlagCarryMsb());
      Assert.AreEqual(false, 0.BitFlagCarryMsb());
    }

    #endregion // BitFlags

    #region BitFolds

    [TestMethod]
    public void BitFoldToLsb()
    {
      Assert.AreEqual(0b00000000000000000000000001111111U, 0b00000000_01011000U.BitFoldLsb());
    }

    [TestMethod]
    public void BitFoldToMsb()
    {
      Assert.AreEqual(0b11111111111111111111111111111000U, 0b00000000_10011000U.BitFoldMsb());
    }

    #endregion // BitFolds

    #region BitIndex

    [TestMethod]
    public void BitIndexClear()
    {
      Assert.AreEqual(0x2U, 0x6U.BitIndexClear(2));
    }

    [TestMethod]
    public void BitIndexFlip()
    {
      Assert.AreEqual(4, 0.BitIndexFlip(2));
    }

    [TestMethod]
    public void BitIndexGet()
    {
      Assert.AreEqual(true, 0x6U.BitIndexCheck(2));
    }

    [TestMethod]
    public void BitIndexOfPow2()
    {
      var tb = 0x18;
      var tbil = tb.LeastSignificant1Bit().BitIndexOfPow2();
      Assert.AreEqual(3, tbil);
      var tbim = tb.MostSignificant1Bit().BitIndexOfPow2();
      Assert.AreEqual(4, tbim);
    }

    [TestMethod]
    public void BitIndexSet()
    {
      Assert.AreEqual(0x4U, 0b00000000U.BitIndexSet(2));
    }

    #endregion // BitIndex

    #region BitLengths

    [TestMethod]
    public void GetBitLength()
    {
      Assert.AreEqual(4, 0x08.GetBitLength());

      Assert.AreEqual(7, 88.GetBitLength());
      Assert.AreEqual(32, (-88).GetBitLength());
    }

    #endregion // BitLengths

    #region BitMasks

    [TestMethod]
    public void BitMaskCheckAll()
    {
      Assert.AreEqual(true, 0b111110.BitMaskCheckAll(0b101010));
    }

    [TestMethod]
    public void BitMaskCheckAny()
    {
      Assert.AreEqual(true, 0b1111.BitMaskCheckAny(0b101010));
    }

    [TestMethod]
    public void BitMaskClear()
    {
      Assert.AreEqual(0b0101, 0b1111.BitMaskClear(0b1010));
    }

    [TestMethod]
    public void CreateBitMaskLsbFromBitLength()
    {
      Assert.AreEqual(127, 7.CreateBitMaskLsbFromBitLength());
    }

    [TestMethod]
    public void CreateBitMaskMsbFromBitLength()
    {
      Assert.AreEqual(-1, 32.CreateBitMaskMsbFromBitLength());
    }

    [TestMethod]
    public void FillBitMaskToLsb()
    {
      var templateBitMask = 0b110;
      var templateBitLength = 3;

      var expected = 0b1011_0110_1101_1011_0110_1101_1011_0110U;
      var actual = unchecked((uint)templateBitMask.FillBitMaskLsb(templateBitLength, expected.GetBitLength()));

      // Debug:
      var e = expected.ToBinaryString();
      var a = actual.ToBinaryString();

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void FillBitMaskToMsb()
    {
      var templateBitMask = 0b110;
      var templateBitLength = 3;

      var expected = 0b1101_1011_0110_1101_1011_0110_1101_1011U;
      var actual = unchecked((uint)templateBitMask.FillBitMaskMsb(templateBitLength, expected.GetBitLength()));

      // Debug:
      var e = expected.ToBinaryString();
      var a = actual.ToBinaryString();

      Assert.AreEqual(expected, actual);
    }

    #endregion // BitMasks

    #region BitSwaps
    #endregion // BitSwaps

    #region GetBitCount

    [TestMethod]
    public void GetBitCount()
    {
      Assert.AreEqual(32, 88.GetBitCount());
    }

    #endregion // GetBitCount

    #region GetByteCount

    [TestMethod]
    public void GetByteCount()
    {
      Assert.AreEqual(4, 88.GetByteCount());
    }

    #endregion // GetByteCount

    #region GetPopCount

    [TestMethod]
    public void GetPopCount()
    {
      Assert.AreEqual(4, 0xF0.GetPopCount());
    }

    #endregion // GetPopCount

    #region GrayCode
    #endregion // GrayCode

    #region Log2

    [TestMethod]
    public void Log2AwayFromZero()
    {
      Assert.AreEqual(8, 215.Log2AwayFromZero());
    }

    [TestMethod]
    public void Log2TowardZero()
    {
      Assert.AreEqual(7, 215.Log2TowardZero());
    }

    #endregion // Log2

    #region Pow2

    //[TestMethod]
    //public void IsPowOf2()
    //{
    //  Assert.AreEqual(false, 88.IsPow2());
    //  Assert.AreEqual(true, 64.IsPow2());
    //}

    [TestMethod]
    public void Pow2TowardZero()
    {
      var towardZero = 88.Pow2TowardZero(false);
      var awayFromZero = 88.Pow2AwayFromZero(false);

      Assert.AreEqual(64, towardZero);
      Assert.AreEqual(128, awayFromZero);
    }

    [TestMethod]
    public void PowOf2UnequalWithRoundingAwayFromZero()
    {
      var value = 88;

      var towardsZero = value.Pow2TowardZero(true);
      var awayFromZero = value.Pow2AwayFromZero(true);

      var actual = 88.RoundToNearest(UniversalRounding.WholeAwayFromZero, towardsZero, awayFromZero);

      Assert.AreEqual(128, actual);
    }

    [TestMethod]
    public void PowOf2UnequalWithRoundingTowardZero()
    {
      var value = 88;

      var towardsZero = value.Pow2TowardZero(true);
      var awayFromZero = value.Pow2AwayFromZero(true);

      var actual = 88.RoundToNearest(UniversalRounding.WholeTowardZero, towardsZero, awayFromZero);

      Assert.AreEqual(64, actual);
    }

    [TestMethod]
    public void Pow2WithRoundingAwayFromZero()
    {
      var value = 88;

      var towardsZero = value.Pow2TowardZero(false);
      var awayFromZero = value.Pow2AwayFromZero(false);

      var rounded = 88.RoundToNearest(UniversalRounding.HalfAwayFromZero, towardsZero, awayFromZero);

      Assert.AreEqual(64, rounded);

      Assert.AreEqual(64, towardsZero);
      Assert.AreEqual(128, awayFromZero);
    }

    [TestMethod]
    public void PowOf2WithRoundingTowardZero()
    {
      var value = 88;

      var towardsZero = value.Pow2TowardZero(false);
      var awayFromZero = value.Pow2AwayFromZero(false);

      var actual = 88.RoundToNearest(UniversalRounding.WholeTowardZero, towardsZero, awayFromZero);

      Assert.AreEqual(64, actual);
    }

    #endregion // Pow2

    #region ReverseBits

    [TestMethod]
    public void ReverseBits()
    {
      // Somehow BigInteger must differ between .NET version 6 and 7. 

      Assert.AreEqual(0x00010000, 0x00008000.ReverseBits()); // This works on .NET 7, but not on .NET 6.
      Assert.AreEqual(0x10000000, 0x00000008.ReverseBits()); // This works on .NET 6, but not on .NET 7.

      Assert.AreEqual(unchecked((int)0xFFFFFFFE), 0x7FFFFFFF.ReverseBits());
    }

    #endregion // ReverseBits

    #region ReverseBytes

    [TestMethod]
    public void ReverseBytes()
    {
      Assert.AreEqual(0x00010000, 0x00000100.ReverseBytes());
    }

    #endregion // ReverseBytes

    #region Significant1Bits

    [TestMethod]
    public void LeastSignificant1Bit()
    {
      Assert.AreEqual(0b00001000, 0b01011000.LeastSignificant1Bit());
    }

    [TestMethod]
    public void LeastSignificant1BitClear()
    {
      Assert.AreEqual(0b01010000, 0b01011000.LeastSignificant1BitClear());
    }

    [TestMethod]
    public void MostSignificant1Bit()
    {
      Assert.AreEqual(0b01000000, 0b01011000.MostSignificant1Bit());
    }

    [TestMethod]
    public void MostSignificant1BitClear()
    {
      Assert.AreEqual(0b00011000, 0b01011000.MostSignificant1BitClear());
    }

    #endregion // Significant1Bits

    #region ZeroCounts

    [TestMethod]
    public void GetLeadingZeroCount()
    {
      Assert.AreEqual(25, 88.GetLeadingZeroCount());
    }

    [TestMethod]
    public void GetTrailingZeroCount()
    {
      Assert.AreEqual(3, 88.GetTrailingZeroCount());
    }

    #endregion // ZeroCounts
  }
}
#endif
