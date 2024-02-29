namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.CurrentDensityUnit unit, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.CurrentDensityUnit.AmperePerSquareMeter => "A/m²",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum CurrentDensityUnit
    {
      /// <summary>This is the default unit for <see cref="CurrentDensity"/>.</summary>
      AmperePerSquareMeter,
    }

    /// <summary>Current density, unit of ampere per square meter.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Current_density"/>
    public readonly record struct CurrentDensity
      : System.IComparable, System.IComparable<CurrentDensity>, System.IFormattable, IUnitValueQuantifiable<double, CurrentDensityUnit>
    {
      private readonly double m_value;

      public CurrentDensity(double value, CurrentDensityUnit unit = CurrentDensityUnit.AmperePerSquareMeter)
        => m_value = unit switch
        {
          CurrentDensityUnit.AmperePerSquareMeter => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      //public MetricMultiplicative ToMetricMultiplicative() => new(GetUnitValue(CurrentDensityUnit.AmperePerSquareMeter), MetricMultiplicativePrefix.One);

      #region Overloaded operators

      public static bool operator <(CurrentDensity a, CurrentDensity b) => a.CompareTo(b) < 0;
      public static bool operator <=(CurrentDensity a, CurrentDensity b) => a.CompareTo(b) <= 0;
      public static bool operator >(CurrentDensity a, CurrentDensity b) => a.CompareTo(b) > 0;
      public static bool operator >=(CurrentDensity a, CurrentDensity b) => a.CompareTo(b) >= 0;

      public static CurrentDensity operator -(CurrentDensity v) => new(-v.m_value);
      public static CurrentDensity operator +(CurrentDensity a, double b) => new(a.m_value + b);
      public static CurrentDensity operator +(CurrentDensity a, CurrentDensity b) => a + b.m_value;
      public static CurrentDensity operator /(CurrentDensity a, double b) => new(a.m_value / b);
      public static CurrentDensity operator /(CurrentDensity a, CurrentDensity b) => a / b.m_value;
      public static CurrentDensity operator *(CurrentDensity a, double b) => new(a.m_value * b);
      public static CurrentDensity operator *(CurrentDensity a, CurrentDensity b) => a * b.m_value;
      public static CurrentDensity operator %(CurrentDensity a, double b) => new(a.m_value % b);
      public static CurrentDensity operator %(CurrentDensity a, CurrentDensity b) => a % b.m_value;
      public static CurrentDensity operator -(CurrentDensity a, double b) => new(a.m_value - b);
      public static CurrentDensity operator -(CurrentDensity a, CurrentDensity b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is CurrentDensity o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(CurrentDensity other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(CurrentDensityUnit.AmperePerSquareMeter, UnitValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="CurrentDensity.Value"/> property is in <see cref="CurrentDensityUnit.AmperePerSquareMeter"/>.</para>
      /// </summary>
      public double Value => m_value;

      //IUnitQuantifiable<>
      public double GetUnitValue(CurrentDensityUnit unit)
        => unit switch
        {
          CurrentDensityUnit.AmperePerSquareMeter => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(CurrentDensityUnit unit, UnitValueStringOptions options = default)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(options.Format, options.FormatProvider));
        sb.Append(options.UnitSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(options.UseFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
