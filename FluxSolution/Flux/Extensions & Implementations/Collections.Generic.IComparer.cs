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

    private readonly SortOrder m_sortOrder;

    private RangeComparer(SortOrder sortOrder)
      => m_sortOrder = sortOrder;

    public SortOrder SortOrder => m_sortOrder;

    #region Implemented interfaces

    public int Compare(System.Range x, System.Range y)
    {
      var cmp
        = x.Start.Value > y.Start.Value ? 1
        : x.Start.Value < y.Start.Value ? -1
        : x.End.Value > y.End.Value ? 1
        : x.End.Value < y.End.Value ? -1
        : 0;

      return m_sortOrder switch
      {
        SortOrder.Ascending => cmp,
        SortOrder.Descending => -cmp,
        _ => throw new NotImplementedException(),
      };
    }

    #endregion

    public override string ToString()
      => $"{GetType().Name} {{ {m_sortOrder} }}";
  }

  #endregion

  #region ReverseComparer

  public sealed class ReverseComparer<T>
    : System.Collections.Generic.IComparer<T>
  {
    private readonly System.Collections.Generic.IComparer<T> m_comparer;

    public ReverseComparer(System.Collections.Generic.IComparer<T> comparer)
      => m_comparer = comparer;

    private ReverseComparer()
      : this(System.Collections.Generic.Comparer<T>.Default)
    { }

    public System.Collections.Generic.IComparer<T> Comparer
      => m_comparer;

    #region Implemented interfaces

    public int Compare(T? x, T? y)
      => -m_comparer.Compare(x, y);

    #endregion

    public override string ToString()
      => $"{GetType().Name} {{ {m_comparer} }}";
  }


  #endregion
}
