namespace Flux
{
  public static partial class Fx
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

      var sourceLength0 = source.GetLength(0);
      var sourceLength1 = source.GetLength(1);
      var targetLength0 = target.GetLength(0);
      var targetLength1 = target.GetLength(1);

      T[,] concat;

      switch (dimension)
      {
        case 0:
          concat = new T[(sourceLength0 + targetLength0), System.Math.Max(sourceLength1, targetLength1)];
          source.CopyTo(concat, 0, 0, 0, 0, sourceLength0, sourceLength1);
          target.CopyTo(concat, 0, 0, sourceLength0, 0, targetLength0, targetLength1);
          break;
        case 1:
          concat = new T[System.Math.Max(sourceLength0, targetLength0), (sourceLength1 + targetLength1)];
          source.CopyTo(concat, 0, 0, 0, 0, sourceLength0, sourceLength1);
          target.CopyTo(concat, 0, 0, 0, sourceLength1, targetLength0, targetLength1);
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
    public static T[,] ConcatToCopy<T>(this T[,] source, T[,] target, ArrayDimension dimension) => source.ConcatToCopy(target, (int)dimension);
  }
}
