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

      var pnr2 = new Flux.Text.PositionalNotation(2);

      Assert.AreEqual(binS102, pnr2.NumberToText(binI102).ToString());
      Assert.AreEqual(binI102, pnr2.TextToNumber(binS102));
    }

    [TestMethod]
    public void DecimalStrings()
    {
      string decS102 = @"102";
      System.Numerics.BigInteger decI102 = 102;

      var pnr10 = new Flux.Text.PositionalNotation(10);

      Assert.AreEqual(decS102, pnr10.NumberToText(decI102).ToString());
      Assert.AreEqual(decI102, pnr10.TextToNumber(decS102));
    }

    [TestMethod]
    public void HexStrings()
    {
      string hexS102 = @"66";
      System.Numerics.BigInteger hexI102 = 0x66;

      var pnr16 = new Flux.Text.PositionalNotation(16);

      Assert.AreEqual(hexS102, pnr16.NumberToText(hexI102).ToString());
      Assert.AreEqual(hexI102, pnr16.TextToNumber(hexS102));
    }
  }
}
