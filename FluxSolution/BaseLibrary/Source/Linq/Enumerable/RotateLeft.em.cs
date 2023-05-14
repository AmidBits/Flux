namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Returns the sequence rotated left by the specified count.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<T> RotateLeft<T>(this System.Collections.Generic.IEnumerable<T> source, int count)
    {
      if (count < 0) throw new System.ArgumentOutOfRangeException(nameof(count));

      var items = source.ToList();

      var index = (items.Count < count ? (count % items.Count) : count);

      for (var i = index; i < items.Count; i++)
        yield return items[i];

      for (var i = 0; i < index; i++)
        yield return items[i];
    }
  }
}
