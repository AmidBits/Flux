namespace Flux
{
  public static partial class ExtensionMethodsDateTime
  {
    /// <summary>Returns whether the source timespan is between start and end. If end is less than start, then it is assumed that the timespan is across days.</summary>
    public static System.DateTime Clamp(this System.DateTime source, System.DateTime min, System.DateTime max) => source < min ? min : source > max ? max : source;
  }
}
