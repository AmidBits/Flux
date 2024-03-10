namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.DensityUnit unit, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.DensityUnit.KilogramPerCubicMeter => "kg/m³",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum DensityUnit
    {
      /// <summary>This is the default unit for <see cref="Density"/>.</summary>
      KilogramPerCubicMeter,
    }

    /// <summary>
    /// <para>Volumetric mass density, unit of kilograms per cubic meter.</para>
    /// <see href="https://en.wikipedia.org/wiki/Density"/>
    /// </summary>
    /// <remarks>Dimensional relationship: <see cref="LinearDensity"/>, <see cref="AreaDensity"/> and <see cref="Density"/>.</remarks>
    public readonly record struct Density
      : System.IComparable, System.IComparable<Density>, System.IFormattable, IUnitValueQuantifiable<double, DensityUnit>
    {
      private readonly double m_value;

      public Density(double value, DensityUnit unit = DensityUnit.KilogramPerCubicMeter)
        => m_value = unit switch
        {
          DensityUnit.KilogramPerCubicMeter => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods
      public static Density From(Mass mass, Volume volume)
        => new(mass.Value / volume.Value);
      #endregion Static methods

      #region Overloaded operators

      public static bool operator <(Density a, Density b) => a.CompareTo(b) < 0;
      public static bool operator <=(Density a, Density b) => a.CompareTo(b) <= 0;
      public static bool operator >(Density a, Density b) => a.CompareTo(b) > 0;
      public static bool operator >=(Density a, Density b) => a.CompareTo(b) >= 0;

      public static Density operator -(Density v) => new(-v.m_value);
      public static Density operator +(Density a, double b) => new(a.m_value + b);
      public static Density operator +(Density a, Density b) => a + b.m_value;
      public static Density operator /(Density a, double b) => new(a.m_value / b);
      public static Density operator /(Density a, Density b) => a / b.m_value;
      public static Density operator *(Density a, double b) => new(a.m_value * b);
      public static Density operator *(Density a, Density b) => a * b.m_value;
      public static Density operator %(Density a, double b) => new(a.m_value % b);
      public static Density operator %(Density a, Density b) => a % b.m_value;
      public static Density operator -(Density a, double b) => new(a.m_value - b);
      public static Density operator -(Density a, Density b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Density o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Density other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(DensityUnit.KilogramPerCubicMeter, format, formatProvider);

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="Density.Value"/> property is in <see cref="DensityUnit.KilogramPerCubicMeter"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(DensityUnit unit)
        => unit switch
        {
          DensityUnit.KilogramPerCubicMeter => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(DensityUnit unit = DensityUnit.KilogramPerCubicMeter, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unicodeSpacing = UnicodeSpacing.None, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unicodeSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(useFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
