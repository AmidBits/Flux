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
    public void RoundDownToPow2()
    {
      Assert.AreEqual(64.ToBigInteger(), 88.ToBigInteger().RoundDownToPow2());
    }

    [TestMethod]
    public void RoundDownToPow2Proper()
    {
      Assert.AreEqual(64.ToBigInteger(), 88.ToBigInteger().RoundDownToPow2Proper());
    }

    [TestMethod]
    public void RoundToNearestPow2()
    {
      Assert.AreEqual(64.ToBigInteger(), 88.ToBigInteger().RoundToNearestPow2(out var greaterThanOrEqual, out var lessThanOrEqual));

      Assert.AreEqual(128.ToBigInteger(), greaterThanOrEqual);
      Assert.AreEqual(64.ToBigInteger(), lessThanOrEqual);
    }

    [TestMethod]
    public void RoundToNearestPow2Proper()
    {
      Assert.AreEqual(64.ToBigInteger(), 88.ToBigInteger().RoundToNearestPow2(out var greaterThan, out var lessThan));

      Assert.AreEqual(128.ToBigInteger(), greaterThan);
      Assert.AreEqual(64.ToBigInteger(), lessThan);
    }

    [TestMethod]
    public void RoundUpToPow2()
    {
      Assert.AreEqual(128.ToBigInteger(), 88.ToBigInteger().RoundUpToPow2());
    }

    [TestMethod]
    public void RoundUpToPow2Proper()
    {
      Assert.AreEqual(128.ToBigInteger(), 88.ToBigInteger().RoundUpToPow2Proper());
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
