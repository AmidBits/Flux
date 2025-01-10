namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns a generic <paramref name="name"/> for the <paramref name="source"/> as if it was an index of a 0-based column-structure.</para>
    /// <para>+1 is added to the <paramref name="source"/> so that the first column (the zeroth) is always "Column1", and the second column (#1) is "Column2", i.e. the column names are ordinal.</para>
    /// </summary>
    public static string ToSingleOrdinalColumnName<TNumber>(this TNumber source, string name = "Column")
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => name + (source + TNumber.One).ToString();

    /// <summary>
    /// <para>Creates an array of generic column-<paramref name="name"/>s for <paramref name="source"/> amount of columns.</para>
    /// <example><paramref name="source"/> = 3, returns <c>["Column1", "Column2", "Column3"]</c></example>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="source"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string[] ToNumberOfOrdinalColumnNames<TNumber>(this TNumber source, string name = "Column")
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      var maxWidth = int.CreateChecked(source.DigitCount(10));

      return System.Linq.Enumerable.Range(1, int.CreateChecked(source)).Select(i => name + i.ToString().PadLeft(maxWidth, '0')).ToArray();
    }
  }
}
