namespace Flux
{
  public static partial class XtensionsReadOnlySpan
  {
    /// <summary>Determines whether the this instance contains the specified target. Uses the default comparer.</summary>
    public static bool Exists<T>(this System.ReadOnlySpan<T> source, System.Func<T, int, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var sourceLength = source.Length;

      for (var index = 0; index < sourceLength; index++)
        if (predicate(source[index], index))
          return true;

      return false;
    }
  }
}
