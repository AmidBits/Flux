#if NET7_0_OR_GREATER
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Numerics
{
  [TestClass]
  public class ApproximateEquality
  {
    [TestMethod]
    public void EqualsWithinAbsoluteTolerance()
    {
      Assert.IsFalse((5.5).EqualsWithinAbsoluteTolerance(5, 0.4));
      Assert.IsTrue((5.5).EqualsWithinAbsoluteTolerance(5, 0.6));
    }

    [TestMethod]
    public void EqualsWithinRelativeTolerance()
    {
      Assert.IsFalse((5.5).EqualsWithinRelativeTolerance(5, 0.05));
      Assert.IsTrue((5.5).EqualsWithinRelativeTolerance(5, 0.5));
    }

    [TestMethod]
    public void EqualsWithinSignificantDigits()
    {
      Assert.IsFalse((5.5).EqualsWithinSignificantDigits(5, 1, 10));
      Assert.IsTrue((5.5).EqualsWithinSignificantDigits(5, -1, 10));

      Assert.IsFalse((1000.02).EqualsWithinSignificantDigits(1000.015, 3));
      Assert.IsTrue((1000.02).EqualsWithinSignificantDigits(1000.015, 2));

      Assert.IsFalse((1334.261).EqualsWithinSignificantDigits(1235.272, -1));
      Assert.IsTrue((1334.261).EqualsWithinSignificantDigits(1235.272, -2));
    }
  }
}
#endif
