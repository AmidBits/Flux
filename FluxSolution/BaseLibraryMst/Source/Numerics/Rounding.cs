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
      Assert.AreEqual(6, (5.5).Envelop());
      Assert.AreEqual(6, (5.5).Round(UniversalRounding.HalfToEven));
      Assert.AreEqual(6, (5.5).Round(UniversalRounding.HalfAwayFromZero));
      Assert.AreEqual(5, (5.5).Round(UniversalRounding.HalfTowardZero));
      Assert.AreEqual(5, (5.5).Round(UniversalRounding.HalfToNegativeInfinity));
      Assert.AreEqual(6, (5.5).Round(UniversalRounding.HalfToPositiveInfinity));
      Assert.AreEqual(5, (5.5).Round(UniversalRounding.HalfAlternating)); // First alternating state. This is also {value}.RoundHalfAlternating().
      Assert.AreEqual(6, (5.5).Round(UniversalRounding.HalfAlternating)); // Second alternating state. This is also {value}.RoundHalfAlternating().
      Assert.IsTrue((5.5).Round(UniversalRounding.HalfToRandom) is 5 or 6); // This is also {value}.RoundHalfToRandom().
      Assert.AreEqual(5, (5.5).Round(UniversalRounding.HalfToOdd)); // This is also {value}.RoundHalfToOdd().
      Assert.AreEqual(5, (5.5).RoundHalfToOdd());
    }

    [TestMethod]
    public void RoundToBoundary()
    {
      Assert.AreEqual(17, (12.ToBigInteger()).RoundToNearest(UniversalRounding.HalfAwayFromZero, 7, 17), UniversalRounding.HalfAwayFromZero.ToString());
      Assert.AreEqual(7, (12.ToBigInteger()).RoundToNearest(UniversalRounding.HalfTowardZero, 7, 17), UniversalRounding.HalfTowardZero.ToString());
    }

    [TestMethod]
    public void RoundToMultipleOf()
    {
      var n = 1.75;
      var m = 0.45;

      n.RoundToMultipleOf(m, false, UniversalRounding.HalfAwayFromZero, out var multipleTowardsZero, out var multipleAwayFromZero);

      Assert.AreEqual(1.8, n.RoundToNearest(Flux.UniversalRounding.HalfAwayFromZero, multipleTowardsZero, multipleAwayFromZero), $"{nameof(RoundToMultipleOf)} {Flux.UniversalRounding.HalfAwayFromZero}");
    }

    [TestMethod]
    public void RoundToPrecision()
    {
      Assert.AreEqual(99.97, (99.96535789).RoundByPrecision(Flux.UniversalRounding.HalfToEven, 2, 10));
    }

    [TestMethod]
    public void RoundToTruncatedPrecision()
    {
      Assert.AreEqual(99.96, (99.96535789).RoundByTruncatedPrecision(Flux.UniversalRounding.HalfToEven, 2, 10));
    }
  }
}
#endif
