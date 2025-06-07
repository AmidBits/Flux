namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Reverse <paramref name="count"/> rows at <paramref name="startIndex"/> of the <paramref name="source"/> <see cref="System.Data.DataTable"/> in-place. The process swaps itemArray's within the data table.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="startIndex"></param>
    /// <param name="count"></param>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static void FlipRowsInPlace(this System.Data.DataTable source, int startIndex, int count)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (startIndex < 0 || startIndex > source.Rows.Count - 2) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
      if (count < 1 || startIndex + count > source.Rows.Count) throw new System.ArgumentOutOfRangeException(nameof(count));

      var sourceRowIndex = startIndex;
      var targetRowIndex = startIndex + count - 1;

      while (sourceRowIndex < targetRowIndex)
        (source.Rows[targetRowIndex].ItemArray, source.Rows[sourceRowIndex].ItemArray) = (source.Rows[sourceRowIndex++].ItemArray, source.Rows[targetRowIndex--].ItemArray);
    }

    /// <summary>
    /// <para>Reverse all rows in the <paramref name="source"/> <see cref="System.Data.DataTable"/>. The process swaps itemArray's within the data table.</para>
    /// </summary>
    public static void FlipRowsInPlace(this System.Data.DataTable source)
      => source.FlipRowsInPlace(0, source.Rows.Count);

  }
}
