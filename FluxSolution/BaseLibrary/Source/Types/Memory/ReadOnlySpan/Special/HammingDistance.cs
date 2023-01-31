namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>
    /// <para>The Hamming distance between two sequences of equal length is the number of positions at which the corresponding symbols are different.</para>
    /// <see href="https://en.wikipedia.org/wiki/Hamming_distance"/>
    /// </summary>
    /// <returns>The minimum number of substitutions required to change the source to target, or the minimum number of errors that could have transformed source to target.</returns>
    public static int GetHammingEditDistance<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      if (source.Length != target.Length) throw new System.ArgumentException($"The source length ({source.Length}) and the target length ({target.Length}) must be equal.");

      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var equalCount = 0;

      for (var index = source.Length - 1; index >= 0; index--)
        if (!equalityComparer.Equals(source[index], target[index]))
          equalCount++;

      return equalCount;
    }
  }
}
