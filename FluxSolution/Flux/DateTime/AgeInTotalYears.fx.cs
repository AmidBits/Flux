namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Determines the age in years (with a decimal point) between the <paramref name="source"/> (e.g. a birth) and the <paramref name="target"/> (e.g. a birthday). The fractional portion of the resulting age-in-total-years is the percentage of the number of days that have passed since the last "birthday" toward the "birthday" that has not yet occured.</para>
    /// </summary>
    /// <remarks>
    /// <para>Note that this is the common or typical western way of calculating age. There are a multitude of scenarios where this way of calulcating would not be applicable.</para>
    /// <para>The method works in both direction of time. When using it backwards, i.e. <paramref name="source"/> is greater than <paramref name="target"/>, the resulting age-in-years will be negative, for distinction.</para>
    /// </remarks>
    public static double AgeInTotalYears(this System.DateTime source, System.DateTime target)
    {
      var ageInYears = AgeInYears(source, target); // Age in years (signed depending on source/target).

      var sign = source <= target ? 1 : -1; // The unit sign of age direction, based on source/target, either 1 (when >= 0) or -1 (when < 0).

      var lastBirthday = source.AddYears(ageInYears); // This is the most recent "birthday" (either direction).
      var nextBirthday = lastBirthday.AddYears(sign); // This is the upcoming "birthday" (either direction).

      var spanSinceLast = (target - lastBirthday).Ticks; // The span of time since the last birthday to the target.
      var spanUntilNext = (nextBirthday - target).Ticks; // The span of time until the next birthday from the target.

      var additionalAge = (double)spanSinceLast / (double)(spanSinceLast + spanUntilNext) * sign; // The quotient of target date and next "birthday" spans to the last "birtday".

      return ageInYears + additionalAge;
    }

    //public static double GetTotalPart(this System.TimeSpan source, TimeSpanPart part)
    //  => part switch
    //  {
    //    TimeSpanPart.Day => source.TotalDays,
    //    TimeSpanPart.Hour => source.TotalHours,
    //    TimeSpanPart.Minute => source.TotalMinutes,
    //    TimeSpanPart.Second => source.TotalSeconds,
    //    TimeSpanPart.Millisecond => source.TotalMilliseconds,
    //    TimeSpanPart.Microsecond => source.TotalMicroseconds,
    //    TimeSpanPart.Nanosecond => source.TotalNanoseconds,
    //    TimeSpanPart.Tick => source.Ticks,
    //    _ => throw new NotImplementedException(),
    //  };

    //public static (double ToPrecision, double RoundedUp) TotalAge(this System.DateTime source, System.DateTime target, TimeSpanPart precision)
    //{
    //  var ageInYears = AgeInYears(source, target); // Age is calculated in either direction (backwards or forward in time).

    //  var sign = int.IsNegative(ageInYears) ? -1 : 1; // The unit sign of age direction, based on source/target, either 1 or -1.

    //  var last = source.AddYears(ageInYears); // This is the most recent "birthday" (either direction).
    //  var next = source.AddYears(ageInYears + sign); // This is the upcoming "birthday" (either direction).

    //  var timeSince = GetTotalPart(target - last, precision);
    //  var timeTotal = GetTotalPart(next - last, precision);

    //  return (
    //    ageInYears + timeSince / timeTotal * sign,
    //    ageInYears + double.Ceiling(timeSince) / timeTotal * sign
    //  );
    //}
  }
}
