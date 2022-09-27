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
    public void TryGetIntegerLog2()
    {
      88.ToBigInteger().TryGetIntegerLog2(out var log2Floor, out var log2Ceiling);

      Assert.AreEqual(6, log2Floor);
      Assert.AreEqual(7, log2Ceiling);
    }

    [TestMethod]
    public void IsPow2()
    {
      Assert.AreEqual(false, 88.ToBigInteger().IsPow2());
    }

    [TestMethod]
    public void RoundToPow2AwayFromZero()
    {
      Assert.AreEqual(128.ToBigInteger(), 88.ToBigInteger().RoundToPow2(false, FullRounding.AwayFromZero, out var _, out var _));
    }

    [TestMethod]
    public void RoundToPow2AwayFromZeroProper()
    {
      Assert.AreEqual(128.ToBigInteger(), 88.ToBigInteger().RoundToPow2(true, FullRounding.AwayFromZero, out var _, out var _));
    }

    [TestMethod]
    public void RoundToNearestPow2()
    {
      Assert.AreEqual(64.ToBigInteger(), 88.ToBigInteger().RoundToNearestPow2(false, HalfwayRounding.ToEven, out var towardsZero, out var awayFromZero));

      Assert.AreEqual(64.ToBigInteger(), towardsZero);
      Assert.AreEqual(128.ToBigInteger(), awayFromZero);
    }

    [TestMethod]
    public void RoundToNearestPow2Proper()
    {
      Assert.AreEqual(64.ToBigInteger(), 88.ToBigInteger().RoundToNearestPow2(true, HalfwayRounding.ToEven, out var towardsZero, out var awayFromZero));

      Assert.AreEqual(64.ToBigInteger(), towardsZero);
      Assert.AreEqual(128.ToBigInteger(), awayFromZero);
    }

    [TestMethod]
    public void RoundToPow2TowardsZero()
    {
      Assert.AreEqual(64.ToBigInteger(), 88.ToBigInteger().RoundToPow2(false, FullRounding.TowardZero, out var _, out var _));
    }

    [TestMethod]
    public void RoundToPow2TowardsZeroProper()
    {
      Assert.AreEqual(64.ToBigInteger(), 88.ToBigInteger().RoundToPow2(true, FullRounding.TowardZero, out var _, out var _));
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
