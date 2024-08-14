namespace Flux
{
  public static partial class Fx
  {
    public static string GetUnitString(this Quantities.ForceUnit unit, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.ForceUnit.Newton => "N",
        Quantities.ForceUnit.KilogramForce => "kgf",
        Quantities.ForceUnit.PoundForce => "lbf",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum ForceUnit
    {
      /// <summary>This is the default unit for <see cref="Force"/>.</summary>
      Newton,
      KilogramForce,
      PoundForce,
    }

    /// <summary>Force, unit of newton. This is an SI derived quantity.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Force"/>
    public readonly record struct Force
      : System.IComparable, System.IComparable<Force>, System.IFormattable, ISiPrefixValueQuantifiable<double, ForceUnit>
    {
      private readonly double m_value;

      public Force(double value, ForceUnit unit = ForceUnit.Newton)
        => m_value = unit switch
        {
          ForceUnit.Newton => value,
          ForceUnit.KilogramForce => value / 0.101971621,
          ForceUnit.PoundForce => value / 0.224808943,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public Force(Mass mass, Acceleration acceleration) : this(mass.Value * acceleration.Value) { }

      #region Static methods

      #endregion // Static methods

      #region Overloaded operators

      public static bool operator <(Force a, Force b) => a.CompareTo(b) < 0;
      public static bool operator <=(Force a, Force b) => a.CompareTo(b) <= 0;
      public static bool operator >(Force a, Force b) => a.CompareTo(b) > 0;
      public static bool operator >=(Force a, Force b) => a.CompareTo(b) >= 0;

      public static Force operator -(Force v) => new(-v.m_value);
      public static Force operator +(Force a, double b) => new(a.m_value + b);
      public static Force operator +(Force a, Force b) => a + b.m_value;
      public static Force operator /(Force a, double b) => new(a.m_value / b);
      public static Force operator /(Force a, Force b) => a / b.m_value;
      public static Force operator *(Force a, double b) => new(a.m_value * b);
      public static Force operator *(Force a, Force b) => a * b.m_value;
      public static Force operator %(Force a, double b) => new(a.m_value % b);
      public static Force operator %(Force a, Force b) => a % b.m_value;
      public static Force operator -(Force a, double b) => new(a.m_value - b);
      public static Force operator -(Force a, Force b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Force o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Force other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(ForceUnit.Newton, format, formatProvider);

      // ISiUnitValueQuantifiable<>
      public (MetricPrefix Prefix, ForceUnit Unit) GetSiPrefixUnit(MetricPrefix prefix) => (prefix, ForceUnit.Newton);

      public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode, bool useFullName) => prefix.GetUnitString(preferUnicode, useFullName) + GetUnitSymbol(GetSiPrefixUnit(prefix).Unit, preferUnicode, useFullName);

      public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.NoPrefix.Convert(m_value, prefix);

      public string ToSiPrefixValueString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetSiPrefixValue(prefix).ToString(format, formatProvider));
        sb.Append(unitSpacing.ToSpacingString());
        sb.Append(GetSiPrefixSymbol(prefix, preferUnicode, useFullName));
        return sb.ToString();
      }

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="Force.Value"/> property is in <see cref="ForceUnit.Newton"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public string GetUnitSymbol(ForceUnit unit, bool preferUnicode, bool useFullName) => unit.GetUnitString(useFullName);

      public double GetUnitValue(ForceUnit unit)
        => unit switch
        {
          ForceUnit.Newton => m_value,
          ForceUnit.KilogramForce => m_value * 0.101971621,
          ForceUnit.PoundForce => m_value * 0.224808943,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(ForceUnit unit = ForceUnit.Newton, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
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
