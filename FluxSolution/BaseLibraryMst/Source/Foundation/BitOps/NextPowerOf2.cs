using System;
using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundation.BitOps
{
  [TestClass]
  public class NextPowerOf2
  {
    readonly System.Numerics.BigInteger[] pbi = System.Linq.Enumerable.Range(0, 255).Select(i => (System.Numerics.BigInteger)i).ToArray();
    readonly int[] pi = System.Linq.Enumerable.Range(0, 255).Select(i => (int)i).ToArray();
    readonly long[] pl = System.Linq.Enumerable.Range(0, 255).Select(i => (long)i).ToArray();
    readonly uint[] pui = System.Linq.Enumerable.Range(0, 255).Select(i => (uint)i).ToArray();
    readonly ulong[] pul = System.Linq.Enumerable.Range(0, 255).Select(i => (ulong)i).ToArray();

    [TestMethod]
    public void NextPowerOf2_UInt32()
    {
      Assert.AreEqual(pui[8], Flux.BitOps.NextHighestPowerOf2(pui[5]));
      Assert.AreEqual(pui[32], Flux.BitOps.NextHighestPowerOf2(pui[17]));
      Assert.AreEqual(pui[64], Flux.BitOps.NextHighestPowerOf2(pui[32]));
    }
    [TestMethod]
    public void NextPowerOf2_UInt64()
    {
      Assert.AreEqual(pul[8], Flux.BitOps.NextHighestPowerOf2(pul[5]));
      Assert.AreEqual(pul[32], Flux.BitOps.NextHighestPowerOf2(pul[17]));
      Assert.AreEqual(pul[64], Flux.BitOps.NextHighestPowerOf2(pul[32]));
    }
  }
}
