namespace Flux
{
  public static partial class ExtensionMethodsSpanBuilder
  {
    /// <summary>Normalize the specified (or all if none specified) consecutive <paramref name="values"/> in the string normalized. Uses the specfied <paramref name="equalityComparer"/>, or default if null.</summary>
    public static SpanBuilder<T> NormalizeAdjacent<T>(this SpanBuilder<T> source, System.Collections.Generic.IList<T> values, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var targetIndex = 0;
      var previous = default(T);

      for (var sourceIndex = 0; sourceIndex < source.Length; sourceIndex++)
      {
        var current = source.GetValue(sourceIndex);

        if (!equalityComparer.Equals(current, previous) || (values.Count > 0 && !values.Contains(current, equalityComparer)))
        {
          source.SetValue(targetIndex++, current);

          previous = current;
        }
      }

      return source.Remove(targetIndex, source.Length - targetIndex);
    }
  }
}
