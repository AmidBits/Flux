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
      var u = new Flux.AbsoluteHumidity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void AmplitudeRatio()
    {
      Assert.AreEqual(1.1220184543019633, Flux.AmplitudeRatio.FromDecibelChange(1).Value);
      Assert.AreEqual(29.999237311923803, Flux.AmplitudeRatio.From(new Flux.Voltage(31.62), new Flux.Voltage(1)).Value);
    }

    [TestMethod]
    public void Azimuth()
    {
      var a = new Flux.Angle(1, Flux.AngleUnit.Degree);

      var u = a.ToAzimuth();

      Assert.AreEqual(a.ToUnitValue(Flux.AngleUnit.Degree), u.Value);
    }

    [TestMethod]
    public void Cent()
    {
      var cent = new Flux.Cent(1);

      Assert.AreEqual(1, cent.Value);
      Assert.AreEqual(Flux.Cent.FrequencyRatio, cent.ToFrequencyRatio());
    }

    [TestMethod]
    public void Latitude()
    {
      var a = new Flux.Angle(1, Flux.AngleUnit.Degree);

      var u = new Flux.Latitude(a);

      Assert.AreEqual(a.ToUnitValue(Flux.AngleUnit.Degree), u.Value);
    }

    [TestMethod]
    public void Longitude()
    {
      var a = new Flux.Angle(1, Flux.AngleUnit.Degree);

      var u = new Flux.Longitude(a);

      Assert.AreEqual(a.ToUnitValue(Flux.AngleUnit.Degree), u.Value);
    }

    [TestMethod]
    public void MidiNote()
    {
      var u = new Flux.Music.Midi.MidiNote(69);

      Assert.AreEqual(69, u.Value);
      Assert.AreEqual(4, u.GetOctave());
      Assert.AreEqual(440.0, u.ToFrequency().Value);
    }

    [TestMethod]
    public void PowerRatio()
    {
      Assert.AreEqual(1.2589254117941673, Flux.PowerRatio.FromDecibelChange(1).Value);
      Assert.AreEqual(30, Flux.PowerRatio.From(new Flux.Power(1000), new Flux.Power(1)).Value);
      Assert.AreEqual(40, Flux.PowerRatio.From(new Flux.Power(10), new Flux.Power(0.001)).Value);
    }

    [TestMethod]
    public void Probability()
    {
      var u = new Flux.Probability(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void RelativeHumidity()
    {
      var u = new Flux.RelativeHumidity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Semitone()
    {
      var semitone = new Flux.Semitone(1);

      Assert.AreEqual(1, semitone.Value);
      Assert.AreEqual(100, semitone.ToCent().Value);
      Assert.AreEqual(Flux.Semitone.FrequencyRatio, semitone.ToFrequencyRatio());
    }
  }
}
