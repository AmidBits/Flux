using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Units
{
  [TestClass]
  public class Other
  {
    [TestMethod]
    public void AbsoluteHumidity()
    {
      var u = new Flux.Units.AbsoluteHumidity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void AmplitudeRatio()
    {
      Assert.AreEqual(1.1220184543019633, Flux.Units.AmplitudeRatio.FromDecibelChange(1).Value);
      Assert.AreEqual(29.999237311923803, Flux.Units.AmplitudeRatio.From(new Flux.Units.ElectricPotential(31.62), new Flux.Units.ElectricPotential(1)).Value);
    }

    [TestMethod]
    public void Currency()
    {
      var u = new Flux.Units.Currency(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void DigitalInformation()
    {
      var u = new Flux.Units.DigitalInformation(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void PartsPerNotation()
    {
      var u = new Flux.Units.PartsPerNotation(1);

      Assert.AreEqual(0.01, u.Value);
    }

    [TestMethod]
    public void PowerRatio()
    {
      Assert.AreEqual(1.2589254117941673, Flux.Units.PowerRatio.FromDecibelChange(1).Value);
      Assert.AreEqual(30, Flux.Units.PowerRatio.From(new Flux.Units.Power(1000), new Flux.Units.Power(1)).Value);
      Assert.AreEqual(40, Flux.Units.PowerRatio.From(new Flux.Units.Power(10), new Flux.Units.Power(0.001)).Value);
    }

    [TestMethod]
    public void RelativeHumidity()
    {
      var u = new Flux.Units.RelativeHumidity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Turbidity()
    {
      var u = new Flux.Units.Turbidity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void UvIndex()
    {
      var u = new Flux.Units.UvIndex(1);

      Assert.AreEqual(1, u.Value);
    }
  }
}
