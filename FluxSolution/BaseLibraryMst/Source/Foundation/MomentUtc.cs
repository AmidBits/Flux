using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace Foundation
{
  [TestClass]
  public class MomentUtc
  {
    [TestMethod]
    public void ComputeJulianDateTimeOfDay()
    {
      var m = new Flux.MomentUtc(-4712, 1, 13, 12, 13, 14);

      Assert.AreEqual(0.5091898148148147, Flux.MomentUtc.ComputeJulianDateTimeOfDay(m.Hour, m.Minute, m.Second, m.Millisecond));
    }

    [TestMethod]
    public void InJulianCalendarEra()
    {
      var m4 = new Flux.MomentUtc(1582, 10, 4, 12, 0, 0);
      Assert.AreEqual(true, Flux.MomentUtc.InJulianCalendarEra(m4.Year, m4.Month, m4.Day));

      var m5 = new Flux.MomentUtc(1582, 10, 5, 12, 0, 0);
      Assert.AreEqual(false, Flux.MomentUtc.InJulianCalendarEra(m5.Year, m5.Month, m5.Day));
    }
    [TestMethod]
    public void InGregorianCalendarEra()
    {
      var m14 = new Flux.MomentUtc(1582, 10, 14, 12, 0, 0);
      Assert.AreEqual(false, Flux.MomentUtc.InGregorianCalendarEra(m14.Year, m14.Month, m14.Day));

      var m15 = new Flux.MomentUtc(1582, 10, 15, 12, 0, 0);
      Assert.AreEqual(true, Flux.MomentUtc.InGregorianCalendarEra(m15.Year, m15.Month, m15.Day));
    }

    [TestMethod]
    public void IsGregorianCalendar()
    {
      var m14 = new Flux.MomentUtc(1582, 10, 14, 12, 0, 0);
      Assert.AreEqual(false, m14.InGregorianCalendar);

      var m15 = new Flux.MomentUtc(1582, 10, 15, 12, 0, 0);
      Assert.AreEqual(true, m15.InGregorianCalendar);
    }
    [TestMethod]
    public void IsJulianCalendar()
    {
      var m14 = new Flux.MomentUtc(1582, 10, 4, 12, 0, 0);
      Assert.AreEqual(true, m14.InJulianCalendar);

      var m15 = new Flux.MomentUtc(1582, 10, 15, 12, 0, 0);
      Assert.AreEqual(false, m15.InJulianCalendar);
    }

    [TestMethod]
    public void TotalSeconds()
    {
      var m = new Flux.MomentUtc(-4712, 1, 13, 12, 13, 14);

      Assert.AreEqual(-148601427194, m.TotalSeconds.Value);
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
