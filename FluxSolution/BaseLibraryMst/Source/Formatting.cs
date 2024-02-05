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
      Assert.AreEqual(_dms1 + 'N', Flux.Units.Angle.ToDmsString(_dms1tp, Flux.Units.AngleDmsFormat.DegreesMinutesDecimalSeconds, Flux.Units.CardinalAxis.NorthSouth, QuantifiableValueStringOptions.Default));
    }

    [TestMethod]
    public void LatitudeTryParse()
    {
      Assert.IsTrue(Flux.Units.Angle.TryParseDms(_dms1, out var dms1tp));

      Assert.AreEqual(_dms1tp, dms1tp.GetUnitValue(Flux.Units.AngleUnit.Degree));
    }

    [TestMethod]
    public void LongitudeFormatter()
    {
      Assert.AreEqual(_dms1 + 'E', Flux.Units.Angle.ToDmsString(_dms1tp, Flux.Units.AngleDmsFormat.DegreesMinutesDecimalSeconds, Flux.Units.CardinalAxis.EastWest, QuantifiableValueStringOptions.Default));
    }

    [TestMethod]
    public void LongitudeTryParse()
    {
      Assert.IsTrue(Flux.Units.Angle.TryParseDms(_dms1, out var dms1tp));

      Assert.AreEqual(_dms1tp, dms1tp.GetUnitValue(Flux.Units.AngleUnit.Degree));
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
