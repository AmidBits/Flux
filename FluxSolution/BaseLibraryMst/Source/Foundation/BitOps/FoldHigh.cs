using System;
using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundation.BitOps
{
  [TestClass]
  public class FoldHigh
  {
    readonly System.Numerics.BigInteger[] pbi = System.Linq.Enumerable.Range(0, 255).Select(i => (System.Numerics.BigInteger)i).ToArray();
    readonly int[] pi = System.Linq.Enumerable.Range(0, 255).Select(i => (int)i).ToArray();
    readonly long[] pl = System.Linq.Enumerable.Range(0, 255).Select(i => (long)i).ToArray();
    readonly uint[] pui = System.Linq.Enumerable.Range(0, 255).Select(i => (uint)i).ToArray();
    readonly ulong[] pul = System.Linq.Enumerable.Range(0, 255).Select(i => (ulong)i).ToArray();

    [TestMethod]
    public void FoldHigh_BigInteger()
      => Assert.AreEqual(254.ToBigInteger(), Flux.BitOps.FoldLeft(18.ToBigInteger()));
    [TestMethod]
    public void FoldHigh_BigInteger_Speed()
    {
      var value = System.Numerics.BigInteger.Parse("670530"); // 0x000a3b42
      var expected = System.Numerics.BigInteger.Parse("4294967294"); // 0x000ffffe
      Flux.Services.Performance.Measure(() => Flux.BitOps.FoldLeft(value), 1000000).Assert(expected, 4);
    }
    [TestMethod]
    public void FoldHigh_Int32()
      => Assert.AreEqual(-2, Flux.BitOps.FoldLeft(18));
    [TestMethod]
    public void FoldHigh_Int32_Speed()
    {
      var value = 670530;
      var expected = -2;
      if (value >= int.MinValue && value <= int.MaxValue) Flux.Services.Performance.Measure(() => Flux.BitOps.FoldLeft(value), 1000000).Assert(expected, 0.3);
    }
    [TestMethod]
    public void FoldHigh_Int64()
      => Assert.AreEqual(-2, Flux.BitOps.FoldLeft(18L));
    [TestMethod]
    public void FoldHigh_Int64_Speed()
    {
      var value = 670530L;
      var expected = -2L;
      Flux.Services.Performance.Measure(() => Flux.BitOps.FoldLeft(value), 1000000).Assert(expected, 0.3);
    }
    [TestMethod]
    public void FoldHigh_UInt32()
      => Assert.AreEqual(4294967294U, Flux.BitOps.FoldLeft(18U));
    [TestMethod]
    public void FoldHigh_UInt32_Speed()
    {
      var value = 670530U;
      var expected = 4294967294U;
      Flux.Services.Performance.Measure(() => Flux.BitOps.FoldLeft(value), 1000000).Assert(expected, 0.3);
    }
    [TestMethod]
    public void FoldHigh_UInt64()
      => Assert.AreEqual(18446744073709551614UL, Flux.BitOps.FoldLeft(18UL));
    [TestMethod]
    public void FoldHigh_UInt64_Speed()
    {
      var value = 670530UL;
      var expected = 18446744073709551614UL;
      Flux.Services.Performance.Measure(() => Flux.BitOps.FoldLeft(value), 1000000).Assert(expected, 0.3);
    }
  }
}
