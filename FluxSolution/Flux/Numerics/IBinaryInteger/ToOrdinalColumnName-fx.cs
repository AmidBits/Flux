namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>
    /// <para>Returns a generic <paramref name="columnNamePrefix"/> for the <paramref name="source"/> as if it was an index of a 0-based column-structure.</para>
    /// <para>+1 is added to the <paramref name="source"/> so that the first column (the zeroth) is always "Column1", and the second column (#1) is "Column2", i.e. the column names are ordinal.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="source"></param>
    /// <param name="numericWidth"></param>
    /// <param name="columnNamePrefix"></param>
    /// <returns></returns>
    public static string ToSingleOrdinalColumnName<TInteger>(this TInteger source, int numericWidth, string columnNamePrefix = "Column")
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => columnNamePrefix + (source + TInteger.One).ToString($"D{numericWidth}", null);

    /// <summary>
    /// <para>Returns a generic <paramref name="columnNamePrefix"/> for the <paramref name="source"/> as if it was an index of a 0-based column-structure.</para>
    /// <para>+1 is added to the <paramref name="source"/> so that the first column (the zeroth) is always "Column1", and the second column (#1) is "Column2", i.e. the column names are ordinal.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="source"></param>
    /// <param name="columnNamePrefix"></param>
    /// <returns></returns>
    public static string ToSingleOrdinalColumnName<TInteger>(this TInteger source, string columnNamePrefix = "Column")
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => source.ToSingleOrdinalColumnName(int.CreateChecked(source.DigitCount(TInteger.CreateChecked(10))), columnNamePrefix);

    /// <summary>
    /// <para>Creates an array of generic column-<paramref name="columnNamePrefix"/>s for <paramref name="source"/> amount of columns.</para>
    /// <example><paramref name="source"/> = 3, returns <c>["Column1", "Column2", "Column3"]</c></example>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="source"></param>
    /// <param name="columnNamePrefix"></param>
    /// <returns></returns>
    public static string[] ToMultipleOrdinalColumnNames<TNumber>(this TNumber source, string columnNamePrefix = "Column")
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      var maxWidth = int.CreateChecked(source.DigitCount(10));

      return [.. System.Linq.Enumerable.Range(1, int.CreateChecked(source)).Select(i => i.ToSingleOrdinalColumnName(maxWidth, columnNamePrefix))];
    }
  }
}
