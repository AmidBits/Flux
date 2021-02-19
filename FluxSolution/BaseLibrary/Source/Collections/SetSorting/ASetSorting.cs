namespace Flux
{
  namespace SetSorting
  {
    /// <summary>Represents a sort algorithm.</summary>
    public abstract class ASetSorting<T>
    {
      public System.Collections.Generic.IComparer<T> Comparer { get; }

      public ASetSorting(System.Collections.Generic.IComparer<T> comparer)
        => Comparer = comparer ?? throw new System.ArgumentNullException(nameof(comparer));
    }
  }
}
