#if NET7_0_OR_GREATER
using System;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericMath
{
  [TestClass]
  public class Core
  {
    [TestMethod]
    public void DetentInterval()
    {
      Assert.AreEqual(520, 515.DetentInterval(20, 5, RoundingMode.HalfAwayFromZero));
    }

    [TestMethod]
    public void DetentPosition()
    {
      Assert.AreEqual(520, 515.DetentPosition(520, 5));
    }

    [TestMethod]
    public void DetentZero()
    {
      Assert.AreEqual(0, 4.DetentZero(5));
    }

    [TestMethod]
    public void Envelop()
    {
      Assert.AreEqual(-1, (-0.5).Envelop());
      Assert.AreEqual(1, (0.5).Envelop());

      Assert.AreEqual(-13, (-12.5).Envelop());
      Assert.AreEqual(13, (12.5).Envelop());
    }

    [TestMethod]
    public void Factorial()
    {
      Assert.AreEqual(362880, 9.Factorial());

      Assert.AreEqual(System.Numerics.BigInteger.Parse("36471110918188685288249859096605464427167635314049524593701628500267962436943872000000000000000"), Flux.GenericMath.Factorial(67.ToBigInteger()));
      Assert.AreEqual(479001600, Flux.GenericMath.Factorial(12.ToBigInteger()));
      Assert.AreEqual(-479001600, Flux.GenericMath.Factorial(-12.ToBigInteger()));
    }

    [TestMethod]
    public void ParallelSplitFactorial()
    {
      Assert.AreEqual(479001600, Flux.ParallelSplitFactorial.ComputeFactorial(12));
      Assert.AreEqual(479001600, Flux.ParallelSplitFactorial.ComputeFactorial(12.ToBigInteger()));
    }

    [TestMethod]
    public void GreatestCommonDivisor()
    {
      Assert.AreEqual(3, 21.GreatestCommonDivisor(6));
    }

    [TestMethod]
    public void IntegerSqrt()
    {
      Assert.AreEqual(4, 21.IntegerSqrt());
    }

    [TestMethod]
    public void IsCoprime()
    {
      Assert.AreEqual(true, 23.IsCoprime(43));
    }

    [TestMethod]
    public void IntegerPow()
    {
      Assert.AreEqual(7766279631452241920, 100L.IntegerPow(10));
    }

    [TestMethod]
    public void IntegerPowRec()
    {
      Assert.AreEqual(7766279631452241920, 100L.IntegerPowRec(10, out double reciprocal));
      Assert.AreEqual(1.2876178137472069E-19, reciprocal);
    }

    [TestMethod]
    public void IsPow()
    {
      Assert.AreEqual(true, 100.IsPow(10));
      Assert.AreEqual(true, 100.IsPow(10));
    }

    [TestMethod]
    public void IsIntegerSqrt()
    {
      var v = 15;

      var iq = v.IntegerSqrt();

      var isiq = v.IsIntegerSqrt(iq);

      Assert.IsTrue(isiq);
    }

    [TestMethod]
    public void IsPerfectIntegerSqrt()
    {
      var v = 15;

      var iq = v.IntegerSqrt();

      var ispiq = v.IsPerfectIntegerSqrt(iq);

      Assert.IsFalse(ispiq);
    }

    [TestMethod]
    public void LeastCommonMultiple()
    {
      Assert.AreEqual(42, 21.LeastCommonMultiple(6));
    }

    [TestMethod]
    public void ModInv()
    {
      Assert.AreEqual(2, 4.ModInv(7));
      Assert.AreEqual(7, 8.ModInv(11));
    }

    [TestMethod]
    public void NearestMultiple()
    {
      var nearestMultiple = 512.NearestMultiple(20, false, RoundingMode.HalfTowardZero, out var nearestTowardsZero, out var nearestAwayFromZero);

      Assert.AreEqual(520, nearestMultiple);

      Assert.AreEqual(500, nearestTowardsZero);
      Assert.AreEqual(520, nearestAwayFromZero);
    }
  }
}
#endif
