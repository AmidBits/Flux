namespace Flux
{
  public static partial class Em
  {
    public static string ToSpacingString(this UnicodeSpacing spacing) => spacing == UnicodeSpacing.None ? string.Empty : $"{(char)(int)spacing}";

    public static bool TryGetUnicodeSpacingChar(this UnicodeSpacing spacing, out char spaceChar) => (spaceChar = (char)(int)spacing) != '\0';
  }

  /// <summary>
  /// <para></para>
  /// </summary>
  public enum UnicodeSpacing
  {
    /// <summary>Represents no spacing.</summary>
    None = 0,
    /// <summary>Depends on font, typically 1/4 em, often adjusted.</summary>
    Space = 0x0020,
    /// <summary>As a space, but often not adjusted.</summary>
    NoBreakSpace = 0x00A0,
    /// <summary>0.</summary>
    MongolianVowelSeparator = 0x180E,
    /// <summary>1 en (= 1/2 em).</summary>
    EnQuad = 0x2000,
    /// <summary>1 em (nominally, the height of the font).</summary>
    EmQuad = 0x2001,
    /// <summary>1 en (= 1/2 em).</summary>
    EnSpace = 0x2002,
    /// <summary>1 em.</summary>
    EmSpace = 0x2003,
    /// <summary>1/3 em.</summary>
    ThreePerEmSpace = 0x2004,
    /// <summary>1/4 em.</summary>
    FourPerEmSpace = 0x2005,
    /// <summary>1/6 em.</summary>
    SixPerEmSpace = 0x2006,
    /// <summary>"Tabular width", the width of digits.</summary>
    FigureSpace = 0x2007,
    /// <summary>The width of a period "."</summary>
    PunctuationSpace = 0x2008,
    /// <summary>1/5 em (or sometimes 1/6 em).</summary>
    ThinSpace = 0x2009,
    /// <summary>Narrower than <see cref="ThinSpace"/>.</summary>
    HairSpace = 0x200A,
    /// <summary>0.</summary>
    ZeroWidthSpace = 0x200B,
    /// <summary>Narrower than <see cref="NoBreakSpace"/> (or <see cref="Space"/>), "typically the width of a thin space or a mid space".</summary>
    NarrowNoBreakSpace = 0x202F,
    /// <summary>4/18 em.</summary>
    MediumMathematicalSpace = 0x205F,
    /// <summary>The width of ideographic (CJK) characters.</summary>
    IdeographicSpace = 0x3000,
    /// <summary>0.</summary>
    ZeroWidthNoBreakSpace = 0xFEFF,
  }
}
