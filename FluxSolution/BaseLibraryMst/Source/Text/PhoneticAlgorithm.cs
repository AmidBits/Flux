using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Text
{
  [TestClass]
  public class PhoneticAlgorithm
  {
    [TestMethod]
    public void AmericanSoundex_Default()
    {
      Assert.AreEqual("S620", new Flux.Text.PhoneticAlgorithm.AmericanSoundex().EncodePhoneticAlgorithm("Señor Hugo"));
    }

    [TestMethod]
    public void AmericanSoundex_WikiSamples()
    {
      var soundex = new Flux.Text.PhoneticAlgorithm.AmericanSoundex();

      Assert.AreEqual("R163", soundex.EncodePhoneticAlgorithm("Robert"));
      Assert.AreEqual("R163", soundex.EncodePhoneticAlgorithm("Rupert"));
      Assert.AreEqual("R150", soundex.EncodePhoneticAlgorithm("Rubin"));
      Assert.AreEqual("A261", soundex.EncodePhoneticAlgorithm("Ashcraft"));
      Assert.AreEqual("A261", soundex.EncodePhoneticAlgorithm("Ashcroft"));
      Assert.AreEqual("T522", soundex.EncodePhoneticAlgorithm("Tymczak"));
      Assert.AreEqual("P236", soundex.EncodePhoneticAlgorithm("Pfister"));
      Assert.AreEqual("H555", soundex.EncodePhoneticAlgorithm("Honeyman"));

    }

    [TestMethod]
    public void Metaphone_Default()
    {
      Assert.AreEqual("SNRHK", new Flux.Text.PhoneticAlgorithm.Metaphone().EncodePhoneticAlgorithm("Señor Hugo"));
    }

    [TestMethod]
    public void Metaphone_NikitaSamples()
    {
      var metaphone = new Flux.Text.PhoneticAlgorithm.Metaphone() { MaxCodeLength = 4 };

      Assert.AreEqual("FXPL", metaphone.EncodePhoneticAlgorithm("Fishpoole"));
      Assert.AreEqual("JLTL", metaphone.EncodePhoneticAlgorithm("Gellately"));
      Assert.AreEqual("LWRS", metaphone.EncodePhoneticAlgorithm("Lowerson"));
      Assert.AreEqual("MLBR", metaphone.EncodePhoneticAlgorithm("Melbourne"));
      Assert.AreEqual("MLBR", metaphone.EncodePhoneticAlgorithm("Mulberry"));
      Assert.AreEqual("SP", metaphone.EncodePhoneticAlgorithm("Sapp"));
    }

    //  [TestMethod]
    //public void Nysiis_Default()
    //{
    //	Assert.AreEqual("SANARA", new Flux.Text.PhoneticAlgorithm.Nysiis().EncodePhoneticAlgorithm("Señor Hugo"));
    //}

    //[TestMethod]
    //public void Nysiis_NikitaSamples()
    //{
    //	var nysiis = new Flux.Text.PhoneticAlgorithm.Nysiis() { MaxCodeLength = 7 };

    //	Assert.AreEqual("DAGAL", nysiis.EncodePhoneticAlgorithm("Dougal"));
    //	Assert.AreEqual("DAGAL", nysiis.EncodePhoneticAlgorithm("Dowgill"));
    //	Assert.AreEqual("GLAND", nysiis.EncodePhoneticAlgorithm("Glinde"));
    //	Assert.AreEqual("PLANRAG", nysiis.EncodePhoneticAlgorithm("Plumridge"));
    //	//Assert.AreEqual("SANAC", nysiis.Encode("Chinnock"));
    //	Assert.AreEqual("SANAC", nysiis.EncodePhoneticAlgorithm("Simic"));
    //	Assert.AreEqual("WABARLY", nysiis.EncodePhoneticAlgorithm("Webberley"));
    //	Assert.AreEqual("WABARLY", nysiis.EncodePhoneticAlgorithm("Wibberley"));
    //}

    [TestMethod]
    public void RefinedSoundex_Default()
    {
      Assert.AreEqual("S309040", new Flux.Text.PhoneticAlgorithm.RefinedSoundex().EncodePhoneticAlgorithm("Señor Hugo"));
    }

    [TestMethod]
    public void RefinedSoundex_NikitaSamples()
    {
      var refinedSoundex = new Flux.Text.PhoneticAlgorithm.RefinedSoundex();

      Assert.AreEqual("B1905", refinedSoundex.EncodePhoneticAlgorithm("Braz"));
      Assert.AreEqual("B1905", refinedSoundex.EncodePhoneticAlgorithm("Broz"));
      Assert.AreEqual("C30908", refinedSoundex.EncodePhoneticAlgorithm("Caren"));
      Assert.AreEqual("C30908", refinedSoundex.EncodePhoneticAlgorithm("Corwin"));
      Assert.AreEqual("H093", refinedSoundex.EncodePhoneticAlgorithm("Hairs"));
      Assert.AreEqual("H093", refinedSoundex.EncodePhoneticAlgorithm("Hayers"));
      Assert.AreEqual("L7081096", refinedSoundex.EncodePhoneticAlgorithm("Lambert"));
      Assert.AreEqual("L7081096", refinedSoundex.EncodePhoneticAlgorithm("Lombard"));
      Assert.AreEqual("N807608", refinedSoundex.EncodePhoneticAlgorithm("Nolton"));
      Assert.AreEqual("N807608", refinedSoundex.EncodePhoneticAlgorithm("Noulton"));
    }
  }
}
