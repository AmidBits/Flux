namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the specified comparer.</summary>
    public static int CountEqualAtStart<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      var minLength = System.Math.Min(source.Length, target.Length);

      var count = 0;

      while (count < minLength && equalityComparer.Equals(source[count], target[count]))
        count++;

      return count;
    }
    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the default comparer.</summary>
    public static int CountEqualAtStart<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => CountEqualAtStart(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
