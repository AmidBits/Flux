namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Quantities.AbsorbedDoseUnit unit, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.AbsorbedDoseUnit.Gray => "Gy",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum AbsorbedDoseUnit
    {
      /// <summary>This is the default unit for <see cref="AbsorbedDose"/>.</summary>
      Gray,
    }

    /// <summary>
    /// <para>Force, unit of newton. This is an SI derived quantity.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Force"/></para>
    /// </summary>
    public readonly record struct AbsorbedDose
      : System.IComparable, System.IComparable<AbsorbedDose>, System.IFormattable, IUnitValueQuantifiable<double, AbsorbedDoseUnit>
    {
      private readonly double m_value;

      public AbsorbedDose(double value, AbsorbedDoseUnit unit = AbsorbedDoseUnit.Gray)
        => m_value = unit switch
        {
          AbsorbedDoseUnit.Gray => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators

      public static bool operator <(AbsorbedDose a, AbsorbedDose b) => a.CompareTo(b) < 0;
      public static bool operator <=(AbsorbedDose a, AbsorbedDose b) => a.CompareTo(b) <= 0;
      public static bool operator >(AbsorbedDose a, AbsorbedDose b) => a.CompareTo(b) > 0;
      public static bool operator >=(AbsorbedDose a, AbsorbedDose b) => a.CompareTo(b) >= 0;

      public static AbsorbedDose operator -(AbsorbedDose v) => new(-v.m_value);
      public static AbsorbedDose operator +(AbsorbedDose a, double b) => new(a.m_value + b);
      public static AbsorbedDose operator +(AbsorbedDose a, AbsorbedDose b) => a + b.m_value;
      public static AbsorbedDose operator /(AbsorbedDose a, double b) => new(a.m_value / b);
      public static AbsorbedDose operator /(AbsorbedDose a, AbsorbedDose b) => a / b.m_value;
      public static AbsorbedDose operator *(AbsorbedDose a, double b) => new(a.m_value * b);
      public static AbsorbedDose operator *(AbsorbedDose a, AbsorbedDose b) => a * b.m_value;
      public static AbsorbedDose operator %(AbsorbedDose a, double b) => new(a.m_value % b);
      public static AbsorbedDose operator %(AbsorbedDose a, AbsorbedDose b) => a % b.m_value;
      public static AbsorbedDose operator -(AbsorbedDose a, double b) => new(a.m_value - b);
      public static AbsorbedDose operator -(AbsorbedDose a, AbsorbedDose b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is AbsorbedDose o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(AbsorbedDose other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(AbsorbedDoseUnit.Gray, format, formatProvider);

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="AbsorbedDose.Value"/> property is in <see cref="AbsorbedDoseUnit.Gray"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public string GetUnitSymbol(AbsorbedDoseUnit unit, bool preferUnicode, bool useFullName) => unit.GetUnitString(useFullName);

      public double GetUnitValue(AbsorbedDoseUnit unit)
        => unit switch
        {
          AbsorbedDoseUnit.Gray => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(AbsorbedDoseUnit unit = AbsorbedDoseUnit.Gray, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
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