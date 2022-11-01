namespace Flux
{
  public static partial class ArrayRank2
  {
    /// <summary>Create a new sequence with all elements from the source, with specified 'major' dimension (by row or by column) first.</summary>
    public static System.Collections.Generic.IEnumerable<(int index0, int index1, T item)> GetAllElements<T>(this T[,] source, int dimension)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (source.Rank != 2) throw new System.ArgumentException($"Invalid rank ({source.Rank}).", nameof(source));

      var sourceLength0 = source.GetLength(0);
      var sourceLength1 = source.GetLength(1);

      switch (dimension)
      {
        case 0:
          for (var s0 = 0; s0 < sourceLength0; s0++)
            for (var s1 = 0; s1 < sourceLength1; s1++)
              yield return (s0, s1, source[s0, s1]);
          break;
        case 1:
          for (var s1 = 0; s1 < sourceLength1; s1++)
            for (var s0 = 0; s0 < sourceLength0; s0++)
              yield return (s0, s1, source[s0, s1]);
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(dimension));
      }
    }
  }
}
