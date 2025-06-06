namespace Flux
{
  public static partial class StringBuilders
  {
    /// <summary>Converts the value of a substring of this instance, starting at <paramref name="startIndex"/>, to a <see langword="string"/>.</summary>
    public static string ToString(this System.Text.StringBuilder source, int startIndex)
      => source.ToString(startIndex, source.Length - startIndex);
  }
}
