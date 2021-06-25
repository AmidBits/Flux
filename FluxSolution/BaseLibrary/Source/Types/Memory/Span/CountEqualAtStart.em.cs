namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Reports the length (or count) of equality at the start of the sequence. Using the default comparer.</summary>
    /// <param name="minLength">The smaller length of the two spans.</param>
    public static int CountEqualAtStart<T>(this System.Span<T> source, System.Span<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer, out int minLength)
    {
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      minLength = System.Math.Min(source.Length, target.Length);

      var index = 0;
      while (index < minLength && comparer.Equals(source[index], target[index]))
        index++;
      return index;
    }
    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the default comparer.</summary>
    /// <param name="minLength">The smaller length of the two spans.</param>
    public static int CountEqualAtStart<T>(this System.Span<T> source, System.Span<T> target, out int minLength)
      => CountEqualAtStart(source, target, System.Collections.Generic.EqualityComparer<T>.Default, out minLength);
  }
}
