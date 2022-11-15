//using System;
//using System.Linq;
//using Flux;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace Maths
//{
//  [TestClass]
//  public class Interval
//  {
//    readonly System.Numerics.BigInteger[] nbi = System.Linq.Enumerable.Range(0, 255).Select(i => -(System.Numerics.BigInteger)i).ToArray();
//    readonly short[] ns = System.Linq.Enumerable.Range(0, 255).Select(i => (short)-i).ToArray();
//    readonly int[] ni = System.Linq.Enumerable.Range(0, 255).Select(i => (int)-i).ToArray();
//    readonly long[] nl = System.Linq.Enumerable.Range(0, 255).Select(i => -(long)i).ToArray();
//    readonly sbyte[] nsb = System.Linq.Enumerable.Range(0, 127).Select(i => (sbyte)-i).ToArray(); // Restricted to -127.

//    readonly System.Numerics.BigInteger[] pbi = System.Linq.Enumerable.Range(0, 255).Select(i => (System.Numerics.BigInteger)i).ToArray();
//    readonly byte[] pb = System.Linq.Enumerable.Range(0, 255).Select(i => (byte)i).ToArray();
//    readonly short[] ps = System.Linq.Enumerable.Range(0, 255).Select(i => (short)i).ToArray();
//    readonly int[] pi = System.Linq.Enumerable.Range(0, 255).Select(i => (int)i).ToArray();
//    readonly long[] pl = System.Linq.Enumerable.Range(0, 255).Select(i => (long)i).ToArray();
//    readonly sbyte[] psb = System.Linq.Enumerable.Range(0, 127).Select(i => (sbyte)i).ToArray(); // Restricted to 127.
//    readonly ushort[] pus = System.Linq.Enumerable.Range(0, 255).Select(i => (ushort)i).ToArray();
//    readonly uint[] pui = System.Linq.Enumerable.Range(0, 255).Select(i => (uint)i).ToArray();
//    readonly ulong[] pul = System.Linq.Enumerable.Range(0, 255).Select(i => (ulong)i).ToArray();

//    [TestMethod]
//    public void Clamp()
//    {
//      Assert.AreEqual(pbi[9], Flux.Maths.Clamp(pbi[19], pbi[3], pbi[9]));
//      Assert.AreEqual(pui[9], Flux.Maths.Clamp(pui[19], pui[3], pui[9]));
//      Assert.AreEqual(pul[9], Flux.Maths.Clamp(pul[19], pul[3], pul[9]));
//    }
//    [TestMethod]
//    public void Distance()
//    {
//      Assert.AreEqual(10.ToBigInteger(), Flux.Maths.Distance(pbi[10], pbi[20]), nameof(Flux.Maths.Rescale) + ".BigInteger");
//      Assert.AreEqual(10, Flux.Maths.Distance(pi[10], pi[20]), nameof(Flux.Maths.Rescale) + ".Int32");
//      Assert.AreEqual(10L, Flux.Maths.Distance(pl[10], pl[20]), nameof(Flux.Maths.Rescale) + ".Int64");
//    }
//    [TestMethod]
//    public void Fold()
//    {
//      Assert.AreEqual(13, Flux.Maths.Fold(pbi[53], pbi[10], pbi[20]));
//      Assert.AreEqual(31, Flux.Maths.Fold(pbi[5], pbi[30], pbi[36]));

//      Assert.AreEqual(13, Flux.Maths.Fold(pi[53], pi[10], pi[20]));
//      Assert.AreEqual(31, Flux.Maths.Fold(pi[5], pi[30], pi[36]));

//      Assert.AreEqual(pl[13], Flux.Maths.Fold(pl[53], pl[10], pl[20]));
//      Assert.AreEqual(pl[31], Flux.Maths.Fold(pl[5], pl[30], pl[36]));

//      Assert.AreEqual(1.3, Flux.Maths.Fold(5.3, 1.0, 2.0), Flux.Maths.EpsilonCpp32);
//      Assert.AreEqual(3.1, Flux.Maths.Fold(0.5, 3.0, 3.6), Flux.Maths.EpsilonCpp32);
//    }
//    [TestMethod]
//    public void Rescale()
//    {
//      Assert.AreEqual(100.ToBigInteger(), Flux.Maths.Rescale(pbi[5], pbi[0], pbi[10], pbi[50], pbi[150]), nameof(Flux.Maths.Rescale) + ".BigInteger");
//      Assert.AreEqual(100, Flux.Maths.Rescale(pi[5], pi[0], pi[10], pi[50], pi[150]), nameof(Flux.Maths.Rescale) + ".Int32");
//      Assert.AreEqual(100L, Flux.Maths.Rescale(pl[5], pl[0], pl[10], pl[50], pl[150]), nameof(Flux.Maths.Rescale) + ".Int64");
//    }
//    [TestMethod]
//    public void Wrap()
//    {
//      Assert.AreEqual(pbi[7], Flux.Maths.Wrap(pbi[19], pbi[3], pbi[9]));
//      Assert.AreEqual(pb[7], Flux.Maths.Wrap(pb[19], pb[3], pb[9]));
//      Assert.AreEqual(pi[7], Flux.Maths.Wrap(pi[19], pi[3], pi[9]));
//      Assert.AreEqual(pl[7], Flux.Maths.Wrap(pl[19], pl[3], pl[9]));
//      Assert.AreEqual(psb[7], Flux.Maths.Wrap(psb[19], psb[3], psb[9]));
//      Assert.AreEqual(ps[7], Flux.Maths.Wrap(ps[19], ps[3], ps[9]));
//      Assert.AreEqual(pui[7], Flux.Maths.Wrap(pui[19], pui[3], pui[9]));
//      Assert.AreEqual(pul[7], Flux.Maths.Wrap(pul[19], pul[3], pul[9]));
//      Assert.AreEqual(pus[7], Flux.Maths.Wrap(pus[19], pus[3], pus[9]));
//    }
//  }
//}
