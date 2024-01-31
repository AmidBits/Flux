namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.AreaDensityUnit unit, QuantifiableValueStringOptions options = default)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.AreaDensityUnit.KilogramPerSquareMeter => "kg/m²",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum AreaDensityUnit
    {
      /// <summary>This is the default unit for <see cref="AreaDensity"/>.</summary>
      KilogramPerSquareMeter,
    }

    /// <summary>Surface density, unit of kilograms per square meter.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Surface_density"/>
    public readonly record struct AreaDensity
      : System.IComparable, System.IComparable<AreaDensity>, System.IFormattable, IUnitValueQuantifiable<double, AreaDensityUnit>
    {
      public const AreaDensityUnit DefaultUnit = AreaDensityUnit.KilogramPerSquareMeter;

      private readonly double m_value;

      public AreaDensity(double value, AreaDensityUnit unit = DefaultUnit)
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
      public static explicit operator double(AreaDensity v) => v.m_value;
      public static explicit operator AreaDensity(double v) => new(v);

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
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToValueString(QuantifiableValueStringOptions options = default) => ToUnitValueString(DefaultUnit, options);

      /// <summary>
      /// <para>The unit of the <see cref="AreaDensity.Value"/> property is in <see cref="AreaDensityUnit.KilogramPerSquareMeter"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(AreaDensityUnit unit)
        => unit switch
        {
          AreaDensityUnit.KilogramPerSquareMeter => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(AreaDensityUnit unit, QuantifiableValueStringOptions options = default)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
