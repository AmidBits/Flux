namespace Flux
{
  public class RangeComparer
    : System.Collections.Generic.IComparer<System.Range>
  {
    public static RangeComparer Ascending { get; } = new(SortOrder.Ascending);
    public static RangeComparer Descending { get; } = new(SortOrder.Descending);

    private readonly SortOrder m_sortOrder;

    private RangeComparer(SortOrder sortOrder) => m_sortOrder = sortOrder;

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
  }
}
