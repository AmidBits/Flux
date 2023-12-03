namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns the last element and its index in <paramref name="source"/> that satisfies the <paramref name="predicate"/>, or <paramref name="value"/> if none is found (with index = -1).</summary>
    public static (T item, int index) LastOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value, System.Func<T, int, bool>? predicate = null)
    {
      predicate ??= (e, i) => true;

      var result = (value, -1);

      var index = 0;

      foreach (var item in source)
      {
        if (predicate(item, index))
          result = (item, index);

        index++;
      }

      return result;
    }
  }
}
