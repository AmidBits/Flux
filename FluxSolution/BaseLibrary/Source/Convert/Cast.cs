namespace Flux
{
  public static partial class Convert
  {
    public static System.ReadOnlySpan<T> ToReadOnlySpan<T>(System.Collections.Generic.IEnumerable<T> sequence)
    {
      return sequence switch
      {
        T[] array => array,
        System.Collections.Generic.IList<T> ilist => (T[])ilist,
        System.Collections.Generic.IReadOnlyList<T> ireadonlylist => (T[])ireadonlylist,
        _ => ToReadOnlySpan(new System.Collections.Generic.List<T>(sequence)),
      };
    }
    public static System.Span<T> ToSpan<T>(System.Collections.Generic.IEnumerable<T> sequence)
    {
      return sequence switch
      {
        T[] array => array,
        System.Collections.Generic.IList<T> ilist => (T[])ilist,
        _ => ToSpan(new System.Collections.Generic.List<T>(sequence)),
      };
    }
  }
}
