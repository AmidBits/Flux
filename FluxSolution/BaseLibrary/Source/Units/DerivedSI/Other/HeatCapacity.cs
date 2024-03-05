namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.HeatCapacityUnit unit, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.HeatCapacityUnit.JoulePerKelvin => "J/K",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum HeatCapacityUnit
    {
      /// <summary>This is the default unit for <see cref="HeatCapacity"/>.</summary>
      JoulePerKelvin,
    }

    /// <summary>Heat capacity, unit of Joule per Kelvin.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Heat_capacity"/>
    public readonly record struct HeatCapacity
      : System.IComparable, System.IComparable<HeatCapacity>, System.IFormattable, IUnitValueQuantifiable<double, HeatCapacityUnit>
    {
      public static readonly HeatCapacity BoltzmannConstant = new(1.380649e-23);

      private readonly double m_value;

      public HeatCapacity(double value, HeatCapacityUnit unit = HeatCapacityUnit.JoulePerKelvin)
        => m_value = unit switch
        {
          HeatCapacityUnit.JoulePerKelvin => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods
      #endregion Static methods

      #region Overloaded operators

      public static bool operator <(HeatCapacity a, HeatCapacity b) => a.CompareTo(b) < 0;
      public static bool operator <=(HeatCapacity a, HeatCapacity b) => a.CompareTo(b) <= 0;
      public static bool operator >(HeatCapacity a, HeatCapacity b) => a.CompareTo(b) > 0;
      public static bool operator >=(HeatCapacity a, HeatCapacity b) => a.CompareTo(b) >= 0;

      public static HeatCapacity operator -(HeatCapacity v) => new(-v.m_value);
      public static HeatCapacity operator +(HeatCapacity a, double b) => new(a.m_value + b);
      public static HeatCapacity operator +(HeatCapacity a, HeatCapacity b) => a + b.m_value;
      public static HeatCapacity operator /(HeatCapacity a, double b) => new(a.m_value / b);
      public static HeatCapacity operator /(HeatCapacity a, HeatCapacity b) => a / b.m_value;
      public static HeatCapacity operator *(HeatCapacity a, double b) => new(a.m_value * b);
      public static HeatCapacity operator *(HeatCapacity a, HeatCapacity b) => a * b.m_value;
      public static HeatCapacity operator %(HeatCapacity a, double b) => new(a.m_value % b);
      public static HeatCapacity operator %(HeatCapacity a, HeatCapacity b) => a % b.m_value;
      public static HeatCapacity operator -(HeatCapacity a, double b) => new(a.m_value - b);
      public static HeatCapacity operator -(HeatCapacity a, HeatCapacity b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is HeatCapacity o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(HeatCapacity other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(HeatCapacityUnit.JoulePerKelvin, format, formatProvider);

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="HeatCapacity.Value"/> property is in <see cref="HeatCapacityUnit.JoulePerKelvin"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(HeatCapacityUnit unit)
        => unit switch
        {
          HeatCapacityUnit.JoulePerKelvin => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(HeatCapacityUnit unit = HeatCapacityUnit.JoulePerKelvin, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unicodeSpacing = UnicodeSpacing.None, bool useFullName = false)
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
