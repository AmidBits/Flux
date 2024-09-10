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
      Assert.AreEqual(6, (5.5).RoundFullAwayFromZero());
      Assert.AreEqual(5, (5.5).RoundFullTowardZero());
      Assert.AreEqual(5, (5.5).RoundFullToNegativeInfinity());
      Assert.AreEqual(6, (5.5).RoundFullToPositiveInfinity());
      Assert.AreEqual(6, (5.5).Round(UniversalRounding.HalfToEven));
      Assert.AreEqual(6, (5.5).Round(UniversalRounding.HalfAwayFromZero));
      Assert.AreEqual(5, (5.5).Round(UniversalRounding.HalfTowardZero));
      Assert.AreEqual(5, (5.5).Round(UniversalRounding.HalfToNegativeInfinity));
      Assert.AreEqual(6, (5.5).Round(UniversalRounding.HalfToPositiveInfinity));
      Assert.AreEqual(5, (5.5).RoundHalfToOdd());
    }

    [TestMethod]
    public void RoundToBoundary()
    {
      Assert.AreEqual(17, (12.ToBigInteger()).RoundToBoundary(UniversalRounding.HalfAwayFromZero, 7, 17), UniversalRounding.HalfAwayFromZero.ToString());
      Assert.AreEqual(7, (12.ToBigInteger()).RoundToBoundary(UniversalRounding.HalfTowardZero, 7, 17), UniversalRounding.HalfTowardZero.ToString());
    }

    [TestMethod]
    public void RoundToMultipleOf()
    {
      var n = 1.75;
      var m = 0.45;

      n.RoundToMultipleOf(m, false, UniversalRounding.HalfAwayFromZero, out var multipleTowardsZero, out var multipleAwayFromZero);

      Assert.AreEqual(1.8, n.RoundToBoundary(Flux.UniversalRounding.HalfAwayFromZero, multipleTowardsZero, multipleAwayFromZero), $"{nameof(RoundToMultipleOf)} {Flux.UniversalRounding.HalfAwayFromZero}");
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
