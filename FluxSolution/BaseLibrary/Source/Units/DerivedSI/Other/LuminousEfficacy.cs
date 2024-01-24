namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.LuminousEfficacyUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.LuminousEfficacyUnit.LumensPerWatt => "lm/W",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum LuminousEfficacyUnit
    {
      LumensPerWatt,
    }

    /// <summary>Torque unit of newton meter.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Torque"/>
    public readonly record struct LuminousEfficacy
      : System.IComparable, System.IComparable<LuminousEfficacy>, System.IFormattable, IUnitValueQuantifiable<double, LuminousEfficacyUnit>
    {
      public const LuminousEfficacyUnit DefaultUnit = LuminousEfficacyUnit.LumensPerWatt;

      public static readonly LuminousEfficacy LuminousEfficacyOf540THzRadiation = new(683);

      private readonly double m_value;

      public LuminousEfficacy(double value, LuminousEfficacyUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          LuminousEfficacyUnit.LumensPerWatt => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods

      public static LuminousEfficacy From(Energy energy, Angle angle)
        => new(energy.Value / angle.Value);
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(LuminousEfficacy v) => v.m_value;
      public static explicit operator LuminousEfficacy(double v) => new(v);

      public static bool operator <(LuminousEfficacy a, LuminousEfficacy b) => a.CompareTo(b) < 0;
      public static bool operator <=(LuminousEfficacy a, LuminousEfficacy b) => a.CompareTo(b) <= 0;
      public static bool operator >(LuminousEfficacy a, LuminousEfficacy b) => a.CompareTo(b) > 0;
      public static bool operator >=(LuminousEfficacy a, LuminousEfficacy b) => a.CompareTo(b) >= 0;

      public static LuminousEfficacy operator -(LuminousEfficacy v) => new(-v.m_value);
      public static LuminousEfficacy operator +(LuminousEfficacy a, double b) => new(a.m_value + b);
      public static LuminousEfficacy operator +(LuminousEfficacy a, LuminousEfficacy b) => a + b.m_value;
      public static LuminousEfficacy operator /(LuminousEfficacy a, double b) => new(a.m_value / b);
      public static LuminousEfficacy operator /(LuminousEfficacy a, LuminousEfficacy b) => a / b.m_value;
      public static LuminousEfficacy operator *(LuminousEfficacy a, double b) => new(a.m_value * b);
      public static LuminousEfficacy operator *(LuminousEfficacy a, LuminousEfficacy b) => a * b.m_value;
      public static LuminousEfficacy operator %(LuminousEfficacy a, double b) => new(a.m_value % b);
      public static LuminousEfficacy operator %(LuminousEfficacy a, LuminousEfficacy b) => a % b.m_value;
      public static LuminousEfficacy operator -(LuminousEfficacy a, double b) => new(a.m_value - b);
      public static LuminousEfficacy operator -(LuminousEfficacy a, LuminousEfficacy b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is LuminousEfficacy o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(LuminousEfficacy other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => ToUnitValueString(DefaultUnit, format, preferUnicode, useFullName, culture);

      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(LuminousEfficacyUnit unit)
        => unit switch
        {
          LuminousEfficacyUnit.LumensPerWatt => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(LuminousEfficacyUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => $"{string.Format(culture, $"{{0{(format is null ? string.Empty : $":{format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
