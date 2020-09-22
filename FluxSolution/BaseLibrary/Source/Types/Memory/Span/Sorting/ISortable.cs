namespace Flux
{
  /// <summary>Represents a sort algorithm.</summary>
	public interface ISortable<T>
  {
    /// <summary>Sort the sequence.</summary>
    /// <param name="source">The sequence to sort.</param>
		void Sort(System.Span<T> source);
  }
}
