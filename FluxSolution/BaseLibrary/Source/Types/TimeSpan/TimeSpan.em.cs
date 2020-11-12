using System.Linq;

namespace Flux
{
	public static partial class Xtensions
	{
		public static string ToStringOptimized(this System.TimeSpan source)
			=> System.Text.RegularExpressions.Regex.Replace(source.ToString(), @"^(00.)(00\:)+0+(?=.)", string.Empty);

		public static string ToStringOf(this System.TimeSpan source)
			=> string.Join(' ', System.Linq.Enumerable.Empty<string>().Append(ToStringOfDays(source), ToStringOfHours(source), ToStringOfMinutes(source), ToStringOfSeconds(source)).Where(s => s.Length > 0));
		public static string ToStringOfDays(this System.TimeSpan source)
			=> source.Days > 0 ? $"{source.Days} day{(source.Days > 1 ? @"s" : string.Empty)}" : string.Empty;
		public static string ToStringOfHours(this System.TimeSpan source)
			=> source.Hours > 0 ? $"{source.Hours} hour{(source.Hours > 1 ? @"s" : string.Empty)}" : string.Empty;
		public static string ToStringOfMinutes(this System.TimeSpan source)
			=> source.Minutes > 0 ? $"{source.Minutes} min{(source.Minutes > 1 ? @"s" : string.Empty)}" : string.Empty;
		public static string ToStringOfSeconds(this System.TimeSpan source)
			=> source.Seconds > 0 ? $"{source.Seconds} sec{(source.Seconds > 1 ? @"s" : string.Empty)}" : string.Empty;

		//public static string ToStringDays(this System.TimeSpan source, string suffix)
		//  => source.Days > 0 ? $"{source.Days}{suffix}" : string.Empty;

		//public static string ToStringHours(this System.TimeSpan source, string suffix)
		//  => source.Hours > 0 ? $"{source.Hours}{suffix}" : string.Empty;

		//public static string ToStringMinutes(this System.TimeSpan source, string suffix)
		//  => source.Minutes > 0 ? $"{source.Minutes}{suffix}" : string.Empty;

		//public static string ToStringSeconds(this System.TimeSpan source, string suffix, bool includeMillisecond)
		//  => source.Seconds > 0 || source.Milliseconds > 0 ? $"{source.Seconds}{(includeMillisecond && source.Milliseconds > 0 ? $".{source.Milliseconds}" : string.Empty)}{suffix}" : string.Empty;
	}
}
