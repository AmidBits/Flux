namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns whether the source timespan is between start and end. If end is less than start, then it is assumed that the timespan is across days.</summary>
    public static bool IsBetween<T>(this T source, T start, T end)
      where T : System.IComparable<T>
      => start.CompareTo(end) <= 0
      ? source.CompareTo(start) >= 0 && source.CompareTo(end) <= 0
      : source.CompareTo(end) >= 0 || source.CompareTo(start) <= 0;
  }
}
