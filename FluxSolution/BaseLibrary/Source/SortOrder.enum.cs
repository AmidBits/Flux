namespace Flux
{
  public static partial class Em
  {
    public static TSelf Signum<TSelf>(this SortOrder order)
      where TSelf : System.Numerics.INumber<TSelf>
      => order switch
      {
        SortOrder.Ascending => TSelf.One,
        SortOrder.Descending => -TSelf.One,
        _ => throw new NotImplementedException(),
      };
  }

  public enum SortOrder
  {
    /// <summary>The order is ascending, i.e. going from low-to-high or first-to-last.</summary>
    Ascending,
    /// <summary>The order is descending, i.e. going from high-to-low or last-to-first.</summary>
    Descending
  }
}
