namespace Flux.IFormatProvider
{
  //public struct NanpNpa
  //{
  //  private readonly int? m_code;

  //  public int? Code
  //    => m_code;

  //  public bool IsEmpty
  //    => !m_code.HasValue;

  //  public NanpNpa(int code)
  //  {
  //    if (!IsValidNPA(code)) throw new System.ArgumentOutOfRangeException(nameof(code));

  //    m_code = code;
  //  }

  //  public bool AssignedByNANP(int code)
  //    => code.ToString()[1] == '9';

  //  public static bool IsValidNPA(int code)
  //    => code >= 200 && code <= 999;

  //  public override string ToString()
  //    => m_code.ToString();
  //}

  //public struct NanpNxx
  //{
  //  private readonly int? m_code;

  //  public int? Code
  //    => m_code;

  //  public bool IsEmpty
  //    => !m_code.HasValue;

  //  public bool IsN11()
  //    => m_code.ToString() is var c && c[1] == '1' && c[2] == '1';

  //  public bool IsValid()
  //    => m_code >= 200 && m_code <= 999;

  //  public NanpNxx(int? code)
  //  {
  //    m_code = code;
  //  }

  //  public override string ToString()
  //    => m_code.ToString();
  //}

  //public struct NanpXxxx
  //{
  //  private readonly int? m_code;

  //  public int? Code
  //    => m_code;

  //  public bool IsEmpty
  //    => !m_code.HasValue;

  //  public bool IsValidXxxx()
  //    => !IsEmpty && m_code >= 0 && m_code <= 9999;

  //  public NanpXxxx(int? code)
  //  {
  //    m_code = code;
  //  }

  //  public override string ToString()
  //    => m_code.ToString().PadLeft(4, '0');
  //}

  /// <summary>The North American Numbering Plan (NANP) is a telephone numbering plan that encompasses 25 distinct regions.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/North_American_Numbering_Plan"/>
  //public struct Nanp
  //{
  //  private int? m_cc;
  //  public int? Cc { get => m_cc; set => m_cc = !value.HasValue || value == 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

  //  private int? m_npa;
  //  public int? Npa { get => m_npa; set => m_npa = !value.HasValue || (value >= 200 && value <= 999 && !IsReservedNPA(value.Value)) ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

  //  private int? m_nxx;
  //  public int? Nxx { get => m_nxx; set => m_nxx = !value.HasValue || (value >= 200 && value <= 999 && !IsN11(value.Value)) ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

  //  private int? m_xxxx;
  //  public int? Xxxx { get => m_xxxx; set => m_xxxx = !value.HasValue || (value >= 0 && value <= 9999) ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

  //  public static bool IsReservedNPA(int npa)
  //    => (npa.ToString() ?? throw new System.InvalidOperationException())[1] == '9';

  //  public bool IsN11(int nxx)
  //    => (nxx.ToString() ?? throw new System.InvalidOperationException()) is var c && c[1] == '1' && c[2] == '1';

  //  public static string TranslateAlphabeticMnemonics(string phoneNumberWithAlphabeticMnemonics)
  //  {
  //    var sb = new System.Text.StringBuilder();

  //    foreach (var c in phoneNumberWithAlphabeticMnemonics)
  //    {
  //      if (c >= 'A' && c <= 'Z')
  //      {
  //        sb.Append(@"22233344455566677778889999"[c - 'A']);
  //      }
  //      else
  //      {
  //        sb.Append(c);
  //      }
  //    }

  //    return sb.ToString();
  //  }

  //  private static readonly System.Text.RegularExpressions.Regex _regexParse = new System.Text.RegularExpressions.Regex(@"(?<!\d)(?<cc>1)?[\s\-\.]*?(?<NPA>[2-9][0-9]{2})?[\s\-\.]*?(?<NXX>[2-9][0-9]{2})[\s\-\.]*?(?<xxxx>[0-9]{4})(?!\d)");

  //  public static Nanp Parse(string text)
  //  {
  //    if (_regexParse.Match(text) is var m && m.Success)
  //    {
  //      var nanp = new Nanp();

  //      if (m.Groups["cc"] is var g1 && g1.Success && int.TryParse(g1.Value, out var cc)) nanp.Cc = cc;

  //      if (m.Groups["NPA"] is var g2 && g2.Success && int.TryParse(g2.Value, out var npa)) nanp.Npa = npa;

  //      if (m.Groups["NXX"] is var g3 && g3.Success && int.TryParse(g3.Value, out var nxx)) nanp.Nxx = nxx;

  //      if (m.Groups["xxxx"] is var g4 && g4.Success && int.TryParse(g4.Value, out var xxxx)) nanp.Xxxx = xxxx;

  //      if (nanp.m_nxx.HasValue && nanp.m_xxxx.HasValue) return nanp;
  //    }

  //    throw new System.ArgumentOutOfRangeException(nameof(text));
  //  }

  //  /// <summary>Try to parse the text, extract a NANP string, and return whether it was succesful.</summary>
  //  public static bool TryParse(string text, out Nanp nanp)
  //  {
  //    try
  //    {
  //      nanp = Parse(text);
  //      return true;
  //    }
  //    catch { }

  //    nanp = default;
  //    return false;
  //  }

  //  public override string ToString()
  //  {
  //    var separator = m_cc.HasValue ? ' ' : '-';

  //    var sb = new System.Text.StringBuilder(15);

  //    if (m_cc.HasValue)
  //    {
  //      sb.Append('+');
  //      sb.Append(m_cc);
  //    }

  //    if (m_npa.HasValue)
  //    {
  //      if (sb.Length > 0) sb.Append(separator);
  //      sb.Append(m_npa);
  //    }

  //    if (m_nxx.HasValue)
  //    {
  //      if (sb.Length > 0) sb.Append(separator);
  //      sb.Append(m_nxx);
  //    }

  //    if (m_xxxx.HasValue)
  //    {
  //      if (sb.Length > 0) sb.Append(separator);
  //      sb.Append((m_xxxx.ToString() ?? string.Empty).PadLeft(4, '0'));
  //    }

  //    return sb.ToString();
  //  }
  //}
}
