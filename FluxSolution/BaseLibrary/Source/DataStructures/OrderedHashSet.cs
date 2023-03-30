namespace Flux
{
  public static partial class DataStructuresExtensionMethods
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
    public sealed class OrderedHashSet<TItemAndKeyInOne>
      : System.Collections.ObjectModel.KeyedCollection<TItemAndKeyInOne, TItemAndKeyInOne>
      where TItemAndKeyInOne : notnull
    {
      protected override TItemAndKeyInOne GetKeyForItem(TItemAndKeyInOne item) => item;

      public override string ToString() => $"{GetType().Name} {{ Count = {Count} }}";
    }
  }
}
