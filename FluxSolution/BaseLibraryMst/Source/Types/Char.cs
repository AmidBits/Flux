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
      Assert.AreEqual(false, GlobalizationEnUs.IsEnglishConsonant('ñ', true));
      Assert.AreEqual(true, GlobalizationEnUs.IsEnglishConsonant('t', true));
      Assert.AreEqual(false, GlobalizationEnUs.IsEnglishConsonant('A', true));
    }

    [TestMethod]
    public void IsEnglishLetter()
    {
      Assert.AreEqual(false, GlobalizationEnUs.IsEnglishLetter('ñ'));
      Assert.AreEqual(true, GlobalizationEnUs.IsEnglishLetter('t'));
      Assert.AreEqual(true, GlobalizationEnUs.IsEnglishLetter('A'));
    }

    [TestMethod]
    public void IsEnglishLetterLower()
    {
      Assert.AreEqual(true, GlobalizationEnUs.IsEnglishLowerCaseLetter('t'));
      Assert.AreEqual(false, GlobalizationEnUs.IsEnglishLowerCaseLetter('A'));
    }

    [TestMethod]
    public void IsEnglishLetterUpper()
    {
      Assert.AreEqual(false, GlobalizationEnUs.IsEnglishUpperCaseLetter('t'));
      Assert.AreEqual(true, GlobalizationEnUs.IsEnglishUpperCaseLetter('A'));
    }

    [TestMethod]
    public void IsEnglishVowel()
    {
      Assert.AreEqual(false, GlobalizationEnUs.IsEnglishVowel('ñ', true));
      Assert.AreEqual(false, GlobalizationEnUs.IsEnglishVowel('y', false));
      Assert.AreEqual(true, GlobalizationEnUs.IsEnglishVowel('y', true));
      Assert.AreEqual(true, GlobalizationEnUs.IsEnglishVowel('A', false));
    }
  }
}
