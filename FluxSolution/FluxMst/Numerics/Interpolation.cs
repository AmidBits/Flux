#if NET7_0_OR_GREATER
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Numerics
{
  [TestClass]
  public class Interpolation
  {
    [TestMethod]
    public void InterpolateCosine()
    {
      Assert.AreEqual(5.732233047033631, (5.0).InterpolateCosine(10, 0.25));
    }

    //[TestMethod]
    //public void InterpolateLinear()
    //{
    //  Assert.AreEqual(6.25, (5.0).InterpolateLinear(10, 0.25));
    //}
  }
}
#endif
