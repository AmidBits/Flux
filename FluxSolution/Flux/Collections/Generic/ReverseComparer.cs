namespace Flux.Collections.Generic
{
  public sealed class ReverseComparer<T>(System.Collections.Generic.IComparer<T> comparer)
    : System.Collections.Generic.Comparer<T>
  {
    private ReverseComparer() : this(System.Collections.Generic.Comparer<T>.Default) { }

    public System.Collections.Generic.IComparer<T> SourceComparer { get; } = comparer;

    public override int Compare(T? x, T? y) => -SourceComparer.Compare(x, y);

    public override string ToString() => $"{GetType().Name} {{ {SourceComparer} }}";
  }
}
