namespace Flux.Sequence.Sort
{
  /// <summary>Represents a sort algorithm.</summary>
  public abstract class ASortable<T>
  {
    public System.Collections.Generic.IComparer<T> Comparer { get; }

    public ASortable(System.Collections.Generic.IComparer<T> comparer)
      => Comparer = comparer ?? throw new System.ArgumentNullException(nameof(comparer));
  }
}
