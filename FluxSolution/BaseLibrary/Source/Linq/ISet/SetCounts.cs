namespace Flux
{
  public static partial class ISet
  {
    /// <summary>Returns the number of unfound (not found) and the number of unique elements. Optionally the function returns early if there are unfound elements. Uses the specified equality comparer.</summary>
    public static (int unfoundCount, int uniqueCount) SetCounts<T>(this System.Collections.Generic.ISet<T> source, System.Collections.Generic.IEnumerable<T> target, bool returnIfUnfound)
    {
      var unfoundCount = 0;

      var hsUnique = new System.Collections.Generic.HashSet<T>();

      using var e = target.GetEnumerator();

      while (e.MoveNext())
      {
        if (e.Current is var current && source.Contains(current))
        {
          if (!hsUnique.Contains(current))
            hsUnique.Add(current);
        }
        else
        {
          unfoundCount++;

          if (returnIfUnfound)
            break;
        }
      }

      return (unfoundCount, hsUnique.Count);
    }
  }
}
