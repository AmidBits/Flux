namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.EquivalentDoseUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.EquivalentDoseUnit.Sievert => preferUnicode ? "\u33DC" : "Sv",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum EquivalentDoseUnit
    {
      /// <summary>Sievert.</summary>
      Sievert,
    }

    /// <summary>Dose equivalent, unit of sievert.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Equivalent_dose"/>
    public readonly record struct EquivalentDose
      : System.IComparable, System.IComparable<EquivalentDose>, IUnitValueQuantifiable<double, EquivalentDoseUnit>
    {
      public const EquivalentDoseUnit DefaultUnit = EquivalentDoseUnit.Sievert;

      private readonly double m_value;

      public EquivalentDose(double value, EquivalentDoseUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          EquivalentDoseUnit.Sievert => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static explicit operator double(EquivalentDose v) => v.m_value;
      public static explicit operator EquivalentDose(double v) => new(v);

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
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => ToUnitValueString(DefaultUnit, format, preferUnicode, useFullName, culture);

      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(EquivalentDoseUnit unit)
        => unit switch
        {
          EquivalentDoseUnit.Sievert => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(EquivalentDoseUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => $"{string.Format(culture, $"{{0{(format is null ? string.Empty : $":{format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
