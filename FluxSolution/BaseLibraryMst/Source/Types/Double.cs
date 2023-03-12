using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Types
{
  [TestClass]
  public class Double
  {
    private readonly double[] d = new double[] { 9d, 27d, 63d, 81d, 90d };

    [TestMethod]
    public void AverageAbsoluteDeviationFrom()
    {
      d.AverageAbsoluteDeviationFrom(out double mean, out double median, out double mode);
      Assert.AreEqual(2.6666666666666665, mean, "AverageAbsoluteDeviationFrom[mean]");
      Assert.AreEqual(2.5, median, "AverageAbsoluteDeviationFrom[median]");
      Assert.AreEqual(4.166666666666667, mode, "AverageAbsoluteDeviationFrom[mode");
    }

    //[TestMethod]
    //public void AverageAbsoluteDeviationFromMean()
    //{
    //  Assert.AreEqual(2.6666666666666665, d.AverageAbsoluteDeviationFromMean(), nameof(AverageAbsoluteDeviationFromMean));
    //}
    //[TestMethod]
    //public void AverageAbsoluteDeviationFromMedian()
    //{
    //  Assert.AreEqual(2.5, d.AverageAbsoluteDeviationFromMedian(), nameof(AverageAbsoluteDeviationFromMedian));
    //}
    //[TestMethod]
    //public void AverageAbsoluteDeviationFromMode()
    //{
    //  Assert.AreEqual(4.166666666666667, d.AverageAbsoluteDeviationFromMode(), nameof(AverageAbsoluteDeviationFromMode));
    //}

    [TestMethod]
    public void Mean()
    {
      d.Mean(out double result, out int _, out double _);

      Assert.AreEqual(54, result, nameof(Mean));
    }

    [TestMethod]
    public void Median()
    {
      d.Median(out double median, out var _);
      Assert.AreEqual(63.0, median, nameof(Median));
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
