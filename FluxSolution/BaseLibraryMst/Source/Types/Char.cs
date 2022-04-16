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
      Assert.AreEqual(false, GlobalizationEnUs.IsEnglishConsonant((System.Text.Rune)'ñ', true));
      Assert.AreEqual(true, GlobalizationEnUs.IsEnglishConsonant((System.Text.Rune)'t', true));
      Assert.AreEqual(false, GlobalizationEnUs.IsEnglishConsonant((System.Text.Rune)'A', true));
    }

    [TestMethod]
    public void IsEnglishLetter()
    {
      Assert.AreEqual(false, GlobalizationEnUs.IsEnglishLetter((System.Text.Rune)'ñ'));
      Assert.AreEqual(true, GlobalizationEnUs.IsEnglishLetter((System.Text.Rune)'t'));
      Assert.AreEqual(true, GlobalizationEnUs.IsEnglishLetter((System.Text.Rune)'A'));
    }

    [TestMethod]
    public void IsEnglishLetterLower()
    {
      Assert.AreEqual(true, GlobalizationEnUs.IsEnglishLetterLower((System.Text.Rune)'t'));
      Assert.AreEqual(false, GlobalizationEnUs.IsEnglishLetterLower((System.Text.Rune)'A'));
    }

    [TestMethod]
    public void IsEnglishLetterUpper()
    {
      Assert.AreEqual(false, GlobalizationEnUs.IsEnglishLetterUpper((System.Text.Rune)'t'));
      Assert.AreEqual(true, GlobalizationEnUs.IsEnglishLetterUpper((System.Text.Rune)'A'));
    }

    [TestMethod]
    public void IsEnglishVowel()
    {
      Assert.AreEqual(false, GlobalizationEnUs.IsEnglishVowel((System.Text.Rune)'ñ', true));
      Assert.AreEqual(false, GlobalizationEnUs.IsEnglishVowel((System.Text.Rune)'y', false));
      Assert.AreEqual(true, GlobalizationEnUs.IsEnglishVowel((System.Text.Rune)'y', true));
      Assert.AreEqual(true, GlobalizationEnUs.IsEnglishVowel((System.Text.Rune)'A', false));
    }
  }
}
