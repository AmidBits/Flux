namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the number of unfound (not found) and the number of unique elements. Optionally the function returns early if there are unfound elements. Uses the specified equality comparer.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static (int unfoundCount, int uniqueCount) SetStatistics<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, bool returnIfUnfound, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var unfoundCount = 0;

      var unique = new System.Collections.Generic.HashSet<T>(equalityComparer);

      for (var index = target.Length - 1; index >= 0; index--)
      {
        var t = target[index];

        if (source.IndexOf(t, equalityComparer) > -1)
        {
          if (!unique.Contains(t))
            unique.Add(t);
        }
        else
        {
          unfoundCount++;

          if (returnIfUnfound)
            break;
        }
      }

      return (unfoundCount, unique.Count);
    }
  }
}
