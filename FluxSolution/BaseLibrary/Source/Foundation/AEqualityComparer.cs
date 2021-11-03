namespace Flux
{
  public abstract class AEqualityComparer<T>
  {
    public System.Collections.Generic.IEqualityComparer<T> EqualityComparer { get; }

    public AEqualityComparer(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      => EqualityComparer = equalityComparer ?? throw new System.ArgumentNullException(nameof(equalityComparer));
    public AEqualityComparer()
      : this(System.Collections.Generic.EqualityComparer<T>.Default)
    { }
  }
}
