using System;
using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundation.BitOps
{
  [TestClass]
  public class FoldLow
  {
    readonly System.Numerics.BigInteger[] pbi = System.Linq.Enumerable.Range(0, 255).Select(i => (System.Numerics.BigInteger)i).ToArray();
    readonly int[] pi = System.Linq.Enumerable.Range(0, 255).Select(i => (int)i).ToArray();
    readonly long[] pl = System.Linq.Enumerable.Range(0, 255).Select(i => (long)i).ToArray();
    readonly uint[] pui = System.Linq.Enumerable.Range(0, 255).Select(i => (uint)i).ToArray();
    readonly ulong[] pul = System.Linq.Enumerable.Range(0, 255).Select(i => (ulong)i).ToArray();

    [TestMethod]
    public void FoldLow_BigInteger()
      => Assert.AreEqual(31, Flux.BitOps.FoldRight(18.ToBigInteger()));
    [TestMethod]
    public void FoldLow_BigInteger_Speed()
    {
      var value = System.Numerics.BigInteger.Parse("670530");
      var expected = 1048575;
      Flux.Services.Performance.Measure(() => Flux.BitOps.FoldRight(value), 1000000).Assert((System.Numerics.BigInteger)expected, 1);
    }
    [TestMethod]
    public void FoldLow_Int32()
      => Assert.AreEqual(31, Flux.BitOps.FoldRight(18));
    [TestMethod]
    public void FoldLow_Int32_Speed()
    {
      var value = 670530;
      var expected = 1048575;
      Flux.Services.Performance.Measure(() => Flux.BitOps.FoldRight(value), 1000000).Assert(expected, 0.3);
    }
    [TestMethod]
    public void FoldLow_Int64()
      => Assert.AreEqual(31, Flux.BitOps.FoldRight(18L));
    [TestMethod]
    public void FoldLow_Int64_Speed()
    {
      var value = 670530L;
      var expected = 1048575L;
      Flux.Services.Performance.Measure(() => Flux.BitOps.FoldRight(value), 1000000).Assert(expected, 0.3);
    }
    [TestMethod]
    public void FoldLow_UInt32()
      => Assert.AreEqual(31U, Flux.BitOps.FoldRight(18U));
    [TestMethod]
    public void FoldLow_UInt32_Speed()
    {
      var value = 670530U;
      var expected = 1048575U;
      Flux.Services.Performance.Measure(() => Flux.BitOps.FoldRight(value), 1000000).Assert(expected, 0.3);
    }
    [TestMethod]
    public void FoldLow_UInt64()
      => Assert.AreEqual(31UL, Flux.BitOps.FoldRight(18UL));
    [TestMethod]
    public void FoldLow_UInt64_Speed()
    {
      var value = 670530UL;
      var expected = 1048575UL;
      Flux.Services.Performance.Measure(() => Flux.BitOps.FoldRight(value), 1000000).Assert(expected, 0.3);
    }
  }
}
