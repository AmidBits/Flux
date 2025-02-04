namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns a one/two-dimensional or jagged array as a new <see cref="SpanMaker{T}"/>, that can be printed in the console.</para>
    /// </summary>
    /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
    public static SpanMaker<char> ToConsole<T>(this System.Array source, ConsoleFormatOptions? options = null)
    {
      return source.GetArrayType() switch
      {
        ArrayType.NotAnArray => new SpanMaker<char>("Not an array."),
        ArrayType.OneDimensionalArray => ((T[])source).Rank1ToConsoleString(options).ToSpanMaker(),
        ArrayType.TwoDimensionalArray => ((T[,])source).Rank2ToConsoleString(options).ToSpanMaker(),
        ArrayType.MultiDimensionalArray => new SpanMaker<char>("Multi-dimensional array."),
        ArrayType.JaggedArray => ((T[][])source).JaggedToConsoleString(options).ToSpanMaker(),
        _ => throw new NotImplementedException(),
      };
    }
  }
}
