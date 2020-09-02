namespace Flux
{
  public static partial class XtendReadOnlySpan
  {
    /// <summary>Normalize all sequences of the specified characters throughout the string. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element.</summary>
    public static T[] NormalizeAdjacent<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IEqualityComparer<T> comparer, params T[] values)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var sourceLength = source.Length;

      var buffer = new T[sourceLength];
      var bufferIndex = 0;

      var previousValue = default(T);

      for (var sourceIndex = 0; sourceIndex < sourceLength; sourceIndex++)
      {
        var currentValue = source[sourceIndex];

        if (!comparer.Equals(currentValue, previousValue) || (values.Length > 0 && !System.Array.Exists(values, value => comparer.Equals(value, currentValue))))
        {
          buffer[sourceIndex++] = currentValue;

          previousValue = currentValue;
        }
      }

      System.Array.Resize(ref buffer, bufferIndex);

      return buffer;
    }
    public static T[] NormalizeAdjacent<T>(this System.ReadOnlySpan<T> source, params T[] normalize)
      => NormalizeAdjacent(source, System.Collections.Generic.EqualityComparer<T>.Default, normalize);
  }
}
