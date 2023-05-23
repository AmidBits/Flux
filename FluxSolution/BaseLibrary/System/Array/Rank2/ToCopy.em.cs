namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class ArrayRank2
  {
    /// <summary>Creates a new two-dimensional array with data from the source array. Use pre and post arguments to add surrounding space in the array.</summary>
    public static T[,] ToCopy<T>(this T[,] source, int sourceIndex0, int sourceIndex1, int count0, int count1, int preCount0, int preCount1, int postCount0, int postCount1)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (source.Rank != 2) throw new System.ArgumentException($"Invalid rank ({source.Rank}).", nameof(source));

      var target = new T[preCount0 + count0 + postCount0, preCount1 + count1 + postCount1];
      CopyTo(source, target, sourceIndex0, sourceIndex1, preCount0, preCount1, count0, count1);
      return target;
    }

    /// <summary>Creates a new two-dimensional array with data from the source array.</summary>
    public static T[,] ToCopy<T>(this T[,] source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (source.Rank != 2) throw new System.ArgumentException($"Invalid rank ({source.Rank}).", nameof(source));

      var target = new T[source.GetLength(0), source.GetLength(1)];
      System.Array.Copy(source, target, source.Length);
      return target;
    }
  }
}
