namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Normalize (in-place) the specified (or all if none specified) consecutive characters in the string. Uses the specfied comparer.</summary>
    public static System.Span<T> NormalizeAdjacent<T>(this System.Span<T> source, System.Collections.Generic.IEqualityComparer<T> comparer, params T[] values)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var index = 0;
      var previous = default(T);

      for (var indexOfSource = 0; indexOfSource < source.Length; indexOfSource++)
      {
        var current = source[indexOfSource];

        if (!comparer.Equals(current, previous) || (values.Length > 0 && !System.Array.Exists(values, value => comparer.Equals(value, current))))
        {
          source[index++] = current;

          previous = current;
        }
      }

      return source.Slice(0, index);
    }
    /// <summary>Normalize (in-place) the specified (or all if none specified) consecutive characters in the string. Uses the default comparer.</summary>
    public static System.Span<T> NormalizeAdjacent<T>(this System.Span<T> source, params T[] values)
      => NormalizeAdjacent(source, System.Collections.Generic.EqualityComparer<T>.Default, values);
  }
}
