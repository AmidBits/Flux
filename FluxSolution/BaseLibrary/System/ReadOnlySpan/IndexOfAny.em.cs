namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>Reports the first index of any of the specified characters within the source, or -1 if none were found. Uses the specified comparer (null for default).</summary>
    public static int IndexOfAny<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IEqualityComparer<T>? equalityComparer, params T[] values)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      for (var index = 0; index < source.Length; index++)
      {
        var character = source[index];

        if (System.Array.Exists(values, c => equalityComparer.Equals(c, character)))
          return index;
      }

      return -1;
    }
  }
}
