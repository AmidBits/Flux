namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns a subset containing the right most specified number of elements, if available, otherwise as many as there are.</summary>
    public static System.ReadOnlySpan<T> RightMost<T>(this System.ReadOnlySpan<T> source, int maxCount)
      => source[System.Math.Max(0, source.Length - maxCount)..];
  }
}
