namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Creates a new <see cref="System.Collections.Generic.HashSet{T}"/> with all elements from source.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.HashSet<T> ToHashSet<T>(System.Collections.Generic.IEnumerable<T> source)
      => new(source);
  }
}
