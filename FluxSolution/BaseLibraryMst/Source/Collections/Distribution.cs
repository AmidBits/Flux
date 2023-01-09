using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Collections.Generic
{
  [TestClass]
  public class Distribution
  {
    private readonly double[] d = new double[] { 9d, 27d, 63d, 81d, 90d };

    [TestMethod]
    public void CumulativeMassFunction()
    {
      Assert.AreEqual(0.4, d.CumulativeMassFunction(45), nameof(CumulativeMassFunction));
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
      Assert.AreEqual(60, d.PercentileRank(65.0), nameof(PercentileRank));
    }

    [TestMethod]
    public void ProbabilityMassFunction()
    {
      Assert.AreEqual(0.6, d.ProbabilityMassFunction(65), nameof(ProbabilityMassFunction));
    }
  }
}
