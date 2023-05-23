namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the specified comparer (null for default).</summary>
    public static int CountEqualAtStart<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var minLength = System.Math.Min(source.Length, target.Length);

      var count = 0;

      while (count < minLength && equalityComparer.Equals(source[count], target[count]))
        count++;

      return count;
    }
  }
}
