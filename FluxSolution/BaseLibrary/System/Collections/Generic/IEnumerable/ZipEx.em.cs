namespace Flux
{
  public static partial class Fx
  {
    /// <summary>This version of Zip runs over all elements in all sequences.</summary>
    public static System.Collections.Generic.IEnumerable<TResult> ZipEx<T, TResult>(System.Func<T[], TResult> resultSelector, params System.Collections.Generic.IEnumerable<T>[] sequence)
    {
      System.ArgumentNullException.ThrowIfNull(resultSelector);

      var e = new System.Collections.Generic.IEnumerator<T>[sequence.Length];

      for (var index = 0; index < sequence.Length; index++)
        e[index] = null!;

      try
      {
        for (var index = 0; index < sequence.Length; index++)
          e[index] = sequence[index].GetEnumerator();

        var b = new bool[sequence.Length];

        while (true)
        {
          for (var index = 0; index < sequence.Length; index++)
            b[index] = e[index]?.MoveNext() ?? false;

          if (b.Any(boolean => boolean))
            yield return resultSelector(e.Select((o, i) => b[i] ? e[i].Current : default!).ToArray());
          else
            yield break;
        }
      }
      finally
      {
        for (var index = 0; index < sequence.Length; index++)
          e[index]?.Dispose();
      }
    }
  }
}
