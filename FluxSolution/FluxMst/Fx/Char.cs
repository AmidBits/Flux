using Flux;

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
      Assert.AreEqual(false, System.Text.Rune.IsConsonant((System.Text.Rune)'ñ', System.Globalization.CultureInfo.CurrentCulture));
      Assert.AreEqual(true, System.Text.Rune.IsConsonant((System.Text.Rune)'t', System.Globalization.CultureInfo.CurrentCulture));
      Assert.AreEqual(false, System.Text.Rune.IsConsonant((System.Text.Rune)'A', System.Globalization.CultureInfo.CurrentCulture));
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
      Assert.AreEqual(false, System.Text.Rune.IsVowel((System.Text.Rune)'ñ', System.Globalization.CultureInfo.CurrentCulture));
      Assert.AreEqual(false, !System.Text.Rune.IsBasicLatinLetterY((System.Text.Rune)'y') && System.Text.Rune.IsVowel((System.Text.Rune)'y', System.Globalization.CultureInfo.CurrentCulture));
      Assert.AreEqual(true, System.Text.Rune.IsVowel((System.Text.Rune)'y', System.Globalization.CultureInfo.CurrentCulture));
      Assert.AreEqual(true, System.Text.Rune.IsVowel((System.Text.Rune)'A', System.Globalization.CultureInfo.CurrentCulture));
    }
  }
}
