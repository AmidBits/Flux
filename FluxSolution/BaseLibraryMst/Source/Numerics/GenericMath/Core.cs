#if NET7_0_OR_GREATER
using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericMath
{
  [TestClass]
  public class Core
  {
    [TestMethod]
    public void DetentInterval()
    {
      Assert.AreEqual(520, 515.DetentInterval(20, 5, RoundingMode.HalfAwayFromZero));
    }

    [TestMethod]
    public void DetentPosition()
    {
      Assert.AreEqual(520, 515.DetentPosition(520, 5));
    }

    [TestMethod]
    public void DetentZero()
    {
      Assert.AreEqual(0, 4.DetentZero(5));
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
    public void Factorial()
    {
      var factorials = typeof(Flux.IFactorialComputable<>).GetDerivedTypes(typeof(Flux.IFactorialComputable<>).Assembly.DefinedTypes).Select(t => (Flux.IFactorialComputable<System.Numerics.BigInteger>)(t.IsGenericType ? t.CreateGenericInstance(typeof(System.Numerics.BigInteger)) : t.CreateInstance())).ToList<Flux.IFactorialComputable<System.Numerics.BigInteger>>();

      foreach (var factorial in factorials)
      {
        Assert.AreEqual(362880, factorial.ComputeFactorial(9), factorial.GetType().Name);
        Assert.AreEqual(System.Numerics.BigInteger.Parse("36471110918188685288249859096605464427167635314049524593701628500267962436943872000000000000000"), factorial.ComputeFactorial(67), factorial.GetType().Name);
        Assert.AreEqual(479001600, factorial.ComputeFactorial(12), factorial.GetType().Name);
        Assert.AreEqual(-479001600, factorial.ComputeFactorial(-12), factorial.GetType().Name);
      }
    }

    [TestMethod]
    public void GreatestCommonDivisor()
    {
      Assert.AreEqual(3, 21.ToBigInteger().GreatestCommonDivisor(6));
    }

    [TestMethod]
    public void IntegerSqrt()
    {
      Assert.AreEqual(4, 21.ToBigInteger().ISqrt());
    }

    [TestMethod]
    public void IsCoprime()
    {
      Assert.AreEqual(true, 23.IsCoprime(43));
    }

    [TestMethod]
    public void IntegerPow()
    {
      Assert.AreEqual(10000000000.ToBigInteger(), 10L.ToBigInteger().IPow(10));
    }

    [TestMethod]
    public void IntegerPowRec()
    {
      Assert.AreEqual(10000000000, 10L.ToBigInteger().IPowRec(10, out double reciprocal));
      Assert.AreEqual(1E-10, reciprocal);
    }

    [TestMethod]
    public void IsPow()
    {
      Assert.AreEqual(true, 100.ToBigInteger().IsPowOf(10));
      Assert.AreEqual(false, 101.ToBigInteger().IsPowOf(10));
    }

    [TestMethod]
    public void IsIntegerSqrt()
    {
      var v = 15.ToBigInteger();

      var iq = v.ISqrt();

      var isiq = v.IsISqrt(iq);

      Assert.IsTrue(isiq);
    }

    [TestMethod]
    public void IsPerfectIntegerSqrt()
    {
      var v = 15.ToBigInteger();

      var iq = v.ISqrt();

      var ispiq = v.IsPerfectISqrt(iq);

      Assert.IsFalse(ispiq);
    }

    [TestMethod]
    public void LeastCommonMultiple()
    {
      Assert.AreEqual(42, 21.ToBigInteger().LeastCommonMultiple(6));
    }

    [TestMethod]
    public void ModInv()
    {
      Assert.AreEqual(2, 4.ToBigInteger().ModInv(7));
      Assert.AreEqual(7, 8.ToBigInteger().ModInv(11));
    }

    [TestMethod]
    public void NearestMultiple()
    {
      var nearestMultiple = 512.NearestMultipleOf(20, false, RoundingMode.HalfTowardZero, out var nearestTowardsZero, out var nearestAwayFromZero);

      Assert.AreEqual(520, nearestMultiple);

      Assert.AreEqual(500, nearestTowardsZero);
      Assert.AreEqual(520, nearestAwayFromZero);
    }
  }
}
#endif
