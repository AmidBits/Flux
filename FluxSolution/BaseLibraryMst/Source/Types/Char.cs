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
      Assert.AreEqual(false, 'ñ'.IsDiacriticalLatinStroke());
      Assert.AreEqual(true, 'ø'.IsDiacriticalLatinStroke());
      Assert.AreEqual(false, 'A'.IsDiacriticalLatinStroke());
    }

    [TestMethod]
    public void IsEnglishConsonant()
    {
      Assert.AreEqual(false, GlobalizationEnUsLanguage.IsEnglishConsonant('ñ', true));
      Assert.AreEqual(true, GlobalizationEnUsLanguage.IsEnglishConsonant('t', true));
      Assert.AreEqual(false, GlobalizationEnUsLanguage.IsEnglishConsonant('A', true));
    }

    [TestMethod]
    public void IsEnglishLetter()
    {
      Assert.AreEqual(false, GlobalizationEnUsLanguage.IsEnglishLetter('ñ'));
      Assert.AreEqual(true, GlobalizationEnUsLanguage.IsEnglishLetter('t'));
      Assert.AreEqual(true, GlobalizationEnUsLanguage.IsEnglishLetter('A'));
    }

    [TestMethod]
    public void IsEnglishLetterLower()
    {
      Assert.AreEqual(true, GlobalizationEnUsLanguage.IsEnglishLowerCaseLetter('t'));
      Assert.AreEqual(false, GlobalizationEnUsLanguage.IsEnglishLowerCaseLetter('A'));
    }

    [TestMethod]
    public void IsEnglishLetterUpper()
    {
      Assert.AreEqual(false, GlobalizationEnUsLanguage.IsEnglishUpperCaseLetter('t'));
      Assert.AreEqual(true, GlobalizationEnUsLanguage.IsEnglishUpperCaseLetter('A'));
    }

    [TestMethod]
    public void IsEnglishVowel()
    {
      Assert.AreEqual(false, GlobalizationEnUsLanguage.IsEnglishVowel('ñ', true));
      Assert.AreEqual(false, GlobalizationEnUsLanguage.IsEnglishVowel('y', false));
      Assert.AreEqual(true, GlobalizationEnUsLanguage.IsEnglishVowel('y', true));
      Assert.AreEqual(true, GlobalizationEnUsLanguage.IsEnglishVowel('A', false));
    }
  }
}
