#if NET7_0_OR_GREATER
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericMath
{
  [TestClass]
  public class TrigonometricFunctions
  {
    [TestMethod]
    public void SincN()
    {
      Assert.AreEqual(0.6366197723675814, Flux.Quantities.Angle.Sincn(0.5));
    }

    [TestMethod]
    public void SincU()
    {
      Assert.AreEqual(0.958851077208406, Flux.Quantities.Angle.Sincu(0.5));
    }
  }
}
#endif
