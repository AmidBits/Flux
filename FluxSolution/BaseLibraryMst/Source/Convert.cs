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

      Assert.AreEqual(65, Flux.Convert.ChangeType(c, null, typeof(int)));
      Assert.AreEqual(65D, Flux.Convert.ChangeType(c, null, typeof(int), typeof(double)));
    }

    [TestMethod]
    public void Em_Convert_TypeConverter()
    {
      var expected = "5/30/1967";
      var actual = Flux.Convert.TypeConverter<string>(System.DateTime.Parse(expected), null);

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void BinaryString1()
    {
      string binS102 = @"1100110";
      System.Numerics.BigInteger binI102 = 0b1100110;

      var ntt = Flux.PositionalNotation.NumberToText(binI102, Flux.PositionalNotation.Base64.Substring(0, 2), '\u002D').ToString();
      var ttn = Flux.PositionalNotation.TextToNumber(binS102, Flux.PositionalNotation.Base64.Substring(0, 2).ToCharArray(), '\u002D', out System.Numerics.BigInteger _);

      Assert.AreEqual(binS102, ntt);
      Assert.AreEqual(binI102, ttn);
    }

    [TestMethod]
    public void DecimalStrings()
    {
      string decS102 = @"102";
      System.Numerics.BigInteger decI102 = 102;

      Assert.AreEqual(decS102, Flux.PositionalNotation.NumberToText(decI102, Flux.PositionalNotation.Base64.Substring(0, 10), '\u002D').ToString());
      Assert.AreEqual(decI102, Flux.PositionalNotation.TextToNumber(decS102, Flux.PositionalNotation.Base64.Substring(0, 10).ToCharArray(), '\u002D', out System.Numerics.BigInteger _));
    }

    [TestMethod]
    public void HexStrings()
    {
      string hexS102 = @"66";
      System.Numerics.BigInteger hexI102 = 0x66;

      Assert.AreEqual(hexS102, Flux.PositionalNotation.NumberToText(hexI102, Flux.PositionalNotation.Base64.Substring(0, 16), '\u002D').ToString());
      Assert.AreEqual(hexI102, Flux.PositionalNotation.TextToNumber(hexS102, Flux.PositionalNotation.Base64.Substring(0, 16).ToCharArray(), '\u002D', out System.Numerics.BigInteger _));
    }
  }
}
