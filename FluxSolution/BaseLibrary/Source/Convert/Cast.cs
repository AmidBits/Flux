namespace Flux
{
  public static partial class Convert
  {
    public static System.ReadOnlySpan<T> ToReadOnlySpan<T>(System.Collections.Generic.IEnumerable<T> sequence)
    {
      return sequence switch
      {
        T[] array => array,
        System.Collections.Generic.List<T> list => System.Runtime.InteropServices.CollectionsMarshal.AsSpan<T>(list),
        _ => ToReadOnlySpan(new System.Collections.Generic.List<T>(sequence)),
      };
    }
    public static System.Span<T> ToSpan<T>(System.Collections.Generic.IEnumerable<T> sequence)
    {
      return sequence switch
      {
        T[] array => array,
        System.Collections.Generic.List<T> list => System.Runtime.InteropServices.CollectionsMarshal.AsSpan(list),
        _ => ToSpan(new System.Collections.Generic.List<T>(sequence)),
      };
    }
  }
}
