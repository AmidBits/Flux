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
      Assert.AreEqual(29.999237311923803, Flux.Quantities.AmplitudeRatio.From(new Flux.Quantities.Voltage(31.62), new Flux.Quantities.Voltage(1)).Value);
    }

    [TestMethod]
    public void Azimuth()
    {
      var a = new Flux.Quantities.Angle(1, Flux.Quantities.AngleUnit.Degree);

      var u = new Flux.Quantities.Azimuth(a);

      Assert.AreEqual(a.GetUnitValue(Flux.Quantities.AngleUnit.Degree), u.Value);
    }

    [TestMethod]
    public void Cent()
    {
      var cent = new Flux.Quantities.Cent(1);

      Assert.AreEqual(1, cent.Value);
      Assert.AreEqual(Flux.Quantities.Cent.FrequencyRatio, cent.ToFrequencyRatio());
    }

    [TestMethod]
    public void Latitude()
    {
      var a = new Flux.Quantities.Angle(1, Flux.Quantities.AngleUnit.Degree);

      var u = new Flux.Quantities.Latitude(a);

      Assert.AreEqual(a.GetUnitValue(Flux.Quantities.AngleUnit.Degree), u.Value);
    }

    [TestMethod]
    public void Longitude()
    {
      var a = new Flux.Quantities.Angle(1, Flux.Quantities.AngleUnit.Degree);

      var u = new Flux.Quantities.Longitude(a);

      Assert.AreEqual(a.GetUnitValue(Flux.Quantities.AngleUnit.Degree), u.Value);
    }

    [TestMethod]
    public void MidiNote()
    {
      var u = new Flux.Quantities.MidiNote(69);

      Assert.AreEqual(69, u.Value);
      Assert.AreEqual(4, u.GetOctave());
      Assert.AreEqual(440.0, u.ToFrequency().Value);
    }

    [TestMethod]
    public void PowerRatio()
    {
      Assert.AreEqual(1.2589254117941673, Flux.Quantities.PowerRatio.FromDecibelChange(1).Value);
      Assert.AreEqual(30, Flux.Quantities.PowerRatio.From(new Flux.Quantities.Power(1000), new Flux.Quantities.Power(1)).Value);
      Assert.AreEqual(40, Flux.Quantities.PowerRatio.From(new Flux.Quantities.Power(10), new Flux.Quantities.Power(0.001)).Value);
    }

    [TestMethod]
    public void Probability()
    {
      var u = new Flux.Quantities.Probability(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void RelativeHumidity()
    {
      var u = new Flux.Quantities.RelativeHumidity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Semitone()
    {
      var semitone = new Flux.Quantities.Semitone(1);

      Assert.AreEqual(1, semitone.Value);
      Assert.AreEqual(100, semitone.ToCent().Value);
      Assert.AreEqual(Flux.Quantities.Semitone.FrequencyRatio, semitone.ToFrequencyRatio());
    }
  }
}
