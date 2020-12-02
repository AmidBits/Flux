using System.Linq;

namespace Flux
{
  public static partial class IEnumerableEm
  {
    /// <summary>This version of CartesianProduct runs over all elements in all sequences.</summary>
    public static System.Collections.Generic.IEnumerable<TResult> CartesianProduct<T1, T2, TResult>(this System.Collections.Generic.IEnumerable<T1> source, System.Collections.Generic.IEnumerable<T2> other, System.Func<(T1, T2), int, TResult> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (other is null) throw new System.ArgumentNullException(nameof(other));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      var index = 0;

      foreach (var a in source)
        foreach (var b in other)
          yield return resultSelector((a, b), index++);
    }
    /// <summary>This version of CartesianProduct runs over all elements in all sequences.</summary>
    public static System.Collections.Generic.IEnumerable<TResult> CartesianProduct<T1, T2, T3, TResult>(this System.Collections.Generic.IEnumerable<T1> source, System.Collections.Generic.IEnumerable<T2> other1, System.Collections.Generic.IEnumerable<T3> other2, System.Func<(T1, T2, T3), int, TResult> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (other1 is null) throw new System.ArgumentNullException(nameof(other1));
      if (other2 is null) throw new System.ArgumentNullException(nameof(other2));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      var index = 0;

      foreach (var a in source)
        foreach (var b in other1)
          foreach (var c in other2)
            yield return resultSelector((a, b, c), index++);
    }
    /// <summary>This version of CartesianProduct runs over all elements in all sequences.</summary>
    public static System.Collections.Generic.IEnumerable<TResult> CartesianProduct<T1, T2, T3, T4, TResult>(this System.Collections.Generic.IEnumerable<T1> source, System.Collections.Generic.IEnumerable<T2> other1, System.Collections.Generic.IEnumerable<T3> other2, System.Collections.Generic.IEnumerable<T4> other3, System.Func<(T1, T2, T3, T4), int, TResult> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (other1 is null) throw new System.ArgumentNullException(nameof(other1));
      if (other2 is null) throw new System.ArgumentNullException(nameof(other2));
      if (other3 is null) throw new System.ArgumentNullException(nameof(other3));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      var index = 0;

      foreach (var a in source)
        foreach (var b in other1)
          foreach (var c in other2)
            foreach (var d in other3)
              yield return resultSelector((a, b, c, d), index++);
    }
    /// <summary>This version of CartesianProduct runs over all elements in all sequences.</summary>
    public static System.Collections.Generic.IEnumerable<TResult> CartesianProduct<T1, T2, T3, T4, T5, TResult>(this System.Collections.Generic.IEnumerable<T1> source, System.Collections.Generic.IEnumerable<T2> other1, System.Collections.Generic.IEnumerable<T3> other2, System.Collections.Generic.IEnumerable<T4> other3, System.Collections.Generic.IEnumerable<T5> other4, System.Func<(T1, T2, T3, T4, T5), int, TResult> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (other1 is null) throw new System.ArgumentNullException(nameof(other1));
      if (other2 is null) throw new System.ArgumentNullException(nameof(other2));
      if (other3 is null) throw new System.ArgumentNullException(nameof(other3));
      if (other4 is null) throw new System.ArgumentNullException(nameof(other4));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      var index = 0;

      foreach (var a in source)
        foreach (var b in other1)
          foreach (var c in other2)
            foreach (var d in other3)
              foreach (var e in other4)
                yield return resultSelector((a, b, c, d, e), index++);
    }
    /// <summary>This version of CartesianProduct runs over all elements in all sequences.</summary>
    public static System.Collections.Generic.IEnumerable<TResult> CartesianProduct<T1, T2, T3, T4, T5, T6, TResult>(this System.Collections.Generic.IEnumerable<T1> source, System.Collections.Generic.IEnumerable<T2> other1, System.Collections.Generic.IEnumerable<T3> other2, System.Collections.Generic.IEnumerable<T4> other3, System.Collections.Generic.IEnumerable<T5> other4, System.Collections.Generic.IEnumerable<T6> other5, System.Func<(T1, T2, T3, T4, T5, T6), int, TResult> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (other1 is null) throw new System.ArgumentNullException(nameof(other1));
      if (other2 is null) throw new System.ArgumentNullException(nameof(other2));
      if (other3 is null) throw new System.ArgumentNullException(nameof(other3));
      if (other4 is null) throw new System.ArgumentNullException(nameof(other4));
      if (other5 is null) throw new System.ArgumentNullException(nameof(other5));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      var index = 0;

      foreach (var a in source)
        foreach (var b in other1)
          foreach (var c in other2)
            foreach (var d in other3)
              foreach (var e in other4)
                foreach (var f in other5)
                  yield return resultSelector((a, b, c, d, e, f), index++);
    }
    /// <summary>This version of CartesianProduct runs over all elements in all sequences.</summary>
    public static System.Collections.Generic.IEnumerable<TResult> CartesianProduct<T1, T2, T3, T4, T5, T6, T7, TResult>(this System.Collections.Generic.IEnumerable<T1> source, System.Collections.Generic.IEnumerable<T2> other1, System.Collections.Generic.IEnumerable<T3> other2, System.Collections.Generic.IEnumerable<T4> other3, System.Collections.Generic.IEnumerable<T5> other4, System.Collections.Generic.IEnumerable<T6> other5, System.Collections.Generic.IEnumerable<T7> other6, System.Func<(T1, T2, T3, T4, T5, T6, T7), int, TResult> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (other1 is null) throw new System.ArgumentNullException(nameof(other1));
      if (other2 is null) throw new System.ArgumentNullException(nameof(other2));
      if (other3 is null) throw new System.ArgumentNullException(nameof(other3));
      if (other4 is null) throw new System.ArgumentNullException(nameof(other4));
      if (other5 is null) throw new System.ArgumentNullException(nameof(other5));
      if (other6 is null) throw new System.ArgumentNullException(nameof(other6));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      var index = 0;

      foreach (var a in source)
        foreach (var b in other1)
          foreach (var c in other2)
            foreach (var d in other3)
              foreach (var e in other4)
                foreach (var f in other5)
                  foreach (var g in other6)
                    yield return resultSelector((a, b, c, d, e, f, g), index++);
    }
    /// <summary>This version of CartesianProduct runs over all elements in all sequences.</summary>
    public static System.Collections.Generic.IEnumerable<TResult> CartesianProduct<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this System.Collections.Generic.IEnumerable<T1> source, System.Collections.Generic.IEnumerable<T2> other1, System.Collections.Generic.IEnumerable<T3> other2, System.Collections.Generic.IEnumerable<T4> other3, System.Collections.Generic.IEnumerable<T5> other4, System.Collections.Generic.IEnumerable<T6> other5, System.Collections.Generic.IEnumerable<T7> other6, System.Collections.Generic.IEnumerable<T8> other7, System.Func<(T1, T2, T3, T4, T5, T6, T7, T8), int, TResult> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (other1 is null) throw new System.ArgumentNullException(nameof(other1));
      if (other2 is null) throw new System.ArgumentNullException(nameof(other2));
      if (other3 is null) throw new System.ArgumentNullException(nameof(other3));
      if (other4 is null) throw new System.ArgumentNullException(nameof(other4));
      if (other5 is null) throw new System.ArgumentNullException(nameof(other5));
      if (other6 is null) throw new System.ArgumentNullException(nameof(other6));
      if (other7 is null) throw new System.ArgumentNullException(nameof(other7));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      var index = 0;

      foreach (var a in source)
        foreach (var b in other1)
          foreach (var c in other2)
            foreach (var d in other3)
              foreach (var e in other4)
                foreach (var f in other5)
                  foreach (var g in other6)
                    foreach (var h in other7)
                      yield return resultSelector((a, b, c, d, e, f, g, h), index++);
    }
    /// <summary>This version of CartesianProduct runs over all elements in all sequences.</summary>
    public static System.Collections.Generic.IEnumerable<TResult> CartesianProduct<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this System.Collections.Generic.IEnumerable<T1> source, System.Collections.Generic.IEnumerable<T2> other1, System.Collections.Generic.IEnumerable<T3> other2, System.Collections.Generic.IEnumerable<T4> other3, System.Collections.Generic.IEnumerable<T5> other4, System.Collections.Generic.IEnumerable<T6> other5, System.Collections.Generic.IEnumerable<T7> other6, System.Collections.Generic.IEnumerable<T8> other7, System.Collections.Generic.IEnumerable<T9> other8, System.Func<(T1, T2, T3, T4, T5, T6, T7, T8, T9), int, TResult> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (other1 is null) throw new System.ArgumentNullException(nameof(other1));
      if (other2 is null) throw new System.ArgumentNullException(nameof(other2));
      if (other3 is null) throw new System.ArgumentNullException(nameof(other3));
      if (other4 is null) throw new System.ArgumentNullException(nameof(other4));
      if (other5 is null) throw new System.ArgumentNullException(nameof(other5));
      if (other6 is null) throw new System.ArgumentNullException(nameof(other6));
      if (other7 is null) throw new System.ArgumentNullException(nameof(other7));
      if (other8 is null) throw new System.ArgumentNullException(nameof(other8));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      var index = 0;

      foreach (var a in source)
        foreach (var b in other1)
          foreach (var c in other2)
            foreach (var d in other3)
              foreach (var e in other4)
                foreach (var f in other5)
                  foreach (var g in other6)
                    foreach (var h in other7)
                      foreach (var i in other8)
                        yield return resultSelector((a, b, c, d, e, f, g, h, i), index++);
    }
    /// <summary>This version of CartesianProduct runs over all elements in all sequences.</summary>
    public static System.Collections.Generic.IEnumerable<TResult> CartesianProduct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(this System.Collections.Generic.IEnumerable<T1> source, System.Collections.Generic.IEnumerable<T2> other1, System.Collections.Generic.IEnumerable<T3> other2, System.Collections.Generic.IEnumerable<T4> other3, System.Collections.Generic.IEnumerable<T5> other4, System.Collections.Generic.IEnumerable<T6> other5, System.Collections.Generic.IEnumerable<T7> other6, System.Collections.Generic.IEnumerable<T8> other7, System.Collections.Generic.IEnumerable<T9> other8, System.Collections.Generic.IEnumerable<T10> other9, System.Func<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10), int, TResult> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (other1 is null) throw new System.ArgumentNullException(nameof(other1));
      if (other2 is null) throw new System.ArgumentNullException(nameof(other2));
      if (other3 is null) throw new System.ArgumentNullException(nameof(other3));
      if (other4 is null) throw new System.ArgumentNullException(nameof(other4));
      if (other5 is null) throw new System.ArgumentNullException(nameof(other5));
      if (other6 is null) throw new System.ArgumentNullException(nameof(other6));
      if (other7 is null) throw new System.ArgumentNullException(nameof(other7));
      if (other8 is null) throw new System.ArgumentNullException(nameof(other8));
      if (other9 is null) throw new System.ArgumentNullException(nameof(other9));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      var index = 0;

      foreach (var a in source)
        foreach (var b in other1)
          foreach (var c in other2)
            foreach (var d in other3)
              foreach (var e in other4)
                foreach (var f in other5)
                  foreach (var g in other6)
                    foreach (var h in other7)
                      foreach (var i in other8)
                        foreach (var j in other9)
                          yield return resultSelector((a, b, c, d, e, f, g, h, i, j), index++);
    }

    /// <summary>This version of CartesianProduct runs over all elements in all sequences.</summary>
    public static System.Collections.Generic.IEnumerable<TResult> CartesianProduct<TResult>(System.Func<object[], TResult> resultSelector, params System.Collections.Generic.IEnumerable<object>[] sequence)
    {
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      var e = new System.Collections.Generic.IEnumerator<object>[sequence.Length];

      for (var index = 0; index < sequence.Length; index++)
      {
        e[index] = null!;
      }

      try
      {
        for (var index = 0; index < sequence.Length; index++)
        {
          e[index] = sequence[index].GetEnumerator();
        }

        var b = new bool[sequence.Length];

        while (true)
        {
          for (var index = 0; index < sequence.Length; index++)
          {
            b[index] = e[index]?.MoveNext() ?? false;
          }

          if (b.Any(boolean => boolean))
          {
            yield return resultSelector(e.Select((o, i) => b[i] ? e[i].Current : null!).ToArray());
          }
          else yield break;
        }
      }
      finally
      {
        for (var index = 0; index < sequence.Length; index++)
        {
          e[index]?.Dispose();
        }
      }
    }

    /// <summary>This version of CartesianProduct runs over all elements in all sequences.</summary>
    public static System.Collections.Generic.IEnumerable<TResult> CartesianProduct<T, TResult>(System.Func<T[], TResult> resultSelector, params System.Collections.Generic.IEnumerable<T>[] sequence)
    {
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      var e = new System.Collections.Generic.IEnumerator<T>[sequence.Length];

      for (var index = 0; index < sequence.Length; index++)
      {
        e[index] = null!;
      }

      try
      {
        for (var index = 0; index < sequence.Length; index++)
        {
          e[index] = sequence[index].GetEnumerator();
        }

        var b = new bool[sequence.Length];

        while (true)
        {
          for (var index = 0; index < sequence.Length; index++)
          {
            b[index] = e[index]?.MoveNext() ?? false;
          }

          if (b.Any(boolean => boolean))
          {
            yield return resultSelector(e.Select((o, i) => b[i] ? e[i].Current : default!).ToArray());
          }
          else yield break;
        }
      }
      finally
      {
        for (var index = 0; index < sequence.Length; index++)
        {
          e[index]?.Dispose();
        }
      }
    }
  }
}
