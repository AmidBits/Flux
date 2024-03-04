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
        => ToUnitValueString(LuminousFluxUnit.Lumen, UnitValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="LuminousFlux.Value"/> property is in <see cref="LuminousFluxUnit.Lumen"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(LuminousFluxUnit unit)
        => unit switch
        {
          LuminousFluxUnit.Lumen => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(LuminousFluxUnit unit, string? format, System.IFormatProvider? formatProvider, bool preferUnicode, UnicodeSpacing unicodeSpacing, bool useFullName)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unicodeSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(preferUnicode, useFullName));
        return sb.ToString();
      }

      public string ToUnitValueString(LuminousFluxUnit unit, UnitValueStringOptions options = default)
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
