namespace Flux.DataStructures.Generic
{
  public class LinkedComparer<TSource>
    : System.Collections.Generic.IComparer<TSource>
  {
    private readonly System.Collections.Generic.IComparer<TSource>[] m_comparers;

    public LinkedComparer(params System.Collections.Generic.IComparer<TSource>[] comparers)
      => m_comparers = comparers;

    int System.Collections.Generic.IComparer<TSource>.Compare(TSource? x, TSource? y)
    {
      for (var index = 0; index < m_comparers.Length; index++)
        if (m_comparers[index].Compare(x, y) is var result && result != 0)
          return result;

      return 0;
    }

    public override string ToString()
      => $"{GetType().Name} {{ Comparers = {m_comparers.Length} }}";
  }
}
