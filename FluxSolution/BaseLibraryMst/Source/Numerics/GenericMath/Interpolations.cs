#if NET7_0_OR_GREATER
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericMath
{
  [TestClass]
  public class Interpolations
  {
    [TestMethod]
    public void InterpolateCosine()
    {
      var ic = new Flux.Interpolation.CosineInterpolation<double>();

      Assert.AreEqual(5.732233047033631, ic.Interpolate2Node(5.0, 10, 0.25));
    }

    [TestMethod]
    public void InterpolateLinear()
    {
      var il = new Flux.Interpolation.LinearInterpolation<double>();

      Assert.AreEqual(6.25, il.Interpolate2Node(5.0, 10, 0.25));
    }
  }
}
#endif
