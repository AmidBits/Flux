using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;
using System;

namespace GenericMath
{
  [TestClass]
  public class TrigonometricFunctions
  {
    [TestMethod]
    public void SincN()
    {
      Assert.AreEqual(0.6366197723675814, (0.5).Sincn());
    }

    [TestMethod]
    public void SincU()
    {
      Assert.AreEqual(0.958851077208406, (0.5).Sincu());
    }
  }
}
