using System;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Static
{
  [TestClass]
  public class Convert
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

      Assert.AreEqual(binS102, Flux.Convert.ToRadixString(binI102, 2));
      Assert.AreEqual(binI102, Flux.Convert.FromRadixString(binS102, 2));
    }

    [TestMethod]
    public void DecimalStrings()
    {
      string decS102 = @"102";
      System.Numerics.BigInteger decI102 = 102;

      Assert.AreEqual(decS102, Flux.Convert.ToRadixString(decI102, 10));
      Assert.AreEqual(decI102, Flux.Convert.FromRadixString(decS102, 10));
    }

    [TestMethod]
    public void HexStrings()
    {
      string hexS102 = @"66";
      System.Numerics.BigInteger hexI102 = 0x66;

      Assert.AreEqual(hexS102, Flux.Convert.ToRadixString(hexI102, 16));
      Assert.AreEqual(hexI102, Flux.Convert.FromRadixString(hexS102, 16));
    }
  }
}
