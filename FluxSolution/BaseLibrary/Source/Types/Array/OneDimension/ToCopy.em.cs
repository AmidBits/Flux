namespace Flux
{
  public static partial class ArrayRank2
  {
    /// <summary>Creates a new array with data from the source array. Use pre and post arguments to add surrounding space in the array.</summary>
    public static T[] ToCopy<T>(this T[] source, int offset, int count, int preCount, int postCount)
    {
      var target = new T[preCount + count + postCount];
      System.Array.Copy(source, offset, target, preCount, count);
      return target;
    }
    /// <summary>Creates a new two-dimensional array with data from the source array.</summary>
    public static T[] ToCopy<T>(this T[] source, int offset, int count)
      => ToCopy(source, offset, count, 0, 0);
  }
}
