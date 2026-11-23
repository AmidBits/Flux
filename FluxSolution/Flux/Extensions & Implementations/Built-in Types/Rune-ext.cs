using System.Collections.Frozen;

namespace Flux
{
  public static partial class RuneExtensions
  {
    /// <summary>
    /// <para>The longest UTF-8 encoded byte sequence.</para>
    /// </summary>
    public const int Utf8EncodedSequenceMaxLength = 4;

    #region ASCII Folding frozen

    // Characters that do not fold correctly through
    // Unicode NFKD decomposition plus Latin-base extraction.
    // Multi-character expansions and special transliterations
    // are handled here explicitly.
    private static readonly FrozenDictionary<System.Text.Rune, string> m_asciiFoldOverrides = new Dictionary<System.Text.Rune, string>
    {
      [new('\u00C6')] = "AE",
      [new('\u00E6')] = "ae",

      [new('\u0152')] = "OE",
      [new('\u0153')] = "oe",

      [new('\u1E9E')] = "SS",
      [new('\u00DF')] = "ss",

      [new('\u00DE')] = "TH",
      [new('\u00FE')] = "th",

      [new('\u00D0')] = "D",
      [new('\u00F0')] = "d",

      [new('\u00D8')] = "O",
      [new('\u00F8')] = "o",

      [new('\u0132')] = "IJ",
      [new('\u0133')] = "ij",

      [new('\u0141')] = "L",
      [new('\u0142')] = "l",

      [new('\u01C4')] = "DZ",
      [new('\u01C5')] = "Dz",
      [new('\u01C6')] = "dz",

      [new('\u01C7')] = "LJ",
      [new('\u01C8')] = "Lj",
      [new('\u01C9')] = "lj",

      [new('\u01CA')] = "NJ",
      [new('\u01CB')] = "Nj",
      [new('\u01CC')] = "nj",

      [new('\u01F6')] = "HV",
      [new('\u0195')] = "hv",
    }
    .ToFrozenDictionary();

    #endregion

    #region Latin Letters With Stroke frozen

    internal static readonly FrozenDictionary<System.Text.Rune, System.Text.Rune> m_latinLettersWithStroke = new Dictionary<System.Text.Rune, System.Text.Rune>
    {
      [new('\u023A')] = new('A'),
      [new('\u0243')] = new('B'),
      [new('\u0180')] = new('b'),
      [new('\u023B')] = new('C'),
      [new('\u023C')] = new('c'),
      [new('\u0110')] = new('D'),
      [new('\u0111')] = new('d'),
      [new('\u0246')] = new('E'),
      [new('\u0247')] = new('e'),
      [new('\u01E4')] = new('G'),
      [new('\u01E5')] = new('g'),
      [new('\u0126')] = new('H'),
      [new('\u0127')] = new('h'),
      [new('\u0197')] = new('I'),
      [new('\u0248')] = new('J'),
      [new('\u0249')] = new('j'),
      [new('\u0141')] = new('L'),
      [new('\u0142')] = new('l'),
      [new('\u00D8')] = new('O'),
      [new('\u01FE')] = new('O'),
      [new('\u00F8')] = new('o'),
      [new('\u01FF')] = new('o'),
      [new('\u024C')] = new('R'),
      [new('\u024D')] = new('r'),
      [new('\u0166')] = new('T'),
      [new('\u023E')] = new('T'),
      [new('\u0167')] = new('t'),
      [new('\u024E')] = new('Y'),
      [new('\u024F')] = new('y'),
      [new('\u01B5')] = new('Z'),
      [new('\u01B6')] = new('z'),
    }
    .ToFrozenDictionary();

    #endregion

    #region Superscript frozen

    private static readonly FrozenDictionary<System.Text.Rune, System.Text.Rune> m_superscripts = new Dictionary<System.Text.Rune, System.Text.Rune>
    {
      // Digits
      [new('0')] = new('⁰'),
      [new('1')] = new('¹'),
      [new('2')] = new('²'),
      [new('3')] = new('³'),
      [new('4')] = new('⁴'),
      [new('5')] = new('⁵'),
      [new('6')] = new('⁶'),
      [new('7')] = new('⁷'),
      [new('8')] = new('⁸'),
      [new('9')] = new('⁹'),

      // Operators
      [new('+')] = new('⁺'),
      [new('-')] = new('⁻'),
      [new('=')] = new('⁼'),
      [new('(')] = new('⁽'),
      [new(')')] = new('⁾'),

      // Lowercase letters
      [new('a')] = new('ᵃ'),
      [new('b')] = new('ᵇ'),
      [new('c')] = new('ᶜ'),
      [new('d')] = new('ᵈ'),
      [new('e')] = new('ᵉ'),
      [new('f')] = new('ᶠ'),
      [new('g')] = new('ᵍ'),
      [new('h')] = new('ʰ'),
      [new('i')] = new('ⁱ'),
      [new('j')] = new('ʲ'),
      [new('k')] = new('ᵏ'),
      [new('l')] = new('ˡ'),
      [new('m')] = new('ᵐ'),
      [new('n')] = new('ⁿ'),
      [new('o')] = new('ᵒ'),
      [new('p')] = new('ᵖ'),
      [new('r')] = new('ʳ'),
      [new('s')] = new('ˢ'),
      [new('t')] = new('ᵗ'),
      [new('u')] = new('ᵘ'),
      [new('v')] = new('ᵛ'),
      [new('w')] = new('ʷ'),
      [new('x')] = new('ˣ'),
      [new('y')] = new('ʸ'),
      [new('z')] = new('ᶻ'),

      // Uppercase letters
      [new('A')] = new('ᴬ'),
      [new('B')] = new('ᴮ'),
      [new('D')] = new('ᴰ'),
      [new('E')] = new('ᴱ'),
      [new('G')] = new('ᴳ'),
      [new('H')] = new('ᴴ'),
      [new('I')] = new('ᴵ'),
      [new('J')] = new('ᴶ'),
      [new('K')] = new('ᴷ'),
      [new('L')] = new('ᴸ'),
      [new('M')] = new('ᴹ'),
      [new('N')] = new('ᴺ'),
      [new('O')] = new('ᴼ'),
      [new('P')] = new('ᴾ'),
      [new('R')] = new('ᴿ'),
      [new('T')] = new('ᵀ'),
      [new('U')] = new('ᵁ'),
      [new('V')] = new('ⱽ'),
      [new('W')] = new('ᵂ'),
    }
    .ToFrozenDictionary();

    #endregion

    #region Subscripts frozen

    private static readonly FrozenDictionary<System.Text.Rune, System.Text.Rune> m_subscripts = new Dictionary<System.Text.Rune, System.Text.Rune>
    {
      // Digits
      [new('0')] = new('₀'),
      [new('1')] = new('₁'),
      [new('2')] = new('₂'),
      [new('3')] = new('₃'),
      [new('4')] = new('₄'),
      [new('5')] = new('₅'),
      [new('6')] = new('₆'),
      [new('7')] = new('₇'),
      [new('8')] = new('₈'),
      [new('9')] = new('₉'),

      // Operators
      [new('+')] = new('₊'),
      [new('-')] = new('₋'),
      [new('=')] = new('₌'),
      [new('(')] = new('₍'),
      [new(')')] = new('₎'),

      // Letters with Unicode subscript equivalents
      [new('a')] = new('ₐ'),
      [new('e')] = new('ₑ'),
      [new('h')] = new('ₕ'),
      [new('i')] = new('ᵢ'),
      [new('j')] = new('ⱼ'),
      [new('k')] = new('ₖ'),
      [new('l')] = new('ₗ'),
      [new('m')] = new('ₘ'),
      [new('n')] = new('ₙ'),
      [new('o')] = new('ₒ'),
      [new('p')] = new('ₚ'),
      [new('r')] = new('ᵣ'),
      [new('s')] = new('ₛ'),
      [new('t')] = new('ₜ'),
      [new('u')] = new('ᵤ'),
      [new('v')] = new('ᵥ'),
      [new('x')] = new('ₓ'),
    }
    .ToFrozenDictionary();

    #endregion

    extension(System.Text.Rune)
    {
      #region ASCII properties

      public static bool IsAsciiBracket(System.Text.Rune rune) => rune.Value is '(' or ')' or '[' or ']' or '{' or '}';

      public static bool IsAsciiConsonant(System.Text.Rune rune) => IsAsciiLetter(rune) && !IsAsciiVowel(rune);

      public static bool IsAsciiControl(System.Text.Rune rune) => ((uint)rune.Value <= 0x1F) || rune.Value is 0x7F;

      public static bool IsAsciiDigit(System.Text.Rune rune) => (uint)(rune.Value - '0') <= 9;

      public static bool IsAsciiHexDigit(System.Text.Rune rune)
      {
        var value = rune.Value;

        return (uint)(value - '0') <= 9 || (uint)((value | 0x20) - 'a') <= 5;
      }

      public static bool IsAsciiLetter(System.Text.Rune rune) => (uint)((rune.Value | 0x20) - 'a') <= ('z' - 'a');

      /// <summary>
      /// <para>Indicates whether the character is the letter 'Y' or 'y', i.e. ignore case.</para>
      /// </summary>
      /// <param name="rune">The <see cref="System.Text.Rune"/> to check.</param>
      /// <returns></returns>
      /// <remarks>Provided for consistent check against consonants and vowels in English.</remarks>
      public static bool IsAsciiLetterY(System.Text.Rune rune) => (rune.Value | 0x20) == 'y';

      public static bool IsAsciiLetterOrDigit(System.Text.Rune rune) => IsAsciiDigit(rune) || IsAsciiLetter(rune);

      public static bool IsAsciiNewLine(System.Text.Rune rune) => rune.Value is '\r' or '\n';

      public static bool IsAsciiLower(System.Text.Rune rune) => (uint)(rune.Value - 'a') <= ('z' - 'a');

      public static bool IsAsciiOperator(System.Text.Rune rune) => rune.Value is '+' or '-' or '*' or '/' or '%' or '=' or '!' or '&' or '|' or '^' or '~' or '<' or '>';

      public static bool IsAsciiPathSeparator(System.Text.Rune rune) => rune.Value is '/' or '\\';

      public static bool IsAsciiPrintable(System.Text.Rune rune) => (uint)(rune.Value - 0x20) <= 0x7E - 0x20;

      public static bool IsAsciiQuote(System.Text.Rune rune) => rune.Value is '"' or '\'';

      public static bool IsAsciiUpper(System.Text.Rune rune) => (uint)(rune.Value - 'A') <= ('Z' - 'A');

      public static bool IsAsciiUriGenDelim(System.Text.Rune rune) => rune.Value is ':' or '/' or '?' or '#' or '[' or ']' or '@';

      public static bool IsAsciiUriPChar(System.Text.Rune rune)
      {
        var value = rune.Value;

        return IsAsciiUriUnreserved(rune) || IsAsciiUriSubDelim(rune)
          || value is ':' or '@';
      }

      /// <summary>
      /// <para>URIs include components and subcomponents that are delimited by characters in the "reserved" set. These characters are called "reserved" because they may(or may not) be defined as delimiters by the generic syntax, by each scheme-specific syntax, or by the implementation-specific syntax of a URI's dereferencing algorithm.</para>
      /// <para>If data for a URI component would conflict with a reserved character's purpose as a delimiter, then the conflicting data must be percent-encoded before the URI is formed.</para>
      /// <para>reserved = <c>gen-delims</c> and <c>sub-delims</c></para>
      /// <para>gen-delims = "<c>:/?#[]@</c>"</para>
      /// <para>sub-delims = "<c>!$&amp;&apos;()*+,;=</c>"</para>
      /// <para>The purpose of reserved characters is to provide a set of delimiting characters that are distinguishable from other data within a URI.</para>
      /// <para>URIs that differ in the replacement of a reserved character with its corresponding percent-encoded octet are not equivalent. Percent-encoding a reserved character, or decoding a percent-encoded octet that corresponds to a reserved character, will change how the URI is interpreted by most applications.Thus, characters in the reserved set are protected from normalization and are therefore safe to be used by scheme-specific and producer-specific algorithms for delimiting data subcomponents within a URI.</para>
      /// <para><see href="https://www.rfc-editor.org/rfc/rfc3986"/></para>
      /// </summary>
      /// <param name="rune"></param>
      /// <returns></returns>
      public static bool IsAsciiUriReserved(System.Text.Rune rune) => IsAsciiUriGenDelim(rune) || IsAsciiUriSubDelim(rune);

      public static bool IsAsciiUriSubDelim(System.Text.Rune rune) => rune.Value is '!' or '$' or '&' or '\'' or '(' or ')' or '*' or '+' or ',' or ';' or '=';

      /// <summary>
      /// <para>Characters that are allowed in a URI but do not have a reserved purpose are called unreserved. These include uppercase and lowercase letters, decimal digits, hyphen, period, underscore, and tilde.</para>
      /// <para>unreserved = <c>ALPHA</c>, <c>DIGIT</c> or "<c>-._~</c>"</para>
      /// <para>URIs that differ in the replacement of an unreserved character with its corresponding percent-encoded US-ASCII octet are equivalent: they identify the same resource. However, URI comparison implementations not always perform normalization prior to comparison (see Section 6).</para>
      /// <para>For consistency, percent-encoded octets in the ranges of ALPHA (%41-%5A and %61-%7A), DIGIT (%30-%39), hyphen (%2D), period (%2E), underscore (%5F), or tilde (%7E) should not be created by URI producers and, when found in a URI, should be decoded to their corresponding unreserved characters by URI normalizers.</para>
      /// <para><see href="https://www.rfc-editor.org/rfc/rfc3986"/></para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      public static bool IsAsciiUriUnreserved(System.Text.Rune rune)
      {
        var value = rune.Value;

        return (uint)((value | 0x20) - 'a') <= ('z' - 'a')
            || (uint)(value - '0') <= 9
            || value is '-' or '.' or '_' or '~';
      }

      public static bool IsAsciiVowel(System.Text.Rune rune) => (rune.Value | 0x20) is 'a' or 'e' or 'i' or 'o' or 'u';

      public static bool IsAsciiWhitespace(System.Text.Rune rune)
      {
        var value = rune.Value;

        return value == ' '
          || (uint)(value - '\t') <= ('\r' - '\t');
      }

      #endregion

      #region ASCII Folding

      /// <summary>
      /// Attempts to obtain the single Latin base letter for a rune.
      /// Examples:
      /// é -> e
      /// Å -> A
      /// Ç -> C
      /// Æ -> false
      /// ß -> false
      /// </summary>
      public static bool TryGetLatinBaseLetter(System.Text.Rune rune, out System.Text.Rune baseLetter)
      {
        var normalized = rune.ToString().Normalize(System.Text.NormalizationForm.FormKD);

        foreach (var candidate in normalized.EnumerateRunes())
        {
          if (IsCombiningMark(candidate))
            continue;

          if (candidate.Value is >= 'A' and <= 'Z' or >= 'a' and <= 'z')
          {
            baseLetter = candidate;
            return true;
          }

          break;
        }

        baseLetter = default;
        return false;
      }

      /// <summary>
      /// Attempts to obtain an ASCII transliteration for a rune.
      /// Examples:
      /// é -> e
      /// Æ -> AE
      /// ß -> ss
      /// ǉ -> lj
      /// Ⓐ -> A
      /// </summary>
      public static bool TryGetAsciiFolded(System.Text.Rune rune, out string folded)
      {
        if (m_asciiFoldOverrides.TryGetValue(rune, out folded!))
          return true;

        var normalized = rune.ToString().Normalize(System.Text.NormalizationForm.FormKD);

        var sb = new System.Text.StringBuilder(normalized.Length);

        foreach (var candidate in normalized.EnumerateRunes())
        {
          if (IsCombiningMark(candidate))
            continue;

          if (candidate.IsAscii)
            sb.Append((char)candidate.Value);
          else if (TryGetLatinBaseLetter(candidate, out System.Text.Rune letter))
            sb.Append(letter);
        }

        if (sb.Length != 0)
        {
          folded = sb.ToString();
          return true;
        }

        folded = string.Empty;
        return false;
      }

      /// <summary>
      /// Returns the ASCII fold if available; otherwise the rune itself.
      /// </summary>
      public static string FoldToAscii(System.Text.Rune rune)
        => TryGetAsciiFolded(rune, out string folded) ? folded : rune.ToString();

      #endregion

      #region Try/GetControlPicture

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
      /// <param name="source"></param>
      /// <returns></returns>
      public static System.Text.Rune GetControlPicture(System.Text.Rune rune)
        => rune.Value is var runeValue
        && runeValue is >= '\u0000' and <= '\u0020' // C0 control codes, U+0000—U+001F, from the Basic Latin block.
        ? new System.Text.Rune(runeValue + 2400) // Yields C0 control pictures.
        : runeValue is '\u007F' // Delete character.
        ? new System.Text.Rune('\u2421') // Yields DEL
        : runeValue is '\u0085' // The NEL (Next Line) is the only C1 control code that has a designated Unicode "control picture".
        ? new System.Text.Rune('\u2424') // Yields NL (Newline)
        ///: rune.Value is >= '\u2028' and <= '\u2029'
        ///? new System.Text.Rune('\u2424') // LS and PS (Line Separator and Paragraph Separator).
        : runeValue is >= '\u0080' and <= '\u009F' // C1 control codes, U+0080-U+009F, from the Latin-1 Supplement block.
        ? System.Text.Rune.ReplacementChar // new System.Text.Rune('\uFFFD') // There are no Unicode "control picture" for these (except NEL, above) so here we use the replacement character (seealso above) for these.
        : rune;

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
      /// <param name="source"></param>
      /// <param name="picture"></param>
      /// <returns></returns>
      public static bool TryGetControlPicture(System.Text.Rune value, out System.Text.Rune picture)
      {
        try { picture = GetControlPicture(value); }
        catch { picture = value; }

        return picture != value;
      }

      #endregion

      #region GetUnicodeRange

      /// <summary>
      /// <para>Locates the Unicode range and block name of the <paramref name="rune"/>.</para>
      /// </summary>
      public static System.Collections.Generic.KeyValuePair<string, System.Text.Unicode.UnicodeRange> GetUnicodeRange(System.Text.Rune rune)
        => System.Text.Unicode.UnicodeRange.GetUnicodeRanges().InfimumSupremum(rune.Value, t2 => t2.Value.FirstCodePoint, false).InfimumElement;

      #endregion

      #region IsCased

      /// <summary>
      /// <para>Indicates whether a <see cref="System.Text.Rune"/> participates in casing.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static bool IsCased(System.Text.Rune value)
        => System.Text.Rune.ToUpperInvariant(value) != System.Text.Rune.ToLowerInvariant(value);

      #endregion

      #region IsCombiningMark

      public static bool IsCombiningMark(System.Text.Rune rune)
        => System.Text.Rune.GetUnicodeCategory(rune)
        is System.Globalization.UnicodeCategory.NonSpacingMark
        or System.Globalization.UnicodeCategory.SpacingCombiningMark
        or System.Globalization.UnicodeCategory.EnclosingMark;

      #endregion

      #region IsConsonant

      /// <summary>
      /// <para>Indicates whether a <see cref="System.Text.Rune"/> is a consonant in the specified <paramref name="culture"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="culture">If null, then <see cref="System.Globalization.CultureInfo.CurrentCulture"/></param>
      /// <returns></returns>
      public static bool IsConsonant(System.Text.Rune rune, System.Globalization.CultureInfo? cultureInfo = null)
        => System.Text.Rune.IsLetter(rune)
        && cultureInfo.GetConsonants().Contains(rune);

      #endregion

      #region IsCategoryCased

      /// <summary>
      /// <para>Indicates whether a <see cref="System.Text.Rune"/> is categorized as a cased letter.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static bool IsCategoryCased(System.Text.Rune value)
        => System.Text.Rune.GetUnicodeCategory(value)
        is System.Globalization.UnicodeCategory.UppercaseLetter
        or System.Globalization.UnicodeCategory.LowercaseLetter
        or System.Globalization.UnicodeCategory.TitlecaseLetter;

      #endregion

      //#region IsUriReserved

      ///// <summary>
      ///// <para>URIs include components and subcomponents that are delimited by characters in the "reserved" set. These characters are called "reserved" because they may(or may not) be defined as delimiters by the generic syntax, by each scheme-specific syntax, or by the implementation-specific syntax of a URI's dereferencing algorithm.</para>
      ///// <para>If data for a URI component would conflict with a reserved character's purpose as a delimiter, then the conflicting data must be percent-encoded before the URI is formed.</para>
      ///// <para>reserved = <c>gen-delims</c> and <c>sub-delims</c></para>
      ///// <para>gen-delims = "<c>:/?#[]@</c>"</para>
      ///// <para>sub-delims = "<c>!$&amp;&apos;()*+,;=</c>"</para>
      ///// <para>The purpose of reserved characters is to provide a set of delimiting characters that are distinguishable from other data within a URI.</para>
      ///// <para>URIs that differ in the replacement of a reserved character with its corresponding percent-encoded octet are not equivalent. Percent-encoding a reserved character, or decoding a percent-encoded octet that corresponds to a reserved character, will change how the URI is interpreted by most applications.Thus, characters in the reserved set are protected from normalization and are therefore safe to be used by scheme-specific and producer-specific algorithms for delimiting data subcomponents within a URI.</para>
      ///// <para><see href="https://www.rfc-editor.org/rfc/rfc3986"/></para>
      ///// </summary>
      ///// <param name="rune"></param>
      ///// <returns></returns>
      //public static bool IsUriReserved(System.Text.Rune rune)
      //  => rune.IsAscii
      //  && rune.Value is
      //  /* gen-delims: */ ':' or '/' or '?' or '#' or '[' or ']' or '@' or
      //  /* sub-delims: */ '!' or '$' or '&' or '\'' or '(' or ')' or '*' or '+' or ',' or ';' or '=';

      //#endregion

      //#region IsUriUnreserved

      ///// <summary>
      ///// <para>Characters that are allowed in a URI but do not have a reserved purpose are called unreserved. These include uppercase and lowercase letters, decimal digits, hyphen, period, underscore, and tilde.</para>
      ///// <para>unreserved = <c>ALPHA</c>, <c>DIGIT</c> or "<c>-._~</c>"</para>
      ///// <para>URIs that differ in the replacement of an unreserved character with its corresponding percent-encoded US-ASCII octet are equivalent: they identify the same resource. However, URI comparison implementations not always perform normalization prior to comparison (see Section 6).</para>
      ///// <para>For consistency, percent-encoded octets in the ranges of ALPHA (%41-%5A and %61-%7A), DIGIT (%30-%39), hyphen (%2D), period (%2E), underscore (%5F), or tilde (%7E) should not be created by URI producers and, when found in a URI, should be decoded to their corresponding unreserved characters by URI normalizers.</para>
      ///// <para><see href="https://www.rfc-editor.org/rfc/rfc3986"/></para>
      ///// </summary>
      ///// <param name="source"></param>
      ///// <returns></returns>
      //public static bool IsUriUnreserved(System.Text.Rune rune)
      //  => (rune.IsAscii && System.Text.Rune.IsLetterOrDigit(rune)) // is ASCII alpha or digit.
      //  || (rune.Value is '-' or '.' or '_' or '~');

      //#endregion

      #region IsVowel

      /// <summary>
      /// <para>Indicates whether a <see cref="System.Text.Rune"/> is a vowel in the specified <paramref name="cultureInfo"/>.</para>
      /// </summary>
      /// <param name="rune"></param>
      /// <param name="cultureInfo">The culture to use, or <see cref="System.Globalization.CultureInfo.CurrentCulture"/> if null.</param>
      /// <returns></returns>
      public static bool IsVowel(System.Text.Rune rune, System.Globalization.CultureInfo? cultureInfo = null)
        => System.Text.Rune.IsLetter(rune)
        && cultureInfo.GetVowels().Contains(rune);

      #endregion

      #region Latin Letters With Stroke functions

      /// <summary>
      /// <para>Replaces a latin stroke letter with a plain letter, i.e. a letter without a diacritic is returned in its place. Characters that are not latin stroke letters are returned as-is.</para>
      /// </summary>
      /// <param name="rune">The rune to evaluate.</param>
      /// <returns></returns>
      public static System.Text.Rune FoldLatinLetterWithStroke(System.Text.Rune rune)
        => m_latinLettersWithStroke.TryGetValue(rune, out var replacementRune) ? replacementRune : rune;

      /// <summary>
      /// <para>Indicates whether a rune is a latin diacritical stroke.</para>
      /// </summary>
      /// <param name="rune">The rune to evaluate.</param>
      /// <returns></returns>
      public static bool IsLatinLetterWithStroke(System.Text.Rune rune)
        => m_latinLettersWithStroke.ContainsKey(rune);

      /// <summary>
      /// <para>Attempts to replace a latin stroke letter with a plain letter and indicates whether a replacement was made.</para>
      /// </summary>
      /// <param name="rune"></param>
      /// <param name="replacementCharacter"></param>
      /// <returns></returns>
      public static bool TryFoldLatinLetterWithStroke(System.Text.Rune rune, out System.Text.Rune replacementRune)
        => m_latinLettersWithStroke.TryGetValue(rune, out replacementRune);

      #endregion

      #region Printable functions

      /// <summary>
      /// <para>The set of Unicode character categories containing non-rendering, unknown, or incomplete characters.</para>
      /// <para>Unicode.Format and Unicode.PrivateUse can NOT be included in this set, because they may (private-use) or do (format) contain at least *some* rendering characters.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      public static bool IsNonRenderingCategory(System.Text.Rune rune, bool nonRenderCategoryFormat = false, bool nonRenderCategoryPrivateUse = false)
        => System.Text.Rune.GetUnicodeCategory(rune) is var uc
        && uc is System.Globalization.UnicodeCategory.Control or System.Globalization.UnicodeCategory.OtherNotAssigned or System.Globalization.UnicodeCategory.Surrogate
        || (nonRenderCategoryFormat && uc == System.Globalization.UnicodeCategory.Format)
        || (nonRenderCategoryPrivateUse && uc == System.Globalization.UnicodeCategory.PrivateUse);

      /// <summary>
      /// <para>Returns whether a character is a printable from the ASCII table, i.e. any character from U+0020 (32 = ' ' space) to U+007E (126 = '~' tilde), both inclusive.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      public static bool IsPrintableAscii(System.Text.Rune rune)
        => rune.Value is >= '\u0020' and <= '\u007E';

      /// <summary>
      /// <para>Returns whether a rune (Unicode codepoint) is a "printable" character. There is no simple solution, so please read the entire blurb of this method.</para>
      /// <para>The following code assumes that all characters in Unicode categories PrivateUse and Format are printable, which means that at least some characters will be misclassified.</para>
      /// <para><see href="https://stackoverflow.com/a/45928048/3178666"/></para>
      /// <para>IsWhiteSpace() includes the whitespace characters that are categorized as control characters. Any other character is printable, unless it falls into the non-rendering categories.</para>
      /// <para>Exclude Unicode categories containing non-rendering, unknown, or incomplete characters. Unicode categories Format and PrivateUse can NOT be included in this set, because they may (private-use) or do (format) contain at least *some* rendering characters.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      public static bool IsPrintableUnicode(System.Text.Rune value)
        => System.Text.Rune.IsWhiteSpace(value) || !IsNonRenderingCategory(value);

      #endregion

      #region Subscript functions

      /// <summary>
      /// <para>Returns the Unicode subscript equivalent of the specified rune, if one exists.</para>
      /// </summary>
      /// <remarks>If the rune does not have a subscript equivalent, the original rune is returned.</remarks>
      /// <param name="rune">The rune to convert to its subscript equivalent.</param>
      /// <returns>A rune representing the subscript equivalent if available; otherwise, the original rune.</returns>
      public static System.Text.Rune GetSubscript(System.Text.Rune rune)
        => m_subscripts.TryGetValue(rune, out var subscriptRune) ? subscriptRune : rune;

      /// <summary>
      /// <para>Indicates whether a specified rune is a subscript rune.</para>
      /// </summary>
      /// <param name="rune"></param>
      /// <returns></returns>
      public static bool IsSubscript(System.Text.Rune rune)
        => m_subscripts.TryGetKeys(rune, out var _);

      /// <summary>
      /// <para>Indicates whether a corresponding subscript rune exists for the current rune.</para>
      /// </summary>
      /// <param name="rune"></param>
      /// <returns></returns>
      public static bool SubscriptExists(System.Text.Rune rune)
        => m_subscripts.ContainsKey(rune);

      /// <summary>
      /// <para>Attemps to convert a rune to subscript, if possible, otherwise the input rune is returned as an out parameter, and returns an indication whether a conversion was made.</para>
      /// </summary>
      /// <param name="rune"></param>
      /// <param name="subscriptRune"></param>
      /// <returns></returns>
      public static bool TryGetSubscript(System.Text.Rune rune, out System.Text.Rune subscriptRune)
        => m_subscripts.TryGetValue(rune, out subscriptRune);

      #endregion

      #region Superscript functions

      /// <summary>
      /// <para>Returns the Unicode superscript equivalent of the specified rune, if one exists.</para>
      /// </summary>
      /// <remarks>If the rune does not have a superscript equivalent, the original rune is returned.</remarks>
      /// <param name="rune">The rune to convert to its superscript equivalent.</param>
      /// <returns>A rune representing the superscript equivalent if available; otherwise, the original rune.</returns>
      public static System.Text.Rune GetSuperscript(System.Text.Rune rune)
        => m_superscripts.TryGetValue(rune, out var superscriptRune) ? superscriptRune : rune;

      /// <summary>
      /// <para>Indicates whether a rune is a superscript rune.</para>
      /// </summary>
      /// <param name="rune"></param>
      /// <returns></returns>
      public static bool IsSuperscript(System.Text.Rune rune)
        => m_superscripts.TryGetKeys(rune, out var _);

      /// <summary>
      /// <para>Indicates whether a corresponding superscript rune exists for the current rune.</para>
      /// </summary>
      /// <param name="rune"></param>
      /// <returns></returns>
      public static bool SuperscriptExists(System.Text.Rune rune)
        => m_subscripts.ContainsKey(rune);

      /// <summary>
      /// <para>Attemps to convert a rune to superscript, if possible.</para>
      /// </summary>
      /// <param name="rune"></param>
      /// <param name="superscriptRune"></param>
      /// <returns>An indication whether a conversion was made.</returns>
      public static bool TryGetSuperscript(System.Text.Rune rune, out System.Text.Rune superscriptRune)
        => m_superscripts.TryGetValue(rune, out superscriptRune);

      #endregion

      #region ToCsharpUtf16Literal

      /// <summary>
      /// <para>Creates a new string with a rune represented as <code>\u0000</code> (a C# UTF-16 literal string).</para>
      /// </summary>
      /// <returns></returns>
      public static string ToCsharpUtf16Literal(System.Text.Rune rune)
      {
        const int Utf16LiteralLength = 6;

        var chars = (stackalloc char[rune.Utf16SequenceLength]);

        rune.EncodeToUtf16(chars);

        var sb = new System.Text.StringBuilder(chars.Length * Utf16LiteralLength);
        foreach (var c in chars)
          sb.Append(char.ToCsharpUtf16Literal(c));
        return sb.ToString();
      }

      #endregion

      #region ToCsharpUtf32Literal

      /// <summary>
      /// <para>Creates a new string with a rune represented as <code>\U00000000</code> (a C# UTF-32 literal string).</para>
      /// </summary>
      /// <returns></returns>
      public static string ToCsharpUtf32Literal(System.Text.Rune rune) => $"\\U{rune.Value:X8}";

      #endregion

      #region ToCsharpVariableHexLiteral

      /// <summary>
      /// <para>Creates a new string with a rune represented as <code>\x000</code> (a C# variable hex literal string).</para>
      /// </summary>
      /// <returns></returns>
      public static string ToCsharpVariableHexLiteral(System.Text.Rune rune) => $"\\x{rune.Value:X1}";

      #endregion

      #region ToDecimalNumericCharacterReference

      /// <summary>
      /// <para>A decimal numeric character reference refers to a character by its Universal Coded Character Set/Unicode code point, and uses the format: <code>&amp;#nnnn;</code> where nnnn is the code point in decimal form.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Numeric_character_reference"/></para>
      /// </summary>
      /// <param name="rune"></param>
      /// <returns></returns>
      public static string ToDecimalNumericCharacterReference(System.Text.Rune rune) => $"&#{rune.Value};";

      #endregion

      #region ToHexadecimalNumericCharacterReference

      /// <summary>
      /// <para>A hexadecimal numeric character reference refers to a character by its Universal Coded Character Set/Unicode code point, and uses the format: <code>&amp;#xhhhh;</code> where the x must be lowercase in XML documents, hhhh is the code point in hexadecimal form.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Numeric_character_reference"/></para>
      /// </summary>
      /// <param name="rune"></param>
      /// <returns></returns>
      public static string ToHexadecimalNumericCharacterReference(System.Text.Rune rune) => $"&#x{rune.Value:X1};";

      #endregion

      #region ToUnicodeUnotation

      /// <summary>
      /// <para>Creates a new string of a rune (Unicode codepoint) in Unicode U-notation format: <code>U+XXXX</code> (at least 4 hex characters, but more if needed).</para>
      /// </summary>
      /// <returns></returns>
      public static string ToUnicodeUnotation(System.Text.Rune rune) => $"U+{rune.Value:X4}";

      #endregion

      #region ToUriPercentEncoding

      /// <summary>
      /// <para>Creates a new string of a rune URI percent-encoded: <code>%E2%88%86</code> (where the encoding can be from one up to three "percent-codes").</para>
      /// <para><see href="https://www.rfc-editor.org/rfc/rfc3986"/></para>
      /// </summary>
      /// <param name="rune"></param>
      /// <returns></returns>
      public static string ToUriPercentEncoding(System.Text.Rune rune)
      {
        if (rune.Value is var utf32 && utf32 <= 0x7F)
          return byte.ToUriPercentEncoding((byte)utf32);

        var bytes = (stackalloc byte[rune.Utf8SequenceLength]);
        var chars = (stackalloc char[rune.Utf8SequenceLength * byte.UriPercentEncodedOctetLength]);

        var byteLength = rune.EncodeToUtf8(bytes);

        for (var index = 0; index < byteLength; index++)
          byte.ToUriPercentEncoding(bytes[index]).CopyTo(chars.Slice(index * byte.UriPercentEncodedOctetLength, byte.UriPercentEncodedOctetLength));

        return chars.ToString();
      }

      #endregion

      #region ToVerboseString

      public static string ToVerboseString(System.Text.Rune rune) => $"{ToUnicodeUnotation(rune)} '{rune}' {System.Text.Rune.GetUnicodeCategory(rune)}";

      #endregion
    }
  }
}
