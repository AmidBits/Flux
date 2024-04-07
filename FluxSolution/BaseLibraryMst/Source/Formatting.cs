using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Formatting
{
  [TestClass]
  public class Dms
  {
    readonly string _dms1 = "40\u00B011\u203215\u2033"; // Default is to use Unicode, so we test with Unicode.
    readonly double _dms1tp = 40.1875;

    [TestMethod]
    public void LatitudeFormatter()
    {
      var expected = $"40\u00B011\u203215\u2033N"; // Default is to use Unicode, so we test with Unicode.
      var actual = Flux.Quantities.Angle.ToDmsString(40.1875, Flux.Quantities.AngleDmsNotation.DegreesMinutesDecimalSeconds, Flux.Quantities.CompassCardinalAxis.NorthSouth);

      var e = expected.ToCharArray(); // For comparing odd unicode choices.
      var a = actual.ToCharArray(); // For comparing odd unicode choices.

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LatitudeTryParse()
    {
      Assert.IsTrue(Flux.Quantities.Angle.TryParseDms(_dms1, out var dms1tp));

      Assert.AreEqual(_dms1tp, dms1tp.GetUnitValue(Flux.Quantities.AngleUnit.Degree));
    }

    [TestMethod]
    public void LongitudeFormatter()
    {
      var expected = $"40\u00B011\u203215\u2033E"; // Default is to use Unicode, so we test with Unicode.
      var actual = Flux.Quantities.Angle.ToDmsString(40.1875, Flux.Quantities.AngleDmsNotation.DegreesMinutesDecimalSeconds, Flux.Quantities.CompassCardinalAxis.EastWest);

      var e = expected.ToCharArray(); // For comparing odd unicode choices.
      var a = actual.ToCharArray(); // For comparing odd unicode choices.

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongitudeTryParse()
    {
      Assert.IsTrue(Flux.Quantities.Angle.TryParseDms(_dms1, out var dms1tp));

      Assert.AreEqual(_dms1tp, dms1tp.GetUnitValue(Flux.Quantities.AngleUnit.Degree));
    }
  }

  [TestClass]
  public class Radix
  {
    readonly System.Numerics.BigInteger _radix10 = 32;
    readonly string _radix16 = @"20";

    [TestMethod]
    public void Formatter()
    {
      Assert.AreEqual(_radix16, string.Format(new Flux.Formatting.RadixFormatter(), "{0:RADIX16}", _radix10));
    }
  }
}
