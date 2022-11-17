//using System;
//using System.Linq;
//using Flux;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace Foundation.BitOps
//{
//  [TestClass]
//  public class CreateMaskRight
//  {
//    [TestMethod]
//    public void CreateMaskRight_BigInteger()
//      => Assert.AreEqual(0b111111111.ToBigInteger(), Flux.BitOps.CreateMaskRightBigInteger( 9));
//    [TestMethod]
//    public void CreateMaskRight_BigInteger_Speed()
//    {
//      var expected = 0b111111111.ToBigInteger();
//      Flux.Services.Performance.Measure(() => Flux.BitOps.CreateMaskRightBigInteger(9), 500000).Assert(expected, 1);
//    }
//    [TestMethod]
//    public void CreateMaskRight_Int32()
//      => Assert.AreEqual(unchecked((int)0b111111111), Flux.BitOps.CreateMaskRightInt32(9));
//    [TestMethod]
//    public void CreateMaskRight_Int32_Speed()
//    {
//      var expected = unchecked((int)0b111111111);
//      Flux.Services.Performance.Measure(() => Flux.BitOps.CreateMaskRightInt32(9), 1000000).Assert(expected, 0.3);
//    }
//    [TestMethod]
//    public void CreateMaskRight_Int64()
//      => Assert.AreEqual(unchecked((long)0b111111111), Flux.BitOps.CreateMaskRightInt64(9));
//    [TestMethod]
//    public void CreateMaskRight_Int64_Speed()
//    {
//      var expected = unchecked((long)0b111111111);
//      Flux.Services.Performance.Measure(() => Flux.BitOps.CreateMaskRightInt64(9), 1000000).Assert(expected, 0.3);
//    }
//    [TestMethod]
//    public void CreateMaskRight_UInt32()
//      => Assert.AreEqual(0b111111111U, Flux.BitOps.CreateMaskRightUInt32(9));
//    [TestMethod]
//    public void CreateMaskRight_UInt32_Speed()
//    {
//      var expected = 0b111111111U;
//      Flux.Services.Performance.Measure(() => Flux.BitOps.CreateMaskRightUInt32(9), 1000000).Assert(expected, 0.3);
//    }
//    [TestMethod]
//    public void CreateMaskRight_UInt64()
//      => Assert.AreEqual(0b111111111UL, Flux.BitOps.CreateMaskRightUInt64(9));
//    [TestMethod]
//    public void CreateMaskRight_UInt64_Speed()
//    {
//      var expected = 0b111111111UL;
//      Flux.Services.Performance.Measure(() => Flux.BitOps.CreateMaskRightUInt64(9), 1000000).Assert(expected, 0.3);
//    }
//  }
//}
