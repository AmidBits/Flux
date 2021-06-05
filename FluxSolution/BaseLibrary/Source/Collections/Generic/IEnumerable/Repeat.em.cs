namespace Flux
{
  public static partial class SystemCollectionsGenericIEnumerableEm
  {
    public static System.Collections.Generic.IEnumerable<T> Repeat<T>(this System.Collections.Generic.IEnumerable<T> source, int count)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (count < 0) throw new System.ArgumentNullException(nameof(count));

      var list = new System.Collections.Generic.List<T>(source);

      while (count-- >= 0)
        foreach (var item in list)
          yield return item;
    }
  }
}
