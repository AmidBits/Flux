namespace Flux
{
  namespace Quantities
  {
    /// <summary>Specifies one or more days of the week, using a <see cref="System.FlagsAttribute"/> bitmask.</summary>
    [System.Flags]
    public enum WeekDays
    {
      None = 0,
      Monday = 1,
      Tuesday = 2,
      Wednesday = 4,
      Thursday = 8,
      Friday = 16,
      Saturday = 32,
      Sunday = 64,
    }
  }
}
