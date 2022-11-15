#if NET7_0_OR_GREATER
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using Flux;

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
      Assert.AreEqual(true, 100.IsIntegerPow(10));
      Assert.AreEqual(true, 100.IsIntegerPow(10));
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
