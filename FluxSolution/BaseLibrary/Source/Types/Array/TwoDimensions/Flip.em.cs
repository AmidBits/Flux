namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class ArrayRank2
  {
    /// <summary>Create a new two-dimensional array from the source, with the order of all elements along the specified dimension flipped.</summary>
    public static T[,] Flip<T>(this T[,] source, int dimension)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (source.Rank != 2) throw new System.ArgumentException($"Invalid rank ({source.Rank}).", nameof(source));

      var sourceLength0 = source.GetLength(0);
      var sourceLength1 = source.GetLength(1);

      var target = new T[sourceLength0, sourceLength1];

      switch (dimension)
      {
        case 0:
          var l0m1 = sourceLength0 - 1;
          var l0d2 = sourceLength0 / 2;
          for (var s1 = sourceLength1 - 1; s1 >= 0; s1--)
            for (var s0 = l0d2; s0 >= 0; s0--)
            {
              target[s0, s1] = source[l0m1 - s0, s1];
              target[l0m1 - s0, s1] = source[s0, s1];
            }
          break;
        case 1:
          var l1m1 = sourceLength1 - 1;
          var l1d2 = sourceLength1 / 2;
          for (var s0 = sourceLength0 - 1; s0 >= 0; s0--)
            for (var s1 = l1d2; s1 >= 0; s1--)
            {
              target[s0, s1] = source[s0, l1m1 - s1];
              target[s0, l1m1 - s1] = source[s0, s1];
            }
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(dimension));
      }

      return target;
    }
  }
}
