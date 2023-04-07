namespace Flux
{
  public static partial class QuantitiesExtensionMethods
  {
    public static string GetUnitString(this Quantities.AreaUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.AreaUnit.SquareMeter => preferUnicode ? "\u33A1" : "m²",
        Quantities.AreaUnit.Hectare => preferUnicode ? "\u33CA" : "ha",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum AreaUnit
    {
      SquareMeter,
      Hectare,
    }

    /// <summary>Area, unit of square meter. This is an SI derived quantity.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Area"/>
    public readonly record struct Area
      : System.IComparable, System.IComparable<Area>, System.IFormattable, IUnitQuantifiable<double, AreaUnit>
    {
      public const AreaUnit DefaultUnit = AreaUnit.SquareMeter;

      private readonly double m_value;

      public Area(double value, AreaUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          AreaUnit.SquareMeter => value,
          AreaUnit.Hectare => value * 10000,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods
      /// <summary>Creates a new Area instance from the specified rectangular length and width.</summary>
      /// <param name="length"></param>
      /// <param name="width"></param>

      public static Area From(Length length, Length width)
        => new(length.Value * width.Value);
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(Area v) => v.m_value;
      public static explicit operator Area(double v) => new(v);

      public static bool operator <(Area a, Area b) => a.CompareTo(b) < 0;
      public static bool operator <=(Area a, Area b) => a.CompareTo(b) <= 0;
      public static bool operator >(Area a, Area b) => a.CompareTo(b) > 0;
      public static bool operator >=(Area a, Area b) => a.CompareTo(b) >= 0;

      public static Area operator -(Area v) => new(-v.m_value);
      public static Area operator +(Area a, double b) => new(a.m_value + b);
      public static Area operator +(Area a, Area b) => a + b.m_value;
      public static Area operator /(Area a, double b) => new(a.m_value / b);
      public static Area operator /(Area a, Area b) => a / b.m_value;
      public static Area operator *(Area a, double b) => new(a.m_value * b);
      public static Area operator *(Area a, Area b) => a * b.m_value;
      public static Area operator %(Area a, double b) => new(a.m_value % b);
      public static Area operator %(Area a, Area b) => a % b.m_value;
      public static Area operator -(Area a, double b) => new(a.m_value - b);
      public static Area operator -(Area a, Area b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Area o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Area other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
      public double Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(AreaUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
      public double ToUnitValue(AreaUnit unit = DefaultUnit)
        => unit switch
        {
          AreaUnit.SquareMeter => m_value,
          AreaUnit.Hectare => m_value / 10000,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
