using System;
using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundation.BitOps
{
  [TestClass]
  public class Pow2GreaterThan
  {
    readonly System.Numerics.BigInteger[] pbi = System.Linq.Enumerable.Range(0, 255).Select(i => (System.Numerics.BigInteger)i).ToArray();
    readonly int[] pi = System.Linq.Enumerable.Range(0, 255).Select(i => (int)i).ToArray();
    readonly long[] pl = System.Linq.Enumerable.Range(0, 255).Select(i => (long)i).ToArray();
    readonly uint[] pui = System.Linq.Enumerable.Range(0, 255).Select(i => (uint)i).ToArray();
    readonly ulong[] pul = System.Linq.Enumerable.Range(0, 255).Select(i => (ulong)i).ToArray();

    [TestMethod]
    public void Pow2GreaterThan_BigInteger()
    {
      Assert.AreEqual(pbi[8], Flux.BitOps.SmallestPowerOf2GreaterThan(pbi[5]));
      Assert.AreEqual(pbi[32], Flux.BitOps.SmallestPowerOf2GreaterThan(pbi[17]));
      Assert.AreEqual(pbi[64], Flux.BitOps.SmallestPowerOf2GreaterThan(pbi[32]));
    }

    [TestMethod]
    public void Pow2GreaterThan_Int32()
    {
      Assert.AreEqual(pi[8], Flux.BitOps.SmallestPowerOf2GreaterThan(pi[5]));
      Assert.AreEqual(pi[32], Flux.BitOps.SmallestPowerOf2GreaterThan(pi[17]));
      Assert.AreEqual(pi[64], Flux.BitOps.SmallestPowerOf2GreaterThan(pi[32]));
    }

    [TestMethod]
    public void Pow2GreaterThan_Int64()
    {
      Assert.AreEqual(pl[8], Flux.BitOps.SmallestPowerOf2GreaterThan(pl[5]));
      Assert.AreEqual(pl[32], Flux.BitOps.SmallestPowerOf2GreaterThan(pl[17]));
      Assert.AreEqual(pl[64], Flux.BitOps.SmallestPowerOf2GreaterThan(pl[32]));
    }

    [TestMethod]
    public void Pow2GreaterThan_UInt32()
    {
      Assert.AreEqual(pui[8], Flux.BitOps.SmallestPowerOf2GreaterThan(pui[5]));
      Assert.AreEqual(pui[32], Flux.BitOps.SmallestPowerOf2GreaterThan(pui[17]));
      Assert.AreEqual(pui[64], Flux.BitOps.SmallestPowerOf2GreaterThan(pui[32]));
    }
    [TestMethod]
    public void Pow2GreaterThan_UInt64()
    {
      Assert.AreEqual(pul[8], Flux.BitOps.SmallestPowerOf2GreaterThan(pul[5]));
      Assert.AreEqual(pul[32], Flux.BitOps.SmallestPowerOf2GreaterThan(pul[17]));
      Assert.AreEqual(pul[64], Flux.BitOps.SmallestPowerOf2GreaterThan(pul[32]));
    }
  }
}
