using Flux;

namespace SystemFx
{
  [TestClass]
  public class DateTime
  {
    private readonly System.DateTime source = new(1967, 5, 30);
    private readonly System.DateTime target = new System.DateTime(2017, 5, 30).AddDays(73);

    [TestMethod]
    public void AgeInTotalYears()
    {
      Assert.AreEqual(50.2, source.AgeInTotalYears(target));
    }

    [TestMethod]
    public void AgeInYears()
    {
      Assert.AreEqual(50, (int)source.AgeInYears(target));
    }

    [TestMethod]
    public void DayOfWeekClosest()
    {
      var (closest, secondClosest) = source.ClosestDayOfWeek(System.DayOfWeek.Friday, false);

      var expectedClosest = new System.DateTime(1967, 6, 2);
      var expectedSecondClosest = new System.DateTime(1967, 5, 26);

      Assert.AreEqual(expectedClosest, closest);
      Assert.AreEqual(expectedSecondClosest, secondClosest);
    }
    [TestMethod]
    public void DayOfWeekNext()
    {
      var next = source.NextDayOfWeek(System.DayOfWeek.Friday, false);

      var expected = new System.DateTime(1967, 6, 2);

      Assert.AreEqual(expected, next);
    }
    [TestMethod]
    public void DayOfWeekPrevious()
    {
      var next = source.LastDayOfWeek(System.DayOfWeek.Friday, false);

      var expected = new System.DateTime(1967, 5, 26);

      Assert.AreEqual(expected, next);
    }

    [TestMethod]
    public void DaysInMonth()
    {
      Assert.AreEqual(31, System.DateTime.DaysInMonth(source.Year, source.Month));
      Assert.AreEqual(31, System.DateTime.DaysInMonth(target.Year, target.Month));
    }
    [TestMethod]
    public void DaysInQuarter()
    {
      Assert.AreEqual(91, System.DateTime.DaysInQuarter(source.Year, source.Quarter));
      Assert.AreEqual(92, System.DateTime.DaysInQuarter(target.Year, target.Quarter));
    }
    [TestMethod]
    public void DaysInYear()
    {

      Assert.AreEqual(365, System.DateTime.DaysInYear(source.Year));
      Assert.AreEqual(365, System.DateTime.DaysInYear(target.Year));
    }

    [TestMethod]
    public void FirstDayOfMonth()
    {
      Assert.AreEqual(new System.DateTime(1967, 5, 1), System.DateTime.StartOfMonth(source.Year, source.Month));
      Assert.AreEqual(new System.DateTime(2017, 8, 1), System.DateTime.StartOfMonth(target.Year, target.Month));
      Assert.AreEqual(new System.DateTime(1967, 4, 1), System.DateTime.StartOfQuarter(source.Year, source.Quarter));
      Assert.AreEqual(new System.DateTime(2017, 7, 1), System.DateTime.StartOfQuarter(target.Year, target.Quarter));
      Assert.AreEqual(new System.DateTime(1967, 5, 28), source.FirstDayOfWeek());
      Assert.AreEqual(new System.DateTime(2017, 8, 6), target.FirstDayOfWeek());
      Assert.AreEqual(new System.DateTime(1967, 1, 1), System.DateTime.StartOfYear(source.Year));
      Assert.AreEqual(new System.DateTime(2017, 1, 1), System.DateTime.StartOfYear(target.Year));
    }
    [TestMethod]
    public void FirstDayOfQuarter()
    {
      Assert.AreEqual(new System.DateTime(1967, 4, 1), System.DateTime.StartOfQuarter(source.Year, source.Quarter));
      Assert.AreEqual(new System.DateTime(2017, 7, 1), System.DateTime.StartOfQuarter(target.Year, target.Quarter));
    }
    [TestMethod]
    public void FirstDayOfWeek()
    {
      Assert.AreEqual(new System.DateTime(1967, 5, 28), source.FirstDayOfWeek());
      Assert.AreEqual(new System.DateTime(2017, 8, 6), target.FirstDayOfWeek());
    }
    [TestMethod]
    public void FirstDayOfYear()
    {
      Assert.AreEqual(new System.DateTime(1967, 1, 1), System.DateTime.StartOfYear(source.Year));
      Assert.AreEqual(new System.DateTime(2017, 1, 1), System.DateTime.StartOfYear(target.Year));
    }

    [TestMethod]
    public void GetDatesInMonth()
    {
      Assert.AreEqual(System.DateTime.DaysInMonth(source.Year, source.Month), source.GetDatesInMonth().Count());
      Assert.AreEqual(System.DateTime.DaysInMonth(target.Year, target.Month), target.GetDatesInMonth().Count());
    }
    [TestMethod]
    public void GetDatesInQuarter()
    {
      Assert.AreEqual(System.DateTime.DaysInQuarter(source.Year, source.Quarter), source.GetDatesInQuarter().Count());
      Assert.AreEqual(System.DateTime.DaysInQuarter(target.Year, target.Quarter), target.GetDatesInQuarter().Count());
    }
    [TestMethod]
    public void GetDatesInWeek()
    {
      Assert.AreEqual(7, source.GetDatesInWeek().Count());
      Assert.AreEqual(7, target.GetDatesInWeek().Count());
    }
    [TestMethod]
    public void GetDatesInYear()
    {
      Assert.AreEqual(System.DateTime.DaysInYear(source.Year), source.GetDatesInYear().Count());
      Assert.AreEqual(System.DateTime.DaysInYear(target.Year), target.GetDatesInYear().Count());
    }

    //[TestMethod]
    //public void JulianCalendar()
    //{
    //  Assert.AreEqual(2439640.5, Flux.JulianDate.FromGregorianCalendar(source.Year, source.Month, source.Day).Value, "JulianDate");
    //}

    [TestMethod]
    public void LastDayOfMonth()
    {
      Assert.AreEqual(new System.DateTime(1967, 5, 31), System.DateTime.EndOfMonth(source.Year, source.Month));
      Assert.AreEqual(new System.DateTime(2017, 8, 31), System.DateTime.EndOfMonth(target.Year, target.Month));
      Assert.AreEqual(new System.DateTime(1967, 6, 30), System.DateTime.EndOfQuarter(source.Year, source.Quarter));
      Assert.AreEqual(new System.DateTime(2017, 9, 30), System.DateTime.EndOfQuarter(target.Year, target.Quarter));
      Assert.AreEqual(new System.DateTime(1967, 6, 3), source.LastDayOfWeek());
      Assert.AreEqual(new System.DateTime(2017, 8, 12), target.LastDayOfWeek());
      Assert.AreEqual(new System.DateTime(1967, 12, 31), System.DateTime.EndOfYear(source.Year));
      Assert.AreEqual(new System.DateTime(2017, 12, 31), System.DateTime.EndOfYear(target.Year));
    }
    [TestMethod]
    public void LastDayOfQuarter()
    {
      Assert.AreEqual(new System.DateTime(1967, 6, 30), System.DateTime.EndOfQuarter(source.Year, source.Quarter));
      Assert.AreEqual(new System.DateTime(2017, 9, 30), System.DateTime.EndOfQuarter(target.Year, target.Quarter));
    }
    [TestMethod]
    public void LastDayOfWeek()
    {
      Assert.AreEqual(new System.DateTime(1967, 6, 3), source.LastDayOfWeek());
      Assert.AreEqual(new System.DateTime(2017, 8, 12), target.LastDayOfWeek());
    }
    [TestMethod]
    public void LastDayOfYear()
    {
      Assert.AreEqual(new System.DateTime(1967, 12, 31), System.DateTime.EndOfYear(source.Year));
      Assert.AreEqual(new System.DateTime(2017, 12, 31), System.DateTime.EndOfYear(target.Year));
    }

    [TestMethod]
    public void Quarter()
    {
      Assert.AreEqual(2, source.Quarter);
      Assert.AreEqual(3, target.Quarter);
    }

    [TestMethod]
    public void ToStringFileNameFriendly()
    {
      Assert.AreEqual(@"19670530 000000 0000000", source.ToFileNameFriendlyString());
      Assert.AreEqual(@"20170811 000000 0000000", target.ToFileNameFriendlyString());
    }

    [TestMethod]
    public void ToStringISO8601()
    {
      Assert.AreEqual(@"1967-05-30T00:00:00.0000000", source.ToIso8601String());
      Assert.AreEqual(@"2017-08-11T00:00:00.0000000", target.ToIso8601String());
    }
    [TestMethod]
    public void ToStringISO8601Date()
    {
      Assert.AreEqual(@"1967-05-30", source.ToIso8601StringDate());
      Assert.AreEqual(@"2017-08-11", target.ToIso8601StringDate());
    }
    [TestMethod]
    public void ToStringISO8601Time()
    {
      Assert.AreEqual(@"00:00", source.ToIso8601StringTime());
      Assert.AreEqual(@"00:00", target.ToIso8601StringTime());
    }
    [TestMethod]
    public void ToStringISO8601OrdinalDate()
    {
      Assert.AreEqual(@"1967-150", source.ToIso8601StringOrdinalDate());
      Assert.AreEqual(@"2017-223", target.ToIso8601StringOrdinalDate());
    }
    [TestMethod]
    public void ToStringISO8601WeekDate()
    {
      Assert.AreEqual(@"1967-W22-2", source.ToIso8601StringWeekDate());
      Assert.AreEqual(@"2017-W32-5", target.ToIso8601StringWeekDate());
    }

    [TestMethod]
    public void ToUnixTimestamp()
    {
      Assert.AreEqual(-81820800, System.DateTime.ToUnixTimestamp(source), nameof(ToUnixTimestamp));
    }
    [TestMethod]
    public void ToUnixUltraTimestamp()
    {
      Assert.AreEqual(-81820800000, System.DateTime.ToUnixUltraTimestamp(source), nameof(ToUnixUltraTimestamp));
    }

    [TestMethod]
    public void WeekOfYear()
    {
      Assert.AreEqual(22, source.WeekOfYear(), nameof(WeekOfYear));
      Assert.AreEqual(32, target.WeekOfYear(), nameof(WeekOfYear));
    }
  }
}
