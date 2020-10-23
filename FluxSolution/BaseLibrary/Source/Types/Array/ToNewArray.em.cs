namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Returns a new array with the range of data and the additional pre/post number of slots specified.</summary>
    public static T[] ToNewArray<T>(this T[] source, int sourceOffset, int sourceCount, int targetPreCount, int targetPostCount)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (sourceOffset < 0 || sourceOffset >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(sourceOffset));
      if (targetPreCount < 0) throw new System.ArgumentOutOfRangeException(nameof(targetPreCount));
      if (targetPostCount < 0) throw new System.ArgumentOutOfRangeException(nameof(targetPostCount));

      var target = new T[targetPreCount + sourceCount + targetPostCount];
      System.Array.Copy(source, sourceOffset, target, targetPreCount, sourceCount);
      return target;
    }
    /// <summary>Returns a new array with the range of data specified.</summary>
    public static T[] ToNewArray<T>(this T[] source, int offset, int count)
      => ToNewArray(source, offset, count, 0, 0);
  }
}
