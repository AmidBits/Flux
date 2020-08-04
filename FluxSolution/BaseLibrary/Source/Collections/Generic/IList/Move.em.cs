namespace Flux
{
  public static partial class XtensionsCollections
  {
    /// <summary>Move the first element in the list to the specified index.</summary>
    public static System.Collections.Generic.IList<T> MoveFirst<T>(this System.Collections.Generic.IList<T> source, int newIndex)
      => MoveItem(source, 0, newIndex);

    /// <summary>Remove the element at fromIndex from the list, then insert the element at toIndex.</summary>
    public static System.Collections.Generic.IList<T> MoveItem<T>(this System.Collections.Generic.IList<T> source, int fromIndex, int toIndex)
    {
      if (fromIndex < 0 || fromIndex >= source.Count) throw new System.ArgumentOutOfRangeException(nameof(fromIndex));
      if (toIndex < 0 || toIndex >= source.Count) throw new System.ArgumentOutOfRangeException(nameof(toIndex));

      var tmp = source[fromIndex];
      source.RemoveAt(fromIndex);
      source.Insert(toIndex, tmp);

      return source;
    }

    /// <summary>Move the last element in the list to the specified index.</summary>
    public static System.Collections.Generic.IList<T> MoveLast<T>(this System.Collections.Generic.IList<T> source, int newIndex)
      => MoveItem(source, source.Count - 1, newIndex);
  }
}
