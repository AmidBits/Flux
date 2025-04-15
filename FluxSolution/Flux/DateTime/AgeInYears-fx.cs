namespace Flux
{
  public static partial class DateTimes
  {
    /// <summary>
    /// <para>Determines the age in integer years between the <paramref name="source"/> (e.g. a birth) and the <paramref name="target"/> (e.g. a birthday).</para>
    /// </summary>
    /// <remarks>
    /// <para>Note that this is the common or typical western way of calculating age. There are a multitude of scenarios where this way of calulcating would not be applicable.</para>
    /// <para>The method works in both direction of time. When using it backwards, i.e. <paramref name="source"/> is greater than <paramref name="target"/>, the resulting age-in-years will be negative, for distinction.</para>
    /// </remarks>
    public static int AgeInYears(this System.DateTime source, System.DateTime target)
      => target.Year - source.Year is int age && age > 0 && source.AddYears(age) > target ? age - 1 : age < 0 && source.AddYears(age) < target ? age + 1 : age;
  }
}
