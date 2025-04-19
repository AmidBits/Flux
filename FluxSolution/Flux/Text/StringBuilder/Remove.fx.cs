namespace Flux
{
  public static partial class StringBuilders
  {
    /// <summary>
    /// <span>Removes the characters from <paramref name="startIndex"/> on to the end of the <paramref name="source"/>.</span>
    /// </summary>
    public static System.Text.StringBuilder Remove(this System.Text.StringBuilder source, int startIndex)
      => source.Remove(startIndex, source.Length - startIndex);
  }
}
