namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class ArrayRank1
  {
    /// <summary>Remove the specified number of elements (<paramref name="count"/>) from the <paramref name="source"/> starting at the <paramref name="index"/>. The result is not a copy of the array.</summary>
    public static void Remove<T>(ref T[] source, int index, int count)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var sourceLength = source.Length;

      if (index < 0 || index >= sourceLength) throw new System.ArgumentOutOfRangeException(nameof(source));

      var endIndex = index + count;

      if (count < 0 || endIndex > sourceLength) throw new System.ArgumentOutOfRangeException(nameof(count));

      if (endIndex < sourceLength) // Copy right-side, if needed.
        System.Array.Copy(source, endIndex, source, index, sourceLength - endIndex);
 
      System.Array.Resize(ref source, sourceLength - count);
    }
  }
}
