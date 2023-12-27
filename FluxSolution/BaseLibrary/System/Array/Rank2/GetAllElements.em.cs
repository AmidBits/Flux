namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>
    /// <para>Create a new sequence with all elements in <paramref name="source"/> with the specified 'major' <paramref name="dimension"/> order, i.e. by row or by column first (then the other).</para>
    /// </summary>
    /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
    public static T[] GetAllElements<T>(this T[,] source, int dimension)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.Rank != 2) throw new System.ArgumentException($"Invalid rank ({source.Rank}).", nameof(source));

      var sourceLength0 = source.GetLength(0);
      var sourceLength1 = source.GetLength(1);

      var target = new T[sourceLength0 * sourceLength1];

      var targetIndex = 0;

      switch (dimension)
      {
        case 0:
          for (var s0 = 0; s0 < sourceLength0; s0++)
            for (var s1 = 0; s1 < sourceLength1; s1++)
              target[targetIndex++] = source[s0, s1];
          break;
        case 1:
          for (var s1 = 0; s1 < sourceLength1; s1++)
            for (var s0 = 0; s0 < sourceLength0; s0++)
              target[targetIndex++] = source[s0, s1];
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(dimension));
      }

      return target;
    }
  }
}
