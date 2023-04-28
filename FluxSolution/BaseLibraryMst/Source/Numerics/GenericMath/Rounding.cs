#if NET7_0_OR_GREATER
using System;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericMath
{
  [TestClass]
  public class Rounding
  {
    [TestMethod]
    public void BoundaryRounding()
    {
      var actual = (11).RoundToBoundary(RoundingMode.HalfAwayFromZero, 7, 17);

      Assert.AreEqual(7, actual);
    }

    [TestMethod]
    public void NearestMultiple()
    {
      Assert.AreEqual(1.8, Flux.GenericMath.NearestMultipleOf<double>(1.75, 0.45, false, Flux.RoundingMode.HalfAwayFromZero, out var _, out var _), $"{nameof(Flux.GenericMath.NearestMultipleOf)} {Flux.RoundingMode.HalfAwayFromZero}");
    }

    [TestMethod]
    public void PrecisionRounding()
    {
      var actual = (99.96535789).RoundToPrecision(Flux.RoundingMode.HalfToEven, 2);

      Assert.AreEqual(99.97, actual);
    }

    [TestMethod]
    public void PrecisionTruncatedRounding()
    {
      var actual = (99.96535789).RoundToTruncatedPrecision(Flux.RoundingMode.HalfToEven, 2);

      Assert.AreEqual(99.96, actual);
    }

    [TestMethod]
    public void RoundingCore()
    {
      var actual = (11.5).Round(RoundingMode.HalfAwayFromZero);

      Assert.AreEqual(12, actual);
    }
  }
}
#endif
