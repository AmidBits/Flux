namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Quantities.TorqueUnit unit, bool preferUnicode = false, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.TorqueUnit.NewtonMeter => preferUnicode ? "N\u22C5m" : "N·m",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum TorqueUnit
    {
      /// <summary>This is the default unit for <see cref="Torque"/>.</summary>
      NewtonMeter,
    }

    /// <summary>Torque unit of newton meter.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Torque"/>
    public readonly record struct Torque
      : System.IComparable, System.IComparable<Torque>, System.IFormattable, IUnitValueQuantifiable<double, TorqueUnit>
    {
      private readonly double m_value;

      public Torque(double value, TorqueUnit unit = TorqueUnit.NewtonMeter)
        => m_value = unit switch
        {
          TorqueUnit.NewtonMeter => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public Torque(Energy energy, Angle angle) : this(energy.Value / angle.Value) { }

      #region Static methods

      #endregion // Static methods

      #region Overloaded operators

      public static bool operator <(Torque a, Torque b) => a.CompareTo(b) < 0;
      public static bool operator <=(Torque a, Torque b) => a.CompareTo(b) <= 0;
      public static bool operator >(Torque a, Torque b) => a.CompareTo(b) > 0;
      public static bool operator >=(Torque a, Torque b) => a.CompareTo(b) >= 0;

      public static Torque operator -(Torque v) => new(-v.m_value);
      public static Torque operator +(Torque a, double b) => new(a.m_value + b);
      public static Torque operator +(Torque a, Torque b) => a + b.m_value;
      public static Torque operator /(Torque a, double b) => new(a.m_value / b);
      public static Torque operator /(Torque a, Torque b) => a / b.m_value;
      public static Torque operator *(Torque a, double b) => new(a.m_value * b);
      public static Torque operator *(Torque a, Torque b) => a * b.m_value;
      public static Torque operator %(Torque a, double b) => new(a.m_value % b);
      public static Torque operator %(Torque a, Torque b) => a % b.m_value;
      public static Torque operator -(Torque a, double b) => new(a.m_value - b);
      public static Torque operator -(Torque a, Torque b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Torque o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Torque other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(TorqueUnit.NewtonMeter, format, formatProvider);

      // IMetricMultiplicable<>
      public double GetMetricValue(MetricPrefix prefix) => MetricPrefix.NoPrefix.Convert(m_value, prefix);

      public string ToMetricValueString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetMetricValue(prefix).ToString(format, formatProvider));
        sb.Append(unitSpacing.ToSpacingString());
        sb.Append(prefix.GetUnitString(preferUnicode, useFullName));
        sb.Append(TorqueUnit.NewtonMeter.GetUnitString(preferUnicode, useFullName));
        return sb.ToString();
      }

      // IQuantifiable<>
      /// <summary>
      ///  <para>The unit of the <see cref="Torque.Value"/> property is in <see cref="TorqueUnit.NewtonMeter"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public string GetUnitSymbol(TorqueUnit unit, bool preferUnicode, bool useFullName) => unit.GetUnitString(preferUnicode, useFullName);

      public double GetUnitValue(TorqueUnit unit)
        => unit switch
        {
          TorqueUnit.NewtonMeter => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(TorqueUnit unit = TorqueUnit.NewtonMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
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
