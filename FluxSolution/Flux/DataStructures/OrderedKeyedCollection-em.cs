namespace Flux
{
  public static partial class DataStructuresExtensions
  {
    public static DataStructures.OrderedKeyedCollection<T> ToOrderedKeyedCollection<T>(this System.Collections.Generic.IEnumerable<T> source)
      where T : notnull
    {
      var ohs = new DataStructures.OrderedKeyedCollection<T>();
      foreach (var item in source)
        ohs.Add(item);
      return ohs;
    }
  }
}
