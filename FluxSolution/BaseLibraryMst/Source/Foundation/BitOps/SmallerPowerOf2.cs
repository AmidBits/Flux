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
      Assert.AreEqual(pbi[4], Flux.BitOps.SmallerPowerOf2(pbi[5], true));
      Assert.AreEqual(pbi[16], Flux.BitOps.SmallerPowerOf2(pbi[17], true));
      Assert.AreEqual(pbi[16], Flux.BitOps.SmallerPowerOf2(pbi[32], true));
    }

    [TestMethod]
    public void Pow2LessThan_Int32()
    {
      Assert.AreEqual(pi[4], Flux.BitOps.SmallerPowerOf2(pi[5], true));
      Assert.AreEqual(pi[16], Flux.BitOps.SmallerPowerOf2(pi[17], true));
      Assert.AreEqual(pi[16], Flux.BitOps.SmallerPowerOf2(pi[32], true));
    }

    [TestMethod]
    public void Pow2LessThan_Int64()
    {
      Assert.AreEqual(pl[4], Flux.BitOps.SmallerPowerOf2(pl[5], true));
      Assert.AreEqual(pl[16], Flux.BitOps.SmallerPowerOf2(pl[17], true));
      Assert.AreEqual(pl[16], Flux.BitOps.SmallerPowerOf2(pl[32], true));
    }

    [TestMethod]
    public void Pow2LessThan_UInt32()
    {
      Assert.AreEqual(pui[4], Flux.BitOps.SmallerPowerOf2(pui[5], true));
      Assert.AreEqual(pui[16], Flux.BitOps.SmallerPowerOf2(pui[17], true));
      Assert.AreEqual(pui[16], Flux.BitOps.SmallerPowerOf2(pui[32], true));
    }
    [TestMethod]
    public void Pow2LessThan_UInt64()
    {
      Assert.AreEqual(pul[4], Flux.BitOps.SmallerPowerOf2(pul[5], true));
      Assert.AreEqual(pul[16], Flux.BitOps.SmallerPowerOf2(pul[17], true));
      Assert.AreEqual(pul[16], Flux.BitOps.SmallerPowerOf2(pul[32], true));
    }
  }
}
