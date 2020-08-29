using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Text
{
	[TestClass]
	public class PhoneticAlgorithm
  {
		[TestMethod]
		public void Metaphone_Default()
		{
			Assert.AreEqual("SNRHK", new Flux.Text.PhoneticAlgorithm.Metaphone().Encode("Señor Hugo"));
		}

		[TestMethod]
		public void Metaphone_NikitaSamples()
		{
			var metaphone = new Flux.Text.PhoneticAlgorithm.Metaphone() { MaxCodeLength = 4 };

			Assert.AreEqual("FXPL", metaphone.Encode("Fishpoole"));
			Assert.AreEqual("JLTL", metaphone.Encode("Gellately"));
			Assert.AreEqual("LWRS", metaphone.Encode("Lowerson"));
			Assert.AreEqual("MLBR", metaphone.Encode("Melbourne"));
			Assert.AreEqual("MLBR", metaphone.Encode("Mulberry"));
			Assert.AreEqual("SP", metaphone.Encode("Sapp"));
		}

    [TestMethod]
		public void Nysiis_Default()
		{
			Assert.AreEqual("SANARA", new Flux.Text.PhoneticAlgorithm.Nysiis().Encode("Señor Hugo"));
		}

		[TestMethod]
		public void Nysiis_NikitaSamples()
		{
			var nysiis = new Flux.Text.PhoneticAlgorithm.Nysiis() { MaxCodeLength = 7 };

			Assert.AreEqual("DAGAL", nysiis.Encode("Dougal"));
			Assert.AreEqual("DAGAL", nysiis.Encode("Dowgill"));
			Assert.AreEqual("GLAND", nysiis.Encode("Glinde"));
			Assert.AreEqual("PLANRAG", nysiis.Encode("Plumridge"));
			//Assert.AreEqual("SANAC", nysiis.Encode("Chinnock"));
			Assert.AreEqual("SANAC", nysiis.Encode("Simic"));
			Assert.AreEqual("WABARLY", nysiis.Encode("Webberley"));
			Assert.AreEqual("WABARLY", nysiis.Encode("Wibberley"));
		}

    [TestMethod]
 		public void RefinedSoundex_Default()
		{
			Assert.AreEqual("S309040", new Flux.Text.PhoneticAlgorithm.RefinedSoundex().Encode("Señor Hugo"));
		}

		[TestMethod]
		public void RefinedSoundex_NikitaSamples()
		{
			var refinedSoundex = new Flux.Text.PhoneticAlgorithm.RefinedSoundex();

			Assert.AreEqual("B1905", refinedSoundex.Encode("Braz"));
			Assert.AreEqual("B1905", refinedSoundex.Encode("Broz"));
			Assert.AreEqual("C30908", refinedSoundex.Encode("Caren"));
			Assert.AreEqual("C30908", refinedSoundex.Encode("Corwin"));
			Assert.AreEqual("H093", refinedSoundex.Encode("Hairs"));
			Assert.AreEqual("H093", refinedSoundex.Encode("Hayers"));
			Assert.AreEqual("L7081096", refinedSoundex.Encode("Lembert"));
			Assert.AreEqual("L7081096", refinedSoundex.Encode("Lombard"));
			Assert.AreEqual("N807608", refinedSoundex.Encode("Nolton"));
			Assert.AreEqual("N807608", refinedSoundex.Encode("Noulton"));
		}

    [TestMethod]
		public void Soundex_Default()
		{
			Assert.AreEqual("S620", new Flux.Text.PhoneticAlgorithm.Soundex().Encode("Señor Hugo"));
		}

		[TestMethod]
		public void Soundex_WikiSamples()
		{
			var soundex = new Flux.Text.PhoneticAlgorithm.Soundex();

			Assert.AreEqual("R163", soundex.Encode("Robert"));
			Assert.AreEqual("R163", soundex.Encode("Rupert"));
			Assert.AreEqual("R150", soundex.Encode("Rubin"));
			Assert.AreEqual("A261", soundex.Encode("Ashcraft"));
			Assert.AreEqual("A261", soundex.Encode("Ashcroft"));
			Assert.AreEqual("T522", soundex.Encode("Tymczak"));
			Assert.AreEqual("P236", soundex.Encode("Pfister"));
		}
	}
}
