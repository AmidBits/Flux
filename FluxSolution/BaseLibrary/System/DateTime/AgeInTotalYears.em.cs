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
      var ageInYears = AgeInYears(source, target); // Age is calculated in either direction (backwards or forward in time).

      var sign = int.IsNegative(ageInYears) ? -1 : 1; // The unit sign of age direction, based on source/target, either 1 or -1.

      var last = source.AddYears(ageInYears); // This is the most recent "birthday" (either direction).
      var next = source.AddYears(ageInYears + sign); // This is the upcoming "birthday" (either direction).

      var fractionalAge = (double)(target - last).Ticks / (double)(next - last).Ticks * (double)sign; // The quotient of target date and next "birthday" spans to the last "birtday".

      return ageInYears + fractionalAge;
    }
  }
}
