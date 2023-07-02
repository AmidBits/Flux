namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class ArrayRank2
  {
    /// <summary>Creates a new two-dimensional array by copying a selection of (<paramref name="count0"/>, <paramref name="count1"/>) elements from <paramref name="source"/> starting at [<paramref name="index0"/>, <paramref name="index1"/>]. Use (<paramref name="preCount0"/>, <paramref name="preCount1"/>) and (<paramref name="postCount0"/>, <paramref name="postCount1"/>) to add surrounding element space in the new array.</summary>
    public static T[,] ToCopy<T>(this T[,] source, int index0, int index1, int count0, int count1, int preCount0, int preCount1, int postCount0, int postCount1)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (source.Rank != 2) throw new System.ArgumentException($"Invalid rank ({source.Rank}).", nameof(source));

      var target = new T[preCount0 + count0 + postCount0, preCount1 + count1 + postCount1];
      Copy(source, target, index0, index1, preCount0, preCount1, count0, count1);
      return target;
    }
  }
}
