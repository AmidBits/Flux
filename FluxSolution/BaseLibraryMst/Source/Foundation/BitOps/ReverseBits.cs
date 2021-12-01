using System;
using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundation.BitOps
{
  [TestClass]
  public class ReverseBits
  {
    readonly System.Numerics.BigInteger[] pbi = System.Linq.Enumerable.Range(0, 255).Select(i => (System.Numerics.BigInteger)i).ToArray();
    readonly int[] pi = System.Linq.Enumerable.Range(0, 255).Select(i => (int)i).ToArray();
    readonly long[] pl = System.Linq.Enumerable.Range(0, 255).Select(i => (long)i).ToArray();
    readonly uint[] pui = System.Linq.Enumerable.Range(0, 255).Select(i => (uint)i).ToArray();
    readonly ulong[] pul = System.Linq.Enumerable.Range(0, 255).Select(i => (ulong)i).ToArray();

    [TestMethod]
    public void ReverseBits_BigInteger()
    {
      Assert.AreEqual(pbi[30], Flux.BitOps.ReverseBits(pbi[120]));
      Assert.AreEqual(pbi[1], Flux.BitOps.ReverseBits(pbi[128]));

      Assert.AreEqual(pbi[48], Flux.BitOps.ReverseBits(pbi[12]));
      Assert.AreEqual(pbi[222], Flux.BitOps.ReverseBits(pbi[123]));
    }
    [TestMethod]
    public void ReverseBits_BigInteger_Speed()
    {
      var value = System.Numerics.BigInteger.Parse("670530");
      var expected = 4381776.ToBigInteger();
      Flux.Services.Performance.Measure(() => Flux.BitOps.ReverseBits(value), 1000000).Assert(expected, 1);
    }
    [TestMethod]
    public void ReverseBits_Int32()
    {
      Assert.AreEqual(805306368, Flux.BitOps.ReverseBits(pi[12]));
      Assert.AreEqual(301989888, Flux.BitOps.ReverseBits(pi[72]));
    }
    [TestMethod]
    public void ReverseBits_Int32_Speed()
    {
      var value = 670530;
      var expected = 1121734656;
      Flux.Services.Performance.Measure(() => Flux.BitOps.ReverseBits(value), 1000000).Assert(expected, 0.3);
    }
    [TestMethod]
    public void ReverseBits_Int64()
    {
      Assert.AreEqual(3458764513820540928, Flux.BitOps.ReverseBits(pl[12]));
      Assert.AreEqual(1297036692682702848, Flux.BitOps.ReverseBits(pl[72]));
    }
    [TestMethod]
    public void ReverseBits_Int64_Speed()
    {
      var value = 670530L;
      var expected = 4817813662309810176L;
      Flux.Services.Performance.Measure(() => Flux.BitOps.ReverseBits(value), 1000000).Assert(expected, 0.3);
    }
    [TestMethod]
    public void ReverseBits_UInt32()
    {
      Assert.AreEqual(805306368U, Flux.BitOps.ReverseBits(pui[12]));
      Assert.AreEqual(301989888U, Flux.BitOps.ReverseBits(pui[72]));
    }
    [TestMethod]
    public void ReverseBits_UInt32_Speed()
    {
      var value = 670530U;
      var expected = 1121734656U;
      Flux.Services.Performance.Measure(() => Flux.BitOps.ReverseBits(value), 1000000).Assert(expected, 0.3);
    }
    [TestMethod]
    public void ReverseBits_UInt64()
    {
      Assert.AreEqual(3458764513820540928U, Flux.BitOps.ReverseBits(pul[12]));
      Assert.AreEqual(1297036692682702848U, Flux.BitOps.ReverseBits(pul[72]));
    }
    [TestMethod]
    public void ReverseBits_UInt64_Speed()
    {
      var value = 670530UL;
      var expected = 4817813662309810176UL;
      Flux.Services.Performance.Measure(() => Flux.BitOps.ReverseBits(value), 1000000).Assert(expected, 0.3);
    }
  }
}
