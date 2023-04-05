using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
      Assert.AreEqual(SexagesimalDegreeFormat.DegreesMinutesDecimalSeconds.ToSexagesimalDegreeString(_dms1tp, SexagesimalDegreeDirection.NorthSouth), _dms1 + 'N');
    }

    [TestMethod]
    public void LatitudeTryParse()
    {
      Assert.IsTrue(Flux.Quantities.Angle.TryParseSexagesimalDegrees(_dms1, out var dms1tp));

      Assert.AreEqual(_dms1tp, dms1tp.ToUnitValue(Flux.Quantities.AngleUnit.Degree));
    }

    [TestMethod]
    public void LongitudeFormatter()
    {
      Assert.AreEqual(SexagesimalDegreeFormat.DegreesMinutesDecimalSeconds.ToSexagesimalDegreeString(_dms1tp, SexagesimalDegreeDirection.WestEast), _dms1 + 'E');
    }

    [TestMethod]
    public void LongitudeTryParse()
    {
      Assert.IsTrue(Flux.Quantities.Angle.TryParseSexagesimalDegrees(_dms1, out var dms1tp));

      Assert.AreEqual(_dms1tp, dms1tp.ToUnitValue(Flux.Quantities.AngleUnit.Degree));
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
