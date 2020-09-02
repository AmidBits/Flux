namespace Flux
{
  public static partial class XtendReadOnlySpan
  {
    /// <summary>Returns a subset containing the right most specified number of elements, if available, otherwise as many as there are.</summary>
    public static System.ReadOnlySpan<T> LeftMost<T>(this System.ReadOnlySpan<T> source, int maxCount)
      => source.Slice(0, System.Math.Min(source.Length, maxCount));

    /// <summary>Returns a subset containing the right most specified number of elements, if available, otherwise as many as there are.</summary>
    public static System.ReadOnlySpan<T> RightMost<T>(this System.ReadOnlySpan<T> source, int maxCount)
      => source.Slice(System.Math.Max(0, source.Length - maxCount));
  }
}
