namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class XtendArray
  {
    /// <summary>Create a new two dimensional array with the perpendicular dimensional indices removed from the specified dimension.</summary>
#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
    public static T[,] Remove<T>(this T[,] source, int dimension, params int[] indices)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (dimension < 0 || dimension > 1) throw new System.ArgumentOutOfRangeException(nameof(dimension));

      var sourceLengthD = source.GetLength(dimension);
      var sourceLength0 = source.GetLength(0);
      var sourceLength1 = source.GetLength(1);

      if (sourceLengthD - indices.Length <= 0) throw new System.ArgumentException($"At least one dimension index must remain in target.");

      var target = new T[sourceLength0 - (dimension == 0 ? indices.Length : 0), sourceLength1 - (dimension == 1 ? indices.Length : 0)];

      for (int s0 = 0, t0 = 0; s0 < sourceLength0 ; s0++)
      {
        if (dimension != 0 || System.Array.IndexOf(indices, s0) == -1)
        {
          for (int s1 = 0, t1 = 0; s1 < sourceLength1 ; s1++)
          {
            if (dimension != 1 || System.Array.IndexOf(indices, s1) == -1)
            {
              target[t0, t1] = source[s0, s1];

              t1++;
            }
          }

          t0++;
        }
      }

      return target;
    }
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional
  }
}
