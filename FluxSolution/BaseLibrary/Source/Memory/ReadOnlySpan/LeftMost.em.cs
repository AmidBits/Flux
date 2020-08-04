namespace Flux
{
  public static partial class XtensionsReadOnlySpan
  {
    /// <summary>Returns a string containing the right most specified number of characters, if available, otherwise as many as there are.</summary>
    public static System.ReadOnlySpan<T> LeftMost<T>(this System.ReadOnlySpan<T> source, int maxCount)
      => source.Slice(0, Math.Min(source.Length, maxCount));
  }
}
