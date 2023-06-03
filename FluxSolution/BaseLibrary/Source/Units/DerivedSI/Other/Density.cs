namespace Flux
{
  public static partial class UnitsExtensionMethods
  {
#pragma warning disable IDE0060 // Remove unused parameter
    public static string GetUnitString(this Units.DensityUnit unit, bool preferUnicode, bool useFullName = false)
#pragma warning restore IDE0060 // Remove unused parameter
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
      KilogramPerCubicMeter,
    }

    /// <summary>Density unit of kilograms per cubic meter.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Density"/>
    public readonly record struct Density
      : System.IComparable, System.IComparable<Density>, System.IFormattable, IUnitQuantifiable<double, DensityUnit>
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
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
      public double Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(DensityUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
      public double ToUnitValue(DensityUnit unit = DefaultUnit)
        => unit switch
        {
          DensityUnit.KilogramPerCubicMeter => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
