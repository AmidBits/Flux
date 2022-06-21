using System;
using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Maths
{
  [TestClass]
  public class General
  {
    readonly System.Numerics.BigInteger[] nbi = System.Linq.Enumerable.Range(0, 255).Select(i => -(System.Numerics.BigInteger)i).ToArray();
    readonly short[] ns = System.Linq.Enumerable.Range(0, 255).Select(i => (short)-i).ToArray();
    readonly int[] ni = System.Linq.Enumerable.Range(0, 255).Select(i => (int)-i).ToArray();
    readonly long[] nl = System.Linq.Enumerable.Range(0, 255).Select(i => -(long)i).ToArray();
    readonly sbyte[] nsb = System.Linq.Enumerable.Range(0, 127).Select(i => (sbyte)-i).ToArray(); // Restricted to -127.

    readonly System.Numerics.BigInteger[] pbi = System.Linq.Enumerable.Range(0, 255).Select(i => (System.Numerics.BigInteger)i).ToArray();
    readonly byte[] pb = System.Linq.Enumerable.Range(0, 255).Select(i => (byte)i).ToArray();
    readonly short[] ps = System.Linq.Enumerable.Range(0, 255).Select(i => (short)i).ToArray();
    readonly int[] pi = System.Linq.Enumerable.Range(0, 255).Select(i => (int)i).ToArray();
    readonly long[] pl = System.Linq.Enumerable.Range(0, 255).Select(i => (long)i).ToArray();
    readonly sbyte[] psb = System.Linq.Enumerable.Range(0, 127).Select(i => (sbyte)i).ToArray(); // Restricted to 127.
    readonly ushort[] pus = System.Linq.Enumerable.Range(0, 255).Select(i => (ushort)i).ToArray();
    readonly uint[] pui = System.Linq.Enumerable.Range(0, 255).Select(i => (uint)i).ToArray();
    readonly ulong[] pul = System.Linq.Enumerable.Range(0, 255).Select(i => (ulong)i).ToArray();

    [TestMethod]
    public void ParallelSplitFactorial()
    {
      Assert.AreEqual(479001600, Flux.ParallelSplitFactorial.Default.ComputeFactorial(12));
      Assert.AreEqual(479001600, Flux.ParallelSplitFactorial.Default.ComputeFactorial(pbi[12]));
    }

    [TestMethod]
    public void Factorial()
    {
      Assert.AreEqual(System.Numerics.BigInteger.Parse("36471110918188685288249859096605464427167635314049524593701628500267962436943872000000000000000"), Flux.Maths.Factorial(67.ToBigInteger()));
      Assert.AreEqual(479001600, Flux.Maths.Factorial(pbi[12]));
      Assert.AreEqual(-479001600, Flux.Maths.Factorial(-pbi[12]));
    }

    [TestMethod]
    public void GetCountOfDivisors()
    {
      Assert.AreEqual(64, Flux.Numerics.Factors.GetCountOfDivisors(670530));
    }

    [TestMethod]
    public void GetCountOfProperDivisors()
    {
      Assert.AreEqual(63, Flux.Numerics.Factors.GetCountOfProperDivisors(670530));
    }

    [TestMethod]
    public void GetFactors()
    {
      var expected = new System.Numerics.BigInteger[] { 1, 60, 2, 30, 3, 20, 4, 15, 5, 12, 6, 10 };
      var actual = Flux.Numerics.Factors.GetDivisors(60).ToArray();
      Assert.IsTrue(actual.SequenceEqual(expected));
    }

    [TestMethod]
    public void GetSumOfDivisors()
    {
      Assert.AreEqual(1916928, Flux.Numerics.Factors.GetSumOfDivisors(670530));
    }

    [TestMethod]
    public void GetSumOfProperDivisors()
    {
      Assert.AreEqual(1246398, Flux.Numerics.Factors.GetSumOfProperDivisors(670530));
    }

    [TestMethod]
    public void GreatestCommonDivisor()
    {
      Assert.AreEqual(3, Flux.Maths.GreatestCommonDivisor(21, 6), nameof(Flux.Maths.GreatestCommonDivisor));
    }

    [TestMethod]
    public void RoundToInterval()
    {
      Assert.AreEqual(1.8, Flux.Maths.RoundToMultiple(1.75, 0.45, HalfRounding.AwayFromZero), $"{nameof(Flux.Maths.RoundToMultiple)} {HalfRounding.AwayFromZero}");
    }

    [TestMethod]
    public void IsCoprime()
    {
      Assert.IsFalse(Flux.Maths.IsCoprime(4, 6), nameof(Flux.Maths.IsCoprime));
      Assert.IsTrue(Flux.Maths.IsCoprime(4, 9), nameof(Flux.Maths.IsCoprime));
    }

    [TestMethod]
    public void LeastCommonMultiple()
    {
      Assert.AreEqual(42, Flux.Maths.LeastCommonMultiple(21, 6), nameof(Flux.Maths.LeastCommonMultiple));
    }

    [TestMethod]
    public void Max()
    {
      Assert.AreEqual(pbi[32], Flux.Maths.Max(pbi[23], pbi[32]));
      Assert.AreEqual(pb[32], Flux.Maths.Max(pb[23], pb[32]));
      Assert.AreEqual(ps[32], Flux.Maths.Max(ps[23], ps[32]));
      Assert.AreEqual(pi[32], Flux.Maths.Max(pi[23], ps[32]));
      Assert.AreEqual(pl[32], Flux.Maths.Max(pl[23], ps[32]));
      Assert.AreEqual(psb[32], Flux.Maths.Max(psb[23], psb[32]));
      Assert.AreEqual(pus[32], Flux.Maths.Max(pus[23], pus[32]));
      Assert.AreEqual(pui[32], Flux.Maths.Max(pui[23], pui[32]));
      Assert.AreEqual(pul[32], Flux.Maths.Max(pul[23], pul[32]));
    }

    [TestMethod]
    public void Min()
    {
      Assert.AreEqual(pbi[23], Flux.Maths.Min(pbi[23], pbi[32]));
      Assert.AreEqual(pb[23], Flux.Maths.Min(pb[23], pb[32]));
      Assert.AreEqual(ps[23], Flux.Maths.Min(ps[23], ps[32]));
      Assert.AreEqual(pi[23], Flux.Maths.Min(pi[23], ps[32]));
      Assert.AreEqual(pl[23], Flux.Maths.Min(pl[23], ps[32]));
      Assert.AreEqual(psb[23], Flux.Maths.Min(psb[23], psb[32]));
      Assert.AreEqual(pus[23], Flux.Maths.Min(pus[23], pus[32]));
      Assert.AreEqual(pui[23], Flux.Maths.Min(pui[23], pui[32]));
      Assert.AreEqual(pul[23], Flux.Maths.Min(pul[23], pul[32]));
    }

    [TestMethod]
    public void PowerOf()
    {
      Assert.AreEqual(64, Flux.Maths.PowerOf(101, 2));
      Assert.AreEqual(64, Flux.Maths.PowerOf(101, 8));
      Assert.AreEqual(100, Flux.Maths.PowerOf(101, 10));
      Assert.AreEqual(16, Flux.Maths.PowerOf(101, 16));
    }

    [TestMethod]
    public void SincN()
    {
      Assert.AreEqual(0.636619772367581, Flux.Angle.Sincn(0.5), Flux.Maths.EpsilonCpp32);
    }

    [TestMethod]
    public void SincU()
    {
      Assert.AreEqual(0.958851077208406, Flux.Angle.Sincu(0.5), Flux.Maths.EpsilonCpp32);
    }

    [TestMethod]
    public void UnitImpulse()
    {
      Assert.AreEqual(1.0, Flux.Maths.UnitImpulse(0.0));
      Assert.AreEqual(0.0, Flux.Maths.UnitImpulse(1.0));
    }

    [TestMethod]
    public void Heaviside()
    {
      Assert.AreEqual(0.0, Flux.Maths.Heaviside(-1.0));
      Assert.AreEqual(0.5, Flux.Maths.Heaviside(0.0));
      Assert.AreEqual(1.0, Flux.Maths.Heaviside(1.0));

      Assert.AreEqual(0.0, Flux.Maths.Heaviside(-1));
      Assert.AreEqual(0.5, Flux.Maths.Heaviside(0));
      Assert.AreEqual(1.0, Flux.Maths.Heaviside(1));
    }
  }
}
