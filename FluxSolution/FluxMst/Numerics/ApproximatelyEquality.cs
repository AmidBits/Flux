#if NET7_0_OR_GREATER
using Flux;

namespace Numerics
{
  [TestClass]
  public class ApproximateEquality
  {
    [TestMethod]
    public void EqualsWithinAbsoluteTolerance()
    {
      Assert.IsFalse(INumber.EqualsWithinAbsoluteTolerance(5.5, 5, 0.4));
      Assert.IsTrue(INumber.EqualsWithinAbsoluteTolerance(5.5, 5, 0.6));
    }

    [TestMethod]
    public void EqualsWithinRelativeTolerance()
    {
      Assert.IsFalse(INumber.EqualsWithinRelativeTolerance(5.5, 5, 0.05));
      Assert.IsTrue(INumber.EqualsWithinRelativeTolerance(5.5, 5, 0.5));
    }

    [TestMethod]
    public void EqualsWithinSignificantDigits()
    {
      Assert.IsFalse(INumber.EqualsWithinSignificantDigits(5.5, 5, 1, 10));
      Assert.IsTrue(INumber.EqualsWithinSignificantDigits(5.5, 5, -1, 10));

      Assert.IsFalse(INumber.EqualsWithinSignificantDigits(1000.02, 1000.015, 3, 10));
      Assert.IsTrue(INumber.EqualsWithinSignificantDigits(1000.02, 1000.015, 2, 10));

      Assert.IsFalse(INumber.EqualsWithinSignificantDigits(1334.261, 1235.272, -1, 10));
      Assert.IsTrue(INumber.EqualsWithinSignificantDigits(1334.261, 1235.272, -2, 10));
    }
  }
}
#endif
