using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;
using System;

namespace GenericMath
{
  [TestClass]
  public class String
  {
    [TestMethod]
    public void ToNamedGroupingString()
    {
      Assert.AreEqual("Five Hundred Twelve", 512.ToBigInteger().ToCompoundStringCardinalNumerals());
    }

    [TestMethod]
    public void ToOrdinalIndicatorString()
    {
      Assert.AreEqual("512th", 512.ToBigInteger().ToOrdinalIndicatorString());
    }

    [TestMethod]
    public void ToRadixString()
    {
      Assert.AreEqual("1000000000", 512.ToBigInteger().ToRadixString(2));
      Assert.AreEqual("512", 512.ToBigInteger().ToRadixString(10));
      Assert.AreEqual("200", 512.ToBigInteger().ToRadixString(16));
    }
  }
}
