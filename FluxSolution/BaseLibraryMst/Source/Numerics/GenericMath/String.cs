#if NET7_0_OR_GREATER
using System;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericMath
{
  [TestClass]
  public class String
  {
    [TestMethod]
    public void ToNamedGroupingString()
    {
      Assert.AreEqual("Five Hundred Twelve", 512.ToType(out System.Numerics.BigInteger _).ToCardinalNumeralCompoundString().ToString());
    }

    [TestMethod]
    public void ToOrdinalIndicatorString()
    {
      Assert.AreEqual("512th", 512.ToType(out System.Numerics.BigInteger _).ToOrdinalIndicatorString().ToString());
    }

    [TestMethod]
    public void ToRadixString()
    {
      Assert.AreEqual("1000000000", 512.ToType(out System.Numerics.BigInteger _).ToRadixString(2).ToString());
      Assert.AreEqual("512", 512.ToType(out System.Numerics.BigInteger _).ToRadixString(10).ToString());
      Assert.AreEqual("200", 512.ToType(out System.Numerics.BigInteger _).ToRadixString(16).ToString());
    }
  }
}
#endif
