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
      var actual = int.IntegerDivRemCeiling(-12, -3);
      Assert.AreEqual(expected, actual);
      expected = (4, 1);
      actual = int.IntegerDivRemCeiling(-11, -3);
      Assert.AreEqual(expected, actual);
      expected = (4, 2);
      actual = int.IntegerDivRemCeiling(-10, -3);
      Assert.AreEqual(expected, actual);
    }

    //[TestMethod]
    //public void ClosestDivision()
    //{
    //  var expected = (4, 0);
    //  var actual = int.DivRemRoundedToEven(-12, -3);
    //  Assert.AreEqual(expected, actual);
    //  expected = (4, 1);
    //  actual = int.DivRemRoundedToEven(-11, -3);
    //  Assert.AreEqual(expected, actual);
    //  expected = (3, -1);
    //  actual = int.DivRemRoundedToEven(-10, -3);
    //  Assert.AreEqual(expected, actual);
    //}

    [TestMethod]
    public void EnvelopedDivision()
    {
      var expected = (4, 0);
      var actual = int.IntegerDivRemEnveloped(-12, -3);
      Assert.AreEqual(expected, actual);
      expected = (4, 1);
      actual = int.IntegerDivRemEnveloped(-11, -3);
      Assert.AreEqual(expected, actual);
      expected = (4, 2);
      actual = int.IntegerDivRemEnveloped(-10, -3);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EuclideanDivision()
    {
      var expected = (4, 0);
      var actual = int.IntegerDivRemEuclidean(-12, -3);
      Assert.AreEqual(expected, actual);
      expected = (4, 1);
      actual = int.IntegerDivRemEuclidean(-11, -3);
      Assert.AreEqual(expected, actual);
      expected = (4, 2);
      actual = int.IntegerDivRemEuclidean(-10, -3);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void FlooredDivision()
    {
      var expected = (4, 0);
      var actual = int.IntegerDivRemFloored(-12, -3);
      Assert.AreEqual(expected, actual);
      expected = (3, -2);
      actual = int.IntegerDivRemFloored(-11, -3);
      Assert.AreEqual(expected, actual);
      expected = (3, -1);
      actual = int.IntegerDivRemFloored(-10, -3);
      Assert.AreEqual(expected, actual);
    }

    //[TestMethod]
    //public void RoundedDivision()
    //{
    //  var expected = (4, 0);
    //  var actual = int.RoundedDivRem(-12, -3);
    //  Assert.AreEqual(expected, actual);
    //  expected = (4, 1);
    //  actual = int.RoundedDivRem(-11, -3);
    //  Assert.AreEqual(expected, actual);
    //  expected = (4, 2);
    //  actual = int.RoundedDivRem(-10, -3);
    //  Assert.AreEqual(expected, actual);
    //}

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
