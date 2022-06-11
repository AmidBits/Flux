namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Returns a sequence with the elements from the source and the specified elements.</summary>
    public static System.Collections.Generic.IEnumerable<T> Append<T>(this System.Collections.Generic.IEnumerable<T> source, params T[] append)
      => System.Linq.Enumerable.Concat(source, append);
    /// <summary>Returns a sequence with the elements from the source and the specified sequences.</summary>
    public static System.Collections.Generic.IEnumerable<T> Append<T>(this System.Collections.Generic.IEnumerable<T> source, params System.Collections.Generic.IEnumerable<T>[] others)
      => System.Linq.Enumerable.Concat(source, System.Linq.Enumerable.SelectMany(others, append => append));
  }
}
