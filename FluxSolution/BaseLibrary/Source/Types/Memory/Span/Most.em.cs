namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns a subset containing the right most specified number of elements, if available, otherwise as many as there are.</summary>
    public static System.Span<T> LeftMost<T>(this System.Span<T> source, int maxCount)
      => source.Slice(0, System.Math.Min(source.Length, maxCount));

    /// <summary>Returns a subset containing the right most specified number of elements, if available, otherwise as many as there are.</summary>
    public static System.Span<T> RightMost<T>(this System.Span<T> source, int maxCount)
      => source[System.Math.Max(0, source.Length - maxCount)..];
  }
}
