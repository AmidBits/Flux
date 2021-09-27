namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class ArrayRank2
  {
    /// <summary>Fill the array with the specified value pattern, at the offset and count.</summary>
    /// <remarks>Is this needed lue to the one below?</remarks>
    public static void Fill<T>(this T[,] source, int dimension, int index, T value)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (source.Rank != 2) throw new System.ArgumentException($"Invalid rank ({source.Rank}).", nameof(source));

      switch (dimension)
      {
        case 0:
          for (var i0 = source.GetLength(dimension) - 1; i0 >= 0; i0--)
            source[i0, index] = value;
          break;
        case 1:
          for (var i1 = source.GetLength(dimension) - 1; i1 >= 0; i1--)
            source[index, i1] = value;
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(dimension));
      }
    }

    /// <summary>Fill the array with the specified items pattern.</summary>
    public static void Fill<T>(this T[,] source, int dimension, int index, bool repeatItemsIfNeeded, params T[] fillItems)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (source.Rank != 2) throw new System.ArgumentException($"Invalid rank ({source.Rank}).", nameof(source));
      if (dimension < 0 || dimension > 1) throw new System.ArgumentOutOfRangeException(nameof(dimension));
      if (index < 0 || (source.GetLength(dimension == 0 ? 1 : 0) is var sourceLengthX && index > sourceLengthX)) throw new System.ArgumentOutOfRangeException(nameof(index));
      if (fillItems is null || fillItems.Length == 0) fillItems = new T[] { default! };

      var itemsLength = fillItems.Length;

      var minLength = repeatItemsIfNeeded ? sourceLengthX : System.Math.Min(sourceLengthX, itemsLength);

      switch (dimension)
      {
        case 0:
          for (var i0 = 0; i0 < minLength; i0++)
            source[index, i0] = fillItems![i0 % itemsLength];
          break;
        case 1:
          for (int i1 = 0; i1 < minLength; i1++)
            source[i1, index] = fillItems![i1 % itemsLength];
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(dimension));
      }
    }
  }
}
