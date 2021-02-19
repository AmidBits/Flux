namespace Flux
{
  namespace SetSorting
  {
    /// <summary>Represents an algorithm that applied sorting in-place on the source.</summary>
    public interface ISortableInPlace<T>
    {
      void SortInPlace(System.Span<T> source);

      T[] CopyAndSortInPlace(System.ReadOnlySpan<T> source)
      {
        var copy = source.ToArray();
        SortInPlace(new System.Span<T>(copy));
        return copy;
      }

    }
  }
}
