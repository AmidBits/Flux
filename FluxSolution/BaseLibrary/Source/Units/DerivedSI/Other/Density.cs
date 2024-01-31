namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.DensityUnit unit, QuantifiableValueStringOptions options = default)
      => options.UseFullName ? unit.ToString() : unit switch
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

    /// <summary>Density unit of kilograms per cubic meter.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Density"/>
    public readonly record struct Density
      : System.IComparable, System.IComparable<Density>, System.IFormattable, IUnitValueQuantifiable<double, DensityUnit>
    {
      public const DensityUnit DefaultUnit = DensityUnit.KilogramPerCubicMeter;

      private readonly double m_value;

      public Density(double value, DensityUnit unit = DefaultUnit)
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
      public static explicit operator double(Density v) => v.m_value;
      public static explicit operator Density(double v) => new(v);

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
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToValueString(QuantifiableValueStringOptions options = default) => ToUnitValueString(DefaultUnit, options);

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

      public string ToUnitValueString(DensityUnit unit, QuantifiableValueStringOptions options = default)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
