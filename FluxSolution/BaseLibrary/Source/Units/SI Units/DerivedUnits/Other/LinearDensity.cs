namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Quantities.LinearDensityUnit unit, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.LinearDensityUnit.KilogramPerMeter => "kg/m",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum LinearDensityUnit
    {
      /// <summary>This is the default unit for <see cref="LinearDensity"/>.</summary>
      KilogramPerMeter,
    }

    /// <summary>
    /// <para>Linear mass density, unit of kilograms per meter.</para>
    /// <see href="https://en.wikipedia.org/wiki/Linear_density"/>
    /// </summary>
    /// <remarks>Dimensional relationship: <see cref="LinearDensity"/>, <see cref="AreaDensity"/> and <see cref="Density"/>.</remarks>
    public readonly record struct LinearDensity
      : System.IComparable, System.IComparable<LinearDensity>, System.IFormattable, IUnitValueQuantifiable<double, LinearDensityUnit>
    {
      private readonly double m_value;

      public LinearDensity(double value, LinearDensityUnit unit = LinearDensityUnit.KilogramPerMeter)
        => m_value = unit switch
        {
          LinearDensityUnit.KilogramPerMeter => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods
      public static LinearDensity From(Mass mass, Volume volume)
        => new(mass.Value / volume.Value);
      #endregion Static methods

      #region Overloaded operators

      public static bool operator <(LinearDensity a, LinearDensity b) => a.CompareTo(b) < 0;
      public static bool operator <=(LinearDensity a, LinearDensity b) => a.CompareTo(b) <= 0;
      public static bool operator >(LinearDensity a, LinearDensity b) => a.CompareTo(b) > 0;
      public static bool operator >=(LinearDensity a, LinearDensity b) => a.CompareTo(b) >= 0;

      public static LinearDensity operator -(LinearDensity v) => new(-v.m_value);
      public static LinearDensity operator +(LinearDensity a, double b) => new(a.m_value + b);
      public static LinearDensity operator +(LinearDensity a, LinearDensity b) => a + b.m_value;
      public static LinearDensity operator /(LinearDensity a, double b) => new(a.m_value / b);
      public static LinearDensity operator /(LinearDensity a, LinearDensity b) => a / b.m_value;
      public static LinearDensity operator *(LinearDensity a, double b) => new(a.m_value * b);
      public static LinearDensity operator *(LinearDensity a, LinearDensity b) => a * b.m_value;
      public static LinearDensity operator %(LinearDensity a, double b) => new(a.m_value % b);
      public static LinearDensity operator %(LinearDensity a, LinearDensity b) => a % b.m_value;
      public static LinearDensity operator -(LinearDensity a, double b) => new(a.m_value - b);
      public static LinearDensity operator -(LinearDensity a, LinearDensity b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is LinearDensity o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(LinearDensity other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(LinearDensityUnit.KilogramPerMeter, format, formatProvider);

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="LinearDensity.Value"/> property is in <see cref="LinearDensityUnit.KilogramPerCubicMeter"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public string GetUnitSymbol(LinearDensityUnit unit, bool preferUnicode, bool useFullName) => unit.GetUnitString(useFullName);

      public double GetUnitValue(LinearDensityUnit unit)
        => unit switch
        {
          LinearDensityUnit.KilogramPerMeter => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(LinearDensityUnit unit = LinearDensityUnit.KilogramPerMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
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
