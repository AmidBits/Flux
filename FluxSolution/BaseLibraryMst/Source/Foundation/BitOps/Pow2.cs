//using System;
//using System.Linq;
//using Flux;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace Foundation.BitOps
//{
//  [TestClass]
//  public class Pow2
//  {
//    readonly System.Numerics.BigInteger[] pbi = System.Linq.Enumerable.Range(0, 255).Select(i => (System.Numerics.BigInteger)i).ToArray();
//    readonly int[] pi = System.Linq.Enumerable.Range(0, 255).Select(i => (int)i).ToArray();
//    readonly long[] pl = System.Linq.Enumerable.Range(0, 255).Select(i => (long)i).ToArray();
//    readonly uint[] pui = System.Linq.Enumerable.Range(0, 255).Select(i => (uint)i).ToArray();
//    readonly ulong[] pul = System.Linq.Enumerable.Range(0, 255).Select(i => (ulong)i).ToArray();

//    [TestMethod]
//    public void Pow2_BigInteger()
//    {
//      Assert.AreEqual(pi[16], Flux.Maths.Pow(pi[4], 2));
//    }
//    [TestMethod]
//    public void Pow2_Int32()
//    {
//      Assert.AreEqual(pi[16], Flux.Maths.Pow(pi[4], 2));
//    }
//    [TestMethod]
//    public void Pow2_Int64()
//    {
//      Assert.AreEqual(pl[16], Flux.Maths.Pow(pl[4], 2));
//    }
//    [TestMethod]
//    public void Pow2_UInt32()
//    {
//      Assert.AreEqual(pui[16], Flux.Maths.Pow(pui[4], 2));
//    }
//    [TestMethod]
//    public void Pow2_UInt64()
//    {
//      Assert.AreEqual(pul[16], Flux.Maths.Pow(pul[4], 2));
//    }
//  }
//}
