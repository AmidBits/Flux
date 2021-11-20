namespace Flux.Colors
{
  /// <summary>CMYK refers to the four ink plates used in some color printing: Cyan, Magenta, Yellow, and Key (black).</summary>
  /// <see cref="https://en.wikipedia.org/wiki/CMYK_color_model"/>
  public struct Cmyk
    : System.IEquatable<Cmyk>
  {
    public static readonly Cmyk Empty;

    private double m_cyan;
    private double m_magenta;
    private double m_yellow;
    private double m_key;

    public Cmyk(double cyan, double magenta, double yellow, double key)
    {
      m_cyan = cyan >= 0 && cyan <= 1 ? cyan : throw new System.ArgumentOutOfRangeException(nameof(cyan));
      m_magenta = magenta >= 0 && magenta <= 1 ? magenta : throw new System.ArgumentOutOfRangeException(nameof(magenta));
      m_yellow = yellow >= 0 && yellow <= 1 ? yellow : throw new System.ArgumentOutOfRangeException(nameof(yellow));
      m_key = key >= 0 && key <= 1 ? key : throw new System.ArgumentOutOfRangeException(nameof(key));
    }

    public double Cyan { get => m_cyan; set => m_cyan = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public double Magenta { get => m_magenta; set => m_magenta = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public double Yellow { get => m_yellow; set => m_yellow = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    public double Key { get => m_key; set => m_key = value >= 0 && value <= 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    /// <summary>Creates an RGB color corresponding to the CMYK instance.</summary>
    public Rgb ToRgb()
    {
      var m1key = 1 - m_key;

      byte red = System.Convert.ToByte(255 * (1 - m_cyan) * m1key);
      byte green = System.Convert.ToByte(255 * (1 - m_magenta) * m1key);
      byte blue = System.Convert.ToByte(255 * (1 - m_yellow) * m1key);

      return new Rgb(red, green, blue);
    }

    #region Static members
    public static Cmyk FromRandom(System.Random rng)
    {
      if (rng is null) throw new System.ArgumentNullException(nameof(rng));

      return new Cmyk(rng.NextDouble(), rng.NextDouble(), rng.NextDouble(), rng.NextDouble());
    }
    public static Cmyk FromRandom()
      => FromRandom(Randomization.NumberGenerator.Crypto);
    #endregion Static members

    #region Overloaded operators
    public static bool operator ==(Cmyk a, Cmyk b)
      => a.Equals(b);
    public static bool operator !=(Cmyk a, Cmyk b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Cmyk other)
      => m_cyan == other.m_cyan && m_magenta == other.m_magenta && m_yellow == other.m_yellow && m_key == other.m_key;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Cmyk o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_cyan, m_magenta, m_yellow, m_key);
    public override string ToString()
      => $"{GetType().Name} {{ Cyan = {m_cyan * 360:N1}\u00B0, Magenta = {m_magenta * 360:N1}\u00B0, Yellow = {m_yellow * 360:N1}\u00B0, Key = {m_key * 360:N1}\u00B0 }}";
    #endregion Object overrides
  }
}
