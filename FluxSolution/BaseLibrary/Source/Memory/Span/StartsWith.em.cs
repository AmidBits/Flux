namespace Flux
{
  public static partial class XtensionsSpan
  {
    /// <summary>Determines whether the end of this ReadOnlySpan instance matches a specified target ReadOnlySpan.</summary>
    public static bool StartsWith<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      var targetLength = target.Length;

      if (source.Length < targetLength) return false;

      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      for (var index = 0; index < targetLength; index++)
        if (!comparer.Equals(source[index], target[index]))
          return false;

      return true;
    }
  }
}
