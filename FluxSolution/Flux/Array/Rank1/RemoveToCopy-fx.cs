namespace Flux
{
  public static partial class Arrays
  {
    /// <summary>
    /// <para>Create a new array with <paramref name="count"/> elements removed from the <paramref name="source"/> starting at <paramref name="index"/>.</para>
    /// </summary>
    public static T[] RemoveCopy<T>(this T[] source, int index, int count)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var sourceLength = source.Length;

      if (index < 0 || index >= sourceLength) throw new System.ArgumentOutOfRangeException(nameof(source));

      var endIndex = index + count;

      if (count < 0 || endIndex > sourceLength) throw new System.ArgumentOutOfRangeException(nameof(count));

      var target = new T[sourceLength - count];

      if (index > 0) // Copy left-side, if needed.
        System.Array.Copy(source, 0, target, 0, index);

      if (endIndex < sourceLength) // Copy right-side, if needed.
        System.Array.Copy(source, endIndex, target, index, sourceLength - endIndex);

      return target;
    }
  }
}
