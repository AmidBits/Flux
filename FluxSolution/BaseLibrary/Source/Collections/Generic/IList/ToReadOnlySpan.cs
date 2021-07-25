namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>If possible use non-allocating code, which currently works for arrays and lists.</summary>
    public static System.ReadOnlySpan<T> ToReadOnlySpan<T>(this System.Collections.Generic.IList<T> source)
    {
      return source switch
      {
        T[] array => array,
        System.Collections.Generic.List<T> list => System.Runtime.InteropServices.CollectionsMarshal.AsSpan<T>(list),
        System.Array array when array.Rank == 1 && array.GetType().GetElementType() == typeof(T) => (T[])array,
        _ => ToReadOnlySpan(new System.Collections.Generic.List<T>(source)),
      };
    }
  }
}
