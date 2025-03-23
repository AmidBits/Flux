namespace Flux.Geodesy
{
  /// <summary>
  /// <para>Unit interval, unit of rational number, with the interval 0.0 and 1.0.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Orbital_eccentricity"/></para>
  /// </summary>
  public readonly record struct OrbitalEccentricity
    : System.IComparable, System.IComparable<OrbitalEccentricity>, System.IFormattable, Units.IValueQuantifiable<double>
  {
    private readonly double m_value;

    public OrbitalEccentricity(double value)
      => m_value = double.IsNegative(value)
      ? throw new System.ArgumentOutOfRangeException(nameof(value)) // Orbital eccentricity cannot be negative.
      : value;

    #region Static methods

    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(OrbitalEccentricity a, OrbitalEccentricity b) => a.CompareTo(b) < 0;
    public static bool operator <=(OrbitalEccentricity a, OrbitalEccentricity b) => a.CompareTo(b) <= 0;
    public static bool operator >(OrbitalEccentricity a, OrbitalEccentricity b) => a.CompareTo(b) > 0;
    public static bool operator >=(OrbitalEccentricity a, OrbitalEccentricity b) => a.CompareTo(b) >= 0;

    public static OrbitalEccentricity operator -(OrbitalEccentricity v) => new(-v.m_value);
    public static OrbitalEccentricity operator +(OrbitalEccentricity a, double b) => new(a.m_value + b);
    public static OrbitalEccentricity operator +(OrbitalEccentricity a, OrbitalEccentricity b) => a + b.m_value;
    public static OrbitalEccentricity operator /(OrbitalEccentricity a, double b) => new(a.m_value / b);
    public static OrbitalEccentricity operator /(OrbitalEccentricity a, OrbitalEccentricity b) => a / b.m_value;
    public static OrbitalEccentricity operator *(OrbitalEccentricity a, double b) => new(a.m_value * b);
    public static OrbitalEccentricity operator *(OrbitalEccentricity a, OrbitalEccentricity b) => a * b.m_value;
    public static OrbitalEccentricity operator %(OrbitalEccentricity a, double b) => new(a.m_value % b);
    public static OrbitalEccentricity operator %(OrbitalEccentricity a, OrbitalEccentricity b) => a % b.m_value;
    public static OrbitalEccentricity operator -(OrbitalEccentricity a, double b) => new(a.m_value - b);
    public static OrbitalEccentricity operator -(OrbitalEccentricity a, OrbitalEccentricity b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is OrbitalEccentricity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(OrbitalEccentricity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

    // IValueQuantifiable<>
    ///// <summary>
    ///// <para>The <see cref="Eccentricity.Value"/> property is dimensionless.</para>
    ///// </summary>
    public double Value => m_value;

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
