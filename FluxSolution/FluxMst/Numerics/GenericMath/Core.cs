#if NET7_0_OR_GREATER
using Flux;

namespace Maths
{
  [TestClass]
  public class Core
  {
    [TestMethod]
    public void DetentInterval()
    {
      Assert.AreEqual(520, 515.DetentInterval(20, 5));
    }

    [TestMethod]
    public void DetentPosition()
    {
      Assert.AreEqual(520, 515.DetentPosition(520, 5));
    }

    [TestMethod]
    public void DetentZero()
    {
      Assert.AreEqual(0, 4.DetentPosition(0, 5));
    }

    [TestMethod]
    public void Envelop()
    {
      Assert.AreEqual(-1, (-0.5).Envelop());
      Assert.AreEqual(1, (0.5).Envelop());

      Assert.AreEqual(-13, (-12.5).Envelop());
      Assert.AreEqual(13, (12.5).Envelop());
    }

    [TestMethod]
    public void BinaryGcd()
    {
      Assert.AreEqual(3, 21.BinaryGcd(6));
    }

    [TestMethod]
    public void EuclidGcd()
    {
      Assert.AreEqual(3, 21.EuclidGcd(6));
    }

    [TestMethod]
    public void EuclidLcm()
    {
      Assert.AreEqual(42, 21.EuclidLcm(6));
    }

    [TestMethod]
    public void LehmerGcd()
    {
      Assert.AreEqual(3, 21.LehmerGcd(6));
    }

    [TestMethod]
    public void Factorial()
    {
      Assert.AreEqual(362880, 9.Factorial(), nameof(Factorials.Factorial));
      //Assert.AreEqual(362880, 9.SplitFactorial(), nameof(Flux.BinaryInteger.SplitFactorial));
      Assert.AreEqual(System.Numerics.BigInteger.Parse("8320987112741390144276341183223364380754172606361245952449277696409600000000000000"), new System.Numerics.BigInteger(60).Factorial(), nameof(Factorials.Factorial));
      //Assert.AreEqual(479001600, 12.SplitFactorial(), nameof(Flux.BinaryInteger.SplitFactorial));
    }

    [TestMethod]
    public void GreatestCommonDivisor()
    {
      Assert.AreEqual(3, 21.Gcd(6));
    }

    [TestMethod]
    public void IntegerRootN()
    {
      Assert.AreEqual((3, 3, 3), 27.RootN(3));
    }

    [TestMethod]
    public void IntegerSqrt()
    {
      Assert.AreEqual((4, 4.58257569495584, 5), 21.Sqrt());
    }

    [TestMethod]
    public void IsCoprime()
    {
      Assert.IsTrue(23.IsCoprime(43));
    }

    [TestMethod]
    public void IntegerPow()
    {
      Assert.AreEqual(10000000000, 10L.IPow(10));
    }

    //[TestMethod]
    //public void IntegerPowRec()
    //{
    //  Assert.AreEqual(10000000000, 10L.IntegerPowRec(10, out double reciprocal));
    //  Assert.AreEqual(1E-10, reciprocal);
    //}

    [TestMethod]
    public void IsPow()
    {
      Assert.IsTrue(100.IsIntegerPowOf(10));
      Assert.IsFalse(101.IsIntegerPowOf(10));
    }

    [TestMethod]
    public void IsIntegerSqrt()
    {
      var v = 15;

      var iq = v.IntegerSqrt();

      var isiq = v.IsSqrt(iq);

      Assert.IsTrue(isiq);
    }

    [TestMethod]
    public void IsPerfectIntegerSqrt()
    {
      var v = 15;

      var iq = v.IntegerSqrt();

      var ispiq = v.IsPerfectSqrt(iq);

      Assert.IsFalse(ispiq);
    }

    [TestMethod]
    public void MaxDigitCount()
    {
      var actual = (10).BitLengthToMaxDigitCount<int, int>(10, false); // Yields 4, because a max value of 1023 can be represented (all bits can be used in an unsigned value).
      var expected = 4;
      Assert.AreEqual(expected, actual);

      actual = (10).BitLengthToMaxDigitCount<int, int>(10, true); // Yields 3, because a max value of 511 can be represented (excluding the MSB used for negative values of signed types).
      expected = 3;
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ModInv()
    {
      Assert.AreEqual(2, 4.ModInv(7));
      Assert.AreEqual(7, 8.ModInv(11));
    }

    [TestMethod]
    public void NearestMultiple()
    {
      var n = 512d;
      var m = 20;

      n.MultipleOfNearest(m, false, HalfRounding.AwayFromZero, out var multipleTowardsZero, out var multipleAwayFromZero);

      var nearestMultiple = n.RoundToNearest(HalfRounding.TowardZero, false, [multipleTowardsZero, multipleAwayFromZero]);

      Assert.AreEqual(520, nearestMultiple);

      Assert.AreEqual(500, multipleTowardsZero);
      Assert.AreEqual(520, multipleAwayFromZero);
    }
  }
}
#endif
