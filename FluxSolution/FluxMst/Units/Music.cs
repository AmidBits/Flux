using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Units
{
  [TestClass]
  public class Music
  {
    [TestMethod]
    public void Cent()
    {
      var cent = new Flux.Units.Cent(1);

      Assert.AreEqual(1, cent.Value);
      Assert.AreEqual(Flux.Units.Cent.FrequencyRatio, Flux.Units.Cent.ConvertCentToFrequencyRatio(cent.Value));
    }

    [TestMethod]
    public void MidiNote()
    {
      var u = new Flux.Units.MidiNote(69);

      Assert.AreEqual(69, u.Value);
      Assert.AreEqual(4, u.Octave);
      Assert.AreEqual(440.0, u.ToFrequency().Value);
    }

    [TestMethod]
    public void Semitone()
    {
      var semitone = new Flux.Units.Semitone(1);

      Assert.AreEqual(1, semitone.Value);
      Assert.AreEqual(100, semitone.ToCent().Value);
      Assert.AreEqual(Flux.Units.Semitone.FrequencyRatio, Flux.Units.Semitone.ConvertSemitoneToFrequencyRatio(semitone.Value));
    }
  }
}
