namespace Flux
{
  public static partial class Xtensions
  {
    public static bool AllEqual<T>(this System.Collections.Generic.IEnumerable<T> source, out T value, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      using (var enumerator = source.GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          value = enumerator.Current;

          while (enumerator.MoveNext())
            if (!comparer.Equals(value, enumerator.Current))
              return false;

          return true;
        }
        else
        {
          value = default!;
          return true;
        }
      }
    }
    public static bool AllEqual<T>(this System.Collections.Generic.IEnumerable<T> source, out T value)
      => AllEqual(source, out value, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
