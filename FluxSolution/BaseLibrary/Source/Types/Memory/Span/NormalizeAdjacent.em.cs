namespace Flux
{
  public static partial class SpanEm
  {
    /// <summary>Normalize (in-place) the specified (or all if none specified) consecutive characters in the string. Uses the specfied comparer.</summary>
    public static System.Span<T> NormalizeAdjacent<T>(this System.Span<T> source, System.Collections.Generic.IEqualityComparer<T> equalityComparer, params T[] values)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      var index = 0;
      var previous = default(T);

      for (var indexOfSource = 0; indexOfSource < source.Length; indexOfSource++)
      {
        var current = source[indexOfSource];

        if (!equalityComparer.Equals(current, previous) || (values.Length > 0 && !System.Array.Exists(values, value => equalityComparer.Equals(value, current))))
        {
          source[index++] = current;

          previous = current;
        }
      }

      return source[..index];
    }
    /// <summary>Normalize (in-place) the specified (or all if none specified) consecutive characters in the string. Uses the default comparer.</summary>
    public static System.Span<T> NormalizeAdjacent<T>(this System.Span<T> source, params T[] values)
      => NormalizeAdjacent(source, System.Collections.Generic.EqualityComparer<T>.Default, values);
  }
}
