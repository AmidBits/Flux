using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace Formatting
{
  [TestClass]
  public class Dms
  {
    readonly string _dms1 = "40\u00B011'15\"";
    readonly double _dms1tp = 40.1875;

    [TestMethod]
    public void LatitudeFormatter()
    {
      Assert.AreEqual(new Angle(_dms1tp, AngleUnit.Degree).ToSexagesimalDegreeString(SexagesimalDegreeFormat.DegreesMinutesDecimalSeconds, SexagesimalDegreeDirection.NorthSouth), _dms1 + 'N');
    }

    [TestMethod]
    public void LatitudeTryParse()
    {
      Assert.IsTrue(Flux.Angle.TryParseSexagesimalDegrees(_dms1, out var dms1tp));

      Assert.AreEqual(_dms1tp, dms1tp.ToUnitValue(AngleUnit.Degree));
    }

    [TestMethod]
    public void LongitudeFormatter()
    {
      Assert.AreEqual(new Angle(_dms1tp, AngleUnit.Degree).ToSexagesimalDegreeString(SexagesimalDegreeFormat.DegreesMinutesDecimalSeconds, SexagesimalDegreeDirection.WestEast), _dms1 + 'E');
    }

    [TestMethod]
    public void LongitudeTryParse()
    {
      Assert.IsTrue(Flux.Angle.TryParseSexagesimalDegrees(_dms1, out var dms1tp));

      Assert.AreEqual(_dms1tp, dms1tp.ToUnitValue(AngleUnit.Degree));
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
