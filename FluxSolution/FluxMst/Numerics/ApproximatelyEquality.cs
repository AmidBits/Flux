#if NET7_0_OR_GREATER
using Flux;

namespace Numerics
{
  [TestClass]
  public class ApproximateEquality
  {
    [TestMethod]
    public void EqualsWithin()
    {
      Assert.IsTrue(Number.EqualsWithin(5.5, 5, 0.6));
      Assert.IsFalse(Number.EqualsWithin(5.5, 5, 0.05));
      Assert.IsTrue(Number.EqualsWithin(5.5, 5, 0.5));
    }

    [TestMethod]
    public void EqualsWithinSignificantDigits()
    {
      Assert.IsFalse(Number.EqualsWithin(5.5, 5, -1, 10));
      Assert.IsTrue(Number.EqualsWithin(5.5, 5, 1, 10));

      Assert.IsFalse(Number.EqualsWithin(1000.02, 1000.015, -3, 10));
      Assert.IsTrue(Number.EqualsWithin(1000.02, 1000.015, -2, 10));

      Assert.IsFalse(Number.EqualsWithin(1334.261, 1235.272, 1, 10));
      Assert.IsTrue(Number.EqualsWithin(1334.261, 1235.272, 2, 10));
    }
  }
}
#endif
