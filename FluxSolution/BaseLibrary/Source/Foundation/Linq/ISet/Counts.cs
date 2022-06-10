namespace Flux
{
  public static partial class ISet
  {
    /// <summary>Returns the number of unfound (not found) and the number of unique elements. Optionally the function returns early if there are unfound elements. Uses the specified equality comparer.</summary>
    public static (int unfoundCount, int uniqueCount) Counts<T>(this System.Collections.Generic.ISet<T> source, System.Collections.Generic.IEnumerable<T> target, bool returnIfUnfound)
    {
      var unfoundCount = 0;

      var unique = new System.Collections.Generic.HashSet<T>();

      foreach (var t in target)
      {
        if (source.Contains(t))
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
