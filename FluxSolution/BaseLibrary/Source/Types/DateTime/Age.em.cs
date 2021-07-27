namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Determines the life time in years (age) with fractions between the source (e.g. a birth) and the specified target (e.g. a birthday). This algorithm works the same in either direction. The fractional portion of the result is the percentage of the number of days that have passed since the last "birthday" to the "birthday" that has not yet occured.</summary>
    public static double AgeInTotalYears(this System.DateTime source, System.DateTime target, out int ageInYears)
    {
      var sign = (source <= target ? 1 : -1); // The sign of age direction, based on source/target, either 1 or -1.

      ageInYears = AgeInYears(source, target); // Age is calculated in either direction (backwards or forward in time).

      var last = source.AddYears(ageInYears); // This is the most recent "birthday" (either direction).
      var next = last.AddYears(sign); // This is the upcoming "birthday" (either direction).

      var fractionalAge = (target - last).TotalDays / (next - last).TotalDays * sign; // The fractional difference between the last "birthday" and target.

      return ageInYears + fractionalAge;
    }
    /// <summary>Determines the life time in years (age) with fractions between the source (e.g. a birth) and the specified target (e.g. a birthday). This algorithm works the same in either direction.</summary>
    public static double AgeInTotalYears(this System.DateTime source, System.DateTime target)
      => AgeInTotalYears(source, target, out var _);

    /// <summary>Determines the life time in integer years (age) between the source (e.g. a birth) and the specified target (e.g. a birthday). I.e. the common western way of calculating age. There are a multitude of scenarios where this way of calulcating would not be applicable. Works in either direction.</summary>
    public static int AgeInYears(this System.DateTime source, System.DateTime target)
      => target.Year - source.Year is int age && age > 0 && source.AddYears(age) > target ? age - 1 : age < 0 && source.AddYears(age) < target ? age + 1 : age;
  }
}
