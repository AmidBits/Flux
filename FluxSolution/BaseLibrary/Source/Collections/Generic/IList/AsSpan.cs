namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static System.Span<T> AsSpan<T>(this System.Collections.Generic.IList<T> source)
    {
      return source switch
      {
        T[] array => array,
        System.Collections.Generic.List<T> list => AsSpan(list),
        System.Array array when array.Rank == 1 && array.GetType().GetElementType() == typeof(T) => (T[])array,
        _ => throw new System.ArgumentOutOfRangeException("Cannot perform non-allocating cast."),
      };
    }
  }
}