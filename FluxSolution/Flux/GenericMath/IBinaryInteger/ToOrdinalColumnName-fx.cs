namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Returns a generic <paramref name="name"/> for the <paramref name="value"/> as if it was an index of a 0-based column-structure.</para>
    /// <para>+1 is added to the <paramref name="value"/> so that the first column (the zeroth) is always "Column1", and the second column (#1) is "Column2", i.e. the column names are ordinal.</para>
    /// </summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static string ToSingleOrdinalColumnName<TNumber>(this TNumber value, string name = "Column")
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => name + (value + TNumber.One).ToString();

    /// <summary>
    /// <para>Creates an array of generic column-<paramref name="name"/>s for <paramref name="value"/> amount of columns.</para>
    /// <example><paramref name="value"/> = 3, returns <c>["Column1", "Column2", "Column3"]</c></example>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string[] ToMultipleOrdinalColumnNames<TNumber>(this TNumber value, string name = "Column")
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      var maxWidth = int.CreateChecked(value.DigitCount(10));

      return System.Linq.Enumerable.Range(1, int.CreateChecked(value)).Select(i => name + i.ToString().PadLeft(maxWidth, '0')).ToArray();
    }
  }
}
