using System;
using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundation.BitOps
{
  [TestClass]
  public class TrailingZeroCount
  {
    readonly System.Numerics.BigInteger[] pbi = System.Linq.Enumerable.Range(0, 255).Select(i => (System.Numerics.BigInteger)i).ToArray();
    readonly int[] pi = System.Linq.Enumerable.Range(0, 255).Select(i => (int)i).ToArray();
    readonly long[] pl = System.Linq.Enumerable.Range(0, 255).Select(i => (long)i).ToArray();
    readonly uint[] pui = System.Linq.Enumerable.Range(0, 255).Select(i => (uint)i).ToArray();
    readonly ulong[] pul = System.Linq.Enumerable.Range(0, 255).Select(i => (ulong)i).ToArray();

    [TestMethod]
    public void TrailingZeroCount_BigInteger()
      => Assert.AreEqual(1, Flux.BitOps.TrailingZeroCount(18.ToBigInteger()));
    [TestMethod]
    public void TrailingZeroCount_BigInteger_Speed()
    {
      var value = System.Numerics.BigInteger.Parse("670530");
      var expected = 1;
      Flux.Services.Performance.Measure(() => Flux.BitOps.TrailingZeroCount(value), 1000000).Assert(expected, 1);
    }
    [TestMethod]
    public void TrailingZeroCount_Int32()
      => Assert.AreEqual(1, Flux.BitOps.TrailingZeroCount(18));
    [TestMethod]
    public void TrailingZeroCount_Int32_Speed()
    {
      var value = 670530;
      var expected = 1;
      Flux.Services.Performance.Measure(() => Flux.BitOps.TrailingZeroCount(value), 1000000).Assert(expected, 0.3);
    }
    [TestMethod]
    public void TrailingZeroCount_Int64()
      => Assert.AreEqual(1, Flux.BitOps.TrailingZeroCount(18L));
    [TestMethod]
    public void TrailingZeroCount_Int64_Speed()
    {
      var value = 670530L;
      var expected = 1;
      Flux.Services.Performance.Measure(() => Flux.BitOps.TrailingZeroCount(value), 1000000).Assert(expected, 0.3);
    }
    [TestMethod]
    public void TrailingZeroCount_UInt32()
      => Assert.AreEqual(1, Flux.BitOps.TrailingZeroCount(18U));
    [TestMethod]
    public void TrailingZeroCount_UInt32_Speed()
    {
      var value = 670530U;
      var expected = 1;
      Flux.Services.Performance.Measure(() => Flux.BitOps.TrailingZeroCount(value), 1000000).Assert(expected, 0.3);
    }
    [TestMethod]
    public void TrailingZeroCount_UInt64()
      => Assert.AreEqual(1, Flux.BitOps.TrailingZeroCount(18UL));
    [TestMethod]
    public void TrailingZeroCount_UInt64_Speed()
    {
      var value = 670530UL;
      var expected = 1;
      Flux.Services.Performance.Measure(() => Flux.BitOps.TrailingZeroCount(value), 1000000).Assert(expected, 0.3);
    }
  }
}
