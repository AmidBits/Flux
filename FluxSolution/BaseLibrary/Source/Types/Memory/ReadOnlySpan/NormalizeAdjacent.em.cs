namespace Flux
{
  public static partial class SpanEm
  {
    /// <summary>Creates a new readonlyspan with the specified (or all if none specified) consecutive characters in the string normalized. Uses the specfied comparer.</summary>
    public static System.ReadOnlySpan<T> NormalizeAdjacent<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IEqualityComparer<T> equalityComparer, System.Collections.Generic.IList<T> values)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      var target = new T[source.Length];

      var index = 0;
      var previous = default(T);

      for (var indexOfSource = 0; indexOfSource < source.Length; indexOfSource++)
      {
        var current = source[indexOfSource];

        if (!equalityComparer.Equals(current, previous) || (values.Count > 0 && !values.Contains(current, equalityComparer)))
        {
          target[index++] = current;

          previous = current;
        }
      }

      return target[..index];
    }
    /// <summary>Creates a new readonlyspan with the specified (or all if none specified) consecutive characters in the string normalized. Uses the default comparer.</summary>
    public static System.ReadOnlySpan<T> NormalizeAdjacent<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IList<T> values)
      => NormalizeAdjacent(source, System.Collections.Generic.EqualityComparer<T>.Default, values);
  }
}
