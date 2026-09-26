#if NET7_0_OR_GREATER
using Flux;

namespace Maths
{
  [TestClass]
  public class Modular
  {
    //[TestMethod]
    //public void DivMod()
    //{
    //  var quotient = (9.0).DivMod(6, out var remainder);

    //  Assert.AreEqual(1.5, quotient);
    //  Assert.AreEqual(3, remainder);
    //}

    //[TestMethod]
    //public void DivModTrunc()
    //{
    //  var quotient = (9.0).DivModTrunc(6, out var remainder, out var truncatedQuotient);

    //  Assert.AreEqual(1.5, quotient);
    //  Assert.AreEqual(3, remainder);
    //  Assert.AreEqual(1, truncatedQuotient);
    //}

    [TestMethod]
    public void CeilingDivRem()
    {
      var actual = int.IntegerDivRemCeiling(9, 6);
      var expected = (2, -3);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EnvelopDivRem()
    {
      var actual = int.IntegerDivRemEnveloped(9, 6);
      var expected = (2, -3);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EuclideanDivRem()
    {
      var actual = int.IntegerDivRemEuclidean(9, 6);
      var expected = (1, 3);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void FlooredDivRem()
    {
      var actual = int.IntegerDivRemFloored(9, 6);
      var expected = (1, 3);
      Assert.AreEqual(expected, actual);
    }

    //[TestMethod]
    //public void RoundedDivRem()
    //{
    //  var actual = int.RoundedDivRem(9, 6);
    //  var expected = (2, -3);
    //  Assert.AreEqual(expected, actual);
    //}

    [TestMethod]
    public void TruncatedDivRem()
    {
      var actual = int.DivRem(9, 6);
      var expected = (1, 3);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ModInv()
    {
      var mi4and7 = BinaryInteger.ModInverse(4, 7); // mi = 2, i.e. "2 is the modular multiplicative inverse of 4 (and vice versa), mod 7".;
      Assert.AreEqual(2, mi4and7);

      var mi8and11 = BinaryInteger.ModInverse(8, 11); // mi = 7, i.e. "7 is the modular inverse of 8, mod 11".
      Assert.AreEqual(7, mi8and11);
    }

    [TestMethod]
    public void TruncMod()
    {
      var (truncatedQuotient, remainder) = Number.IntegerDivRemTruncated(9.0, 6);

      Assert.AreEqual(1, truncatedQuotient);
      Assert.AreEqual(3, remainder);
    }
  }
}
#endif
