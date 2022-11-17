//using System;
//using System.Linq;
//using Flux;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace Foundation.BitOps
//{
//  [TestClass]
//  public class PopCount
//  {
//    readonly System.Numerics.BigInteger[] pbi = System.Linq.Enumerable.Range(0, 255).Select(i => (System.Numerics.BigInteger)i).ToArray();
//    readonly int[] pi = System.Linq.Enumerable.Range(0, 255).Select(i => (int)i).ToArray();
//    readonly long[] pl = System.Linq.Enumerable.Range(0, 255).Select(i => (long)i).ToArray();
//    readonly uint[] pui = System.Linq.Enumerable.Range(0, 255).Select(i => (uint)i).ToArray();
//    readonly ulong[] pul = System.Linq.Enumerable.Range(0, 255).Select(i => (ulong)i).ToArray();

//    [TestMethod]
//    public void PopCount_BigInteger()
//      => Assert.AreEqual(2, Flux.BitOps.PopCount(18.ToBigInteger()));
//    [TestMethod]
//    public void PopCount_BigInteger_Speed()
//    {
//      var value = System.Numerics.BigInteger.Parse("670530");
//      var expected = 9;
//      Flux.Services.Performance.Measure(() => Flux.BitOps.PopCount(value), 500000).Assert(expected, 0.3); // Large discrepancy between Debug and Release code. Not a problem.
//    }
//    [TestMethod]
//    public void PopCount_Int32()
//      => Assert.AreEqual(2, Flux.BitOps.PopCount(18));
//    [TestMethod]
//    public void PopCount_Int32_Speed()
//    {
//      var value = 670530;
//      var expected = 9;
//      Flux.Services.Performance.Measure(() => Flux.BitOps.PopCount(value), 1000000).Assert(expected, 0.3); // Large discrepancy between Debug and Release code. Not a problem.
//    }
//    [TestMethod]
//    public void PopCount_Int64()
//      => Assert.AreEqual(2, Flux.BitOps.PopCount(18));
//    [TestMethod]
//    public void PopCount_Int64_Speed()
//    {
//      var value = 670530L;
//      var expected = 9;
//      Flux.Services.Performance.Measure(() => Flux.BitOps.PopCount(value), 1000000).Assert(expected, 0.3); // Large discrepancy between Debug and Release code. Not a problem.
//    }
//    [TestMethod]
//    public void PopCount_UInt32()
//      => Assert.AreEqual(2, Flux.BitOps.PopCount(18));
//    [TestMethod]
//    public void PopCount_UInt32_Speed()
//    {
//      var value = 670530U;
//      var expected = 9;
//      Flux.Services.Performance.Measure(() => Flux.BitOps.PopCount(value), 1000000).Assert(expected, 0.3); // Large discrepancy between Debug and Release code. Not a problem.
//    }
//    [TestMethod]
//    public void PopCount_UInt64()
//      => Assert.AreEqual(2, Flux.BitOps.PopCount(18));
//    [TestMethod]
//    public void PopCount_UInt64_Speed()
//    {
//      var value = 670530UL;
//      var expected = 9;
//      Flux.Services.Performance.Measure(() => Flux.BitOps.PopCount(value), 1000000).Assert(expected, 0.3); // Large discrepancy between Debug and Release code. Not a problem.
//    }
//  }
//}
