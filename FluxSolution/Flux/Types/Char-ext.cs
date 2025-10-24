namespace Flux
{
  public static partial class XtensionChar
  {
    //public const string SuperscriptAlphaLower = "ᵃᵇᶜᵈᵉᶠᵍʰⁱʲᵏˡᵐⁿᵒᵖ\0ʳˢᵗᵘᵛʷˣʸᶻ";
    //public const string SuperscriptNumeric = "⁰¹²³⁴⁵⁶⁷⁸⁹";

    //public const string SubscriptAlphaLower = "ₐ\0\0\0ₑ\0\0ₕᵢⱼₖₗₘₙₒₚ\0ᵣₛₜᵤᵥ\0ₓ\0\0";
    //public const string SubscriptNumeric = "₀₁₂₃₄₅₆₇₈₉";

    private static System.Collections.Generic.Dictionary<char, char> m_subscriptDictionary = new()
    {
      { '\u0028', '\u208D' },
      { '\u0029', '\u208E' },
      { '\u002B', '\u208A' },
      { '\u0030', '\u2080' },
      { '\u0031', '\u2081' },
      { '\u0032', '\u2082' },
      { '\u0033', '\u2083' },
      { '\u0034', '\u2084' },
      { '\u0035', '\u2085' },
      { '\u0036', '\u2086' },
      { '\u0037', '\u2087' },
      { '\u0038', '\u2088' },
      { '\u0039', '\u2089' },
      { '\u003D', '\u208C' },
      { '\u0061', '\u2090' },
      { '\u0065', '\u2091' },
      { '\u0068', '\u2095' },
      { '\u0069', '\u1D62' },
      { '\u006A', '\u2C7C' },
      { '\u006B', '\u2096' },
      { '\u006C', '\u2097' },
      { '\u006D', '\u2098' },
      { '\u006E', '\u2099' },
      { '\u006F', '\u2092' },
      { '\u0070', '\u209A' },
      { '\u0072', '\u1D63' },
      { '\u0073', '\u209B' },
      { '\u0074', '\u209C' },
      { '\u0075', '\u1D64' },
      { '\u0076', '\u1D65' },
      { '\u0078', '\u2093' },
    };

    private static System.Collections.Generic.Dictionary<char, char> m_superscriptDictionary = new()
    {
      { '\u0028', '\u207D' },
      { '\u0029', '\u207E' },
      { '\u002B', '\u207A' },
      { '\u0030', '\u2070' },
      { '\u0031', '\u00B9' },
      { '\u0032', '\u00B2' },
      { '\u0033', '\u00B3' },
      { '\u0034', '\u2074' },
      { '\u0035', '\u2075' },
      { '\u0036', '\u2076' },
      { '\u0037', '\u2077' },
      { '\u0038', '\u2078' },
      { '\u0039', '\u2079' },
      { '\u003D', '\u207C' },
      { '\u0041', '\u1D2C' },
      { '\u0042', '\u1D2E' },
      { '\u0043', '\uA7F2' },
      { '\u0044', '\u1D30' },
      { '\u0045', '\u1D31' },
      { '\u0046', '\uA7F3' },
      { '\u0047', '\u1D33' },
      { '\u0048', '\u1D34' },
      { '\u0049', '\u1D35' },
      { '\u004A', '\u1D36' },
      { '\u004B', '\u1D37' },
      { '\u004C', '\u1D38' },
      { '\u004D', '\u1D39' },
      //{ '\u004D 0043', '\u1F16A' },
      //{ '\u004D 0044', '\u1F16B' },
      //{ '\u004D 0052', '\u1F16C' },
      { '\u004E', '\u1D3A' },
      { '\u004F', '\u1D3C' },
      { '\u0050', '\u1D3E' },
      { '\u0051', '\uA7F4' },
      { '\u0052', '\u1D3F' },
      //{ '\u0053 004D', '\u2120' },
      { '\u0054', '\u1D40' },
      //{ '\u0054 004D', '\u2122' },
      { '\u0055', '\u1D41' },
      { '\u0056', '\u2C7D' },
      { '\u0057', '\u1D42' },
      //{ '\u0061', '\u00AA' },
      { '\u0061', '\u1D43' },
      { '\u0062', '\u1D47' },
      { '\u0063', '\u1D9C' },
      { '\u0064', '\u1D48' },
      { '\u0065', '\u1D49' },
      { '\u0066', '\u1DA0' },
      { '\u0067', '\u1D4D' },
      { '\u0068', '\u02B0' },
      { '\u0069', '\u2071' },
      { '\u006A', '\u02B2' },
      { '\u006B', '\u1D4F' },
      { '\u006C', '\u02E1' },
      { '\u006D', '\u1D50' },
      { '\u006E', '\u207F' },
      //{ '\u006F', '\u00BA' },
      { '\u006F', '\u1D52' },
      { '\u0070', '\u1D56' },
      //{ '\u0071', '\u107A5' },
      { '\u0072', '\u02B3' },
      { '\u0073', '\u02E2' },
      { '\u0074', '\u1D57' },
      { '\u0075', '\u1D58' },
      { '\u0076', '\u1D5B' },
      { '\u0077', '\u02B7' },
      { '\u0078', '\u02E3' },
      { '\u0079', '\u02B8' },
      { '\u007A', '\u1DBB' },
      { '\u00C6', '\u1D2D' },
      //{ '\u00E6', '\u10783' },
      { '\u00F0', '\u1D9E' },
      //{ '\u00F8', '\u107A2' },
    };

    extension(System.Char)
    {
      /// <summary>
      /// <para>Indicates whether the specified Unicode character is categorized as a subscript character.</para>
      /// </summary>
      /// <param name="c"></param>
      /// <returns></returns>
      public static bool IsSubscript(char c)
        => m_subscriptDictionary.ContainsKey(c);

      /// <summary>
      /// <para>Indicates whether the specified Unicode character is categorized as a superscript character.</para>
      /// </summary>
      /// <param name="c"></param>
      /// <returns></returns>
      public static bool IsSuperscript(char c)
        => m_superscriptDictionary.ContainsKey(c);

      /// <summary>
      /// <para>Converts the value of a Unicode character to its subscript equivalent.</para>
      /// </summary>
      /// <param name="c"></param>
      /// <returns></returns>
      public static char ToSubscript(char c)
        => m_subscriptDictionary.TryGetValue(c, out var subscriptCharacter) ? subscriptCharacter : c;

      /// <summary>
      /// <para>Converts the value of a Unicode character to its superscript equivalent.</para>
      /// </summary>
      /// <param name="c"></param>
      /// <returns></returns>
      public static char ToSuperscript(char c)
        => m_superscriptDictionary.TryGetValue(c, out var superscriptCharacter) ? superscriptCharacter : c;
    }

    extension(System.Char source)
    {
      public bool TryConvertToSubscript(out char subscriptCharacter)
        => (subscriptCharacter = char.ToSubscript(source)) != source;

      public bool TryConvertToSuperscript(out char superscriptCharacter)
        => (superscriptCharacter = char.ToSuperscript(source)) != source;

      #region ControlPicture

      /// <summary>
      /// <para>Control Pictures is a Unicode block containing characters for graphically representing the C0 control codes, and other control characters.</para>
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
      /// <returns></returns>
      public char GetControlPicture()
        => (char)new System.Text.Rune(source).GetControlPicture().Value;

      /// <summary>
      /// <para>Control Pictures is a Unicode block containing characters for graphically representing the C0 control codes, and other control characters.</para>
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
      /// <returns></returns>
      public bool TryGetControlPicture(out char picture)
      {
        try
        {
          picture = GetControlPicture(source);
        }
        catch
        {
          picture = source;
        }

        return picture != source;
      }

      #endregion

      #region FoldToAscii

      /// <summary>
      /// <para>Folds chars representing characters above ASCII as a reasonable ASCII equivalent. Only characters from certain blocks are converted.</para>
      /// </summary>
      public string FoldToAscii()
        => new System.Text.Rune(source).FoldToAscii();

      #endregion

      #region IsPrintable

      /// <summary>
      /// <para>Returns whether a character is a printable from the ASCII table, i.e. any character from U+0020 (32 = ' ' space) to U+007E (126 = '~' tilde), both inclusive.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      public bool IsPrintableAscii()
        => new System.Text.Rune(source).IsPrintableAscii();

      #endregion

      #region ToVerboseString

      public string ToVerboseString()
      {
        var uc = char.GetUnicodeCategory(source);

        return $"{source.UnicodeUnotationEncode()} '{source}' ({uc}, {uc.ToUnicodeCategoryMajorMinor()})";
      }

      #endregion

      #region Utf8SequenceLength

      public int Utf8SequenceLength()
        => (source <= 0x7F)
        ? 1
        : (source <= 0x7FF || char.IsLowSurrogate(source) || char.IsHighSurrogate(source))
        ? 2
        : 3;

      #endregion
    }
  }
}
