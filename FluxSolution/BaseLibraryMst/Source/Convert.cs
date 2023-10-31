using System;
using Flux;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Static
{
  [TestClass]
  public class Units
  {
    [TestMethod]
    public void Em_Convert_ChangeType()
    {
      var c = 'A';

      Assert.AreEqual(65, Flux.Convert.ChangeType<byte>(c, null));
      Assert.AreEqual(65D, Flux.Convert.ChangeType(c, null, new System.Type[] { typeof(int), typeof(double) }));
    }

    [TestMethod]
    public void Em_Convert_TypeConverter()
    {
      Assert.AreEqual("5/30/1967", Flux.Convert.TypeConverter<string>(System.DateTime.Parse("05/30/1967"), null));
    }

    [TestMethod]
    public void BinaryStrings()
    {
      string binS102 = @"1100110";
      System.Numerics.BigInteger binI102 = 0b1100110;

      Assert.AreEqual(binS102, Flux.Text.PositionalNotation.NumberToText(binI102, Flux.Text.PositionalNotation.Base64.Substring(0, 2), (char)UnicodeCodepoint.HyphenMinus).ToString());
      Assert.AreEqual(binI102, Flux.Text.PositionalNotation.TextToNumber(binS102, Flux.Text.PositionalNotation.Base64.Substring(0, 2).ToCharArray(), (char)UnicodeCodepoint.HyphenMinus, out System.Numerics.BigInteger _));
    }

    [TestMethod]
    public void DecimalStrings()
    {
      string decS102 = @"102";
      System.Numerics.BigInteger decI102 = 102;

      Assert.AreEqual(decS102, Flux.Text.PositionalNotation.NumberToText(decI102, Flux.Text.PositionalNotation.Base64.Substring(0, 10), (char)UnicodeCodepoint.HyphenMinus).ToString());
      Assert.AreEqual(decI102, Flux.Text.PositionalNotation.TextToNumber(decS102, Flux.Text.PositionalNotation.Base64.Substring(0, 10).ToCharArray(), (char)UnicodeCodepoint.HyphenMinus, out System.Numerics.BigInteger _));
    }

    [TestMethod]
    public void HexStrings()
    {
      string hexS102 = @"66";
      System.Numerics.BigInteger hexI102 = 0x66;

      Assert.AreEqual(hexS102, Flux.Text.PositionalNotation.NumberToText(hexI102, Flux.Text.PositionalNotation.Base64.Substring(0, 16), (char)UnicodeCodepoint.HyphenMinus).ToString());
      Assert.AreEqual(hexI102, Flux.Text.PositionalNotation.TextToNumber(hexS102, Flux.Text.PositionalNotation.Base64.Substring(0, 16).ToCharArray(), (char)UnicodeCodepoint.HyphenMinus, out System.Numerics.BigInteger _));
    }
  }
}
