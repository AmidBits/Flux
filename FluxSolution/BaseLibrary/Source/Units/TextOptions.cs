﻿namespace Flux.Units
{
  public readonly record struct TextOptions
  {
    public static TextOptions Default => default;

    private readonly System.Globalization.CultureInfo? m_cultureInfo;
    private readonly string? m_format;
    private readonly System.IFormatProvider? m_formatProvider;
    private readonly bool? m_preferUnicode;
    private readonly UnitSpacing? m_unitSpacing;
    private readonly bool m_useFullName;

    public TextOptions(string? format, System.IFormatProvider? formatProvider)
    {
      Format = format;
      FormatProvider = formatProvider;
    }

    public TextOptions(string? format)
    {
      Format = format;
    }

    /// <summary>
    /// <para>The culture info. The default is <see cref="System.Globalization.CultureInfo.InvariantCulture"/>.</para>
    /// </summary>
    public System.Globalization.CultureInfo CultureInfo { get => m_cultureInfo ?? System.Globalization.CultureInfo.InvariantCulture; init => m_cultureInfo = value; }

    /// <summary>
    /// <para>The format for the value. The default is <see cref="null"/>.</para>
    /// </summary>
    public string? Format { get => m_format; init => m_format = value; }

    /// <summary>
    /// <para>The format provider. The default is <see cref="null"/>.</para>
    /// </summary>
    public System.IFormatProvider? FormatProvider { get => m_formatProvider; init => m_formatProvider = value; }

    /// <summary>
    /// <para>Whether to prefer Unicode symbols, where and when available. The default is true.</para>
    /// </summary>
    public bool PreferUnicode { get => m_preferUnicode.HasValue ? m_preferUnicode.Value : true; init => m_preferUnicode = value; }

    /// <summary>
    /// <para>The spacing to use between value and unit. The default is <see cref="UnitSpacing.Space"/>.</para>
    /// </summary>
    public UnitSpacing UnitSpacing { get => m_unitSpacing.HasValue ? m_unitSpacing.Value : UnitSpacing.Space; init => m_unitSpacing = value; }

    /// <summary>
    /// <para>Whether to use the full actual name of the enum value, rather than symbols or shorter (e.g. acronym) variants. The default is false.</para>
    /// </summary>
    public bool UseFullName { get => m_useFullName; init => m_useFullName = value; }
  }
}