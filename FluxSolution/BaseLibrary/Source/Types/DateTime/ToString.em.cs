namespace Flux
{
	public static partial class SystemDateTimeEm
	{
		/// <summary>Returns the source as a string formatted for use in a file name using "yyyyMMdd HHmmss fffffff".</summary>
		public static string ToStringFileNameFriendly(this System.DateTime source)
			=> source.ToString("yyyyMMdd HHmmss fffffff", System.Globalization.CultureInfo.InvariantCulture);

		/// <summary>Returns the System.DateTime as a string that complies with ISO 8601. The native .ToString("o") is used internally.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/ISO_8601"/>
		//[System.Obsolete(@"Please use the built-in [datetime].ToString(@""o"") instead.")]
		public static string ToStringISO8601(this System.DateTime source)
			=> source.ToString("o", System.Globalization.CultureInfo.InvariantCulture);

		/// <summary>Returns the System.DateTime date part only as a string using the ISO 8601 format "YYYY-MM-DD".</summary>
		/// <see cref="https://en.wikipedia.org/wiki/ISO_8601"/>
		public static string ToStringISO8601Date(this System.DateTime source)
			=> source.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

		/// <summary>Returns the System.DateTime time part only as a string in a dynamic ISO 8601 output "hh:mm[[:ss].sss]" (seconds and fractional seconds are omitted if zero).</summary>
		/// <see cref="https://en.wikipedia.org/wiki/ISO_8601"/>
		public static string ToStringISO8601Time(this System.DateTime source)
			=> source.Millisecond > 0 ? ToStringISO8601TimeHhMmSsSss(source) : source.Second > 0 ? ToStringISO8601TimeHhMmSs(source) : ToStringISO8601TimeHhMm(source);
		/// <summary>Returns the System.DateTime time part only as a string using the ISO 8601 format hh:mm".</summary>
		/// <see cref="https://en.wikipedia.org/wiki/ISO_8601"/>
		public static string ToStringISO8601TimeHhMm(this System.DateTime source)
			=> source.ToString("HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
		/// <summary>Returns the System.DateTime time part only as a string using the ISO 8601 format hh:mm:ss".</summary>
		/// <see cref="https://en.wikipedia.org/wiki/ISO_8601"/>
		public static string ToStringISO8601TimeHhMmSs(this System.DateTime source)
			=> source.ToString("HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
		/// <summary>Returns the System.DateTime time part only as a string using the ISO 8601 format hh:mm:ss.sss".</summary>
		/// <see cref="https://en.wikipedia.org/wiki/ISO_8601"/>
		public static string ToStringISO8601TimeHhMmSsSss(this System.DateTime source)
			=> source.ToString("HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);

		/// <summary></summary>
		/// <see cref="https://en.wikipedia.org/wiki/Ordinal_date"/>
		/// <seealso cref="https://en.wikipedia.org/wiki/ISO_8601#Week_dates"/>
		public static string ToStringISO8601OrdinalDate(this System.DateTime source)
			=> $"{System.Globalization.ISOWeek.GetYear(source)}-{source.DayOfYear}";

		/// <summary></summary>
		/// <see cref="https://en.wikipedia.org/wiki/ISO_week_date"/>
		/// <seealso cref="https://en.wikipedia.org/wiki/ISO_8601#Ordinal_dates"/>
		public static string ToStringISO8601WeekDate(this System.DateTime source)
			=> $"{System.Globalization.ISOWeek.GetYear(source)}-W{System.Globalization.ISOWeek.GetWeekOfYear(source)}-{(int)source.DayOfWeek}";

		/// <summary>Returns the System.DateTime 'kind' part only as a string using the format "zzz". This is the <see cref="System.DateTimeKind"/> value used when the value was created.</summary>
		public static string ToStringKind(this System.DateTime source)
			=> source.ToString("zzz", System.Globalization.CultureInfo.InvariantCulture);

		/// <summary>Returns a string in SQL Server parsable format, with dynamic precision based on zero values for fractional seconds.</summary>
		public static string ToStringSql(this System.DateTime source)
			=> source.Millisecond >= 1000 ? source.ToStringSqlYyyyMmDdHhMmSsFffffff() : source.Millisecond >= 1 ? source.ToStringSqlYyyyMmDdHhMmSsFff() : source.ToStringSqlYyyyMmDdHhMmSs();
		/// <summary>Returns a string in SQL Server parsable format, precision includes seconds.</summary>
		public static string ToStringSqlYyyyMmDdHhMmSs(this System.DateTime source)
			=> source.ToString(@"yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
		/// <summary>Returns a string in SQL Server parsable format, precision includes milliseconds.</summary>
		public static string ToStringSqlYyyyMmDdHhMmSsFff(this System.DateTime source)
			=> source.ToString(@"yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
		/// <summary>Returns a string in SQL Server parsable format, precision includes ticks (one more digit than microseconds).</summary>
		public static string ToStringSqlYyyyMmDdHhMmSsFffffff(this System.DateTime source)
			=> source.ToString(@"yyyy-MM-dd HH:mm:ss.fffffff", System.Globalization.CultureInfo.InvariantCulture);
	}
}
