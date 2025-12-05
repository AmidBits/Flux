namespace Flux
{
  public static partial class DataStructuresExtensions
  {
    public static DataStructures.OrderedKeySet<T> ToOrderedKeySet<T>(this System.Collections.Generic.IEnumerable<T> source)
      where T : notnull
    {
      var ohs = new DataStructures.OrderedKeySet<T>();
      foreach (var item in source)
        ohs.Add(item);
      return ohs;
    }
  }
}
