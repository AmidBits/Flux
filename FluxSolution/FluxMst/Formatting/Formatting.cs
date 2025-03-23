using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Formatting
{
  [TestClass]
  public class Dms
  {
    [TestMethod]
    public void LatitudeFormatter()
    {
      var expected = $"40\u00B011\u203215\u2033N"; // Default is to use Unicode, so we test with Unicode.
      var actual = new Flux.Geodesy.Latitude(40.1875).ToDmsNotationString();

      var e = expected.ToCharArray(); // For comparing odd unicode choices.
      var a = actual.ToCharArray(); // For comparing odd unicode choices.

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LatitudeTryParse()
    {
      var expected = 40.1875;
      var actual = Flux.Geodesy.Latitude.ParseDmsNotation("40\u00B011\u2032 15\u2033 N").Value;

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongitudeFormatter()
    {
      var expected = $"40\u00B011\u203215\u2033W"; // Default is to use Unicode, so we test with Unicode.
      var actual = new Flux.Geodesy.Longitude(-40.1875).ToDmsNotationString();

      var e = expected.ToCharArray(); // For comparing odd unicode choices.
      var a = actual.ToCharArray(); // For comparing odd unicode choices.

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongitudeTryParse()
    {
      var expected = -40.1875;
      var actual = Flux.Geodesy.Longitude.ParseDmsNotation("40\u00B011\u2032 15\u2033 W").Value;

      Assert.AreEqual(expected, actual);
    }
  }

  //[TestClass]
  //public class Radix
  //{
  //  readonly System.Numerics.BigInteger _radix10 = 32;
  //  readonly string _radix16 = @"20";

  //  [TestMethod]
  //  public void Formatter()
  //  {
  //    Assert.AreEqual(_radix16, string.Format(new Flux.Formatting.RadixFormatter(), "{0:RADIX16}", _radix10));
  //  }
  //}
}
