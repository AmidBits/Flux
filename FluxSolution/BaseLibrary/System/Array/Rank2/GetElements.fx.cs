namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Create a new sequence with elements in <paramref name="source"/> from the specified <paramref name="dimension"/> and <paramref name="index"/> (within the <paramref name="dimension"/>).</para>
    /// <example>One can interpret the parameters as all elements of the fourth (<paramref name="index"/> = 3, zero-based) "row" (<paramref name="dimension"/> = 0), or all elements of the fourth (<paramref name="index"/> = 3, zero-based) "column" (<paramref name="dimension"/> = 1).</example>
    /// </summary>
    /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
    public static T[] GetElements<T>(this T[,] source, int dimension, int index)
    {
      source.AssertRank(2);

      var sourceLength0 = source.GetLength(0);
      var sourceLength1 = source.GetLength(1);

      T[] target;

      switch (dimension)
      {
        case 0:
          target = new T[sourceLength1];
          for (var s1 = 0; s1 < sourceLength1; s1++)
            target[s1] = source[index, s1];
          break;
        case 1:
          target = new T[sourceLength0];
          for (int s0 = 0; s0 < sourceLength0; s0++)
            target[s0] = source[s0, index];
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(dimension));
      }

      return target;
    }

    /// <summary>
    /// <para>Create a new sequence with elements in <paramref name="source"/> from the specified <paramref name="dimension"/> and <paramref name="index"/> (within the <paramref name="dimension"/>).</para>
    /// <example>One can interpret the parameters as all elements of the fourth (<paramref name="index"/> = 3, zero-based) "row" (<paramref name="dimension"/> = 0), or all elements of the fourth (<paramref name="index"/> = 3, zero-based) "column" (<paramref name="dimension"/> = 1).</example>
    /// </summary>
    /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
    public static T[] GetElements<T>(this T[,] source, ArrayDimension dimension, int index) => source.GetElements((int)dimension, index);
  }
}
