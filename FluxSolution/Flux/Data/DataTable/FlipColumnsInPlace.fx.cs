namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Reverse <paramref name="count"/> columns at <paramref name="startIndex"/> of the <paramref name="source"/> <see cref="System.Data.DataTable"/> in-place. The process re-orders the columns (using the SetOrdinal() method) within the data table.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="startIndex"></param>
    /// <param name="count"></param>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static void FlipColumnsInPlace(this System.Data.DataTable source, int startIndex, int count)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (startIndex < 0 || startIndex > source.Columns.Count - 2) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
      if (count < 1 || startIndex + count > source.Columns.Count) throw new System.ArgumentOutOfRangeException(nameof(count));

      for (int columnIndex = startIndex + count - 1; columnIndex >= startIndex; columnIndex--)
        source.Columns[startIndex].SetOrdinal(columnIndex);
    }

    /// <summary>
    /// <para>Reverse <paramref name="count"/> columns at <paramref name="startIndex"/> of the <paramref name="source"/> <see cref="System.Data.DataTable"/> in-place. The process re-orders the columns (using the SetOrdinal() method) within the data table.</para>
    /// </summary>
    /// <param name="source"></param>
    public static void FlipColumnsInPlace(this System.Data.DataTable source)
      => source.FlipColumnsInPlace(0, source.Columns.Count);
  }
}
