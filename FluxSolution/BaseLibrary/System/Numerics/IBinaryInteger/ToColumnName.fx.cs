namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns a generic column name for the <paramref name="source"/> as if it were an index of a 0-based column-structure.</para>
    /// <para>1 is added to the column name so that the first column (the zeroth) is always "Column_1".</para>
    /// </summary>
    public static string ToColumnName<TValue>(this TValue source)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => $"Column_{source + TValue.One}";
  }
}
