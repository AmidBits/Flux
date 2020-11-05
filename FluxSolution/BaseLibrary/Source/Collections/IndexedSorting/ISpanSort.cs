namespace Flux
{
  /// <summary>Represents an sort algorithm for Span/ReadOnlySpan.</summary>
	public interface ISpanSort<T>
  {
    /// <summary>Sort the Span inline.</summary>
    /// <param name="source">The Span to sort inline.</param>
    void SortInline(System.Span<T> source);
    /// <summary>Sort the ReadOnlySpan into a Span copy.</summary>
    /// <param name="source">The ReadOnlySpan to sort into a Span copy.</param>
    T[] SortToCopy(System.ReadOnlySpan<T> source);
  }
}
