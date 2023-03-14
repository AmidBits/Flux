namespace Flux
{
  public static partial class ExtensionMethodsTimeSpan
  {
    /// <summary>Returns whether the source timespan is between start and end. If end is less than start, then it is assumed that the timespan is across days.</summary>
    public static System.TimeSpan Clamp(this System.TimeSpan source, System.TimeSpan min, System.TimeSpan max) => source < min ? min : source > max ? max : source;
  }
}
