namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Quantities.IrradianceUnit unit, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.IrradianceUnit.WattPerSquareMeter => "W/m²",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum IrradianceUnit
    {
      /// <summary>This is the default unit for <see cref="Irradiance"/>.</summary>
      WattPerSquareMeter,
    }

    /// <summary>
    /// <para>Irradiance, unit of watt per square meter.</para>
    /// <see href="https://en.wikipedia.org/wiki/Irradiance"/>
    /// </summary>
    public readonly record struct Irradiance
      : System.IComparable, System.IComparable<Irradiance>, System.IFormattable, IUnitValueQuantifiable<double, IrradianceUnit>
    {
      private readonly double m_value;

      public Irradiance(double value, IrradianceUnit unit = IrradianceUnit.WattPerSquareMeter)
        => m_value = unit switch
        {
          IrradianceUnit.WattPerSquareMeter => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods

      public static Irradiance From(Power power, Area area)
        => new(power.Value / area.Value);
      #endregion Static methods

      #region Overloaded operators

      public static bool operator <(Irradiance a, Irradiance b) => a.CompareTo(b) < 0;
      public static bool operator <=(Irradiance a, Irradiance b) => a.CompareTo(b) <= 0;
      public static bool operator >(Irradiance a, Irradiance b) => a.CompareTo(b) > 0;
      public static bool operator >=(Irradiance a, Irradiance b) => a.CompareTo(b) >= 0;

      public static Irradiance operator -(Irradiance v) => new(-v.m_value);
      public static Irradiance operator +(Irradiance a, double b) => new(a.m_value + b);
      public static Irradiance operator +(Irradiance a, Irradiance b) => a + b.m_value;
      public static Irradiance operator /(Irradiance a, double b) => new(a.m_value / b);
      public static Irradiance operator /(Irradiance a, Irradiance b) => a / b.m_value;
      public static Irradiance operator *(Irradiance a, double b) => new(a.m_value * b);
      public static Irradiance operator *(Irradiance a, Irradiance b) => a * b.m_value;
      public static Irradiance operator %(Irradiance a, double b) => new(a.m_value % b);
      public static Irradiance operator %(Irradiance a, Irradiance b) => a % b.m_value;
      public static Irradiance operator -(Irradiance a, double b) => new(a.m_value - b);
      public static Irradiance operator -(Irradiance a, Irradiance b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Irradiance o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Irradiance other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(IrradianceUnit.WattPerSquareMeter, format, formatProvider);

      // IMetricMultiplicable<>
      public double GetMetricValue(MetricPrefix prefix) => MetricPrefix.NoPrefix.Convert(m_value, prefix);

      public string ToMetricValueString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetMetricValue(prefix).ToString(format, formatProvider));
        sb.Append(unitSpacing.ToSpacingString());
        sb.Append(prefix.GetUnitString(preferUnicode, useFullName));
        sb.Append(IrradianceUnit.WattPerSquareMeter.GetUnitString(useFullName));
        return sb.ToString();
      }

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="Irradiance.Value"/> property is in <see cref="IrradianceUnit.WattPerSquareMeter"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public string GetUnitSymbol(IrradianceUnit unit, bool preferUnicode, bool useFullName) => unit.GetUnitString(useFullName);

      public double GetUnitValue(IrradianceUnit unit)
        => unit switch
        {
          IrradianceUnit.WattPerSquareMeter => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(IrradianceUnit unit = IrradianceUnit.WattPerSquareMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
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
