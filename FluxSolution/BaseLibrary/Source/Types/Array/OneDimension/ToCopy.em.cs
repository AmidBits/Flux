namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class ArrayRank2
  {
    /// <summary>Creates a new two-dimensional array with data from the source array. Use pre and post arguments to add surrounding space in the array.</summary>
    public static T[] ToCopy<T>(this T[] source, int offset, int count, int preCount, int postCount)
    {
      var target = new T[preCount + count + postCount];
      while (count-- > 0)
        target[preCount++] = source[offset++];
      return target;
    }
    /// <summary>Creates a new two-dimensional array with data from the source array.</summary>
    public static T[] ToCopy<T>(this T[] source, int offset, int count)
      => ToCopy(source, offset, count, 0, 0);
  }
}
