using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Units
{
  [TestClass]
  public class Numerics
  {
    [TestMethod]
    public void BigRational()
    {
      var u = new Flux.Numerics.BigRational(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Probability()
    {
      var u = new Flux.Units.Probability(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Radix()
    {
      var u = new Flux.Units.Radix(2);

      Assert.AreEqual(2, u.Value);
    }

    [TestMethod]
    public void UnitInterval()
    {
      var u = new Flux.Units.UnitInterval(1);

      Assert.AreEqual(1, u.Value);
    }
  }
}
