namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class ArrayRank1
  {
    /// <summary>Create a new array with <paramref name="count"/> elements removed from the <paramref name="source"/> starting at <paramref name="index"/>.</summary>
    public static T[] Remove<T>(this T[] source, int index, int count)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

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
