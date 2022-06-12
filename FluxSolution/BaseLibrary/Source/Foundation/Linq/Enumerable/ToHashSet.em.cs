namespace Flux
{
  public static partial class Enumerable
  {
    public static System.Collections.Generic.HashSet<T> ToHashSet<T>(System.Collections.Generic.IEnumerable<T> source)
      => new(source);
  }
}
