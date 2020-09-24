namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Returns a sub-array from the specified array from the specified offset and count.</summary>
    public static T[] ToArray<T>(this T[] source, int offset, int count)
    {
      var target = new T[count];
      System.Array.Copy(source, offset, target, 0, count);
      return target;
    }
  }
}
