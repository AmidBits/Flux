namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Creates a new sequence of elements from the specified collection that are not in the source set. Uses the specified equality comparer.</summary>
    public static (int unfoundCount, int uniqueCount) GetCounts<T>(this System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> other, bool returnIfUnfound, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      var unique = new System.Collections.Generic.HashSet<T>(equalityComparer);

      var unfoundCount = 0;

      foreach (var t in other)
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
    /// <summary>Creates a new sequence of elements from the specified collection that are not in the source set. Uses the default equality comparer.</summary>
    public static (int unfoundCount, int uniqueCount) GetCounts<T>(this System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target, bool returnIfUnfound)
      => GetCounts(source, target, returnIfUnfound, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
