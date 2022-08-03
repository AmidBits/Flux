using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace Types
{
  [TestClass]
  public class Char
  {
    [TestMethod]
    public void IsDiacriticalStroke()
    {
      Assert.AreEqual(false, ((System.Text.Rune)'ñ').IsDiacriticalLatinStroke());
      Assert.AreEqual(true, ((System.Text.Rune)'ø').IsDiacriticalLatinStroke());
      Assert.AreEqual(false, ((System.Text.Rune)'A').IsDiacriticalLatinStroke());
    }

    [TestMethod]
    public void IsEnglishConsonant()
    {
      Assert.AreEqual(false, ExtensionMethods.IsEnglishConsonant((System.Text.Rune)'ñ', true));
      Assert.AreEqual(true, ExtensionMethods.IsEnglishConsonant((System.Text.Rune)'t', true));
      Assert.AreEqual(false, ExtensionMethods.IsEnglishConsonant((System.Text.Rune)'A', true));
    }

    [TestMethod]
    public void IsEnglishLetter()
    {
      Assert.AreEqual(false, ExtensionMethods.IsEnglishLetter((System.Text.Rune)'ñ'));
      Assert.AreEqual(true, ExtensionMethods.IsEnglishLetter((System.Text.Rune)'t'));
      Assert.AreEqual(true, ExtensionMethods.IsEnglishLetter((System.Text.Rune)'A'));
    }

    [TestMethod]
    public void IsEnglishLetterLower()
    {
      Assert.AreEqual(true, ExtensionMethods.IsEnglishLetterLower((System.Text.Rune)'t'));
      Assert.AreEqual(false, ExtensionMethods.IsEnglishLetterLower((System.Text.Rune)'A'));
    }

    [TestMethod]
    public void IsEnglishLetterUpper()
    {
      Assert.AreEqual(false, ExtensionMethods.IsEnglishLetterUpper((System.Text.Rune)'t'));
      Assert.AreEqual(true, ExtensionMethods.IsEnglishLetterUpper((System.Text.Rune)'A'));
    }

    [TestMethod]
    public void IsEnglishVowel()
    {
      Assert.AreEqual(false, ExtensionMethods.IsEnglishVowel((System.Text.Rune)'ñ', true));
      Assert.AreEqual(false, ExtensionMethods.IsEnglishVowel((System.Text.Rune)'y', false));
      Assert.AreEqual(true, ExtensionMethods.IsEnglishVowel((System.Text.Rune)'y', true));
      Assert.AreEqual(true, ExtensionMethods.IsEnglishVowel((System.Text.Rune)'A', false));
    }
  }
}
