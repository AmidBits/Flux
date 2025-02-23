namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Swap two values, [<paramref name="a0"/>, <paramref name="a1"/>] and [<paramref name="b0"/>, <paramref name="b1"/>], in <paramref name="source"/>.</para>
    /// </summary>
    /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
    public static void Swap<T>(this T[,] source, int a0, int a1, int b0, int b1)
      => (source[b0, b1], source[a0, a1]) = (source[a0, a1], source[b0, b1]);
  }
}
