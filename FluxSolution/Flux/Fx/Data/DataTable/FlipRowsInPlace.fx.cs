namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Reverse <paramref name="count"/> rows at <paramref name="startIndex"/> of the <paramref name="source"/> <see cref="System.Data.DataTable"/> in-line. The process swaps itemArray's within the data table.</para>
    /// </summary>
    public static void FlipRowsInPlace(this System.Data.DataTable source, int startIndex, int count)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (startIndex < 0 || startIndex > source.Rows.Count - 2) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
      if (count < 1 || startIndex + count > source.Rows.Count) throw new System.ArgumentOutOfRangeException(nameof(count));

      for (int sourceRowIndex = startIndex, targetRowIndex = startIndex + count - 1; sourceRowIndex < targetRowIndex; sourceRowIndex++, targetRowIndex--)
        (source.Rows[targetRowIndex].ItemArray, source.Rows[sourceRowIndex].ItemArray) = (source.Rows[sourceRowIndex].ItemArray, source.Rows[targetRowIndex].ItemArray);
    }
  }
}
