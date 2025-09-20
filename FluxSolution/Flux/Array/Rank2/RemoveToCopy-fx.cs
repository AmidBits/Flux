namespace Flux
{
  public static partial class Arrays
  {
    public static T[,] Remove0ToCopy<T>(this T[,] source, params int[] indices)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var hs = new System.Collections.Generic.HashSet<int>(indices);

      var sourceLength0 = source.GetLength(0);
      var sourceLength1 = source.GetLength(1);

      var target = new T[sourceLength0 - hs.Count, sourceLength1];

      source.Copy0Remove(target, indices);

      return target;
    }

    public static T[,] Remove1ToCopy<T>(this T[,] source, params int[] indices)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var hs = new System.Collections.Generic.HashSet<int>(indices);

      var sourceLength0 = source.GetLength(0);
      var sourceLength1 = source.GetLength(1);

      var target = new T[sourceLength0, sourceLength1 - hs.Count];

      source.Copy1Remove(target, indices);

      return target;
    }

    ///// <summary>
    ///// <para>Create a new array from <paramref name="source"/> with the <paramref name="indices"/> of strands (rows or columns) removed from the specified <paramref name="dimension"/>.</para>
    ///// </summary>
    ///// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
    //public static T[,] RemoveToCopy<T>(this T[,] source, int dimension, params int[] indices)
    //{
    //  source.AssertRank(2);

    //  var hs = new System.Collections.Generic.HashSet<int>(indices);

    //  var sourceLength0 = source.GetLength(0);
    //  var sourceLength1 = source.GetLength(1);

    //  T[,] target;

    //  switch (dimension)
    //  {
    //    case 0:
    //      target = new T[sourceLength0 - hs.Count, sourceLength1];
    //      source.Copy0Except(target, indices);
    //      //for (int s0 = 0, t0 = 0; s0 < sourceLength0; s0++)
    //      //  if (!hs.Contains(s0))
    //      //  {
    //      //    for (var s1 = 0; s1 < sourceLength1; s1++) // All dimension 1 elements are always copied.
    //      //      target[t0, s1] = source[s0, s1];

    //      //    t0++;
    //      //  }
    //      break;
    //    case 1:
    //      target = new T[sourceLength0, sourceLength1 - hs.Count];
    //      source.Copy1Except(target, indices);
    //      //for (var s0 = 0; s0 < sourceLength0; s0++) // All dimension 0 elements are always copied.
    //      //  for (int s1 = 0, t1 = 0; s1 < sourceLength1; s1++)
    //      //    if (!hs.Contains(s1))
    //      //    {
    //      //      target[s0, t1] = source[s0, s1];

    //      //      t1++;
    //      //    }
    //      break;
    //    default:
    //      throw new System.ArgumentOutOfRangeException(nameof(dimension));
    //  }

    //  return target;
    //}

    ///// <summary>
    ///// <para>Create a new array from <paramref name="source"/> with the <paramref name="indices"/> of strands (rows or columns) removed from the specified <paramref name="dimension"/>.</para>
    ///// </summary>
    ///// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
    //public static T[,] RemoveToCopy<T>(this T[,] source, ArrayDimensionLabel dimension, params int[] indices)
    //  => source.RemoveToCopy((int)dimension, indices);
  }
}
