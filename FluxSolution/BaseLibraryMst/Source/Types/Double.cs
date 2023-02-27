using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Types
{
  [TestClass]
  public class Double
  {
    double[] d = new double[] { 9d, 27d, 63d, 81d, 90d };

    [TestMethod]
    public void AverageAbsoluteDeviationFromMean()
    {
      Assert.AreEqual(2.6666666666666665, d.AverageAbsoluteDeviationFromMean(), nameof(AverageAbsoluteDeviationFromMean));
    }
    [TestMethod]
    public void AverageAbsoluteDeviationFromMedian()
    {
      Assert.AreEqual(2.5, d.AverageAbsoluteDeviationFromMedian(), nameof(AverageAbsoluteDeviationFromMedian));
    }
    [TestMethod]
    public void AverageAbsoluteDeviationFromMode()
    {
      Assert.AreEqual(4.166666666666667, d.AverageAbsoluteDeviationFromMode(), nameof(AverageAbsoluteDeviationFromMode));
    }

    [TestMethod]
    public void Mean()
    {
      d.Mean(out int _, out double _, out double result);

      Assert.AreEqual(54, result, nameof(Mean));
    }

    [TestMethod]
    public void Median()
    {
      Assert.AreEqual(63, d.Median(out var _), nameof(Median));
    }

    [TestMethod]
    public void PercentileRank75th()
    {
      Assert.AreEqual(60, (int)d.ToHistogram(k => k, f => 1).ComputeCdfPercentRank(75, 100.0), nameof(PercentileRank75th));
    }

    //[TestMethod]
    //public void StandardDeviation()
    //{
    //  Assert.AreEqual(34.85685011586675, d.StandardDeviation(), nameof(StandardDeviation));

    //}

    //[TestMethod]
    //public void Variance()
    //{
    //  Assert.AreEqual(972, d.Variance().populationVariance, nameof(Variance) + ".populationVariance");
    //  Assert.AreEqual(1215, d.Variance().sampleVariance, nameof(Variance) + ".sampleVariance");
    //}
  }
}
