using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace Double
{
  [TestClass]
  public class Double
  {
    double[] d = new double[] { 9d, 27d, 63d, 81d };

    [TestMethod]
    public void Median()
    {
      Assert.AreEqual(45, d.Median());
    }

    [TestMethod]
    public void StandardDeviation()
    {
      Assert.AreEqual(32.863353450309965, d.StandardDeviation());
    }

    [TestMethod]
    public void Variance()
    {
      Assert.AreEqual((4, 45, 1080, 810, 28.460498941515414), d.Variance());
    }
  }
}
