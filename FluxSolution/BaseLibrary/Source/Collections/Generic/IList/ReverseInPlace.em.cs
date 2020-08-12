namespace Flux
{
  public static partial class XtensionsCollections
  {
    /// <summary>Reverses the IList in-place.</summary>
    public static void ReverseInPlace<T>(this System.Collections.Generic.IList<T> source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      for (int left = 0, right = source.Count - 1; left <= right; left++, right--)
      {
        var tmp = source[left];
        source[left] = source[right];
        source[right] = tmp;
      }
    }
  }
}
