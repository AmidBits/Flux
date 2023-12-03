namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Determines the life time in integer years (age) between the source (e.g. a birth) and the specified target (e.g. a birthday). I.e. the common western way of calculating age. There are a multitude of scenarios where this way of calulcating would not be applicable. Works in either direction.</summary>
    public static int AgeInYears(this System.DateTime source, System.DateTime target)
      => target.Year - source.Year is int age && age > 0 && source.AddYears(age) > target ? age - 1 : age < 0 && source.AddYears(age) < target ? age + 1 : age;
  }
}
