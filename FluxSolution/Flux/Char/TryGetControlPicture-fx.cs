namespace Flux
{
  public static partial class Chars
  {
    /// <summary>
    /// <para></para>
    /// <para><see href="https://en.wikipedia.org/wiki/Control_Pictures"/></para>
    /// <para><see href="https://en.wikipedia.org/wiki/Control_character"/></para>
    /// <para><see href="https://en.wikipedia.org/wiki/C0_and_C1_control_codes"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Space_(punctuation)"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Whitespace_character"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Newline"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Delete_character"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Substitute_character"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Delete_character"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Specials_(Unicode_block)#Replacement_character"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static char GetControlPicture(this char source)
      => source is >= '\u0000' and <= '\u0020' // C0 control codes, U+0000—U+001F, from the Basic Latin block.
      ? (char)(source + 2400) // Yields C0 control pictures.
      : source is '\u007F' // Delete character.
      ? '\u2421' // Yields DEL
      : source is '\u0085' // The NEL (Next Line) is the only C1 control code that has a designated Unicode "control picture".
      ? '\u2424' // Yields NL (Newline)
      ///: source is >= '\u2028' and <= '\u2029'
      ///? '\u2424' // LS and PS (Line Separator and Paragraph Separator).
      : source is >= '\u0080' and <= '\u009F' // C1 control codes, U+0080-U+009F, from the Latin-1 Supplement block.
      ? '\uFFFD' // There are no Unicode "control picture" for these (except NEL, above) so here we use the replacement character (seealso above) for these.
      : source;

    /// <summary>
    /// <para><see href="https://en.wikipedia.org/wiki/Control_Pictures"/></para>
    /// <para><see href="https://en.wikipedia.org/wiki/Control_character"/></para>
    /// <para><see href="https://en.wikipedia.org/wiki/C0_and_C1_control_codes"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Space_(punctuation)"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Whitespace_character"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Newline"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Delete_character"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Substitute_character"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Delete_character"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Specials_(Unicode_block)#Replacement_character"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="picture"></param>
    /// <returns></returns>
    public static bool TryGetControlPicture(this char source, out char picture)
    {
      try { picture = source.GetControlPicture(); }
      catch { picture = source; }

      return picture != source;
    }
  }
}
