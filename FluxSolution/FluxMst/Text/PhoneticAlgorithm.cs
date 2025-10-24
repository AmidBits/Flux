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
      Assert.AreEqual("S620", new Flux.PhoneticAlgorithm.Soundex.AmericanSoundex().EncodePhonetic("Señor Hugo"));
    }

    [TestMethod]
    public void AmericanSoundex_WikiSamples()
    {
      var soundex = new Flux.PhoneticAlgorithm.Soundex.AmericanSoundex();

      Assert.AreEqual("R163", soundex.EncodePhonetic("Robert"));
      Assert.AreEqual("R163", soundex.EncodePhonetic("Rupert"));
      Assert.AreEqual("R150", soundex.EncodePhonetic("Rubin"));
      Assert.AreEqual("A261", soundex.EncodePhonetic("Ashcraft"));
      Assert.AreEqual("A261", soundex.EncodePhonetic("Ashcroft"));
      Assert.AreEqual("T522", soundex.EncodePhonetic("Tymczak"));
      Assert.AreEqual("P236", soundex.EncodePhonetic("Pfister"));
      Assert.AreEqual("H555", soundex.EncodePhonetic("Honeyman"));

    }

    [TestMethod]
    public void Metaphone_Default()
    {
      Assert.AreEqual("SNRHK", new Flux.PhoneticAlgorithm.Metaphone().EncodePhonetic("Señor Hugo"));
    }

    [TestMethod]
    public void Metaphone_NikitaSamples()
    {
      var metaphone = new Flux.PhoneticAlgorithm.Metaphone() { MaxCodeLength = 4 };

      Assert.AreEqual("FXPL", metaphone.EncodePhonetic("Fishpoole"));
      Assert.AreEqual("JLTL", metaphone.EncodePhonetic("Gellately"));
      Assert.AreEqual("LWRS", metaphone.EncodePhonetic("Lowerson"));
      Assert.AreEqual("MLBR", metaphone.EncodePhonetic("Melbourne"));
      Assert.AreEqual("MLBR", metaphone.EncodePhonetic("Mulberry"));
      Assert.AreEqual("SP", metaphone.EncodePhonetic("Sapp"));
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
      Assert.AreEqual("S309040", new Flux.PhoneticAlgorithm.Soundex.RefinedSoundex().EncodePhonetic("Señor Hugo"));
    }

    [TestMethod]
    public void RefinedSoundex_NikitaSamples()
    {
      var refinedSoundex = new Flux.PhoneticAlgorithm.Soundex.RefinedSoundex();

      Assert.AreEqual("B1905", refinedSoundex.EncodePhonetic("Braz"));
      Assert.AreEqual("B1905", refinedSoundex.EncodePhonetic("Broz"));
      Assert.AreEqual("C30908", refinedSoundex.EncodePhonetic("Caren"));
      Assert.AreEqual("C30908", refinedSoundex.EncodePhonetic("Corwin"));
      Assert.AreEqual("H093", refinedSoundex.EncodePhonetic("Hairs"));
      Assert.AreEqual("H093", refinedSoundex.EncodePhonetic("Hayers"));
      Assert.AreEqual("L7081096", refinedSoundex.EncodePhonetic("Lambert"));
      Assert.AreEqual("L7081096", refinedSoundex.EncodePhonetic("Lombard"));
      Assert.AreEqual("N807608", refinedSoundex.EncodePhonetic("Nolton"));
      Assert.AreEqual("N807608", refinedSoundex.EncodePhonetic("Noulton"));
    }
  }
}
