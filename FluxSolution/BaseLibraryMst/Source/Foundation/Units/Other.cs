using Flux;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundation.Units
{
  [TestClass]
  public class Other
  {
    [TestMethod]
    public void AbsoluteHumidity()
    {
      var u = new Flux.Quantity.AbsoluteHumidity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void AmplitudeRatio()
    {
      Assert.AreEqual(1.1220184543019633, Flux.Quantity.AmplitudeRatio.FromDecibelChange(1).Value);
      Assert.AreEqual(29.999237311923803, Flux.Quantity.AmplitudeRatio.From(new Flux.Quantity.Voltage(31.62), new Flux.Quantity.Voltage(1)).Value);
    }

    [TestMethod]
    public void Azimuth()
    {
      var a = Flux.Quantity.Angle.FromUnitValue(Flux.Quantity.AngleUnit.Degree, 1);

      var u = new Flux.Quantity.Azimuth(a);

      Assert.AreEqual(a, u.Angle);
    }

    [TestMethod]
    public void Cent()
    {
      var u = new Flux.Quantity.Cent(1);

      Assert.AreEqual(1, u.Cents);
      Assert.AreEqual(Flux.Quantity.Cent.FrequencyRatio, u.ToFrequencyRatio());
    }

    [TestMethod]
    public void Latitude()
    {
      var a = Flux.Quantity.Angle.FromUnitValue(Flux.Quantity.AngleUnit.Degree, 1);

      var u = new Flux.Quantity.Latitude(a);

      Assert.AreEqual(a, u.Angle);
    }

    [TestMethod]
    public void Longitude()
    {
      var a = Flux.Quantity.Angle.FromUnitValue(Flux.Quantity.AngleUnit.Degree, 1);

      var u = new Flux.Quantity.Longitude(a);

      Assert.AreEqual(a, u.Angle);
    }

    [TestMethod]
    public void MidiNote()
    {
      var u = new Flux.Quantity.MidiNote(69);

      Assert.AreEqual(69, u.Number);
      Assert.AreEqual(4, u.Octave);
      Assert.AreEqual(440.0, u.ToFrequency().Value);
    }

    [TestMethod]
    public void PowerRatio()
    {
      Assert.AreEqual(1.2589254117941673, Flux.Quantity.PowerRatio.FromDecibelChange(1).Value);
      Assert.AreEqual(30, Flux.Quantity.PowerRatio.From(new Flux.Quantity.Power(1000), new Flux.Quantity.Power(1)).Value);
      Assert.AreEqual(40, Flux.Quantity.PowerRatio.From(new Flux.Quantity.Power(10), new Flux.Quantity.Power(0.001)).Value);
    }

    [TestMethod]
    public void Probability()
    {
      var u = new Flux.Quantity.Probability(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void RelativeHumidity()
    {
      var u = new Flux.Quantity.RelativeHumidity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Semitone()
    {
      var u = new Flux.Quantity.Semitone(1);

      Assert.AreEqual(1, u.Semitones);
      Assert.AreEqual(100, u.ToCent().Cents);
      Assert.AreEqual(Flux.Quantity.Semitone.FrequencyRatio, u.ToFrequencyRatio());
    }
  }
}
