namespace Flux
{
  public static partial class XtensionsString
  {
    /// <summary>Returns a string containing the right most specified number of characters, if available, otherwise as many as there are.</summary>
    public static string LeftMost(this string source, int maxCount)
      => source.Substring(0, System.Math.Min(source.Length, maxCount));
  }
}
