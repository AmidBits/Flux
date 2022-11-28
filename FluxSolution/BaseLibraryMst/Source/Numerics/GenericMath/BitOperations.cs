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
    public void GetBitLength()
    {
      Assert.AreEqual(7, 88.ToBigInteger().GetBitLength());
    }

    [TestMethod]
    public void GetShortestBitLength()
    {
      Assert.AreEqual(7, 88.ToBigInteger().GetShortestBitLength());
    }

    [TestMethod]
    public void LocateNearestIntegerLog2()
    {
      88.ToBigInteger().LocateNearestIntegerLog2(out int nearestTowardsZero, out int nearestAwayFromZero);

      Assert.AreEqual(6, nearestTowardsZero);
      Assert.AreEqual(7, nearestAwayFromZero);
    }

    [TestMethod]
    public void NearestIntegerLog2AwayFromZero()
    {
      Assert.AreEqual(7, 88.ToBigInteger().NearestIntegerLog2AwayFromZero(out int _));
    }

    [TestMethod]
    public void NearestIntegerLog2TowardsZero()
    {
      Assert.AreEqual(6, 88.ToBigInteger().NearestIntegerLog2TowardsZero(out int _));
    }

    [TestMethod]
    public void GetLeadingZeroCount()
    {
      Assert.AreEqual(25, 88.ToBigInteger().GetLeadingZeroCount());
    }

    [TestMethod]
    public void GetTrailingZeroCount()
    {
      Assert.AreEqual(3, 88.ToBigInteger().GetTrailingZeroCount());
    }

    [TestMethod]
    public void IsPow2()
    {
      Assert.AreEqual(false, 88.ToBigInteger().IsPow2());
    }

    [TestMethod]
    public void LocateNearestPow2()
    {
      88.ToBigInteger().LocateNearestPow2(false, out var towardsZero, out var awayFromZero);

      Assert.AreEqual(64.ToBigInteger(), towardsZero);
      Assert.AreEqual(128.ToBigInteger(), awayFromZero);
    }

    [TestMethod]
    public void NearestPow2()
    {
      var nearest = 88.ToBigInteger().NearestPow2(false, RoundingMode.HalfToEven, out var towardsZero, out var awayFromZero);

      Assert.AreEqual(64.ToBigInteger(), nearest);

      Assert.AreEqual(64.ToBigInteger(), towardsZero);
      Assert.AreEqual(128.ToBigInteger(), awayFromZero);
    }

    [TestMethod]
    public void NearestPow2Proper()
    {
      var nearest = 88.ToBigInteger().NearestPow2(true, RoundingMode.HalfToEven, out var towardsZero, out var awayFromZero);

      Assert.AreEqual(64.ToBigInteger(), nearest);

      Assert.AreEqual(64.ToBigInteger(), towardsZero);
      Assert.AreEqual(128.ToBigInteger(), awayFromZero);
    }

    [TestMethod]
    public void NearestPow2AwayFromZero()
    {
      var actual = 88.ToBigInteger().NearestPow2AwayFromZero(false);

      Assert.AreEqual(128.ToBigInteger(), actual);
    }

    [TestMethod]
    public void NearestPow2AwayFromZeroProper()
    {
      var actual = 88.ToBigInteger().NearestPow2AwayFromZero(true);

      Assert.AreEqual(128.ToBigInteger(), actual);
    }

    [TestMethod]
    public void NearestPow2TowardsZero()
    {
      var actual = 88.ToBigInteger().NearestPow2TowardZero(false);

      Assert.AreEqual(64.ToBigInteger(), actual);
    }

    [TestMethod]
    public void NearestPow2TowardsZeroProper()
    {
      var actual = 88.ToBigInteger().NearestPow2TowardZero(true);

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
      Assert.AreEqual(65536.ToBigInteger(), Flux.BitOps.ReverseBits(32768.ToBigInteger()));
    }

    [TestMethod]
    public void ReverseBytes()
    {
      Assert.AreEqual(65536.ToBigInteger(), Flux.BitOps.ReverseBytes(256.ToBigInteger()));
    }
  }
}
#endif
