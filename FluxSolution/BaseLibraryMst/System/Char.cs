using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SystemFx
{
  [TestClass]
  public class Char
  {
    //[TestMethod]
    //public void IsDiacriticalStroke()
    //{
    //  Assert.AreEqual(false, ((System.Text.Rune)'ñ').IsUnicodeLatinStroke());
    //  Assert.AreEqual(true, ((System.Text.Rune)'ø').IsUnicodeLatinStroke());
    //  Assert.AreEqual(false, ((System.Text.Rune)'A').IsUnicodeLatinStroke());
    //}

    [TestMethod]
    public void IsEnglishConsonant()
    {
      Assert.AreEqual(false, System.Globalization.CultureInfo.CurrentCulture.IsConsonantOf((System.Text.Rune)'ñ'));
      Assert.AreEqual(true, System.Globalization.CultureInfo.CurrentCulture.IsConsonantOf((System.Text.Rune)'t'));
      Assert.AreEqual(false, System.Globalization.CultureInfo.CurrentCulture.IsConsonantOf((System.Text.Rune)'A'));
    }

    //[TestMethod]
    //public void IsEnglishLetter()
    //{
    //  Assert.AreEqual(false, ExtensionMethods.IsEnglishLetter((System.Text.Rune)'ñ'));
    //  Assert.AreEqual(true, ExtensionMethods.IsEnglishLetter((System.Text.Rune)'t'));
    //  Assert.AreEqual(true, ExtensionMethods.IsEnglishLetter((System.Text.Rune)'A'));
    //}

    //[TestMethod]
    //public void IsEnglishLetterLower()
    //{
    //  Assert.AreEqual(true, ExtensionMethods.IsEnglishLetterLower((System.Text.Rune)'t'));
    //  Assert.AreEqual(false, ExtensionMethods.IsEnglishLetterLower((System.Text.Rune)'A'));
    //}

    //[TestMethod]
    //public void IsEnglishLetterUpper()
    //{
    //  Assert.AreEqual(false, ExtensionMethods.IsEnglishLetterUpper((System.Text.Rune)'t'));
    //  Assert.AreEqual(true, ExtensionMethods.IsEnglishLetterUpper((System.Text.Rune)'A'));
    //}

    [TestMethod]
    public void IsEnglishVowel()
    {
      Assert.AreEqual(false, System.Globalization.CultureInfo.CurrentCulture.IsVowelOf((System.Text.Rune)'ñ'));
      Assert.AreEqual(false, !((System.Text.Rune)'y').IsBasicLatinLetterY() && System.Globalization.CultureInfo.CurrentCulture.IsVowelOf((System.Text.Rune)'y'));
      Assert.AreEqual(true, System.Globalization.CultureInfo.CurrentCulture.IsVowelOf((System.Text.Rune)'y'));
      Assert.AreEqual(true, System.Globalization.CultureInfo.CurrentCulture.IsVowelOf((System.Text.Rune)'A'));
    }
  }
}
