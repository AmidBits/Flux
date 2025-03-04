﻿using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Collections.Generic
{
  [TestClass]
  public class Distribution
  {
    private readonly double[] d = new double[] { 1, 2, 3, 3, 3, 4, 4, 5, 5, 7 };

    [TestMethod]
    public void CumulativeMassFunctionPercentRank()
    {
      Assert.AreEqual(0.7, d.ToHistogram(k => k, f => 1).ComputeCdfPercentRank(4, 1d), nameof(CumulativeMassFunctionPercentRank));
    }

    //[TestMethod]
    //public void PercentRank()
    //{
    //  var actual = d.PercentRank().ToArray();
    //  var expected = new double[] { 0, 25, 50, 75, 100 };
    //  CollectionAssert.AreEqual(expected, actual, nameof(PercentileRank));
    //}

    [TestMethod]
    public void PercentileRank()
    {
      //Assert.AreEqual(60, d.PercentileRank(65.0), nameof(PercentileRank));
      //Assert.AreEqual(60, Flux.Numerics.PercentileVariant3.PercentScore(0.75, d.Count()), "ComputePercentileScore");
      Assert.AreEqual(5, Flux.Statistics.Percentile.NearestRank.PercentileScore(d, 0.75), "ComputePercentileScore");
      Assert.AreEqual(4.75, Flux.Statistics.Percentile.Variant2.PercentileScore(d, 0.75), "ComputePercentileScore");
      Assert.AreEqual(5, Flux.Statistics.Percentile.Variant3.PercentileScore(d, 0.75), "ComputePercentileScore");

      Assert.AreEqual(7.5, Flux.Statistics.Percentile.NearestRank.PercentileRank(d.Length, 0.75), "ComputePercentileRank");
      Assert.AreEqual(7.75, Flux.Statistics.Percentile.Variant2.PercentileRank(d.Length, 0.75), "ComputePercentileRank");
      Assert.AreEqual(8.25, Flux.Statistics.Percentile.Variant3.PercentileRank(d.Length, 0.75), "ComputePercentileRank");

      Assert.AreEqual(4.75, Flux.Statistics.Quantile.R7.Default.EstimateQuantileValue(d, 0.75), "QuantileR5");
      Assert.AreEqual(5, Flux.Statistics.Quantile.R6.Default.EstimateQuantileValue(d, 0.75), "QuantileR6");



      //Assert.AreEqual(0, Flux.Numerics.QuantileR6.Estimate(d, 0.75), "ComputePercentile");
    }

    [TestMethod]
    public void ProbabilityMassFunction()
    {
      Assert.AreEqual(0.2, d.ToHistogram(k => k, f => 1).ComputePmfProbability(4, 1d), nameof(ProbabilityMassFunction));
    }
  }
}
