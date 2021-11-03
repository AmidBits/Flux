namespace Flux.Sorting
{
  /// <summary>Abstract base class for sort algorithms.</summary>
  public abstract class ASortable<T>
  {
    public System.Collections.Generic.IComparer<T> Comparer { get; }

    public ASortable(System.Collections.Generic.IComparer<T> comparer)
      => Comparer = comparer ?? throw new System.ArgumentNullException(nameof(comparer));
    public ASortable()
      : this(System.Collections.Generic.Comparer<T>.Default)
    { }
  }
}
