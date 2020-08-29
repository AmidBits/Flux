using System;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Static
{
  [TestClass]
  public class Convert
  {
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
