﻿using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace Foundation
{
  [TestClass]
  public class JulianDate
  {
    [TestMethod]
    public void ComputeJulianPeriod()
    {
      Assert.AreEqual(2015, Flux.JulianDate.ComputeJulianPeriod(8, 2, 8));
    }

    [TestMethod]
    public void IsGregorianCalendar()
    {
      Assert.AreEqual(true, Flux.JulianDate.FirstGregorianDate.IsGregorianCalendar);
      Assert.AreEqual(false, Flux.JulianDate.FirstJulianDate.IsGregorianCalendar);
      Assert.AreEqual(false, Flux.JulianDate.LastJulianDate.IsGregorianCalendar);
    }

    [TestMethod]
    public void FirstGregorianDate()
    {
      var fgd = Flux.JulianDate.FirstGregorianDate;

      Assert.AreEqual(2299161.5, fgd.Value);
    }
    [TestMethod]
    public void FirstJulianDate()
    {
      var fjd = Flux.JulianDate.FirstJulianDate;

      Assert.AreEqual(0.5, fjd.Value);
    }
    [TestMethod]
    public void LastJulianDate()
    {
      var ljd = Flux.JulianDate.LastJulianDate;

      Assert.AreEqual(2299160.5, ljd.Value);
    }

    [TestMethod]
    public void AddDays()
    {
      var jd = new Flux.JulianDate(0).AddDays(1);

      Assert.AreEqual(1, jd.Value);
    }
    [TestMethod]
    public void AddHours()
    {
      var jd = new Flux.JulianDate(0).AddHours(1);

      Assert.AreEqual(0.041666666666666664, jd.Value);
    }
    [TestMethod]
    public void AddMillieconds()
    {
      var jd = new Flux.JulianDate(0).AddMillieconds(1);

      Assert.AreEqual(1.1574074074074074E-08, jd.Value);
    }
    [TestMethod]
    public void AddMinutes()
    {
      var jd = new Flux.JulianDate(0).AddMinutes(1);

      Assert.AreEqual(0.0006944444444444445, jd.Value);
    }
    [TestMethod]
    public void AddSeconds()
    {
      var jd = new Flux.JulianDate(0).AddSeconds(1);

      Assert.AreEqual(1.1574074074074073E-05, jd.Value);
    }
    [TestMethod]
    public void AddWeeks()
    {
      var jd = new Flux.JulianDate(0).AddWeeks(1);

      Assert.AreEqual(7, jd.Value);
    }

    [TestMethod]
    public void DayOfWeek()
    {
      var jd1 = new Flux.MomentUtc(1991, 7, 11).ToJulianDate(ConversionCalendar.GregorianCalendar);

      Assert.AreEqual(System.DayOfWeek.Thursday, jd1.DayOfWeek);
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
    public void TimeOfDay()
    {
      var m = new Flux.MomentUtc(-4712, 1, 13, 12, 13, 14).ToJulianDate(ConversionCalendar.JulianCalendar);

      Assert.AreEqual(43993.99999999993, m.TimeOfDay.Value);
    }

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
  }
}