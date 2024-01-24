namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.LuminousFluxUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.LuminousFluxUnit.Lumen => preferUnicode ? "\u33D0" : "lm",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum LuminousFluxUnit
    {
      /// <summary>Lumen.</summary>
      Lumen,
    }

    /// <summary>Luminous flux unit of lumen.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Luminous_flux"/>
    public readonly record struct LuminousFlux
      : System.IComparable, System.IComparable<LuminousFlux>, IUnitValueQuantifiable<double, LuminousFluxUnit>
    {
      public const LuminousFluxUnit DefaultUnit = LuminousFluxUnit.Lumen;

      private readonly double m_value;

      public LuminousFlux(double value, LuminousFluxUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          LuminousFluxUnit.Lumen => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static explicit operator double(LuminousFlux v) => v.m_value;
      public static explicit operator LuminousFlux(double v) => new(v);

      public static bool operator <(LuminousFlux a, LuminousFlux b) => a.CompareTo(b) < 0;
      public static bool operator <=(LuminousFlux a, LuminousFlux b) => a.CompareTo(b) <= 0;
      public static bool operator >(LuminousFlux a, LuminousFlux b) => a.CompareTo(b) > 0;
      public static bool operator >=(LuminousFlux a, LuminousFlux b) => a.CompareTo(b) >= 0;

      public static LuminousFlux operator -(LuminousFlux v) => new(-v.m_value);
      public static LuminousFlux operator +(LuminousFlux a, double b) => new(a.m_value + b);
      public static LuminousFlux operator +(LuminousFlux a, LuminousFlux b) => a + b.m_value;
      public static LuminousFlux operator /(LuminousFlux a, double b) => new(a.m_value / b);
      public static LuminousFlux operator /(LuminousFlux a, LuminousFlux b) => a / b.m_value;
      public static LuminousFlux operator *(LuminousFlux a, double b) => new(a.m_value * b);
      public static LuminousFlux operator *(LuminousFlux a, LuminousFlux b) => a * b.m_value;
      public static LuminousFlux operator %(LuminousFlux a, double b) => new(a.m_value % b);
      public static LuminousFlux operator %(LuminousFlux a, LuminousFlux b) => a % b.m_value;
      public static LuminousFlux operator -(LuminousFlux a, double b) => new(a.m_value - b);
      public static LuminousFlux operator -(LuminousFlux a, LuminousFlux b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is LuminousFlux o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(LuminousFlux other) => m_value.CompareTo(other.m_value);

      // IQuantifiable<>
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => ToUnitValueString(DefaultUnit, format, preferUnicode, useFullName, culture);

      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(LuminousFluxUnit unit)
        => unit switch
        {
          LuminousFluxUnit.Lumen => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(LuminousFluxUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => $"{string.Format(culture, $"{{0{(format is null ? string.Empty : $":{format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
