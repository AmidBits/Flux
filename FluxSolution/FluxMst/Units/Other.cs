using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Units
{
  [TestClass]
  public class Other
  {
    [TestMethod]
    public void AbsoluteHumidity()
    {
      var u = new Flux.Quantities.AbsoluteHumidity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void AmplitudeRatio()
    {
      Assert.AreEqual(1.1220184543019633, Flux.Quantities.AmplitudeRatio.FromDecibelChange(1).Value);
      Assert.AreEqual(29.999237311923803, Flux.Quantities.AmplitudeRatio.From(new Flux.Quantities.ElectricPotential(31.62), new Flux.Quantities.ElectricPotential(1)).Value);
    }

    [TestMethod]
    public void Currency()
    {
      var u = new Flux.Quantities.Currency(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void DigitalInformation()
    {
      var u = new Flux.Quantities.DigitalInformation(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void PartsPerNotation()
    {
      var u = new Flux.Quantities.PartsPerNotation(1);

      Assert.AreEqual(0.01, u.Value);
    }

    [TestMethod]
    public void PowerRatio()
    {
      Assert.AreEqual(1.2589254117941673, Flux.Quantities.PowerRatio.FromDecibelChange(1).Value);
      Assert.AreEqual(30, Flux.Quantities.PowerRatio.From(new Flux.Quantities.Power(1000), new Flux.Quantities.Power(1)).Value);
      Assert.AreEqual(40, Flux.Quantities.PowerRatio.From(new Flux.Quantities.Power(10), new Flux.Quantities.Power(0.001)).Value);
    }

    [TestMethod]
    public void RelativeHumidity()
    {
      var u = new Flux.Quantities.RelativeHumidity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Turbidity()
    {
      var u = new Flux.Quantities.Turbidity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void UvIndex()
    {
      var u = new Flux.Quantities.UvIndex(1);

      Assert.AreEqual(1, u.Value);
    }
  }
}
