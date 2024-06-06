namespace Flux
{
  public static partial class Fx
  {
    public static string GetUnitString(this Quantities.LuminousFluxUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.LuminousFluxUnit.Lumen => preferUnicode ? "\u33D0" : "lm",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum LuminousFluxUnit
    {
      /// <summary>This is the default unit for <see cref="LuminousFlux"/>.</summary>
      Lumen,
    }

    /// <summary>Luminous flux unit of lumen.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Luminous_flux"/>
    public readonly record struct LuminousFlux
      : System.IComparable, System.IComparable<LuminousFlux>, System.IFormattable, IUnitValueQuantifiable<double, LuminousFluxUnit>
    {
      private readonly double m_value;

      public LuminousFlux(double value, LuminousFluxUnit unit = LuminousFluxUnit.Lumen)
        => m_value = unit switch
        {
          LuminousFluxUnit.Lumen => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators

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

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(LuminousFluxUnit.Lumen, format, formatProvider);

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="LuminousFlux.Value"/> property is in <see cref="LuminousFluxUnit.Lumen"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public string GetUnitSymbol(LuminousFluxUnit unit, bool preferUnicode, bool useFullName) => unit.GetUnitString(preferUnicode, useFullName);

      public double GetUnitValue(LuminousFluxUnit unit)
        => unit switch
        {
          LuminousFluxUnit.Lumen => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(LuminousFluxUnit unit = LuminousFluxUnit.Lumen, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
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
