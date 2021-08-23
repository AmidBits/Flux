namespace Flux.DataStructures.Generic
{
  public class LinkedComparer<TSource>
    : System.Collections.Generic.IComparer<TSource>
  {
    private readonly System.Collections.Generic.IComparer<TSource> m_primary, m_secondary;

    public LinkedComparer(System.Collections.Generic.IComparer<TSource> primary, System.Collections.Generic.IComparer<TSource> secondary)
    {
      if (primary is null) throw new System.ArgumentNullException(nameof(primary));
      if (secondary is null) throw new System.ArgumentNullException(nameof(secondary));

      m_primary = primary;
      m_secondary = secondary;
    }

    int System.Collections.Generic.IComparer<TSource>.Compare(TSource? x, TSource? y)
      => m_primary.Compare(x, y) is var result && result == 0 ? m_secondary.Compare(x, y) : result;
  }
}
