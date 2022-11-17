//using System;
//using System.Linq;
//using Flux;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace Foundation.BitOps
//{
//  [TestClass]
//  public class Pow2GreaterThan
//  {
//    readonly System.Numerics.BigInteger[] pbi = System.Linq.Enumerable.Range(0, 255).Select(i => (System.Numerics.BigInteger)i).ToArray();
//    readonly int[] pi = System.Linq.Enumerable.Range(0, 255).Select(i => (int)i).ToArray();
//    readonly long[] pl = System.Linq.Enumerable.Range(0, 255).Select(i => (long)i).ToArray();
//    readonly uint[] pui = System.Linq.Enumerable.Range(0, 255).Select(i => (uint)i).ToArray();
//    readonly ulong[] pul = System.Linq.Enumerable.Range(0, 255).Select(i => (ulong)i).ToArray();

//    [TestMethod]
//    public void Pow2GreaterThan_BigInteger()
//    {
//      Assert.AreEqual(pbi[8], Flux.BitOps.RoundUpToPowerOf2(pbi[5], true));
//      Assert.AreEqual(pbi[32], Flux.BitOps.RoundUpToPowerOf2(pbi[17], true));
//      Assert.AreEqual(pbi[64], Flux.BitOps.RoundUpToPowerOf2(pbi[32], true));
//    }

//    [TestMethod]
//    public void Pow2GreaterThan_Int32()
//    {
//      Assert.AreEqual(pi[8], Flux.BitOps.RoundUpToPowerOf2(pi[5], true));
//      Assert.AreEqual(pi[32], Flux.BitOps.RoundUpToPowerOf2(pi[17], true));
//      Assert.AreEqual(pi[64], Flux.BitOps.RoundUpToPowerOf2(pi[32], true));
//    }

//    [TestMethod]
//    public void Pow2GreaterThan_Int64()
//    {
//      Assert.AreEqual(pl[8], Flux.BitOps.RoundUpToPowerOf2(pl[5], true));
//      Assert.AreEqual(pl[32], Flux.BitOps.RoundUpToPowerOf2(pl[17], true));
//      Assert.AreEqual(pl[64], Flux.BitOps.RoundUpToPowerOf2(pl[32], true));
//    }

//    [TestMethod]
//    public void Pow2GreaterThan_UInt32()
//    {
//      Assert.AreEqual(pui[8], Flux.BitOps.RoundUpToPowerOf2(pui[5], true));
//      Assert.AreEqual(pui[32], Flux.BitOps.RoundUpToPowerOf2(pui[17], true));
//      Assert.AreEqual(pui[64], Flux.BitOps.RoundUpToPowerOf2(pui[32], true));
//    }
//    [TestMethod]
//    public void Pow2GreaterThan_UInt64()
//    {
//      Assert.AreEqual(pul[8], Flux.BitOps.RoundUpToPowerOf2(pul[5], true));
//      Assert.AreEqual(pul[32], Flux.BitOps.RoundUpToPowerOf2(pul[17], true));
//      Assert.AreEqual(pul[64], Flux.BitOps.RoundUpToPowerOf2(pul[32], true));
//    }
//  }
//}
