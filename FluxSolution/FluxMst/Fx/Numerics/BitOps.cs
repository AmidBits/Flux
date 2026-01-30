#if NET7_0_OR_GREATER
using Flux;

namespace Numerics
{
  [TestClass]
  public class BitOps
  {
    //#region BitFlags

    //[TestMethod]
    //public void BitFlagCarryLsb()
    //{
    //  Assert.IsTrue(0xFFFFFFFFU.BitGetLs1b());
    //  Assert.IsFalse(0.BitGetLs1b());
    //}

    //[TestMethod]
    //public void BitFlagCarryMsb()
    //{
    //  Assert.IsTrue(0xFFFFFFFFU.BitGetMs1b());
    //  Assert.IsFalse(0.BitGetMs1b());
    //}

    //#endregion // BitFlags

    #region BitFolds

    [TestMethod]
    public void BitFoldToRight()
    {
      Assert.AreEqual(0b00000000000000000000000001111111U, uint.BitFoldRight(0b00000000_01011000U));
    }

    [TestMethod]
    public void BitFoldToLeft()
    {
      Assert.AreEqual(0b11111111_11111111_11111111_11111000U, uint.BitFoldLeft(0b00000000_10011000U));
    }

    #endregion // BitFolds

    //#region BitIndex

    //[TestMethod]
    //public void BitIndexClear()
    //{
    //  Assert.AreEqual(0x2U, 0x6U.ClearBit(2));
    //}

    //[TestMethod]
    //public void BitIndexFlip()
    //{
    //  Assert.AreEqual(4, 0.FlipBit(2));
    //}

    //[TestMethod]
    //public void BitIndexGet()
    //{
    //  Assert.IsTrue(0x6U.GetBit(2));
    //}

    //[TestMethod]
    //public void BitIndexOfPow2()
    //{
    //  var tb = 0x18;
    //  var tbil = int.LeastSignificant1Bit(tb).BitIndexOfPow2();
    //  Assert.AreEqual(3, tbil);
    //  var tbim = int.MostSignificant1Bit(tb).BitIndexOfPow2();
    //  Assert.AreEqual(4, tbim);
    //}

    //[TestMethod]
    //public void BitIndexSet()
    //{
    //  Assert.AreEqual(0x4U, 0b00000000U.SetBit(2));
    //}

    //#endregion // BitIndex

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

    //[TestMethod]
    //public void BitMaskCheckAll()
    //{
    //  Assert.IsTrue(0b111110.BitMaskCheckAll(0b101010));
    //}

    //[TestMethod]
    //public void BitMaskCheckAny()
    //{
    //  Assert.IsTrue(0b1111.BitMaskCheckAny(0b101010));
    //}

    //[TestMethod]
    //public void BitMaskClear()
    //{
    //  Assert.AreEqual(0b0101, 0b1111.BitMaskClear(0b1010));
    //}

    [TestMethod]
    public void CreateBitMaskLsbFromBitLength()
    {
      Assert.AreEqual(127, Flux.IBinaryInteger.CreateBitMaskRight(7));
    }

    [TestMethod]
    public void CreateBitMaskMsbFromBitLength()
    {
      Assert.AreEqual(-1, Flux.IBinaryInteger.CreateBitMaskLeft(32));
    }

    [TestMethod]
    public void FillBitMaskToLsb()
    {
      var templateBitMask = 0b110;
      var templateBitLength = 3;

      var expected = 0b1011_0110_1101_1011_0110_1101_1011_0110U;
      var actual = unchecked(Flux.IBinaryInteger.CreateBitMaskRight((uint)templateBitMask, templateBitLength, expected.GetBitLength()));

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
      var actual = unchecked(Flux.IBinaryInteger.CreateBitMaskLeft((uint)templateBitMask, templateBitLength, expected.GetBitLength()));

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

    //#region GetByteCount

    //[TestMethod]
    //public void GetByteCount()
    //{
    //  Assert.AreEqual(4, 88.GetByteCount());
    //}

    //#endregion // GetByteCount

    //#region GetPopCount

    //[TestMethod]
    //public void GetPopCount()
    //{
    //  Assert.AreEqual(4, 0xF0.GetPopCount());
    //}

    //#endregion // GetPopCount

    #region GrayCode
    #endregion // GrayCode

    #region Log2

    [TestMethod]
    public void Log2AwayFromZero()
    {
      Assert.AreEqual(8, IBinaryInteger.IntegerLog(215, 2).IntegralLogAwayFromZero);
    }

    [TestMethod]
    public void Log2TowardZero()
    {
      Assert.AreEqual(4, IBinaryInteger.IntegerLog(215, 3).IntegralLogTowardZero);
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
      var towardZero = Flux.IBinaryInteger.RoundDownToPowerOf2(88, false);
      var awayFromZero = Flux.IBinaryInteger.RoundUpToPowerOf2(88, false);

      Assert.AreEqual(64, towardZero);
      Assert.AreEqual(128, awayFromZero);
    }

    //[TestMethod]
    //public void PowOf2UnequalWithRoundingAwayFromZero()
    //{
    //  var value = 88;

    //  var towardsZero = value.Pow2TowardZero(true);
    //  var awayFromZero = value.Pow2AwayFromZero(true);

    //  var actual = 88.RoundToNearest(UniversalRounding.IntegralAwayFromZero, towardsZero, awayFromZero);

    //  Assert.AreEqual(128, actual);
    //}

    //[TestMethod]
    //public void PowOf2UnequalWithRoundingTowardZero()
    //{
    //  var value = 88;

    //  var towardsZero = value.Pow2TowardZero(true);
    //  var awayFromZero = value.Pow2AwayFromZero(true);

    //  var actual = 88.RoundToNearest(UniversalRounding.IntegralTowardZero, towardsZero, awayFromZero);

    //  Assert.AreEqual(64, actual);
    //}

    [TestMethod]
    public void Pow2WithRoundingAwayFromZero()
    {
      var value = 88;

      var towardsZero = Flux.IBinaryInteger.RoundDownToPowerOf2(value, false);
      var awayFromZero = Flux.IBinaryInteger.RoundUpToPowerOf2(value, false);

      var rounded = INumber.RoundToNearest(88, HalfRounding.AwayFromZero, false, [towardsZero, awayFromZero]);

      Assert.AreEqual(64, rounded);

      Assert.AreEqual(64, towardsZero);
      Assert.AreEqual(128, awayFromZero);
    }

    //[TestMethod]
    //public void PowOf2WithRoundingTowardZero()
    //{
    //  var value = 88;

    //  var towardsZero = value.Pow2TowardZero(false);
    //  var awayFromZero = value.Pow2AwayFromZero(false);

    //  var actual = 88.RoundToNearest(UniversalRounding.IntegralTowardZero, towardsZero, awayFromZero);

    //  Assert.AreEqual(64, actual);
    //}

    #endregion // Pow2

    #region ReverseBits

    [TestMethod]
    public void ReverseBits()
    {
      // Somehow BigInteger must differ between .NET version 6 and 7. 

      Assert.AreEqual(0x00010000, Flux.IBinaryInteger.ReverseBits(0x00008000)); // This works on .NET 7, but not on .NET 6.
      Assert.AreEqual(0x10000000, Flux.IBinaryInteger.ReverseBits(0x00000008)); // This works on .NET 6, but not on .NET 7.

      Assert.AreEqual(unchecked((int)0xFFFFFFFE), Flux.IBinaryInteger.ReverseBits(0x7FFFFFFF));
    }

    #endregion // ReverseBits

    #region ReverseBytes

    [TestMethod]
    public void ReverseBytes()
    {
      Assert.AreEqual(0x00010000, Flux.IBinaryInteger.ReverseBytes(0x00000100));
    }

    #endregion // ReverseBytes

    #region Significant1Bits

    [TestMethod]
    public void LeastSignificant1Bit()
    {
      Assert.AreEqual(0b00001000, int.LeastSignificant1Bit(0b01011000));
    }

    [TestMethod]
    public void LeastSignificant1BitClear()
    {
      Assert.AreEqual(0b01010000, int.ClearLeastSignificant1Bit(0b01011000));
    }

    [TestMethod]
    public void MostSignificant1Bit()
    {
      Assert.AreEqual(0b01000000, int.MostSignificant1Bit(0b01011000));
    }

    [TestMethod]
    public void MostSignificant1BitClear()
    {
      Assert.AreEqual(0b00011000, int.ClearMostSignificant1Bit(0b01011000));
    }

    #endregion // Significant1Bits

    #region ZeroCounts

    //[TestMethod]
    //public void GetLeadingZeroCount()
    //{
    //  Assert.AreEqual(25, 88.GetLeadingZeroCount());
    //}

    //[TestMethod]
    //public void GetTrailingZeroCount()
    //{
    //  Assert.AreEqual(3, 88.GetTrailingZeroCount());
    //}

    #endregion // ZeroCounts
  }
}
#endif
