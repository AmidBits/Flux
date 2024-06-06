namespace Flux
{
  public static partial class Fx
  {
    public static string GetUnitString(this Quantities.MassUnit unit, bool preferUnicode = false, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        //Units.MassUnit.Milligram => preferUnicode ? "\u338E" : "mg",
        Quantities.MassUnit.Gram => "g",
        Quantities.MassUnit.Ounce => "oz",
        Quantities.MassUnit.Pound => "lb",
        Quantities.MassUnit.Kilogram => preferUnicode ? "\u338F" : "kg",
        Quantities.MassUnit.Tonne => "t",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum MassUnit
    {
      /// <summary>This is the default unit for <see cref="Mass"/>.</summary>
      Kilogram,
      //Milligram,
      Gram,
      Ounce,
      Pound,
      /// <summary>The metric ton.</summary>
      Tonne,
    }

    /// <summary>
    /// <para>Mass. SI unit of kilogram. This is a base quantity.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Mass"/></para>
    /// </summary>
    public readonly record struct Mass
      : System.IComparable, System.IComparable<Mass>, System.IFormattable, ISiPrefixValueQuantifiable<double, MassUnit>
    {
      public static Mass ElectronMass => new(9.109383701528e-31);

      private readonly double m_value;

      /// <summary>
      /// <para>Creates a new instance from the specified <paramref name="value"/> of <paramref name="unit"/>. The default <paramref name="unit"/> is <see cref="MassUnit.Kilogram"/></para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="unit"></param>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
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

      /// <summary>
      /// <para>Creates a new instance from the specified <see cref="MetricPrefix"/> (metric multiple) of <see cref="MassUnit.Gram"/>, e.g. <see cref="MetricPrefix.Kilo"/> for kilogram.</para>
      /// </summary>
      /// <param name="grams"></param>
      /// <param name="prefix"></param>
      public Mass(double grams, MetricPrefix prefix) => m_value = prefix.Convert(grams, MetricPrefix.Kilo);

      #region Static methods

      #endregion // Static methods

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
        => ToUnitValueString(MassUnit.Kilogram, format, formatProvider);

      // ISiUnitValueQuantifiable<>
      public MassUnit BaseUnit => MassUnit.Kilogram;

      public MassUnit UnprefixedUnit => MassUnit.Gram;

      public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode, bool useFullName) => prefix.GetUnitString(preferUnicode, useFullName) + GetUnitSymbol(UnprefixedUnit, preferUnicode, useFullName);

      public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.Kilo.Convert(m_value, prefix);

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
      /// <para>The unit of the <see cref="Mass.Value"/> property is in <see cref="MassUnit.Kilogram"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public string GetUnitSymbol(MassUnit unit, bool preferUnicode, bool useFullName) => unit.GetUnitString(preferUnicode, useFullName);

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

      public string ToUnitValueString(MassUnit unit = MassUnit.Kilogram, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
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
