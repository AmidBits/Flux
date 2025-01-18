namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns the one-dimensional array as a string-builder, that can be printed in the console.</para>
    /// </summary>
    /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
    public static SpanMaker<char> Rank1ToConsole<T>(this T[] source, ConsoleFormatOptions? options = null)
      => new T[][] { source }.JaggedToConsole(options);
  }
}
