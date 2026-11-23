#if NET7_0_OR_GREATER
using Flux;

namespace GenericMath
{
  [TestClass]
  public class String
  {
    [TestMethod]
    public void ToNamedGroupingString()
    {
      Assert.AreEqual("Five Hundred Twelve", 512.ToCardinalNumeralString().ToString());
    }

    [TestMethod]
    public void ToOrdinalIndicatorString()
    {
      Assert.AreEqual("512th", BinaryInteger.ToOrdinalIndicatorString(512).ToString());
    }

    [TestMethod]
    public void ToRadixString()
    {
      Assert.AreEqual("1000000000", BinaryInteger.ToRadixString(512, 2).ToString());
      Assert.AreEqual("512", BinaryInteger.ToRadixString(512, 10).ToString());
      Assert.AreEqual("200", BinaryInteger.ToRadixString(512, 16).ToString());
    }
  }
}
#endif
