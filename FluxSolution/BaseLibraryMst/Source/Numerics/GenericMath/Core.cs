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
    public void IsIntegerPow()
    {
      Assert.AreEqual(true, 100.IsPow(10));
      Assert.AreEqual(true, 100.IsPow(10));
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
