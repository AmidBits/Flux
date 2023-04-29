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
      Assert.AreEqual(0xFFFFFFF8U, 0x98U.BitFoldLeft());
    }

    [TestMethod]
    public void BitFoldRight()
    {
      Assert.AreEqual(0x0000007FU, 0x58U.BitFoldRight());
    }

    [TestMethod]
    public void GetBitLengthEx()
    {
      Assert.AreEqual(7, 88.ToBigInteger().BitLength());
      Assert.AreEqual(7, 88.ToBigInteger().BitLength());
    }

    [TestMethod]
    public void FLog2()
    {
      Assert.AreEqual(6.467605550082998, (88.5).Log2F());
    }

    [TestMethod]
    public void ILog2()
    {
      Assert.AreEqual(6, 88.ILog2());
    }

    [TestMethod]
    public void GetLeadingZeroCount()
    {
      Assert.AreEqual(25, 88.ToBigInteger().LeadingZeroCount());
    }

    [TestMethod]
    public void GetTrailingZeroCount()
    {
      Assert.AreEqual(3, 88.ToBigInteger().TrailingZeroCount());
    }

    //[TestMethod]
    //public void IsPow2Ex()
    //{
    //  Assert.AreEqual(false, 88.ToBigInteger().IsPow2Ex());
    //}

    //[TestMethod]
    //public void LocatePow2()
    //{
    //  88.LocatePow2(false, out var towardsZero, out var awayFromZero);

    //  Assert.AreEqual(64.ToBigInteger(), towardsZero);
    //  Assert.AreEqual(128.ToBigInteger(), awayFromZero);
    //}

    [TestMethod]
    public void NearestPow2()
    {
      var nearest = 88.ToBigInteger().NearestPowOf2(false, RoundingMode.HalfToEven, out System.Numerics.BigInteger towardsZero, out System.Numerics.BigInteger awayFromZero);

      Assert.AreEqual(64.ToBigInteger(), nearest);

      Assert.AreEqual(64.ToBigInteger(), towardsZero);
      Assert.AreEqual(128.ToBigInteger(), awayFromZero);
    }

    [TestMethod]
    public void NearestPow2Proper()
    {
      var nearest = 88.ToBigInteger().NearestPowOf2(true, RoundingMode.HalfToEven, out System.Numerics.BigInteger towardsZero, out System.Numerics.BigInteger awayFromZero);

      Assert.AreEqual(64.ToBigInteger(), nearest);

      Assert.AreEqual(64.ToBigInteger(), towardsZero);
      Assert.AreEqual(128.ToBigInteger(), awayFromZero);
    }

    [TestMethod]
    public void Pow2AwayFromZero()
    {
      var actual = 88.ToBigInteger().PowOf2Afz(false);

      Assert.AreEqual(128.ToBigInteger(), actual);
    }

    [TestMethod]
    public void Pow2AwayFromZeroProper()
    {
      var actual = 88.ToBigInteger().PowOf2Afz(true);

      Assert.AreEqual(128.ToBigInteger(), actual);
    }

    [TestMethod]
    public void Pow2TowardsZero()
    {
      var actual = 88.ToBigInteger().PowOf2Tz(false);

      Assert.AreEqual(64.ToBigInteger(), actual);
    }

    [TestMethod]
    public void Pow2TowardsZeroProper()
    {
      var actual = 88.ToBigInteger().PowOf2Tz(true);

      Assert.AreEqual(64.ToBigInteger(), actual);
    }

    [TestMethod]
    public void LeastSignificant1Bit()
    {
      Assert.AreEqual(8.ToBigInteger(), 88.ToBigInteger().LeastSignificant1Bit());
    }

    [TestMethod]
    public void MostSignificant1Bit()
    {
      Assert.AreEqual(64.ToBigInteger(), 88.ToBigInteger().MostSignificant1Bit());
    }

    [TestMethod]
    public void ReverseBits()
    {
      Assert.AreEqual(65536.ToBigInteger(), Flux.Bits.ReverseBits(32768.ToBigInteger()));
    }

    [TestMethod]
    public void ReverseBytes()
    {
      Assert.AreEqual(65536, Flux.Bits.ReverseBytes(256));
    }
  }
}
#endif
