using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace Foundation
{
  [TestClass]
  public class MomentUtc
  {
    [TestMethod]
    public void IsGregorianCalendar()
    {
      var m14 = new Flux.MomentUtc(1582, 10, 14, 12, 0, 0);
      Assert.AreEqual(false, m14.IsGregorianCalendar);

      var m15 = new Flux.MomentUtc(1582, 10, 15, 12, 0, 0);
      Assert.AreEqual(true, m15.IsGregorianCalendar);
    }
    [TestMethod]
    public void IsJulianCalendar()
    {
      var m14 = new Flux.MomentUtc(1582, 10, 14, 12, 0, 0);
      Assert.AreEqual(true, m14.IsJulianCalendar);

      var m15 = new Flux.MomentUtc(1582, 10, 15, 12, 0, 0);
      Assert.AreEqual(false, m15.IsJulianCalendar);
    }

    [TestMethod]
    public void TimeOfDay()
    {
      var m = new Flux.MomentUtc(-4712, 1, 13, 12, 13, 14);

      Assert.AreEqual(0.5091898148148147, m.TimeOfDay.Second);
    }

    [TestMethod]
    public void TotalSeconds()
    {
      var m = new Flux.MomentUtc(-4712, 1, 13, 12, 13, 14);

      Assert.AreEqual(-148593836806, m.TotalSeconds.Second);
    }

    [TestMethod]
    public void ToJulianDateGC()
    {
      var jd1 = new Flux.MomentUtc(1858, 11, 16, 12, 0, 0).ToJulianDate(ConversionCalendar.GregorianCalendar);
      Assert.AreEqual(2400000.5, jd1.Value);
    }
    [TestMethod]
    public void ToJulianDateProlepticGC()
    {
      var jd1 = new Flux.MomentUtc(-423, 10, 4, 12, 0, 0).ToJulianDate(ConversionCalendar.GregorianCalendar);
      Assert.AreEqual(1566839.5, jd1.Value);
    }
    [TestMethod]
    public void ToJulianDateJC()
    {
      var jd1 = new Flux.MomentUtc(-763, 3, 23, 12, 0, 0).ToJulianDate(ConversionCalendar.JulianCalendar);
      Assert.AreEqual(1442454.5, jd1.Value);
    }
  }
}
