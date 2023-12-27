namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Determines the life time in years (age) with fractions between the source (e.g. a birth) and the specified target (e.g. a birthday). This algorithm works the same in either direction. The fractional portion of the result is the percentage of the number of days that have passed since the last "birthday" to the "birthday" that has not yet occured.</summary>
    public static double AgeInTotalYears(this System.DateTime source, System.DateTime target, out int ageInYears)
    {
      var sign = (source <= target ? 1 : -1); // The sign of age direction, based on source/target, either 1 or -1.

      ageInYears = AgeInYears(source, target); // Age is calculated in either direction (backwards or forward in time).

      var last = source.AddYears(ageInYears); // This is the most recent "birthday" (either direction).
      var next = source.AddYears(ageInYears + sign); // This is the upcoming "birthday" (either direction).

      var fractionalAge = (double)(target - last).Ticks / (double)(next - last).Ticks * sign; // The fractional difference between the last "birthday" and target.

      return ageInYears + fractionalAge;
    }
    /// <summary>Determines the life time in years (age) with fractions between the source (e.g. a birth) and the specified target (e.g. a birthday). This algorithm works the same in either direction.</summary>
    public static double AgeInTotalYears(this System.DateTime source, System.DateTime target)
      => AgeInTotalYears(source, target, out var _);
  }
}
