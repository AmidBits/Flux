namespace Flux
{
  public static partial class Arrays
  {
    /// <summary>
    /// <para>Allocates a new array, the same size as <paramref name="source"/>.</para>
    /// <para>No data is copied.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static T[,] New<T>(this T[,] source)
      => new T[source.GetLength(0), source.GetLength(1)];

    /// <summary>
    /// <para>Allocates a new array, based on the size of <paramref name="source"/>. Dimension-1 (i.e. rows) are added/removed with <paramref name="addOrRemove0"/> and dimension-1 (i.e. columns) are added/removed with <paramref name="addOrRemove1"/>.</para>
    /// <para>A negative value will remove and a positive value will add, as many column as the number represents.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="addOrRemove0">A negative value removes and a positive value adds, as many rows (dimension-0) as the number represents.</param>
    /// <param name="addOrRemove1">A negative value removes and a positive value adds, as many columns (dimension-1) as the number represents.</param>
    /// <returns></returns>
    public static T[,] NewResize<T>(this T[,] source, int addOrRemove0, int addOrRemove1)
      => new T[source.GetLength(0) + addOrRemove0, source.GetLength(1) + addOrRemove1];
  }
}
