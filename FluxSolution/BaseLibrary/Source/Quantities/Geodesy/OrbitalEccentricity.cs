namespace Flux
{
  public static partial class Fx
  {
    public static Quantities.OrbitalEccentricityClass GetOrbitalEccentricityClass(this Quantities.OrbitalEccentricity source)
      => source.Value switch
      {
        0 => Quantities.OrbitalEccentricityClass.CircularOrbit,
        > 0 and < 1 => Quantities.OrbitalEccentricityClass.EllipticOrbit,
        1 => Quantities.OrbitalEccentricityClass.ParabolicTrajectory,
        > 1 => Quantities.OrbitalEccentricityClass.HyperbolicTrajectory,
        _ => throw new System.ArgumentOutOfRangeException(nameof(source)),
      };
  }

  namespace Quantities
  {
    public enum OrbitalEccentricityClass
    {
      CircularOrbit,
      EllipticOrbit,
      ParabolicTrajectory,
      HyperbolicTrajectory
    }

    /// <summary>
    /// <para>Unit interval, unit of rational number, with the interval 0.0 and 1.0.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Orbital_eccentricity"/></para>
    /// </summary>
    public readonly record struct OrbitalEccentricity
      : System.IComparable, System.IComparable<OrbitalEccentricity>, System.IFormattable, IValueQuantifiable<double>
    {
      private readonly double m_value;

      public OrbitalEccentricity(double value) => m_value = value;

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
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => string.Format(formatProvider, $"{{0{(format is null ? string.Empty : $":{format}")}}}", m_value);

      // IQuantifiable<>
      ///// <summary>
      ///// <para>The <see cref="Eccentricity.Value"/> property is dimensionless.</para>
      ///// </summary>
      public double Value => m_value;

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
