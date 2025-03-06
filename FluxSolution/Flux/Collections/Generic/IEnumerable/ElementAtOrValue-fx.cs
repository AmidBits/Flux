namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns a tuple with the item and <paramref name="index"/> if available, otherwise <paramref name="value"/> and index = -1.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static (T item, int index) ElementAtOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value, int index)
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(index);

      var internalIndex = 0;

      foreach (var item in source)
        if (internalIndex == index) return (item, index);
        else internalIndex++;

      return (value, -1);
    }
  }
}
