namespace Flux
{
  public static partial class Chars
  {
    /// <summary>
    /// <para>Folds chars representing characters above ASCII as a reasonable ASCII equivalent. Only characters from certain blocks are converted.</para>
    /// </summary>
    public static string FoldToAscii(this char source)
      => new System.Text.Rune(source).FoldToAscii();
  }
}
