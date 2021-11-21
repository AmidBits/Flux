namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static DataStructures.OrderedHashSet<T> ToOrderedHashSet<T>(this System.Collections.Generic.IEnumerable<T> source)
      where T : notnull
    {
      var ohs = new DataStructures.OrderedHashSet<T>();
      foreach (var item in source)
        ohs.Add(item);
      return ohs;
    }
  }

  namespace DataStructures
  {
    public sealed class OrderedHashSet<T>
      : System.Collections.ObjectModel.KeyedCollection<T, T>
      where T : notnull
    {
      protected override T GetKeyForItem(T item)
        => item;

      public override string ToString()
        => $"{GetType().Name} {{ Count = {Count} }}";
    }
  }
}
