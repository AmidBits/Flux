namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Quantities.AreaDensityUnit unit, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.AreaDensityUnit.KilogramPerSquareMeter => "kg/m²",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum AreaDensityUnit
    {
      /// <summary>This is the default unit for <see cref="AreaDensity"/>.</summary>
      KilogramPerSquareMeter,
    }

    /// <summary>
    /// <para>Area mass density, unit of kilograms per square meter.</para>
    /// <see href="https://en.wikipedia.org/wiki/Area_density"/>
    /// </summary>
    /// <remarks>Dimensional relationship: <see cref="LinearDensity"/>, <see cref="AreaDensity"/> and <see cref="Density"/>.</remarks>
    public readonly record struct AreaDensity
      : System.IComparable, System.IComparable<AreaDensity>, System.IFormattable, IUnitValueQuantifiable<double, AreaDensityUnit>
    {
      private readonly double m_value;

      public AreaDensity(double value, AreaDensityUnit unit = AreaDensityUnit.KilogramPerSquareMeter)
        => m_value = unit switch
        {
          AreaDensityUnit.KilogramPerSquareMeter => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods

      public static AreaDensity From(Mass mass, Area volume)
        => new(mass.Value / volume.Value);
      #endregion Static methods

      #region Overloaded operators

      public static bool operator <(AreaDensity a, AreaDensity b) => a.CompareTo(b) < 0;
      public static bool operator <=(AreaDensity a, AreaDensity b) => a.CompareTo(b) <= 0;
      public static bool operator >(AreaDensity a, AreaDensity b) => a.CompareTo(b) > 0;
      public static bool operator >=(AreaDensity a, AreaDensity b) => a.CompareTo(b) >= 0;

      public static AreaDensity operator -(AreaDensity v) => new(-v.m_value);
      public static AreaDensity operator +(AreaDensity a, double b) => new(a.m_value + b);
      public static AreaDensity operator +(AreaDensity a, AreaDensity b) => a + b.m_value;
      public static AreaDensity operator /(AreaDensity a, double b) => new(a.m_value / b);
      public static AreaDensity operator /(AreaDensity a, AreaDensity b) => a / b.m_value;
      public static AreaDensity operator *(AreaDensity a, double b) => new(a.m_value * b);
      public static AreaDensity operator *(AreaDensity a, AreaDensity b) => a * b.m_value;
      public static AreaDensity operator %(AreaDensity a, double b) => new(a.m_value % b);
      public static AreaDensity operator %(AreaDensity a, AreaDensity b) => a % b.m_value;
      public static AreaDensity operator -(AreaDensity a, double b) => new(a.m_value - b);
      public static AreaDensity operator -(AreaDensity a, AreaDensity b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is AreaDensity o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(AreaDensity other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(AreaDensityUnit.KilogramPerSquareMeter, format, formatProvider);

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="AreaDensity.Value"/> property is in <see cref="AreaDensityUnit.KilogramPerSquareMeter"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public string GetUnitSymbol(AreaDensityUnit unit, bool preferUnicode, bool useFullName) => unit.GetUnitString(useFullName);

      public double GetUnitValue(AreaDensityUnit unit)
        => unit switch
        {
          AreaDensityUnit.KilogramPerSquareMeter => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(AreaDensityUnit unit = AreaDensityUnit.KilogramPerSquareMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unitSpacing.ToSpacingString());
        sb.Append(GetUnitSymbol(unit, preferUnicode, useFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
