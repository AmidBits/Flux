namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns the number of unfound (not found) and the number of unique elements between <paramref name="source"/> and <paramref name="target"/>. Optionally, with <paramref name="returnIfUnfound"/> = true, the function returns early if there are unfound elements. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="returnIfUnfound"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static (int unfoundCount, int uniqueCount) SetStatistics<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, bool returnIfUnfound, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var unfoundCount = 0;

      var unique = new System.Collections.Generic.HashSet<T>(equalityComparer);

      for (var index = target.Length - 1; index >= 0; index--)
      {
        var t = target[index];

        if (source.IndexOf(t, equalityComparer) > -1)
          unique.Add(t);
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
