using System;
using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundation.BitOps
{
  [TestClass]
  public class Log2
  {
    readonly System.Numerics.BigInteger[] pbi = System.Linq.Enumerable.Range(0, 255).Select(i => (System.Numerics.BigInteger)i).ToArray();
    readonly int[] pi = System.Linq.Enumerable.Range(0, 255).Select(i => (int)i).ToArray();
    readonly long[] pl = System.Linq.Enumerable.Range(0, 255).Select(i => (long)i).ToArray();
    readonly uint[] pui = System.Linq.Enumerable.Range(0, 255).Select(i => (uint)i).ToArray();
    readonly ulong[] pul = System.Linq.Enumerable.Range(0, 255).Select(i => (ulong)i).ToArray();

    [TestMethod]
    public void Log2_BigInteger()
    {
      Assert.AreEqual(pi[4], Flux.BitOps.Log2(pbi[18]));
    }
    [TestMethod]
    public void Log2_BigInteger_Speed()
    {
      var value = System.Numerics.BigInteger.Parse("670530");
      var expected = 19; // Log2() returns an int.
      Flux.Services.Performance.Measure(() => Flux.BitOps.Log2(value), 1000000).Assert(expected, 1);
    }
    [TestMethod]
    public void Log2_UInt32()
    {
      Assert.AreEqual(pi[4], Flux.BitOps.Log2(pbi[18]));
    }
    [TestMethod]
    public void Log2_UInt32_Speed()
    {
      var value = 670530U;
      var expected = 19; // Log2() returns an int.
      Flux.Services.Performance.Measure(() => Flux.BitOps.Log2(value), 1000000).Assert(expected, 0.3);
    }
    [TestMethod]
    public void Log2_UInt64()
    {
      Assert.AreEqual(pi[4], Flux.BitOps.Log2(pbi[18]));
    }
    [TestMethod]
    public void Log2_UInt64_Speed()
    {
      var value = 670530UL;
      var expected = 19; // Log2() returns an int.
      Flux.Services.Performance.Measure(() => Flux.BitOps.Log2(value), 1000000).Assert(expected, 0.3);
    }
  }
}
