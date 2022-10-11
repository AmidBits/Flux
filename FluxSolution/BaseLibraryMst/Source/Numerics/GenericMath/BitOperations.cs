#if NET7_0_OR_GREATER
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

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
    public void GetIntegerLog2Ceiling()
    {
      Assert.AreEqual(7, 88.ToBigInteger().GetIntegerLog2Ceiling());
    }

    [TestMethod]
    public void GetIntegerLog2Floor()
    {
      Assert.AreEqual(6, 88.ToBigInteger().GetIntegerLog2Floor());
    }

    [TestMethod]
    public void IsPow2()
    {
      Assert.AreEqual(false, 88.ToBigInteger().IsPow2());
    }

    [TestMethod]
    public void GetNearestPow2HalfwayAwayFromZero()
    {
      //var rounding = new RoundToBoundary<System.Numerics.BigInteger>(RoundingMode.HalfwayToEven);
      
      Assert.AreEqual(128.ToBigInteger(), 88.ToBigInteger().GetNearestPow2(false, RoundingMode.HalfAwayFromZero, out var _, out var _));
    }

    [TestMethod]
    public void GetNearestPow2AwayFromZeroProper()
    {
      Assert.AreEqual(128.ToBigInteger(), 88.ToBigInteger().GetNearestPow2(true, RoundingMode.HalfAwayFromZero, out var _, out var _));
    }

    [TestMethod]
    public void GetNearestPow2()
    {
      Assert.AreEqual(64.ToBigInteger(), 88.ToBigInteger().GetNearestPow2(false, RoundingMode.HalfToEven, out var towardsZero, out var awayFromZero));

      Assert.AreEqual(64.ToBigInteger(), towardsZero);
      Assert.AreEqual(128.ToBigInteger(), awayFromZero);
    }

    [TestMethod]
    public void GetNearestPow2Proper()
    {
      Assert.AreEqual(64.ToBigInteger(), 88.ToBigInteger().GetNearestPow2(true, RoundingMode.HalfToEven, out var towardsZero, out var awayFromZero));

      Assert.AreEqual(64.ToBigInteger(), towardsZero);
      Assert.AreEqual(128.ToBigInteger(), awayFromZero);
    }

    [TestMethod]
    public void GetNearestPow2TowardsZero()
    {
      Assert.AreEqual(64.ToBigInteger(), 88.ToBigInteger().GetNearestPow2(false, RoundingMode.HalfTowardZero, out var _, out var _));
    }

    [TestMethod]
    public void GetNearestPow2TowardsZeroProper()
    {
      Assert.AreEqual(64.ToBigInteger(), 88.ToBigInteger().GetNearestPow2(true, RoundingMode.HalfTowardZero, out var _, out var _));
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
  }
}
#endif
