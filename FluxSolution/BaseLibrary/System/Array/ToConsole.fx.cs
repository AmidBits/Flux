namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns the two-dimensional array as a new sequence of grid-like formatted strings, that can be printed in the console.</para>
    /// </summary>
    /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
    public static System.Text.StringBuilder ToConsole<T>(this System.Array source, ConsoleFormatOptions? options = null)
    {
      return source.GetArrayType() switch
      {
        ArrayType.NotAnArray => new System.Text.StringBuilder("Not an array."),
        ArrayType.OneDimensionalArray => new T[][] { (T[])source }.JaggedToConsole(options),
        ArrayType.TwoDimensionalArray => ((T[,])source).Rank2ToConsole(options),
        ArrayType.MultiDimensionalArray => new System.Text.StringBuilder("Multi-dimensional array."),
        ArrayType.JaggedArray => ((T[][])source).JaggedToConsole(options),
        _ => throw new NotImplementedException(),
      };
    }
  }
}
