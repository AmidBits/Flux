using System.Linq;

namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>This version of Zip runs over all elements in all sequences.</summary>
    public static System.Collections.Generic.IEnumerable<TResult> ZipEx<T1, T2, TResult>(this System.Collections.Generic.IEnumerable<T1> source1, System.Collections.Generic.IEnumerable<T2> source2, System.Func<T1, T2, TResult> resultSelector)
      => ZipEx<T1, T2, object, object, object, object, object, object, object, TResult>(source1, source2, null, null, null, null, null, null, null, (a, b, c, d, e, f, g, h, i) => resultSelector(a, b));
    /// <summary>This version of Zip runs over all elements in all sequences.</summary>
    public static System.Collections.Generic.IEnumerable<TResult> ZipEx<T1, T2, T3, TResult>(this System.Collections.Generic.IEnumerable<T1> source1, System.Collections.Generic.IEnumerable<T2> source2, System.Collections.Generic.IEnumerable<T3> source3, System.Func<T1, T2, T3, TResult> resultSelector)
      => ZipEx<T1, T2, T3, object, object, object, object, object, object, TResult>(source1, source2, source3, null, null, null, null, null, null, (a, b, c, d, e, f, g, h, i) => resultSelector(a, b, c));
    /// <summary>This version of Zip runs over all elements in all sequences.</summary>
    public static System.Collections.Generic.IEnumerable<TResult> ZipEx<T1, T2, T3, T4, TResult>(this System.Collections.Generic.IEnumerable<T1> source1, System.Collections.Generic.IEnumerable<T2> source2, System.Collections.Generic.IEnumerable<T3> source3, System.Collections.Generic.IEnumerable<T4> source4, System.Func<T1, T2, T3, T4, TResult> resultSelector)
      => ZipEx<T1, T2, T3, T4, object, object, object, object, object, TResult>(source1, source2, source3, source4, null, null, null, null, null, (a, b, c, d, e, f, g, h, i) => resultSelector(a, b, c, d));
    /// <summary>This version of Zip runs over all elements in all sequences.</summary>
    public static System.Collections.Generic.IEnumerable<TResult> ZipEx<T1, T2, T3, T4, T5, TResult>(this System.Collections.Generic.IEnumerable<T1> source1, System.Collections.Generic.IEnumerable<T2> source2, System.Collections.Generic.IEnumerable<T3> source3, System.Collections.Generic.IEnumerable<T4> source4, System.Collections.Generic.IEnumerable<T5> source5, System.Func<T1, T2, T3, T4, T5, TResult> resultSelector)
      => ZipEx<T1, T2, T3, T4, T5, object, object, object, object, TResult>(source1, source2, source3, source4, source5, null, null, null, null, (a, b, c, d, e, f, g, h, i) => resultSelector(a, b, c, d, e));
    /// <summary>This version of Zip runs over all elements in all sequences.</summary>
    public static System.Collections.Generic.IEnumerable<TResult> ZipEx<T1, T2, T3, T4, T5, T6, TResult>(this System.Collections.Generic.IEnumerable<T1> source1, System.Collections.Generic.IEnumerable<T2> source2, System.Collections.Generic.IEnumerable<T3> source3, System.Collections.Generic.IEnumerable<T4> source4, System.Collections.Generic.IEnumerable<T5> source5, System.Collections.Generic.IEnumerable<T6> source6, System.Func<T1, T2, T3, T4, T5, T6, TResult> resultSelector)
      => ZipEx<T1, T2, T3, T4, T5, T6, object, object, object, TResult>(source1, source2, source3, source4, source5, source6, null, null, null, (a, b, c, d, e, f, g, h, i) => resultSelector(a, b, c, d, e, f));
    /// <summary>This version of Zip runs over all elements in all sequences.</summary>
    public static System.Collections.Generic.IEnumerable<TResult> ZipEx<T1, T2, T3, T4, T5, T6, T7, TResult>(this System.Collections.Generic.IEnumerable<T1> source1, System.Collections.Generic.IEnumerable<T2> source2, System.Collections.Generic.IEnumerable<T3> source3, System.Collections.Generic.IEnumerable<T4> source4, System.Collections.Generic.IEnumerable<T5> source5, System.Collections.Generic.IEnumerable<T6> source6, System.Collections.Generic.IEnumerable<T7> source7, System.Func<T1, T2, T3, T4, T5, T6, T7, TResult> resultSelector)
      => ZipEx<T1, T2, T3, T4, T5, T6, T7, object, object, TResult>(source1, source2, source3, source4, source5, source6, source7, null, null, (a, b, c, d, e, f, g, h, i) => resultSelector(a, b, c, d, e, f, g));
    /// <summary>This version of Zip runs over all elements in all sequences.</summary>
    public static System.Collections.Generic.IEnumerable<TResult> ZipEx<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this System.Collections.Generic.IEnumerable<T1> source1, System.Collections.Generic.IEnumerable<T2> source2, System.Collections.Generic.IEnumerable<T3> source3, System.Collections.Generic.IEnumerable<T4> source4, System.Collections.Generic.IEnumerable<T5> source5, System.Collections.Generic.IEnumerable<T6> source6, System.Collections.Generic.IEnumerable<T7> source7, System.Collections.Generic.IEnumerable<T8> source8, System.Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> resultSelector)
      => ZipEx<T1, T2, T3, T4, T5, T6, T7, T8, object, TResult>(source1, source2, source3, source4, source5, source6, source7, source8, null, (a, b, c, d, e, f, g, h, i) => resultSelector(a, b, c, d, e, f, g, h));
    /// <summary>This version of Zip runs over all elements in all sequences.</summary>
    public static System.Collections.Generic.IEnumerable<TResult> ZipEx<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this System.Collections.Generic.IEnumerable<T1>? source1, System.Collections.Generic.IEnumerable<T2>? source2, System.Collections.Generic.IEnumerable<T3>? source3, System.Collections.Generic.IEnumerable<T4>? source4, System.Collections.Generic.IEnumerable<T5>? source5, System.Collections.Generic.IEnumerable<T6>? source6, System.Collections.Generic.IEnumerable<T7>? source7, System.Collections.Generic.IEnumerable<T8>? source8, System.Collections.Generic.IEnumerable<T9>? source9, System.Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> resultSelector)
    {
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      System.Collections.Generic.IEnumerator<T1> e1 = null!;
      System.Collections.Generic.IEnumerator<T2> e2 = null!;
      System.Collections.Generic.IEnumerator<T3> e3 = null!;
      System.Collections.Generic.IEnumerator<T4> e4 = null!;
      System.Collections.Generic.IEnumerator<T5> e5 = null!;
      System.Collections.Generic.IEnumerator<T6> e6 = null!;
      System.Collections.Generic.IEnumerator<T7> e7 = null!;
      System.Collections.Generic.IEnumerator<T8> e8 = null!;
      System.Collections.Generic.IEnumerator<T9> e9 = null!;

      try
      {
        e1 = source1?.GetEnumerator() ?? null!;
        e2 = source2?.GetEnumerator() ?? null!;
        e3 = source3?.GetEnumerator() ?? null!;
        e4 = source4?.GetEnumerator() ?? null!;
        e5 = source5?.GetEnumerator() ?? null!;
        e6 = source6?.GetEnumerator() ?? null!;
        e7 = source7?.GetEnumerator() ?? null!;
        e8 = source8?.GetEnumerator() ?? null!;
        e9 = source9?.GetEnumerator() ?? null!;

        while (true)
        {
          var b1 = e1?.MoveNext() ?? false;
          var b2 = e2?.MoveNext() ?? false;
          var b3 = e3?.MoveNext() ?? false;
          var b4 = e4?.MoveNext() ?? false;
          var b5 = e5?.MoveNext() ?? false;
          var b6 = e6?.MoveNext() ?? false;
          var b7 = e7?.MoveNext() ?? false;
          var b8 = e8?.MoveNext() ?? false;
          var b9 = e9?.MoveNext() ?? false;

          if (b1 || b2 || b3 || b4 || b5 || b6 || b7 || b8 || b9)
          {
            yield return resultSelector(b1 ? e1!.Current : default!, b2 ? e2!.Current : default!, b3 ? e3!.Current : default!, b4 ? e4!.Current : default!, b5 ? e5!.Current : default!, b6 ? e6!.Current : default!, b7 ? e7!.Current : default!, b8 ? e8!.Current : default!, b9 ? e9!.Current : default!);
          }
          else
          {
            yield break;
          }
        }
      }
      finally
      {
        e1?.Dispose();
        e2?.Dispose();
        e3?.Dispose();
        e4?.Dispose();
        e5?.Dispose();
        e6?.Dispose();
        e7?.Dispose();
        e8?.Dispose();
        e9?.Dispose();
      }
    }

    /// <summary>This version of Zip runs over all elements in all sequences.</summary>
    public static System.Collections.Generic.IEnumerable<TResult> ZipEx<TResult>(System.Func<object[], TResult> resultSelector, params System.Collections.Generic.IEnumerable<object>[] sequence)
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

    /// <summary>This version of Zip runs over all elements in all sequences.</summary>
    public static System.Collections.Generic.IEnumerable<TResult> ZipEx<T, TResult>(System.Func<T[], TResult> resultSelector, params System.Collections.Generic.IEnumerable<T>[] sequence)
    {
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      var e = new System.Collections.Generic.IEnumerator<T>[sequence.Length];

      for (var index = 0; index < sequence.Length; index++)
      {
        e[index] = null!;
      }

      try
      {
        for (var index = 0; index < sequence.Length; index++) e[index] = sequence[index].GetEnumerator();

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
          else
          {
            yield break;
          }
        }
      }
      finally
      {
        for (var index = 0; index < sequence.Length; index++) e[index]?.Dispose();
      }
    }
  }
}
