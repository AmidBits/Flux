namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns whether the source timespan is between start and end. If end is less than start, then it is assumed that the timespan is across days.</summary>
    public static bool IsBetween(this System.TimeSpan source, System.TimeSpan start, System.TimeSpan end)
      => start <= end
      ? source >= start && source <= end
      : source >= end || source <= start;
  }
}
