using System;
using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundation.BitOps
{
  [TestClass]
  public class RotateRight
  {
    readonly System.Numerics.BigInteger[] pbi = System.Linq.Enumerable.Range(0, 255).Select(i => (System.Numerics.BigInteger)i).ToArray();
    readonly int[] pi = System.Linq.Enumerable.Range(0, 255).Select(i => (int)i).ToArray();
    readonly long[] pl = System.Linq.Enumerable.Range(0, 255).Select(i => (long)i).ToArray();
    readonly uint[] pui = System.Linq.Enumerable.Range(0, 255).Select(i => (uint)i).ToArray();
    readonly ulong[] pul = System.Linq.Enumerable.Range(0, 255).Select(i => (ulong)i).ToArray();

    [TestMethod]
    public void RotateRight_BigInteger()
    {
      Assert.AreEqual(483.ToBigInteger(), Flux.BitOps.RotateRight(pbi[120], 5));
      Assert.AreEqual(1028.ToBigInteger(), Flux.BitOps.RotateRight(pbi[128], 5));
      Assert.AreEqual(6.ToBigInteger(), Flux.BitOps.RotateRight(pbi[12], 5));
      Assert.AreEqual(495.ToBigInteger(), Flux.BitOps.RotateRight(pbi[123], 5));
    }
    [TestMethod]
    public void RotateRight_UInt32()
    {
      Assert.AreEqual(3221225475U, Flux.BitOps.RotateRight(pui[120], 5));
      Assert.AreEqual(4U, Flux.BitOps.RotateRight(pui[128], 5));
      Assert.AreEqual(786432U, Flux.BitOps.RotateRight(pui[12], 16));
      Assert.AreEqual(3623878659U, Flux.BitOps.RotateRight(pui[123], 5));
    }
    [TestMethod]
    public void RotateRight_UInt64()
    {
      Assert.AreEqual(13835058055282163715UL, Flux.BitOps.RotateRight(pul[120], 5));
      Assert.AreEqual(4UL, Flux.BitOps.RotateRight(pul[128], 5));
      Assert.AreEqual(3377699720527872UL, Flux.BitOps.RotateRight(pul[12], 16));
      Assert.AreEqual(15564440312192434179UL, Flux.BitOps.RotateRight(pul[123], 5));
    }
  }
}
