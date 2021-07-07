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

      Assert.AreEqual(1, u.GramPerCubicMeter);
    }

    [TestMethod]
    public void AmplitudeRatio()
    {
      Assert.AreEqual(1.1220184543019633, Flux.Units.AmplitudeRatio.FromDecibelChange(1).DecibelVolt);
      Assert.AreEqual(29.999237311923803, Flux.Units.AmplitudeRatio.FromAmplitudeRatio(new Flux.Units.Voltage(31.62), new Flux.Units.Voltage(1)).DecibelVolt);
    }

    [TestMethod]
    public void Cent()
    {
      var u = new Flux.Units.Cent(1);

      Assert.AreEqual(1, u.Value);
      Assert.AreEqual(Flux.Units.Cent.FrequencyRatio, u.ToFrequencyRatio().Hertz);
    }

    [TestMethod]
    public void MidiNote()
    {
      var u = new Flux.Units.MidiNote(69);

      Assert.AreEqual(69, u.Number);
      Assert.AreEqual(4, u.Octave);
      Assert.AreEqual(440.0, u.ToFrequency().Hertz);
    }

    [TestMethod]
    public void PowerRatio()
    {
      Assert.AreEqual(1.2589254117941673, Flux.Units.PowerRatio.FromDecibelChange(1).DecibelWatt);
      Assert.AreEqual(30, Flux.Units.PowerRatio.FromPowerRatio(new Flux.Units.Power(1000), new Flux.Units.Power(1)).DecibelWatt);
      Assert.AreEqual(40, Flux.Units.PowerRatio.FromPowerRatio(new Flux.Units.Power(10), new Flux.Units.Power(0.001)).DecibelWatt);
    }

    [TestMethod]
    public void Probability()
    {
      var u = new Flux.Units.Probability(1);

      Assert.AreEqual(1, u.Ratio);
    }

    [TestMethod]
    public void RelativeHumidity()
    {
      var u = new Flux.Units.RelativeHumidity(1);

      Assert.AreEqual(1, u.Percent);
    }

    [TestMethod]
    public void Semitone()
    {
      var u = new Flux.Units.Semitone(1);

      Assert.AreEqual(1, u.Value);
      Assert.AreEqual(100, u.ToCent().Value);
      Assert.AreEqual(Flux.Units.Semitone.FrequencyRatio, u.ToFrequencyRatio().Hertz);
    }
  }
}
