namespace Flux
{
  public static partial class Arrays
  {
    /// <summary>
    /// <para>Determines whether the <see cref="System.Array"/> is a jagged-array, i.e. an array of arrays.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsJaggedArray(this System.Array source)
      => source.GetType().GetElementType()?.IsArray ?? false;
  }
}
