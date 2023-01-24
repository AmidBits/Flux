namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>Returns a subset containing the right most specified number of elements, if available, otherwise as many as there are.</summary>
    public static System.ReadOnlySpan<T> LeftMost<T>(this System.ReadOnlySpan<T> source, int maxCount)
      => source[..System.Math.Min(source.Length, maxCount)];
  }
}
