namespace Flux
{
  public static partial class XtensionsSpan
  {
    /// <summary>Returns a string containing the right most specified number of characters, if available, otherwise as many as there are.</summary>
    public static System.Span<T> LeftMost<T>(this System.Span<T> source, int maxCount)
      => source.Slice(0, System.Math.Min(source.Length, maxCount));

    /// <summary>Returns a string containing the right most specified number of characters, if available, otherwise as many as there are.</summary>
    public static System.Span<T> RightMost<T>(this System.Span<T> source, int maxCount)
      => source.Slice(System.Math.Max(0, source.Length - maxCount));
  }
}
