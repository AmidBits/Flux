﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Units
{
  [TestClass]
  public class Music
  {
    [TestMethod]
    public void Cent()
    {
      var cent = new Flux.Quantities.Cent(1);

      Assert.AreEqual(1, cent.Value);
      Assert.AreEqual(Flux.Quantities.Cent.FrequencyRatio, Flux.Quantities.Cent.ConvertCentToFrequencyRatio(cent.Value));
    }

    [TestMethod]
    public void MidiNote()
    {
      var u = new Flux.Quantities.MidiNote(69);

      Assert.AreEqual(69, u.Value);
      Assert.AreEqual(4, u.Octave);
      Assert.AreEqual(440.0, u.ToFrequency().Value);
    }

    [TestMethod]
    public void Semitone()
    {
      var semitone = new Flux.Quantities.Semitone(1);

      Assert.AreEqual(1, semitone.Value);
      Assert.AreEqual(100, semitone.ToCent().Value);
      Assert.AreEqual(Flux.Quantities.Semitone.FrequencyRatio, Flux.Quantities.Semitone.ConvertSemitoneToFrequencyRatio(semitone.Value));
    }
  }
}