namespace Flux
{
  public static partial class ExtensionMethodsIEnumerableT
  {
    /// <summary>
    /// <para>Compares the number of elements in <paramref name="source"/> that satisfies the <paramref name="predicate"/> (all elements if null), against the specified <paramref name="count"/>.</para>
    /// </summary>
    /// <returns>Depending on <paramref name="source"/> count: -1 when less than, 0 when equal to, or 1 when greater than, the specified <paramref name="count"/>.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException"/>
    public static int CompareCount<T>(this System.Collections.Generic.IEnumerable<T> source, int count, System.Func<T, int, bool>? predicate = null)
    {
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      predicate ??= (e, i) => true; // Default predicate to all elements;

      var index = 0;
      var counter = 0;

      foreach (var item in source)
        if (predicate(item, index++))
          if (++counter > count)
            break;

      return counter > count ? 1 : counter < count ? -1 : 0;
    }
  }
}
