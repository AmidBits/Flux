namespace Flux
{
  public static partial class XtensionsReadOnlySpan
  {
    /// <summary>Determines the index of the last occurence of target in source. Uses the specified comparer.</summary>
    public static int LastIndexOf<T>(this System.ReadOnlySpan<T> source, System.Func<T, int, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var sourceLength = source.Length;

      for (var index = source.Length - 1; index >= 0; index--)
        if (predicate(source[index], index))
          return index;

      return -1;
    }
  }
}
