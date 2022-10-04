using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;
using System;

namespace GenericMath
{
  [TestClass]
  public class Core
  {
    [TestMethod]
    public void DetentInterval()
    {
      Assert.AreEqual(520, (515).ToBigInteger().DetentInterval(20, 5, HalfwayRounding.AwayFromZero));
    }

    [TestMethod]
    public void DetentPosition()
    {
      Assert.AreEqual(520, 515.ToBigInteger().DetentPosition(520, 5));
    }

    [TestMethod]
    public void DetentZero()
    {
      Assert.AreEqual(0, 4.ToBigInteger().DetentZero(5));
    }

    [TestMethod]
    public void DivRem()
    {
      var quotient = (9.0).DivMod(6, out var remainder);

      Assert.AreEqual(1.5, quotient);
      Assert.AreEqual(3, remainder);
    }

    [TestMethod]
    public void DivRemTrunc()
    {
      var quotient = (9.0).DivModTrunc(6, out var remainder, out var truncatedQuotient);

      Assert.AreEqual(1.5, quotient);
      Assert.AreEqual(3, remainder);
      Assert.AreEqual(1, truncatedQuotient);
    }

    [TestMethod]
    public void TruncDivRem()
    {
      var truncatedQuotient = (9.0).TruncMod(6, out var remainder);

      Assert.AreEqual(1, truncatedQuotient);
      Assert.AreEqual(3, remainder);
    }

    [TestMethod]
    public void GreatestCommonDivisor()
    {
      Assert.AreEqual(3, 21.GreatestCommonDivisor(6));
    }

    [TestMethod]
    public void IntegerPow()
    {
      Assert.AreEqual(85766121, 21.IntegerPow(6));
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
    public void NearestMultipleOf()
    {
      Assert.AreEqual(520, 512.RoundToNearestMultipleOf(20, false, HalfwayRounding.TowardZero, out var smaller, out var larger));

      Assert.AreEqual(500, smaller);
      Assert.AreEqual(520, larger);
    }
  }
}
