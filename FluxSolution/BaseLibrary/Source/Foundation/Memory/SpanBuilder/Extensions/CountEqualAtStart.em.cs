namespace Flux
{
  public static partial class ReadOnlySpanEm
  {
    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the specified comparer.</summary>
    public static int CountEqualAtStart<T>(ref this SpanBuilder<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      var minLength = System.Math.Min(source.Length, target.Length);

      var index = 0;
      while (index < minLength && equalityComparer.Equals(source[index], target[index]))
        index++;
      return index;
    }
    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the default comparer.</summary>
    public static int CountEqualAtStart<T>(ref this SpanBuilder<T> source, System.ReadOnlySpan<T> target)
      => CountEqualAtStart(ref source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
