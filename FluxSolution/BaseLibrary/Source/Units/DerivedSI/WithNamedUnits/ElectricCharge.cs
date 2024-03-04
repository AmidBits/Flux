namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.ElectricChargeUnit unit, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.ElectricChargeUnit.Coulomb => "C",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum ElectricChargeUnit
    {
      /// <summary>This is the default unit for <see cref="ElectricCharge"/>.</summary>
      Coulomb,
    }

    /// <summary>Electric charge, unit of Coulomb.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Electric_charge"/>
    public readonly record struct ElectricCharge
      : System.IComparable, System.IComparable<ElectricCharge>, System.IFormattable, IUnitValueQuantifiable<double, ElectricChargeUnit>
    {
      public static ElectricCharge ElementaryCharge => new(1.602176634e-19);

      private readonly double m_value;

      public ElectricCharge(double value, ElectricChargeUnit unit = ElectricChargeUnit.Coulomb)
        => m_value = unit switch
        {
          ElectricChargeUnit.Coulomb => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators

      public static bool operator <(ElectricCharge a, ElectricCharge b) => a.CompareTo(b) < 0;
      public static bool operator <=(ElectricCharge a, ElectricCharge b) => a.CompareTo(b) <= 0;
      public static bool operator >(ElectricCharge a, ElectricCharge b) => a.CompareTo(b) > 0;
      public static bool operator >=(ElectricCharge a, ElectricCharge b) => a.CompareTo(b) >= 0;

      public static ElectricCharge operator -(ElectricCharge v) => new(-v.m_value);
      public static ElectricCharge operator +(ElectricCharge a, double b) => new(a.m_value + b);
      public static ElectricCharge operator +(ElectricCharge a, ElectricCharge b) => a + b.m_value;
      public static ElectricCharge operator /(ElectricCharge a, double b) => new(a.m_value / b);
      public static ElectricCharge operator /(ElectricCharge a, ElectricCharge b) => a / b.m_value;
      public static ElectricCharge operator *(ElectricCharge a, double b) => new(a.m_value * b);
      public static ElectricCharge operator *(ElectricCharge a, ElectricCharge b) => a * b.m_value;
      public static ElectricCharge operator %(ElectricCharge a, double b) => new(a.m_value % b);
      public static ElectricCharge operator %(ElectricCharge a, ElectricCharge b) => a % b.m_value;
      public static ElectricCharge operator -(ElectricCharge a, double b) => new(a.m_value - b);
      public static ElectricCharge operator -(ElectricCharge a, ElectricCharge b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is ElectricCharge o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(ElectricCharge other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(ElectricChargeUnit.Coulomb, UnitValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="ElectricCharge.Value"/> property is in <see cref="ElectricChargeUnit.Ohm"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(ElectricChargeUnit unit)
        => unit switch
        {
          ElectricChargeUnit.Coulomb => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(ElectricChargeUnit unit, string? format, System.IFormatProvider? formatProvider, bool preferUnicode, UnicodeSpacing unicodeSpacing, bool useFullName)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unicodeSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(useFullName));
        return sb.ToString();
      }

      public string ToUnitValueString(ElectricChargeUnit unit, UnitValueStringOptions options = default)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(options.Format, options.FormatProvider));
        sb.Append(' ');
        sb.Append(unit.GetUnitString(options.UseFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
