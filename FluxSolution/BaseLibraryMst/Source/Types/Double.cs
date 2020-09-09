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
      Assert.AreEqual(2.6666666666666665, d.AverageAbsoluteDeviationFromMean(), nameof(Flux.XtendDouble.AverageAbsoluteDeviationFromMean));
      Assert.AreEqual(2.5, d.AverageAbsoluteDeviationFromMedian(), nameof(Flux.XtendDouble.AverageAbsoluteDeviationFromMedian));
      Assert.AreEqual(4.166666666666667, d.AverageAbsoluteDeviationFromMode(), nameof(Flux.XtendDouble.AverageAbsoluteDeviationFromMode));
    }

    [TestMethod]
    public void Mean()
    {
      Assert.AreEqual(54, d.Mean(), nameof(Flux.XtendDouble.Mean));
    }

    [TestMethod]
    public void Median()
    {
      Assert.AreEqual(63, d.Median(), nameof(Flux.XtendDouble.Median));
    }

    [TestMethod]
    public void Percentile75th()
    {
      Assert.AreEqual(81, d.Percentile(75), nameof(Flux.XtendDouble.Percentile));
    }

    [TestMethod]
    public void StandardDeviation()
    {
      Assert.AreEqual(34.85685011586675, d.StandardDeviation(), nameof(Flux.XtendDouble.StandardDeviation));

    }

    [TestMethod]
    public void Variance()
    {
      Assert.AreEqual(972, d.Variance().populationVariance, nameof(Flux.XtendDouble.Variance) + ".populationVariance");
      Assert.AreEqual(1215, d.Variance().sampleVariance, nameof(Flux.XtendDouble.Variance) + ".sampleVariance");
    }
  }
}
