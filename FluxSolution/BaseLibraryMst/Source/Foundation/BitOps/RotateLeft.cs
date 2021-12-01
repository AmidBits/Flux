using System;
using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundation.BitOps
{
  [TestClass]
  public class RotateLeft
  {
    readonly System.Numerics.BigInteger[] pbi = System.Linq.Enumerable.Range(0, 255).Select(i => (System.Numerics.BigInteger)i).ToArray();
    readonly int[] pi = System.Linq.Enumerable.Range(0, 255).Select(i => (int)i).ToArray();
    readonly long[] pl = System.Linq.Enumerable.Range(0, 255).Select(i => (long)i).ToArray();
    readonly uint[] pui = System.Linq.Enumerable.Range(0, 255).Select(i => (uint)i).ToArray();
    readonly ulong[] pul = System.Linq.Enumerable.Range(0, 255).Select(i => (ulong)i).ToArray();

    [TestMethod]
    public void RotateLeft_BigInteger()
    {
      Assert.AreEqual(254.ToBigInteger(), Flux.BitOps.RotateLeft(pbi[120], 5));
      Assert.AreEqual(272.ToBigInteger(), Flux.BitOps.RotateLeft(pbi[128], 5));
      Assert.AreEqual(24.ToBigInteger(), Flux.BitOps.RotateLeft(pbi[12], 5));
      Assert.AreEqual(254.ToBigInteger(), Flux.BitOps.RotateLeft(pbi[123], 5));
    }
    [TestMethod]
    public void RotateLeft_UInt32()
    {
      Assert.AreEqual(3840U, Flux.BitOps.RotateLeft(pui[120], 5));
      Assert.AreEqual(4096U, Flux.BitOps.RotateLeft(pui[128], 5));
      Assert.AreEqual(786432U, Flux.BitOps.RotateLeft(pui[12], 16));
      Assert.AreEqual(3936U, Flux.BitOps.RotateLeft(pui[123], 5));
    }
    [TestMethod]
    public void RotateLeft_UInt64()
    {
      Assert.AreEqual(3840UL, Flux.BitOps.RotateLeft(pul[120], 5));
      Assert.AreEqual(4096UL, Flux.BitOps.RotateLeft(pul[128], 5));
      Assert.AreEqual(786432UL, Flux.BitOps.RotateLeft(pul[12], 16));
      Assert.AreEqual(3936UL, Flux.BitOps.RotateLeft(pul[123], 5));
    }
  }
}
