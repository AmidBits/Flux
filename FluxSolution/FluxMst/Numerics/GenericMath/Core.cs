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
      Assert.AreEqual(520, INumber.DetentInterval(515, 20, 5));
    }

    [TestMethod]
    public void DetentPosition()
    {
      Assert.AreEqual(520, INumber.DetentPosition(515, 520, 5));
    }

    [TestMethod]
    public void DetentZero()
    {
      Assert.AreEqual(0, INumber.DetentPosition(4, 0, 5));
    }

    [TestMethod]
    public void Envelop()
    {
      Assert.AreEqual(-1, double.Envelop(-0.5));
      Assert.AreEqual(1, double.Envelop(0.5));

      Assert.AreEqual(-13, double.Envelop(-12.5));
      Assert.AreEqual(13, double.Envelop(12.5));
    }

    [TestMethod]
    public void Gcd()
    {
      Assert.AreEqual(3, Flux.IBinaryInteger.Gcd(21, 6));
    }

    [TestMethod]
    public void Lcm()
    {
      Assert.AreEqual(42, Flux.IBinaryInteger.Lcm(21, 6));
    }

    [TestMethod]
    public void Factorial()
    {
      Assert.AreEqual(362880, IBinaryInteger.Factorial(9), nameof(IBinaryInteger.Factorial));
      //Assert.AreEqual(362880, 9.SplitFactorial(), nameof(Flux.BinaryInteger.SplitFactorial));
      Assert.AreEqual(System.Numerics.BigInteger.Parse("8320987112741390144276341183223364380754172606361245952449277696409600000000000000"), IBinaryInteger.Factorial(new System.Numerics.BigInteger(60)), nameof(IBinaryInteger.Factorial));
      //Assert.AreEqual(479001600, 12.SplitFactorial(), nameof(Flux.BinaryInteger.SplitFactorial));
    }

    [TestMethod]
    public void GreatestCommonDivisor()
    {
      Assert.AreEqual(3, Flux.IBinaryInteger.GreatestCommonDivisor(21, 6));
    }

    [TestMethod]
    public void IntegerRootN()
    {
      Assert.AreEqual((3, 3), INumber.RootN(27, 3));
    }

    [TestMethod]
    public void IntegerSqrt()
    {
      Assert.AreEqual(4, IBinaryInteger.IntegerSqrt(21));
    }

    [TestMethod]
    public void IsCoprime()
    {
      Assert.IsTrue(Flux.IBinaryInteger.IsCoprime(23, 43));
    }

    [TestMethod]
    public void IntegerPow()
    {
      Assert.AreEqual(10000000000, IBinaryInteger.Pow(10L, 10));
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
      Assert.IsTrue(IBinaryInteger.IsPowOf(100, 10));
      Assert.IsFalse(IBinaryInteger.IsPowOf(101, 10));
    }

    [TestMethod]
    public void IsIntegerSqrt()
    {
      var v = 15;

      var iq = IBinaryInteger.IntegerSqrt(v);

      var isiq = IBinaryInteger.IsIntegerSqrt(v, iq);

      Assert.IsTrue(isiq);
    }

    [TestMethod]
    public void IsPerfectIntegerSqrt()
    {
      var v = 15;

      var iq = IBinaryInteger.IntegerSqrt(v);

      var ispiq = IBinaryInteger.IsPerfectIntegerSqrt(v, iq);

      Assert.IsFalse(ispiq);
    }

    [TestMethod]
    public void MaxDigitCount()
    {
      var actual = Flux.Units.Radix.GetMaxDigitCount(10, 10, false); // Yields 4, because a max value of 1023 can be represented (all bits can be used in an unsigned value).
      var expected = 4;
      Assert.AreEqual(expected, actual);

      actual = Flux.Units.Radix.GetMaxDigitCount(10, 10, true); // Yields 3, because a max value of 511 can be represented (excluding the MSB used for negative values of signed types).
      expected = 3;
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ModInv()
    {
      Assert.AreEqual(2, IBinaryInteger.ModInv(4, 7));
      Assert.AreEqual(7, IBinaryInteger.ModInv(8, 11));
    }

    [TestMethod]
    public void NearestMultiple()
    {
      var n = 512d;
      var m = 20;

      var (multipleTowardsZero, nNearestMultiple, multipleAwayFromZero) = INumber.MultipleOf(n, m, false, HalfRounding.AwayFromZero);
      //n.MultipleOfNearest(m, false, HalfRounding.AwayFromZero, out var multipleTowardsZero, out var multipleAwayFromZero);

      var nearestMultiple = INumber.RoundToNearest(n, HalfRounding.TowardZero, false, [multipleTowardsZero, multipleAwayFromZero]);

      Assert.AreEqual(520, nearestMultiple);

      Assert.AreEqual(500, multipleTowardsZero);
      Assert.AreEqual(520, multipleAwayFromZero);
    }
  }
}
#endif
