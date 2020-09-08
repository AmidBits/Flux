using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace Types
{
  [TestClass]
  public class Double
  {
    double[] d = new double[] { 9d, 27d, 63d, 81d };

    [TestMethod]
    public void CumulativeDistributionFunction()
    {
      Assert.AreEqual(0.5, d.CumulativeDistributionFunction(45));
    }

    [TestMethod]
    public void Mean()
    {
      Assert.AreEqual(45, d.Mean());
    }

    [TestMethod]
    public void Median()
    {
      Assert.AreEqual(45, d.Median());
    }

    [TestMethod]
    public void PercentileTopScore()
    {
      Assert.AreEqual(27, d.Percentile(50));
    }

    [TestMethod]
    public void ProbabilityMassFunction()
    {
      Assert.AreEqual(0.75, d.ProbabilityMassFunction(65, out var _, out var _));
    }

    [TestMethod]
    public void StandardDeviation()
    {
      Assert.AreEqual(32.863353450309965, d.StandardDeviation());

    }

    [TestMethod]
    public void Variance()
    {
      Assert.AreEqual(810, d.Variance().populationVariance);
      Assert.AreEqual(1080, d.Variance().sampleVariance);
    }
  }
}
