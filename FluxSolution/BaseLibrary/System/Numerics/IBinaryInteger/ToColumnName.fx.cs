namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns a generic <paramref name="name"/> for the <paramref name="source"/> as if it was an index of a 0-based column-structure.</para>
    /// <para>+1 is added to the <paramref name="source"/> so that the first column (the zeroth) is always "Column1", and the second column (#1) is "Column2", i.e. the column names are ordinal.</para>
    /// </summary>
    public static string ToOrdinalColumnName<TNumber>(this TNumber source, string name = "Column")
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => name + (source + TNumber.One).ToString();
  }
}
