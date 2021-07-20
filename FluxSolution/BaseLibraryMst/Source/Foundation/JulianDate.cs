using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace Foundation
{
  [TestClass]
  public class JulianDate
  {
    [TestMethod]
    public void ToMomentUtcGC()
    {
      var m1 = new Flux.JulianDate(2400000.5).ToMomentUtc(ConversionCalendar.GregorianCalendar);
      var m2 = new Flux.MomentUtc(1858, 11, 16, 12, 0, 0);
      Assert.AreEqual(m1, m2);
    }
    [TestMethod]
    public void ToMomentUtcProlepticGC()
    {
      var m1 = new Flux.JulianDate(1566839.5).ToMomentUtc(ConversionCalendar.GregorianCalendar);
      var m2 = new Flux.MomentUtc(-423, 10, 4, 12, 0, 0);
      Assert.AreEqual(m1, m2);
    }
    [TestMethod]
    public void ToMomentUtcJC()
    {
      var m1 = new Flux.JulianDate(1442454.5).ToMomentUtc(ConversionCalendar.JulianCalendar);
      var m2 = new Flux.MomentUtc(-763, 3, 23, 12, 0, 0);
      Assert.AreEqual(m1, m2);
    }

    [TestMethod]
    public void TimeIntervalBetweenTwo()
    {
      // Dependencies on MomentUtc for creation from dates.

      var jd1 = new Flux.MomentUtc(1910, 4, 20).ToJulianDate(ConversionCalendar.GregorianCalendar);
      var jd2 = new Flux.MomentUtc(1986, 2, 9).ToJulianDate(ConversionCalendar.GregorianCalendar);

      var diff12 = jd2.Value - jd1.Value;

      Assert.AreEqual(27689, diff12);
    }

    [TestMethod]
    public void ExactlyTenThousandDaysAfter()
    {
      // Dependencies on MomentUtc for creation from dates.

      var jd1 = new Flux.MomentUtc(1991, 7, 11).ToJulianDate(ConversionCalendar.GregorianCalendar);
      var jd2 = jd1.AddDays(10000);

      var diff12 = jd2.Value - jd1.Value;

      Assert.AreEqual(10000, diff12);
    }
  }
}
