namespace Flux
{
  public abstract class AComparer<T>
  {
    public System.Collections.Generic.IComparer<T> Comparer { get; }

    public AComparer(System.Collections.Generic.IComparer<T> comparer)
      => Comparer = comparer ?? throw new System.ArgumentNullException(nameof(comparer));
    public AComparer()
      : this(System.Collections.Generic.Comparer<T>.Default)
    { }
  }
}
