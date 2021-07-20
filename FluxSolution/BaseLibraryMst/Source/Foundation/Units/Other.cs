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
      var u = new Flux.Units.AbsoluteHumidity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void AmplitudeRatio()
    {
      Assert.AreEqual(1.1220184543019633, Flux.Units.AmplitudeRatio.FromDecibelChange(1).Value);
      Assert.AreEqual(29.999237311923803, Flux.Units.AmplitudeRatio.From(new Flux.Units.Voltage(31.62), new Flux.Units.Voltage(1)).Value);
    }

    [TestMethod]
    public void Azimuth()
    {
      var a = Flux.Units.Angle.FromUnitValue(Flux.Units.AngleUnit.Degree, 1);

      var u = new Flux.Units.Azimuth(a);

      Assert.AreEqual(a, u.Angle);
    }

    [TestMethod]
    public void Cent()
    {
      var u = new Flux.Units.Cent(1);

      Assert.AreEqual(1, u.Cents);
      Assert.AreEqual(Flux.Units.Cent.FrequencyRatio, u.ToFrequencyRatio());
    }

    [TestMethod]
    public void Latitude()
    {
      var a = Flux.Units.Angle.FromUnitValue(Flux.Units.AngleUnit.Degree, 1);

      var u = new Flux.Units.Latitude(a);

      Assert.AreEqual(a, u.Angle);
    }

    [TestMethod]
    public void Longitude()
    {
      var a = Flux.Units.Angle.FromUnitValue(Flux.Units.AngleUnit.Degree, 1);

      var u = new Flux.Units.Longitude(a);

      Assert.AreEqual(a, u.Angle);
    }

    [TestMethod]
    public void MidiNote()
    {
      var u = new Flux.Units.MidiNote(69);

      Assert.AreEqual(69, u.Number);
      Assert.AreEqual(4, u.Octave);
      Assert.AreEqual(440.0, u.ToFrequency().Value);
    }

    [TestMethod]
    public void PowerRatio()
    {
      Assert.AreEqual(1.2589254117941673, Flux.Units.PowerRatio.FromDecibelChange(1).Value);
      Assert.AreEqual(30, Flux.Units.PowerRatio.From(new Flux.Units.Power(1000), new Flux.Units.Power(1)).Value);
      Assert.AreEqual(40, Flux.Units.PowerRatio.From(new Flux.Units.Power(10), new Flux.Units.Power(0.001)).Value);
    }

    [TestMethod]
    public void Probability()
    {
      var u = new Flux.Units.Probability(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void RelativeHumidity()
    {
      var u = new Flux.Units.RelativeHumidity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Semitone()
    {
      var u = new Flux.Units.Semitone(1);

      Assert.AreEqual(1, u.Semitones);
      Assert.AreEqual(100, u.ToCent().Cents);
      Assert.AreEqual(Flux.Units.Semitone.FrequencyRatio, u.ToFrequencyRatio());
    }
  }
}
