using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Units
{
  [TestClass]
  public class JulianDate
  {
    [TestMethod]
    public void ComputeJulianPeriod()
    {
      Assert.AreEqual(2015, Flux.Temporal.JulianDayNumber.GetJulianPeriod(8, 2, 8));
    }

    [TestMethod]
    public void ComputeTimeOfDay()
    {
      var m = new Flux.Temporal.Moment(-4712, 1, 13, 12, 13, 14).ToJulianDate(Flux.Temporal.TemporalCalendar.JulianCalendar);

      Assert.AreEqual(794.0000000000000000000000160, Flux.Temporal.JulianDate.ConvertTimeOfDayToTime(m.Value).Value, 1E-6);
    }

    [TestMethod]
    public void FirstGregorianCalendarDate()
    {
      var fgd = new Flux.Temporal.Moment(1582, 10, 15, 0, 0, 0).ToJulianDate(Flux.Temporal.TemporalCalendar.GregorianCalendar);

      Assert.AreEqual(2299160.5, fgd.Value, 1E-6);
    }
    [TestMethod]
    public void FirstJulianCalendarDate()
    {
      var fjd = new Flux.Temporal.JulianDate(0);

      Assert.AreEqual(0, fjd.Value);
    }
    [TestMethod]
    public void LastJulianCalendarDate()
    {
      var ljd = new Flux.Temporal.Moment(1582, 10, 4, 23, 59, 59, 999).ToJulianDate(Flux.Temporal.TemporalCalendar.JulianCalendar);

      Assert.AreEqual(2299160.4999910995370370370370, ljd.Value, 1E-5);
    }

    [TestMethod]
    public void AddDays()
    {
      var jd = new Flux.Temporal.JulianDate(0).AddDays(1);

      Assert.AreEqual(1, jd.Value);
    }
    [TestMethod]
    public void AddHours()
    {
      var jd = new Flux.Temporal.JulianDate(0).AddHours(1);

      Assert.AreEqual(0.0416666666666666666666666667, jd.Value, 1E-6);
    }
    [TestMethod]
    public void AddMillieconds()
    {
      var jd = new Flux.Temporal.JulianDate(0).AddMilliseconds(1);

      Assert.AreEqual(0.0000000115740740740740740741, jd.Value, 1E-6);
    }
    [TestMethod]
    public void AddMinutes()
    {
      var jd = new Flux.Temporal.JulianDate(0).AddMinutes(1);

      Assert.AreEqual(0.0006944444444444444444444444, jd.Value, 1E-6);
    }
    [TestMethod]
    public void AddSeconds()
    {
      var jd = new Flux.Temporal.JulianDate(0).AddSeconds(1);

      Assert.AreEqual(0.0000115740740740740740740741, jd.Value, 1E-6);
    }

    [TestMethod]
    public void DayOfWeek()
    {
      var jd1 = new Flux.Temporal.Moment(1991, 7, 11).ToJulianDayNumber(Flux.Temporal.TemporalCalendar.GregorianCalendar);

      Assert.AreEqual(System.DayOfWeek.Thursday, jd1.DayOfWeek);
    }

    [TestMethod]
    public void ExactlyTenThousandDaysAfter()
    {
      // Dependencies on MomentUtc for creation from dates.

      var jd1 = new Flux.Temporal.Moment(1991, 7, 11).ToJulianDate(Flux.Temporal.TemporalCalendar.GregorianCalendar);
      var jd2 = jd1.AddDays(10000);

      var diff12 = jd2.Value - jd1.Value;

      Assert.AreEqual(10000, diff12);
    }

    [TestMethod]
    public void TimeIntervalBetweenTwo()
    {
      // Dependencies on MomentUtc for creation from dates.

      var jd1 = new Flux.Temporal.Moment(1910, 4, 20).ToJulianDate(Flux.Temporal.TemporalCalendar.GregorianCalendar);
      var jd2 = new Flux.Temporal.Moment(1986, 2, 9).ToJulianDate(Flux.Temporal.TemporalCalendar.GregorianCalendar);

      var diff12 = jd2.Value - jd1.Value;

      Assert.AreEqual(27689, diff12);
    }

    [TestMethod]
    public void ToMomentUtcGC()
    {
      var actual = new Flux.Temporal.JulianDate(2400000.5).ToMoment(Flux.Temporal.TemporalCalendar.GregorianCalendar);
      var expected = new Flux.Temporal.Moment(1858, 11, 17, 0, 0, 0);
      Assert.AreEqual(expected, actual);
    }
    [TestMethod]
    public void ToMomentUtcProlepticGC()
    {
      var actual = new Flux.Temporal.JulianDate(1566839.5).ToMoment(Flux.Temporal.TemporalCalendar.GregorianCalendar);
      var expected = new Flux.Temporal.Moment(-423, 10, 5, 0, 0, 0);
      Assert.AreEqual(expected, actual);
    }
    [TestMethod]
    public void ToMomentUtcJC()
    {
      var actual = new Flux.Temporal.JulianDate(1442454.5).ToMoment(Flux.Temporal.TemporalCalendar.JulianCalendar);
      var expected = new Flux.Temporal.Moment(-763, 3, 24, 0, 0, 0);
      Assert.AreEqual(expected, actual);
    }
  }
}
