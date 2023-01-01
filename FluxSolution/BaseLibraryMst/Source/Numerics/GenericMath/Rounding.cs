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
      var actual = Flux.BoundaryRounding<int, int>.Round(11, RoundingMode.HalfAwayFromZero, 7, 17);

      Assert.AreEqual(7, actual);
    }

    [TestMethod]
    public void NearestMultiple()
    {
      Assert.AreEqual(1.8, Flux.GenericMath.NearestMultiple<double>(1.75, 0.45, false, Flux.RoundingMode.HalfAwayFromZero, out var _, out var _), $"{nameof(Flux.GenericMath.NearestMultiple)} {Flux.RoundingMode.HalfAwayFromZero}");
    }

    [TestMethod]
    public void PrecisionRounding()
    {
      var actual = Flux.PrecisionRounding<double>.Round(99.96535789, Flux.RoundingMode.HalfToEven, 2);

      Assert.AreEqual(99.97, actual);
    }

    [TestMethod]
    public void PrecisionTruncatedRounding()
    {
      var actual = Flux.TruncatedPrecisionRounding<double>.Round(99.96535789, Flux.RoundingMode.HalfToEven, 2);

      Assert.AreEqual(99.96, actual);
    }

    [TestMethod]
    public void Rounding_()
    {
      var actual = Flux.Rounding<double>.Round(11.5, RoundingMode.HalfAwayFromZero);

      Assert.AreEqual(12, actual);
    }
  }
}
#endif
