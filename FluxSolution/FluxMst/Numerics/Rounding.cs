#if NET7_0_OR_GREATER
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Maths
{
  [TestClass]
  public class Rounding
  {
    [TestMethod]
    public void Round()
    {
      Assert.AreEqual(-6, (-5.5).Envelop());
      Assert.AreEqual(6, (5.5).Round(HalfRounding.ToEven));
      Assert.AreEqual(6, (5.5).Round(HalfRounding.AwayFromZero));
      Assert.AreEqual(5, (5.5).Round(HalfRounding.TowardZero));
      Assert.AreEqual(5, (5.5).Round(HalfRounding.ToNegativeInfinity));
      Assert.AreEqual(6, (5.5).Round(HalfRounding.ToPositiveInfinity));
      Assert.AreEqual(5, (5.5).Round(HalfRounding.ToAlternating)); // First alternating state. This is also {value}.RoundHalfAlternating().
      Assert.AreEqual(6, (5.5).Round(HalfRounding.ToAlternating)); // Second alternating state. This is also {value}.RoundHalfAlternating().
      Assert.IsTrue((5.5).Round(HalfRounding.ToRandom) is 5 or 6); // This is also {value}.RoundHalfToRandom(). (below)
      Assert.AreEqual(5, (5.5).Round(HalfRounding.ToOdd)); // This is also {value}.RoundHalfToOdd(). (below)

      Assert.AreEqual(5, (5.5).RoundHalfToAlternating()); // Third alternating state.
      Assert.AreEqual(6, (5.5).RoundHalfToAlternating()); // Fourth alternating state.
      Assert.IsTrue((5.5).RoundHalfToRandom() is 5 or 6);
      Assert.AreEqual(5, (5.5).RoundHalfToOdd());
    }

    [TestMethod]
    public void RoundToBoundary()
    {
      Assert.AreEqual(17, 12.RoundToNearest(HalfRounding.AwayFromZero, 7, 17), HalfRounding.AwayFromZero.ToString());
      Assert.AreEqual(7, 12.RoundToNearest(HalfRounding.TowardZero, 7, 17), HalfRounding.TowardZero.ToString());
    }

    [TestMethod]
    public void RoundToMultipleOf()
    {
      var n = 1.75;
      var m = 0.45;

      n.MultipleOfNearest(m, false, HalfRounding.AwayFromZero, out var multipleTowardsZero, out var multipleAwayFromZero);

      Assert.AreEqual(1.8, n.RoundToNearest(Flux.UniversalRounding.HalfAwayFromZero, multipleTowardsZero, multipleAwayFromZero), $"{nameof(RoundToMultipleOf)} {Flux.UniversalRounding.HalfAwayFromZero}");
    }

    [TestMethod]
    public void RoundToPrecision()
    {
      Assert.AreEqual(99.97, (99.96535789).RoundUniversalByPrecision(Flux.UniversalRounding.HalfToEven, 2, 10));
    }

    [TestMethod]
    public void RoundToTruncatedPrecision()
    {
      Assert.AreEqual(99.96, (99.96535789).RoundUniversalByTruncatedPrecision(Flux.UniversalRounding.HalfToEven, 2, 10));
    }
  }
}
#endif
