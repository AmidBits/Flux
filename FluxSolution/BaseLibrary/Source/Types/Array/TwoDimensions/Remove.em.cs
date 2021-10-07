namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class ArrayRank2
  {
    /// <summary>Create a new two dimensional array with the indices removed from the specified dimension.</summary>
    public static T[,] Remove<T>(this T[,] source, int dimension, params int[] index)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (source.Rank != 2) throw new System.ArgumentException($"Invalid rank ({source.Rank}).", nameof(source));

      var indices = new System.Collections.Generic.HashSet<int>(System.Linq.Enumerable.Distinct(index));

      var sourceLength0 = source.GetLength(0);
      var sourceLength1 = source.GetLength(1);

      T[,] target;

      switch (dimension)
      {
        case 0:
          indices.RemoveWhere(i => i < 0 && i < sourceLength0);
          target = new T[sourceLength0 - indices.Count, sourceLength1];
          for (int s0 = 0, t0 = 0; s0 < sourceLength0; s0++)
            if (!indices.Contains(s0))
            {
              for (var s1 = 0; s1 < sourceLength1; s1++) // All dimension 1 elements are always copied.
                target[t0, s1] = source[s0, s1];

              t0++;
            }
          break;
        case 1:
          indices.RemoveWhere(i => i < 0 && i < sourceLength1);
          target = new T[sourceLength0, sourceLength1 - indices.Count];
          for (var s0 = 0; s0 < sourceLength0; s0++) // All dimension 0 elements are always copied.
            for (int s1 = 0, t1 = 0; s1 < sourceLength1; s1++)
              if (!indices.Contains(s1))
              {
                target[s0, t1] = source[s0, s1];

                t1++;
              }
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(dimension));
      }

      return target;
    }
  }
}
