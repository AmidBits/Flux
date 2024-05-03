namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Quantities.EquivalentDoseUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.EquivalentDoseUnit.Sievert => preferUnicode ? "\u33DC" : "Sv",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum EquivalentDoseUnit
    {
      /// <summary>This is the default unit for <see cref="EquivalentDose"/>.</summary>
      Sievert,
    }

    /// <summary>Dose equivalent, unit of sievert.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Equivalent_dose"/>
    public readonly record struct EquivalentDose
      : System.IComparable, System.IComparable<EquivalentDose>, System.IFormattable, IUnitValueQuantifiable<double, EquivalentDoseUnit>
    {
      private readonly double m_value;

      public EquivalentDose(double value, EquivalentDoseUnit unit = EquivalentDoseUnit.Sievert)
        => m_value = unit switch
        {
          EquivalentDoseUnit.Sievert => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators

      public static bool operator <(EquivalentDose a, EquivalentDose b) => a.CompareTo(b) < 0;
      public static bool operator <=(EquivalentDose a, EquivalentDose b) => a.CompareTo(b) <= 0;
      public static bool operator >(EquivalentDose a, EquivalentDose b) => a.CompareTo(b) > 0;
      public static bool operator >=(EquivalentDose a, EquivalentDose b) => a.CompareTo(b) >= 0;

      public static EquivalentDose operator -(EquivalentDose v) => new(-v.m_value);
      public static EquivalentDose operator +(EquivalentDose a, double b) => new(a.m_value + b);
      public static EquivalentDose operator +(EquivalentDose a, EquivalentDose b) => a + b.m_value;
      public static EquivalentDose operator /(EquivalentDose a, double b) => new(a.m_value / b);
      public static EquivalentDose operator /(EquivalentDose a, EquivalentDose b) => a / b.m_value;
      public static EquivalentDose operator *(EquivalentDose a, double b) => new(a.m_value * b);
      public static EquivalentDose operator *(EquivalentDose a, EquivalentDose b) => a * b.m_value;
      public static EquivalentDose operator %(EquivalentDose a, double b) => new(a.m_value % b);
      public static EquivalentDose operator %(EquivalentDose a, EquivalentDose b) => a % b.m_value;
      public static EquivalentDose operator -(EquivalentDose a, double b) => new(a.m_value - b);
      public static EquivalentDose operator -(EquivalentDose a, EquivalentDose b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is EquivalentDose o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(EquivalentDose other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(EquivalentDoseUnit.Sievert, format, formatProvider);

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="EquivalentDose.Value"/> property is in <see cref="EquivalentDoseUnit.Sievert"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public string GetUnitSymbol(EquivalentDoseUnit unit, bool preferUnicode, bool useFullName) => unit.GetUnitString(preferUnicode, useFullName);

      public double GetUnitValue(EquivalentDoseUnit unit)
        => unit switch
        {
          EquivalentDoseUnit.Sievert => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(EquivalentDoseUnit unit = EquivalentDoseUnit.Sievert, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
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
