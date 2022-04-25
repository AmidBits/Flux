using System;
using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundation.BitOps
{
  [TestClass]
  public class Pow2LessThan
  {
    readonly System.Numerics.BigInteger[] pbi = System.Linq.Enumerable.Range(0, 255).Select(i => (System.Numerics.BigInteger)i).ToArray();
    readonly int[] pi = System.Linq.Enumerable.Range(0, 255).Select(i => (int)i).ToArray();
    readonly long[] pl = System.Linq.Enumerable.Range(0, 255).Select(i => (long)i).ToArray();
    readonly uint[] pui = System.Linq.Enumerable.Range(0, 255).Select(i => (uint)i).ToArray();
    readonly ulong[] pul = System.Linq.Enumerable.Range(0, 255).Select(i => (ulong)i).ToArray();

    [TestMethod]
    public void Pow2LessThan_BigInteger()
    {
      Assert.AreEqual(pbi[4], Flux.BitOps.Pow2LessThan(pbi[5]));
      Assert.AreEqual(pbi[16], Flux.BitOps.Pow2LessThan(pbi[17]));
      Assert.AreEqual(pbi[16], Flux.BitOps.Pow2LessThan(pbi[32]));
    }

    [TestMethod]
    public void Pow2LessThan_Int32()
    {
      Assert.AreEqual(pi[4], Flux.BitOps.Pow2LessThan(pi[5]));
      Assert.AreEqual(pi[16], Flux.BitOps.Pow2LessThan(pi[17]));
      Assert.AreEqual(pi[16], Flux.BitOps.Pow2LessThan(pi[32]));
    }

    [TestMethod]
    public void Pow2LessThan_Int64()
    {
      Assert.AreEqual(pl[4], Flux.BitOps.Pow2LessThan(pl[5]));
      Assert.AreEqual(pl[16], Flux.BitOps.Pow2LessThan(pl[17]));
      Assert.AreEqual(pl[16], Flux.BitOps.Pow2LessThan(pl[32]));
    }

    [TestMethod]
    public void Pow2LessThan_UInt32()
    {
      Assert.AreEqual(pui[4], Flux.BitOps.Pow2LessThan(pui[5]));
      Assert.AreEqual(pui[16], Flux.BitOps.Pow2LessThan(pui[17]));
      Assert.AreEqual(pui[16], Flux.BitOps.Pow2LessThan(pui[32]));
    }
    [TestMethod]
    public void Pow2LessThan_UInt64()
    {
      Assert.AreEqual(pul[4], Flux.BitOps.Pow2LessThan(pul[5]));
      Assert.AreEqual(pul[16], Flux.BitOps.Pow2LessThan(pul[17]));
      Assert.AreEqual(pul[16], Flux.BitOps.Pow2LessThan(pul[32]));
    }
  }
}
