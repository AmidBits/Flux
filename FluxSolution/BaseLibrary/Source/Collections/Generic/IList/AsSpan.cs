namespace Flux
{
  public static partial class IListEm
  {
    public static System.Span<T> AsSpan<T>(this System.Collections.Generic.IList<T> source)
    {
      return source switch
      {
        T[] array => array,
        System.Collections.Generic.List<T> list => System.Runtime.InteropServices.CollectionsMarshal.AsSpan(list),
        System.Array array when array.Rank == 1 && array.GetType().GetElementType() == typeof(T) => (T[])array,
        _ => throw new System.ArgumentException("Cannot perform non-allocating cast."),
      };
    }
  }
}
