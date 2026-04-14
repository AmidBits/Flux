namespace Flux
{
  public static partial class DataStructuresExtensions
  {
    public static DataStructures.OrderedCollection<T> ToOrderedCollection<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      where T : notnull
      => new(source, equalityComparer ?? System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
