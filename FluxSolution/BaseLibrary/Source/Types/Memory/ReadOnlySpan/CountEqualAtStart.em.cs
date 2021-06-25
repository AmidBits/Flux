namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the specified comparer.</summary>
    public static int CountEqualAtStart<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> equalityComparer, out int minLength)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      minLength = System.Math.Min(source.Length, target.Length);

      var index = 0;
      while (index < minLength && equalityComparer.Equals(source[index], target[index]))
        index++;
      return index;
    }
    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the default comparer.</summary>
    public static int CountEqualAtStart<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, out int minLength)
      => CountEqualAtStart(source, target, System.Collections.Generic.EqualityComparer<T>.Default, out minLength);

    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the specified comparer.</summary>
    /// <param name="minLength">The smaller length of the two spans.</param>
    public static int CountEqualAtStart(this string source, string target, System.Collections.Generic.IEqualityComparer<char> equalityComparer, out int minLength)
      => CountEqualAtStart(source, target, equalityComparer, out minLength);
    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the default comparer.</summary>
    /// <param name="minLength">The smaller length of the two spans.</param>
    public static int CountEqualAtStart(this string source, string target, out int minLength)
      => CountEqualAtStart(source, target, System.Collections.Generic.EqualityComparer<char>.Default, out minLength);
  }
}
