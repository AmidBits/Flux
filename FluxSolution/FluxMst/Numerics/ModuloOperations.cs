using Flux;

namespace Numerics
{
  [TestClass]
  public class ModuloOperations
  {
    [TestMethod]
    public void CeilingDivision()
    {
      var expected = (4, 0);
      var actual = int.CeilingDivRem(-12, -3);
      Assert.AreEqual(expected, actual);
      expected = (4, 1);
      actual = int.CeilingDivRem(-11, -3);
      Assert.AreEqual(expected, actual);
      expected = (3, -1);
      actual = int.CeilingDivRem(-10, -3);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ClosestDivision()
    {
      var expected = (4, 0);
      var actual = int.ClosestDivRem(-12, -3);
      Assert.AreEqual(expected, actual);
      expected = (4, 1);
      actual = int.ClosestDivRem(-11, -3);
      Assert.AreEqual(expected, actual);
      expected = (3, -1);
      actual = int.ClosestDivRem(-10, -3);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EnvelopedDivision()
    {
      var expected = (4, 0);
      var actual = int.EnvelopedDivRem(-12, -3);
      Assert.AreEqual(expected, actual);
      expected = (4, 1);
      actual = int.EnvelopedDivRem(-11, -3);
      Assert.AreEqual(expected, actual);
      expected = (4, 2);
      actual = int.EnvelopedDivRem(-10, -3);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EuclideanDivision()
    {
      var expected = (-4, 0);
      var actual = int.EuclideanDivRem(-12, -3);
      Assert.AreEqual(expected, actual);
      expected = (-3, -2);
      actual = int.EuclideanDivRem(-11, -3);
      Assert.AreEqual(expected, actual);
      expected = (-3, -1);
      actual = int.EuclideanDivRem(-10, -3);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void FlooredDivision()
    {
      var expected = (4, 0);
      var actual = int.FlooredDivRem(-12, -3);
      Assert.AreEqual(expected, actual);
      expected = (3, -2);
      actual = int.FlooredDivRem(-11, -3);
      Assert.AreEqual(expected, actual);
      expected = (3, -1);
      actual = int.FlooredDivRem(-10, -3);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void RoundedDivision()
    {
      var expected = (4, 0);
      var actual = int.RoundedDivRem(-12, -3);
      Assert.AreEqual(expected, actual);
      expected = (4, 1);
      actual = int.RoundedDivRem(-11, -3);
      Assert.AreEqual(expected, actual);
      expected = (4, 2);
      actual = int.RoundedDivRem(-10, -3);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TruncatedDivision()
    {
      var expected = (4, 0);
      var actual = int.DivRem(-12, -3);
      Assert.AreEqual(expected, actual);
      expected = (3, -2);
      actual = int.DivRem(-11, -3);
      Assert.AreEqual(expected, actual);
      expected = (3, -1);
      actual = int.DivRem(-10, -3);
      Assert.AreEqual(expected, actual);
    }
  }
}
