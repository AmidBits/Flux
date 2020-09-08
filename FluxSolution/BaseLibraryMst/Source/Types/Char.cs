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
      Assert.AreEqual(false, 'ñ'.IsDiacriticalStroke());
      Assert.AreEqual(true, 'ø'.IsDiacriticalStroke());
      Assert.AreEqual(false, 'A'.IsDiacriticalStroke());
    }

    [TestMethod]
    public void IsEnglishConsonant()
    {
      Assert.AreEqual(false, Flux.Globalization.EnUs.Language.IsEnglishConsonant('ñ', true));
      Assert.AreEqual(true, Flux.Globalization.EnUs.Language.IsEnglishConsonant('t', true));
      Assert.AreEqual(false, Flux.Globalization.EnUs.Language.IsEnglishConsonant('A', true));
    }

    [TestMethod]
    public void IsEnglishLetter()
    {
      Assert.AreEqual(false, Flux.Globalization.EnUs.Language.IsEnglishLetter('ñ'));
      Assert.AreEqual(true, Flux.Globalization.EnUs.Language.IsEnglishLetter('t'));
      Assert.AreEqual(true, Flux.Globalization.EnUs.Language.IsEnglishLetter('A'));
    }

    [TestMethod]
    public void IsEnglishLetterLower()
    {
      Assert.AreEqual(true, Flux.Globalization.EnUs.Language.IsEnglishLetterLower('t'));
      Assert.AreEqual(false, Flux.Globalization.EnUs.Language.IsEnglishLetterLower('A'));
    }

    [TestMethod]
    public void IsEnglishLetterUpper()
    {
      Assert.AreEqual(false, Flux.Globalization.EnUs.Language.IsEnglishLetterUpper('t'));
      Assert.AreEqual(true, Flux.Globalization.EnUs.Language.IsEnglishLetterUpper('A'));
    }

    [TestMethod]
    public void IsEnglishVowel()
    {
      Assert.AreEqual(false, Flux.Globalization.EnUs.Language.IsEnglishVowel('ñ', true));
      Assert.AreEqual(false, Flux.Globalization.EnUs.Language.IsEnglishVowel('y', false));
      Assert.AreEqual(true, Flux.Globalization.EnUs.Language.IsEnglishVowel('y', true));
      Assert.AreEqual(true, Flux.Globalization.EnUs.Language.IsEnglishVowel('A', false));
    }
  }
}
