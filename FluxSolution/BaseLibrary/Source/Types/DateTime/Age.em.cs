namespace Flux
{
	public static partial class DateTimeEm
	{
		/// <summary>Determines the life time in years (age) with fractions between the source (e.g. a birth) and the specified target (e.g. a birthday). This algorithm takes into account time, and works the same in either direction.</summary>
		public static double AgeInTotalYears(this System.DateTime source, System.DateTime target, out int ageInYears)
		{
			var sign = (source <= target ? 1 : -1); // The sign of age direction, based on source/target, either 1 or -1.

			ageInYears = AgeInYears(source, target); // Age is calculated in either direction (backwards or forward in time).

			var last = source.AddYears(ageInYears); // This is the most recent "birthday" (either direction).

			return ageInYears + ((target - last).TotalDays * sign / (last.AddYears(sign) - last).TotalDays);
		}
		/// <summary>Determines the life time in years (age) with fractions between the source (e.g. a birth) and the specified target (e.g. a birthday). This algorithm takes into account time, and works the same in either direction.</summary>
		public static double AgeInTotalYears(this System.DateTime source, System.DateTime target)
			=> AgeInTotalYears(source, target, out var _);

		/// <summary>Determines the life time in integer years (age) between the source (e.g. a birth) and the specified target (e.g. a birthday). I.e. the typical western way of calculating age. Work in either direction.</summary>
		public static int AgeInYears(this System.DateTime source, System.DateTime target)
			=> unchecked(target.Year - source.Year is int age && age > 0 && source.AddYears(age) > target ? age - 1 : age < 0 && source.AddYears(age) < target ? age + 1 : age);
	}
}
