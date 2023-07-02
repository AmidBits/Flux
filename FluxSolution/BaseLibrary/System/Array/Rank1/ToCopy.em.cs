namespace Flux
{
  public static partial class ArrayRank1
  {
    /// <summary>Creates a new array with <paramref name="count"/> elements from <paramref name="source"/> starting at <paramref name="index"/>. Use <paramref name="preCount"/> and <paramref name="postCount"/> arguments to add surrounding element space in the new array.</summary>
    /// <param name="source">W</param>
    /// <param name="index"></param>
    /// <param name="count"></param>
    /// <param name="preCount"></param>
    /// <param name="postCount"></param>
    /// <returns></returns>
    public static T[] ToCopy<T>(this T[] source, int index, int count, int preCount = 0, int postCount = 0)
    {
      var target = new T[preCount + count + postCount];
      System.Array.Copy(source, index, target, preCount, count);
      return target;
    }
  }
}
