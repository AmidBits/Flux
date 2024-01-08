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
      Assert.AreEqual(29.999237311923803, Flux.Units.AmplitudeRatio.From(new Flux.Units.Voltage(31.62), new Flux.Units.Voltage(1)).Value);
    }

    [TestMethod]
    public void Azimuth()
    {
      var a = new Flux.Units.Angle(1, Flux.Units.AngleUnit.Degree);

      var u = new Flux.Units.Azimuth(a.Degrees);

      Assert.AreEqual(a.ToUnitValue(Flux.Units.AngleUnit.Degree), u.Value);
    }

    [TestMethod]
    public void Cent()
    {
      var cent = new Flux.Units.Cent(1);

      Assert.AreEqual(1, cent.Value);
      Assert.AreEqual(Flux.Units.Cent.FrequencyRatio, cent.ToFrequencyRatio());
    }

    [TestMethod]
    public void Latitude()
    {
      var a = new Flux.Units.Angle(1, Flux.Units.AngleUnit.Degree);

      var u = new Flux.Units.Latitude(a);

      Assert.AreEqual(a.ToUnitValue(Flux.Units.AngleUnit.Degree), u.Value);
    }

    [TestMethod]
    public void Longitude()
    {
      var a = new Flux.Units.Angle(1, Flux.Units.AngleUnit.Degree);

      var u = new Flux.Units.Longitude(a);

      Assert.AreEqual(a.ToUnitValue(Flux.Units.AngleUnit.Degree), u.Value);
    }

    [TestMethod]
    public void MidiNote()
    {
      var u = new Flux.Units.MidiNote(69);

      Assert.AreEqual(69, u.Value);
      Assert.AreEqual(4, u.GetOctave());
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
      var semitone = new Flux.Units.Semitone(1);

      Assert.AreEqual(1, semitone.Value);
      Assert.AreEqual(100, semitone.ToCent().Value);
      Assert.AreEqual(Flux.Units.Semitone.FrequencyRatio, semitone.ToFrequencyRatio());
    }
  }
}
