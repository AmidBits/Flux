using System;
using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundation.BitOps
{
  [TestClass]
  public class CreateMaskLeft
  {
    [TestMethod]
    public void CreateMaskLeft_BigInteger()
      => Assert.AreEqual(0b111111111000000000.ToBigInteger(), Flux.BitOps.CreateMaskLeftBigInteger(9, 9));
    [TestMethod]
    public void CreateMaskLeft_BigInteger_Speed()
    {
      var expected = 0b111111111000000000.ToBigInteger();
      Flux.Services.Performance.Measure(() => Flux.BitOps.CreateMaskLeftBigInteger(9, 9), 500000).Assert(expected, 1);
    }
    [TestMethod]
    public void CreateMaskLeft_Int32()
      => Assert.AreEqual(unchecked((int)0b11111111100000000000000000000000), Flux.BitOps.CreateMaskLeftInt32(9));
    [TestMethod]
    public void CreateMaskLeft_Int32_Speed()
    {
      var expected = unchecked((int)0b11111111100000000000000000000000);
      Flux.Services.Performance.Measure(() => Flux.BitOps.CreateMaskLeftInt32(9), 1000000).Assert(expected, 0.3);
    }
    [TestMethod]
    public void CreateMaskLeft_Int64()
      => Assert.AreEqual(unchecked((long)0b1111111110000000000000000000000000000000000000000000000000000000), Flux.BitOps.CreateMaskLeftInt64(9));
    [TestMethod]
    public void CreateMaskLeft_Int64_Speed()
    {
      var expected = unchecked((long)0b1111111110000000000000000000000000000000000000000000000000000000);
      Flux.Services.Performance.Measure(() => Flux.BitOps.CreateMaskLeftInt64(9), 1000000).Assert(expected, 0.3);
    }
    [TestMethod]
    public void CreateMaskLeft_UInt32()
      => Assert.AreEqual(0b11111111100000000000000000000000U, Flux.BitOps.CreateMaskLeftUInt32(9));
    [TestMethod]
    public void CreateMaskLeft_UInt32_Speed()
    {
      var expected = 0b11111111100000000000000000000000U;
      Flux.Services.Performance.Measure(() => Flux.BitOps.CreateMaskLeftUInt32(9), 1000000).Assert(expected, 0.3);
    }
    [TestMethod]
    public void CreateMaskLeft_UInt64()
      => Assert.AreEqual(0b1111111110000000000000000000000000000000000000000000000000000000UL, Flux.BitOps.CreateMaskLeftUInt64(9));
    [TestMethod]
    public void CreateMaskLeft_UInt64_Speed()
    {
      var expected = 0b1111111110000000000000000000000000000000000000000000000000000000UL;
      Flux.Services.Performance.Measure(() => Flux.BitOps.CreateMaskLeftUInt64(9), 1000000).Assert(expected, 0.3);
    }
  }
}
