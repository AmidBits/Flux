﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Units
{
  [TestClass]
  public class MomentUtc
  {
    [TestMethod]
    public void ComputeJulianDateGC()
    {
      var now = new System.DateTime(2011, 11, 11, 11, 11, 11, 11);

      var m = new Flux.Units.MomentUtc(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, now.Millisecond);

      var expected = 2455876.9660996643518518518519;
      var actual = new Flux.Units.JulianDate(m.Year, m.Month, m.Day, m.Hour, m.Minute, m.Second, m.Millisecond, Flux.Units.TemporalCalendar.GregorianCalendar);

      Assert.AreEqual(expected, actual.Value, 1E-6);
    }
    [TestMethod]
    public void ComputeJulianDateJC()
    {
      var m = new Flux.Units.MomentUtc(-4712, 1, 13, 12, 13, 14);

      var expected = 12.009189814814814814814814815;
      var actual = new Flux.Units.JulianDate(m.Year, m.Month, m.Day, m.Hour, m.Minute, m.Second, m.Millisecond, Flux.Units.TemporalCalendar.JulianCalendar);

      Assert.AreEqual(expected, actual.Value, 1E-6);
    }
    [TestMethod]
    public void ComputeJulianDateTimeOfDay()
    {
      var m = new Flux.Units.MomentUtc(-4712, 1, 13, 12, 13, 14);

      Assert.AreEqual(0.0091898148148148148148148148, Flux.Units.JulianDate.ConvertFromTimeParts(m.Hour, m.Minute, m.Second, m.Millisecond), 1E-6);
    }
    [TestMethod]
    public void ComputeJulianDayNumberGC()
    {
      var now = new System.DateTime(2011, 11, 11);

      var m = new Flux.Units.MomentUtc(now.Year, now.Month, now.Day);

      Assert.AreEqual(2455877, Flux.Units.JulianDayNumber.ConvertFromDateParts(m.Year, m.Month, m.Day, Flux.Units.TemporalCalendar.GregorianCalendar));
    }
    [TestMethod]
    public void ComputeJulianDayNumberJC()
    {
      var m = new Flux.Units.MomentUtc(-4712, 1, 13);

      Assert.AreEqual(12, Flux.Units.JulianDayNumber.ConvertFromDateParts(m.Year, m.Month, m.Day, Flux.Units.TemporalCalendar.JulianCalendar));
    }

    [TestMethod]
    public void IsJulianCalendar()
    {
      var m4 = new Flux.Units.MomentUtc(1582, 10, 4, 12, 0, 0);
      Assert.AreEqual(true, Flux.Units.MomentUtc.IsJulianCalendar(m4.Year, m4.Month, m4.Day));

      var m5 = new Flux.Units.MomentUtc(1582, 10, 5, 12, 0, 0);
      Assert.AreEqual(false, Flux.Units.MomentUtc.IsJulianCalendar(m5.Year, m5.Month, m5.Day));
    }
    [TestMethod]
    public void IsGregorianCalendar()
    {
      var m14 = new Flux.Units.MomentUtc(1582, 10, 14, 12, 0, 0);
      Assert.AreEqual(false, Flux.Units.MomentUtc.IsGregorianCalendar(m14.Year, m14.Month, m14.Day));

      var m15 = new Flux.Units.MomentUtc(1582, 10, 15, 12, 0, 0);
      Assert.AreEqual(true, Flux.Units.MomentUtc.IsGregorianCalendar(m15.Year, m15.Month, m15.Day));
    }

    [TestMethod]
    public void IsValidGregorianCalendarDate()
    {
      var m14 = new Flux.Units.MomentUtc(1582, 10, 14);
      Assert.AreEqual(false, Flux.Units.MomentUtc.IsValidGregorianCalendarDate(m14.Year, m14.Month, m14.Day));

      var m15 = new Flux.Units.MomentUtc(1582, 10, 15);
      Assert.AreEqual(true, Flux.Units.MomentUtc.IsValidGregorianCalendarDate(m15.Year, m15.Month, m15.Day));
    }

    //[TestMethod]
    //public void TotalSeconds()
    //{
    //  var m = new Flux.MomentUtc(-4712, 1, 13, 12, 13, 14);

    //  Assert.AreEqual(-148601427194, m.TotalTime.Value);
    //}

    [TestMethod]
    public void ToJulianDateGC()
    {
      var jd1 = new Flux.Units.MomentUtc(1858, 11, 16, 12, 0, 0).ToJulianDate(Flux.Units.TemporalCalendar.GregorianCalendar);
      Assert.AreEqual(2400000, jd1.Value);
    }
    [TestMethod]
    public void ToJulianDateProlepticGC()
    {
      var jd1 = new Flux.Units.MomentUtc(-423, 10, 4, 12, 0, 0).ToJulianDate(Flux.Units.TemporalCalendar.GregorianCalendar);
      Assert.AreEqual(1566839, jd1.Value);
    }
    [TestMethod]
    public void ToJulianDateJC()
    {
      var jd1 = new Flux.Units.MomentUtc(-763, 3, 23, 12, 0, 0).ToJulianDate(Flux.Units.TemporalCalendar.JulianCalendar);
      Assert.AreEqual(1442454, jd1.Value);
    }
  }
}