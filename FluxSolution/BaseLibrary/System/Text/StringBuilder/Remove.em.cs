namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Converts the value of a substring of this instance, starting at <paramref name="startIndex"/>, to a <see langword="string"/>.</summary>
    public static System.Text.StringBuilder Remove(this System.Text.StringBuilder source, int startIndex)
      => source.Remove(startIndex, source.Length - startIndex);
  }
}
