namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Returns the sequence rotated right by the specified count.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<T> RotateRight<T>(this System.Collections.Generic.IEnumerable<T> source, int count)
    {
      if (count < 0) throw new System.ArgumentOutOfRangeException(nameof(count));

      var list = source.ToList();

      var index = list.Count - (list.Count < count ? (count % list.Count) : count);

      for (var i = index; i < list.Count; i++)
        yield return list[i];

      for (var i = 0; i < index; i++)
        yield return list[i];
    }
  }
}
