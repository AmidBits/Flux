namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns a generic <paramref name="name"/> for the <paramref name="source"/> as if it was an index of a 0-based column-structure.</para>
    /// <para>1 is added to the <paramref name="name"/> so that the first column (the zeroth) is always "Column 1".</para>
    /// </summary>
    public static string ToOrdinalName<TValue>(this TValue source, string name = "Column")
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => $"{name} {source + TValue.One}";
  }
}
