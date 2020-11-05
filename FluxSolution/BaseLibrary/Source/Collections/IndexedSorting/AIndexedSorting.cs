namespace Flux
{
  namespace IndexedSorting
  {
    /// <summary>Represents a sort algorithm.</summary>
    public abstract class AIndexedSorting<T>
      : ISpanSort<T>
    {
      public System.Collections.Generic.IComparer<T> Comparer { get; }

      public AIndexedSorting(System.Collections.Generic.IComparer<T> comparer)
        => Comparer = comparer ?? throw new System.ArgumentNullException(nameof(comparer));

      public abstract void SortInline(System.Span<T> source);
      public abstract T[] SortToCopy(System.ReadOnlySpan<T> source);

      public void SortInline(System.Collections.Generic.IList<T> source)
        => SortInline(new System.Span<T>((T[])source));
      public System.Collections.Generic.IList<T> SortToCopy(System.Collections.Generic.IList<T> source)
      {
        var target = new System.Collections.Generic.List<T>(source);
        SortInline(new System.Span<T>((T[])(System.Collections.Generic.IList<T>)target));
        return target;
      }
    }
  }
}
