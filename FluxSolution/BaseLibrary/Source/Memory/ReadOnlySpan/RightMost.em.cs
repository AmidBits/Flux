namespace Flux
{
  public static partial class XtensionsReadOnlySpan
  {
    /// <summary>Returns a string containing the right most specified number of characters, if available, otherwise as many as there are.</summary>
    public static System.ReadOnlySpan<T> RightMost<T>(this System.ReadOnlySpan<T> source, int maxCount)
      => source.Slice(Math.Max(0, source.Length - maxCount));
  }
}
