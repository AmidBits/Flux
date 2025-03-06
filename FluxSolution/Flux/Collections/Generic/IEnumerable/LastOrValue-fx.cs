namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns the last element and its index in <paramref name="source"/> that satisfies the <paramref name="predicate"/>, or <paramref name="value"/> if none is found (with index = -1).</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static (T item, int index) LastOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value, System.Func<T, int, bool>? predicate = null)
    {
      predicate ??= (e, i) => true;

      var result = (value, -1);

      var index = -1;

      foreach (var item in source)
        if (predicate(item, ++index))
          result = (item, index);

      return result;
    }
  }
}
