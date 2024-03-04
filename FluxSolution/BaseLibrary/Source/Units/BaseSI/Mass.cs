namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.MassUnit unit, bool preferUnicode = false, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        //Units.MassUnit.Milligram => preferUnicode ? "\u338E" : "mg",
        Units.MassUnit.Gram => "g",
        Units.MassUnit.Ounce => "oz",
        Units.MassUnit.Pound => "lb",
        Units.MassUnit.Kilogram => preferUnicode ? "\u338F" : "kg",
        Units.MassUnit.Tonne => "t",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum MassUnit
    {
      /// <summary>This is the default unit for <see cref="Mass"/>.</summary>
      Kilogram,
      //Milligram,
      Gram,
      Ounce,
      Pound,
      /// <summary>Metric ton.</summary>
      Tonne,
    }

    /// <summary>Mass. SI unit of kilogram. This is a base quantity.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Mass"/>
    public readonly record struct Mass
      : System.IComparable, System.IComparable<Mass>, System.IFormattable, IUnitValueQuantifiable<double, MassUnit>
    {
      public static Mass ElectronMass => new(9.109383701528e-31);

      private readonly double m_value;

      public Mass(double value, MassUnit unit = MassUnit.Kilogram)
        => m_value = unit switch
        {
          MassUnit.Kilogram => value,

          //MassUnit.Milligram => value / 1000000,
          MassUnit.Gram => value / 1000,
          MassUnit.Ounce => value / 35.27396195,
          MassUnit.Pound => value * 0.45359237,
          MassUnit.Tonne => value * 1000,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public Mass(MetricPrefix prefix, double gram) => m_value = prefix.Convert(gram, MetricPrefix.Kilo);

      #region Static methods
      #endregion Static methods

      #region Overloaded operators

      public static bool operator <(Mass a, Mass b) => a.CompareTo(b) < 0;
      public static bool operator <=(Mass a, Mass b) => a.CompareTo(b) <= 0;
      public static bool operator >(Mass a, Mass b) => a.CompareTo(b) > 0;
      public static bool operator >=(Mass a, Mass b) => a.CompareTo(b) >= 0;

      public static Mass operator -(Mass v) => new(-v.m_value);
      public static Mass operator +(Mass a, double b) => new(a.m_value + b);
      public static Mass operator +(Mass a, Mass b) => a + b.m_value;
      public static Mass operator /(Mass a, double b) => new(a.m_value / b);
      public static Mass operator /(Mass a, Mass b) => a / b.m_value;
      public static Mass operator *(Mass a, double b) => new(a.m_value * b);
      public static Mass operator *(Mass a, Mass b) => a * b.m_value;
      public static Mass operator %(Mass a, double b) => new(a.m_value % b);
      public static Mass operator %(Mass a, Mass b) => a % b.m_value;
      public static Mass operator -(Mass a, double b) => new(a.m_value - b);
      public static Mass operator -(Mass a, Mass b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Mass o ? CompareTo(o) : -1;

      // IComparable<T>
      public int CompareTo(Mass other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(MassUnit.Kilogram, UnitValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      //IMetricMultiplicable<>
      public double ToMetricValue(MetricPrefix prefix) => MetricPrefix.Kilo.Convert(m_value, prefix);

      public string ToMetricValueString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.NarrowNoBreakSpace)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(ToMetricValue(prefix).ToString(format, formatProvider));
        sb.Append(spacing.ToSpacingString());
        sb.Append(prefix.GetUnitString(true, false));
        sb.Append(MassUnit.Gram.GetUnitString(false, false));
        return sb.ToString();
      }

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="Mass.Value"/> property is in <see cref="MassUnit.Kilogram"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(MassUnit unit)
        => unit switch
        {
          //MassUnit.Milligram => m_value * 1000000,
          MassUnit.Gram => m_value * 1000,
          MassUnit.Ounce => m_value * 35.27396195,
          MassUnit.Pound => m_value / 0.45359237,
          MassUnit.Kilogram => m_value,
          MassUnit.Tonne => m_value / 1000,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(MassUnit unit, string? format, System.IFormatProvider? formatProvider, bool preferUnicode, UnicodeSpacing unicodeSpacing, bool useFullName)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unicodeSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(preferUnicode, useFullName));
        return sb.ToString();
      }

      public string ToUnitValueString(MassUnit unit, UnitValueStringOptions options = default)
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
