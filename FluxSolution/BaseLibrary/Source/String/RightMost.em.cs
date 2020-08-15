namespace Flux
{
  public static partial class XtendString
  {
    /// <summary>Returns a string containing the right most specified number of characters, if available, otherwise as many as there are.</summary>
    public static string RightMost(this string source, int maxCount)
      => source.Substring(System.Math.Max(0, source.Length - maxCount));
  }
}
