namespace Flux
{
  public static partial class Em
  {
    public static DataStructures.OrderedSet<T> ToOrderedSet<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      where T : notnull
      => new(source, equalityComparer ?? System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
