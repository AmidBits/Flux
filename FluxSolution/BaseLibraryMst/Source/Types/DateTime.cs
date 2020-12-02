using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace Types
{
	[TestClass]
	public class DateTime
	{
		private readonly System.DateTime source = new System.DateTime(1967, 5, 30);
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
			var closest = source.Closest(System.DayOfWeek.Friday, out var secondClosest);

			var expectedClosest = new System.DateTime(1967, 6, 2);
			var expectedSecondClosest = new System.DateTime(1967, 5, 26);

			Assert.AreEqual(expectedClosest, closest);
			Assert.AreEqual(expectedSecondClosest, secondClosest);
		}
		[TestMethod]
		public void DayOfWeekNext()
		{
			var next = source.Next(System.DayOfWeek.Friday, false);

			var expected = new System.DateTime(1967, 6, 2);

			Assert.AreEqual(expected, next);
		}
		[TestMethod]
		public void DayOfWeekPrevious()
		{
			var next = source.Previous(System.DayOfWeek.Friday, false);

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
			Assert.AreEqual(91, source.DaysInQuarter());
			Assert.AreEqual(92, target.DaysInQuarter());
		}
		[TestMethod]
		public void DaysInYear()
		{

			Assert.AreEqual(365, source.DaysInYear());
			Assert.AreEqual(365, target.DaysInYear());
		}

		[TestMethod]
		public void FirstDayOfMonth()
		{
			Assert.AreEqual(new System.DateTime(1967, 5, 1), source.FirstDayOfMonth());
			Assert.AreEqual(new System.DateTime(2017, 8, 1), target.FirstDayOfMonth());
			Assert.AreEqual(new System.DateTime(1967, 4, 1), source.FirstDayOfQuarter());
			Assert.AreEqual(new System.DateTime(2017, 7, 1), target.FirstDayOfQuarter());
			Assert.AreEqual(new System.DateTime(1967, 5, 28), source.FirstDayOfWeek());
			Assert.AreEqual(new System.DateTime(2017, 8, 6), target.FirstDayOfWeek());
			Assert.AreEqual(new System.DateTime(1967, 1, 1), source.FirstDayOfYear());
			Assert.AreEqual(new System.DateTime(2017, 1, 1), target.FirstDayOfYear());
		}
		[TestMethod]
		public void FirstDayOfQuarter()
		{
			Assert.AreEqual(new System.DateTime(1967, 4, 1), source.FirstDayOfQuarter());
			Assert.AreEqual(new System.DateTime(2017, 7, 1), target.FirstDayOfQuarter());
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
			Assert.AreEqual(new System.DateTime(1967, 1, 1), source.FirstDayOfYear());
			Assert.AreEqual(new System.DateTime(2017, 1, 1), target.FirstDayOfYear());
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
			Assert.AreEqual(source.DaysInQuarter(), source.GetDatesInQuarter().Count());
			Assert.AreEqual(target.DaysInQuarter(), target.GetDatesInQuarter().Count());
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
			Assert.AreEqual(source.DaysInYear(), source.GetDatesInYear().Count());
			Assert.AreEqual(target.DaysInYear(), target.GetDatesInYear().Count());
		}

		[TestMethod]
		public void GetQuartersOfYear()
		{
			Assert.AreEqual(4, source.GetQuarters().Count());
			Assert.AreEqual(4, target.GetQuarters().Count());
		}

		[TestMethod]
		public void JuliuanCalendar()
		{
			Assert.AreEqual(2439640.5, source.ToJulianDate(), "JulianDate");
			Assert.AreEqual(39640, source.ToModifiedJulianDate(), "ModifiedJulianDate");
			Assert.AreEqual(39640.5, source.ToReducedJulianDate(), "ReducedJulianDate");
			Assert.AreEqual(-360, source.ToTruncatedJulianDate(), "TruncatedJulianDate");
		}

		[TestMethod]
		public void LastDayOfMonth()
		{
			Assert.AreEqual(new System.DateTime(1967, 5, 31), source.LastDayOfMonth());
			Assert.AreEqual(new System.DateTime(2017, 8, 31), target.LastDayOfMonth());
			Assert.AreEqual(new System.DateTime(1967, 6, 30), source.LastDayOfQuarter());
			Assert.AreEqual(new System.DateTime(2017, 9, 30), target.LastDayOfQuarter());
			Assert.AreEqual(new System.DateTime(1967, 6, 3), source.LastDayOfWeek());
			Assert.AreEqual(new System.DateTime(2017, 8, 12), target.LastDayOfWeek());
			Assert.AreEqual(new System.DateTime(1967, 12, 31), source.LastDayOfYear());
			Assert.AreEqual(new System.DateTime(2017, 12, 31), target.LastDayOfYear());
		}
		[TestMethod]
		public void LastDayOfQuarter()
		{
			Assert.AreEqual(new System.DateTime(1967, 6, 30), source.LastDayOfQuarter());
			Assert.AreEqual(new System.DateTime(2017, 9, 30), target.LastDayOfQuarter());
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
			Assert.AreEqual(new System.DateTime(1967, 12, 31), source.LastDayOfYear());
			Assert.AreEqual(new System.DateTime(2017, 12, 31), target.LastDayOfYear());
		}

		[TestMethod]
		public void Quarter()
		{
			Assert.AreEqual(2, source.QuarterOfYear());
			Assert.AreEqual(3, target.QuarterOfYear());
		}

		[TestMethod]
		public void ToStringFileNameFriendly()
		{
			Assert.AreEqual(@"19670530 000000 0000000", source.ToStringFileNameFriendly());
			Assert.AreEqual(@"20170811 000000 0000000", target.ToStringFileNameFriendly());
		}

		[TestMethod]
		public void ToStringISO8601()
		{
			Assert.AreEqual(@"1967-05-30T00:00:00.0000000", source.ToStringISO8601());
			Assert.AreEqual(@"2017-08-11T00:00:00.0000000", target.ToStringISO8601());
		}
		[TestMethod]
		public void ToStringISO8601Date()
		{
			Assert.AreEqual(@"1967-05-30", source.ToStringISO8601Date());
			Assert.AreEqual(@"2017-08-11", target.ToStringISO8601Date());
		}
		[TestMethod]
		public void ToStringISO8601Time()
		{
			Assert.AreEqual(@"00:00:00", source.ToStringISO8601Time());
			Assert.AreEqual(@"00:00:00", target.ToStringISO8601Time());
		}
		[TestMethod]
		public void ToStringISO8601OrdinalDate()
		{
			Assert.AreEqual(@"1967-150", source.ToStringISO8601OrdinalDate());
			Assert.AreEqual(@"2017-223", target.ToStringISO8601OrdinalDate());
		}
		[TestMethod]
		public void ToStringISO8601WeekDate()
		{
			Assert.AreEqual(@"1967-W22-2", source.ToStringISO8601WeekDate());
			Assert.AreEqual(@"2017-W32-5", target.ToStringISO8601WeekDate());
		}

		[TestMethod]
		public void UnixTime()
		{
			Assert.AreEqual(-81820800, source.ToUnixTimestamp(), nameof(Flux.DateTimeEm.ToUnixTimestamp));
			Assert.AreEqual(-81820800000, source.ToUnixUltraTimestamp(), nameof(Flux.DateTimeEm.ToUnixUltraTimestamp));
		}

		[TestMethod]
		public void Week()
		{
			Assert.AreEqual(22, source.WeekOfYear(), nameof(Flux.DateTimeEm.WeekOfYear));
			Assert.AreEqual(32, target.WeekOfYear(), nameof(Flux.DateTimeEm.WeekOfYear));
		}
	}
}
