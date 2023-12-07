namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class Fx
  {
    /// <summary>Modify <paramref name="source"/> by removing <paramref name="count"/> elements starting at <paramref name="index"/>.</summary>
    public static void Remove<T>(ref T[] source, int index, int count)
    {
      System.ArgumentNullException.ThrowIfNull(source);

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
