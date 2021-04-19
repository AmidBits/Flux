namespace Flux
{
  namespace Sequence.Sort
  {
    /// <summary>Represents an algorithm that produce a sorted copy of the source.</summary>
    public interface ISortableToCopy<T>
    {
      T[] SortToCopy(System.ReadOnlySpan<T> source);
    }
  }
}
