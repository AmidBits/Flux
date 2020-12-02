using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace Collections.Generic
{
  [TestClass]
  public class Distribution
  {
    double[] d = new double[] { 9d, 27d, 63d, 81d, 90d };

    [TestMethod]
    public void CumulativeMassFunction()
    {
      Assert.AreEqual(0.4, d.CumulativeMassFunction(45), nameof(Flux.IEnumerableEm.CumulativeMassFunction));
    }

    [TestMethod]
    public void PercentRank()
    {
      CollectionAssert.AreEqual(new double[] { 10, 30, 50, 70, 90 }, d.PercentRank().ToArray(), nameof(Flux.IEnumerableEm.PercentileRank));
    }

    [TestMethod]
    public void PercentileRank()
    {
      Assert.AreEqual(60, d.PercentileRank(65.0), nameof(Flux.IEnumerableEm.PercentileRank));
    }

    [TestMethod]
    public void ProbabilityMassFunction()
    {
      Assert.AreEqual(0.6, d.ProbabilityMassFunction(65, out var _, out var _), nameof(Flux.IEnumerableEm.ProbabilityMassFunction));
    }
  }
}
