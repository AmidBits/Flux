﻿using Flux;

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

      Assert.AreEqual(1, u.GeneralUnitValue);
    }

    [TestMethod]
    public void AmplitudeRatio()
    {
      Assert.AreEqual(1.1220184543019633, Flux.AmplitudeRatio.FromDecibelChange(1).GeneralUnitValue);
      Assert.AreEqual(29.999237311923803, Flux.AmplitudeRatio.From(new Flux.Voltage(31.62), new Flux.Voltage(1)).GeneralUnitValue);
    }

    [TestMethod]
    public void Bearing()
    {
      var a = new Flux.Angle(1, Flux.AngleUnit.Degree);

      var u = new Flux.Azimuth(a);

      Assert.AreEqual(a.ToUnitValue(Flux.AngleUnit.Degree), u.GeneralUnitValue);
    }

    [TestMethod]
    public void Cent()
    {
      var u = new Flux.Cent(1);

      Assert.AreEqual(1, u.Cents);
      Assert.AreEqual(Flux.Cent.FrequencyRatio, u.ToFrequencyRatio());
    }

    [TestMethod]
    public void Latitude()
    {
      var a = new Flux.Angle(1, Flux.AngleUnit.Degree);

      var u = new Flux.Latitude(a);

      Assert.AreEqual(a.ToUnitValue(Flux.AngleUnit.Degree), u.GeneralUnitValue);
    }

    [TestMethod]
    public void Longitude()
    {
      var a = new Flux.Angle(1, Flux.AngleUnit.Degree);

      var u = new Flux.Longitude(a);

      Assert.AreEqual(a.ToUnitValue(Flux.AngleUnit.Degree), u.GeneralUnitValue);
    }

    [TestMethod]
    public void MidiNote()
    {
      var u = new Flux.MidiNote(69);

      Assert.AreEqual(69, u.GeneralUnitValue);
      Assert.AreEqual(4, u.Octave);
      Assert.AreEqual(440.0, u.ToFrequency().GeneralUnitValue);
    }

    [TestMethod]
    public void PowerRatio()
    {
      Assert.AreEqual(1.2589254117941673, Flux.PowerRatio.FromDecibelChange(1).GeneralUnitValue);
      Assert.AreEqual(30, Flux.PowerRatio.From(new Flux.Power(1000), new Flux.Power(1)).GeneralUnitValue);
      Assert.AreEqual(40, Flux.PowerRatio.From(new Flux.Power(10), new Flux.Power(0.001)).GeneralUnitValue);
    }

    [TestMethod]
    public void Probability()
    {
      var u = new Flux.Probability(1);

      Assert.AreEqual(1, u.GeneralUnitValue);
    }

    [TestMethod]
    public void RelativeHumidity()
    {
      var u = new Flux.RelativeHumidity(1);

      Assert.AreEqual(1, u.GeneralUnitValue);
    }

    [TestMethod]
    public void Semitone()
    {
      var u = new Flux.Semitone(1);

      Assert.AreEqual(1, u.Semitones);
      Assert.AreEqual(100, u.ToCent().Cents);
      Assert.AreEqual(Flux.Semitone.FrequencyRatio, u.ToFrequencyRatio());
    }
  }
}