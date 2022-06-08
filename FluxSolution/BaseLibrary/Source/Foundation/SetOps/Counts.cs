namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Returns the number of unfound (not found) and the number of unique elements. Optionally the function returns early if there are unfound elements. Uses the specified equality comparer.</summary>
    public static (int unfoundCount, int uniqueCount) Counts<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, bool returnIfUnfound, System.Collections.Generic.IEqualityComparer<T> equalityComparer, out int sourceCount)
    {
      var unfoundCount = 0;

      var shs = new System.Collections.Generic.HashSet<T>(source, equalityComparer);

      sourceCount = shs.Count;

      var unique = new System.Collections.Generic.HashSet<T>(equalityComparer);

      foreach (var t in target)
      {
        if (shs.Contains(t))
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
    /// <summary>Returns the number of unfound (not found) and the number of unique elements. Optionally the function returns early if there are unfound elements. Uses the default equality comparer.</summary>
    public static (int unfoundCount, int uniqueCount) Counts<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, bool returnIfUnfound, out int sourceCount)
      => Counts(source, target, returnIfUnfound, System.Collections.Generic.EqualityComparer<T>.Default, out sourceCount);
  }
}
