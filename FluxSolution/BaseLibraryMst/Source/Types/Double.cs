using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace Types
{
  [TestClass]
  public class Double
  {
    double[] d = new double[] { 9d, 27d, 63d, 81d, 90d };

    [TestMethod]
    public void AverageAbsoluteDeviation()
    {
      Assert.AreEqual(2.6666666666666665, d.AverageAbsoluteDeviationFromMean(), nameof(Flux.ExtensionMethods.AverageAbsoluteDeviationFromMean));
      Assert.AreEqual(2.5, d.AverageAbsoluteDeviationFromMedian(), nameof(Flux.ExtensionMethods.AverageAbsoluteDeviationFromMedian));
      Assert.AreEqual(4.166666666666667, d.AverageAbsoluteDeviationFromMode(), nameof(Flux.ExtensionMethods.AverageAbsoluteDeviationFromMode));
    }

    [TestMethod]
    public void Mean()
    {
      Assert.AreEqual(54, d.Mean(), nameof(Flux.ExtensionMethods.Mean));
    }

    [TestMethod]
    public void Median()
    {
      Assert.AreEqual(63, d.Median(), nameof(Flux.ExtensionMethods.Median));
    }

    [TestMethod]
    public void Percentile75th()
    {
      Assert.AreEqual(60, (int)d.PercentileRank(75), nameof(ExtensionMethods.PercentRank));
    }

    [TestMethod]
    public void StandardDeviation()
    {
      Assert.AreEqual(34.85685011586675, d.StandardDeviation(), nameof(Flux.ExtensionMethods.StandardDeviation));

    }

    [TestMethod]
    public void Variance()
    {
      Assert.AreEqual(972, d.Variance().populationVariance, nameof(Flux.ExtensionMethods.Variance) + ".populationVariance");
      Assert.AreEqual(1215, d.Variance().sampleVariance, nameof(Flux.ExtensionMethods.Variance) + ".sampleVariance");
    }
  }
}
