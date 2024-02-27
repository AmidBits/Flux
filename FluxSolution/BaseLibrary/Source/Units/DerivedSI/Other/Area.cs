namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.AreaUnit unit, bool preferUnicode = false, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.AreaUnit.SquareMeter => preferUnicode ? "\u33A1" : "m²",
        Units.AreaUnit.Hectare => preferUnicode ? "\u33CA" : "ha",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum AreaUnit
    {
      /// <summary>This is the default unit for <see cref="Area"/>.</summary>
      SquareMeter,
      Hectare,
    }

    /// <summary>Area, unit of square meter. This is an SI derived quantity.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Area"/>
    public readonly record struct Area
      : System.IComparable, System.IComparable<Area>, System.IFormattable, IUnitValueQuantifiable<double, AreaUnit>
    {
      private readonly double m_value;

      public Area(double value, AreaUnit unit = AreaUnit.SquareMeter)
        => m_value = unit switch
        {
          AreaUnit.SquareMeter => value,
          AreaUnit.Hectare => value * 10000,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods

      #endregion // Static methods

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
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(AreaUnit.SquareMeter, UnitValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="Area.Value"/> property is in <see cref="AreaUnit.SquareMeter"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(AreaUnit unit)
        => unit switch
        {
          AreaUnit.SquareMeter => m_value,
          AreaUnit.Hectare => m_value / 10000,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(AreaUnit unit, UnitValueStringOptions options = default)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(options.Format, options.FormatProvider));
        sb.Append(options.UnitSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(options.PreferUnicode, options.UseFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
