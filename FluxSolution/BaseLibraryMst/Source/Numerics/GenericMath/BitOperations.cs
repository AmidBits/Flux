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
      Assert.AreEqual(7, 88.ToBigInteger().ShortestBitLength());
      Assert.AreEqual(7, 88.ToBigInteger().ShortestBitLength());
    }

    [TestMethod]
    public void FLog2()
    {
      Assert.AreEqual(6.467605550082998, (88.5).Log2());
    }

    [TestMethod]
    public void ILog2()
    {
      Assert.AreEqual(6, 88.IntegerLog2());
    }

    [TestMethod]
    public void GetLeadingZeroCount()
    {
      Assert.AreEqual(25, 88.LeadingZeroCount());
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
    public void PowOf2()
    {
      88.ToBigInteger().RoundToPow2(false, RoundingMode.HalfToEven, out System.Numerics.BigInteger towardsZero, out System.Numerics.BigInteger awayFromZero);

      Assert.AreEqual(64.ToBigInteger(), towardsZero);
      Assert.AreEqual(128.ToBigInteger(), awayFromZero);
    }

    [TestMethod]
    public void RoundToPowOf2AwayFromZero()
    {
      var actual = 88.ToBigInteger().RoundToPow2(false, RoundingMode.AwayFromZero, out var _, out var _);

      Assert.AreEqual(128.ToBigInteger(), actual);
    }

    [TestMethod]
    public void RoundToPowOf2AwayFromZeroProper()
    {
      var actual = 88.ToBigInteger().RoundToPow2(true, RoundingMode.AwayFromZero, out var _, out var _);

      Assert.AreEqual(128.ToBigInteger(), actual);
    }

    [TestMethod]
    public void RoundToPowOf2TowardZero()
    {
      var actual = 88.ToBigInteger().RoundToPow2(false, RoundingMode.TowardZero, out var _, out var _);

      Assert.AreEqual(64.ToBigInteger(), actual);
    }

    [TestMethod]
    public void RoundToPowOf2TowardZeroProper()
    {
      var actual = 88.ToBigInteger().RoundToPow2(true, RoundingMode.TowardZero, out var _, out var _);

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
      // Somehow BigInteger must differ between .NET version 6 and 7. 

      Assert.AreEqual(65536, Flux.Bits.ReverseBits(32768)); // This works on .NET 7, but not on .NET 6.
      Assert.AreEqual(268435456, Flux.Bits.ReverseBits(8)); // This works on .NET 6, but not on .NET 7.
    }

    [TestMethod]
    public void ReverseBytes()
    {
      Assert.AreEqual(65536, Flux.Bits.ReverseBytes(256));
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
