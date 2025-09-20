namespace Flux
{
  public static partial class Arrays
  {
    /// <summary>
    /// <para>Concatenates the <paramref name="source"/> and <paramref name="target"/> in the direction <paramref name="dimension"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">The "first" array.</param>
    /// <param name="target">The "second" array.</param>
    /// <param name="dimension">The direction of the concatenation.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static T[,] ConcatToCopy<T>(this T[,] source, T[,] target, int dimension)
    {
      source.AssertRank(2);
      target.AssertRank(2);

      var sl0 = source.GetLength(0);
      var sl1 = source.GetLength(1);
      var tl0 = target.GetLength(0);
      var tl1 = target.GetLength(1);

      T[,] concat;

      switch (dimension)
      {
        case 0:
          concat = new T[(sl0 + tl0), int.Max(sl1, tl1)];
          source.Copy(concat, 0, 0, 0, 0, sl0, sl1);
          target.Copy(concat, 0, 0, sl0, 0, tl0, tl1);
          break;
        case 1:
          concat = new T[int.Max(sl0, tl0), (sl1 + tl1)];
          source.Copy(concat, 0, 0, 0, 0, sl0, sl1);
          target.Copy(concat, 0, 0, 0, sl1, tl0, tl1);
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(dimension));
      }

      return concat;
    }

    /// <summary>
    /// <para>Concatenates the <paramref name="source"/> and <paramref name="target"/> in the direction <paramref name="dimension"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">The "first" array.</param>
    /// <param name="target">The "second" array.</param>
    /// <param name="dimension">The direction of the concatenation.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static T[,] ConcatToCopy<T>(this T[,] source, T[,] target, ArrayDimensionLabel dimension)
      => source.ConcatToCopy(target, (int)dimension);
  }
}
