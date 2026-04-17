namespace Flux
{
  public static class IComparerExtensions
  {
    extension<T>(System.Collections.Generic.IComparer<T> source)
    {
      public System.Collections.Generic.IComparer<T> CreateReverseComparer()
        => new ReverseComparer<T>(source);
    }
  }

  #region RangeComparer

  public sealed class RangeComparer
    : System.Collections.Generic.IComparer<System.Range>
  {
    public static RangeComparer Ascending { get; } = new(SortOrder.Ascending);
    public static RangeComparer Descending { get; } = new(SortOrder.Descending);

    private RangeComparer(SortOrder sortOrder) => SortOrder = sortOrder;

    public SortOrder SortOrder { get; }

    public int Compare(System.Range x, System.Range y)
    {
      var cmp
        = x.Start.Value > y.Start.Value ? 1
        : x.Start.Value < y.Start.Value ? -1
        : x.End.Value > y.End.Value ? 1
        : x.End.Value < y.End.Value ? -1
        : 0;

      return SortOrder switch
      {
        SortOrder.Ascending => cmp,
        SortOrder.Descending => -cmp,
        _ => throw new System.NotImplementedException(),
      };
    }

    public override string ToString() => $"{GetType().Name} {{ {SortOrder} }}";
  }

  #endregion

  #region ReverseComparer

  public sealed class ReverseComparer<T>(System.Collections.Generic.IComparer<T> comparer)
    : System.Collections.Generic.IComparer<T>
  {
    public static System.Collections.Generic.IComparer<T> Default => new ReverseComparer<T>(System.Collections.Generic.Comparer<T>.Default);

    public System.Collections.Generic.IComparer<T> Comparer { get; } = comparer;

    public int Compare(T? x, T? y) => -Comparer.Compare(x, y);

    public override string ToString() => $"{GetType().Name} {{ {Comparer} }}";
  }

  #endregion
}
