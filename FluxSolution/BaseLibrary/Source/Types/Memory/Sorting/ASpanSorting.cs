namespace Flux
{
  namespace SpanSorting
  {
    /// <summary>Represents a sort algorithm.</summary>
    public abstract class ASpanSorting<T>
    {
      public System.Collections.Generic.IComparer<T> Comparer { get; }

      public ASpanSorting(System.Collections.Generic.IComparer<T> comparer)
        => Comparer = comparer ?? throw new System.ArgumentNullException(nameof(comparer));
    }
  }
}
