namespace Flux
{
  public static partial class Fx
  {
    public static string GetUnitString(this Quantities.PartsPerNotationUnit source, bool preferUnicode = false, bool useFullName = false)
      => useFullName ? source.ToString() : source switch
      {
        Quantities.PartsPerNotationUnit.PartsPerQuadrillion => "ppq",
        Quantities.PartsPerNotationUnit.PartsPerTrillion => "ppt",
        Quantities.PartsPerNotationUnit.PartsPerBillion => "ppb",
        Quantities.PartsPerNotationUnit.PartsPerMillion => preferUnicode ? "\u33D9" : "ppm",
        Quantities.PartsPerNotationUnit.PerCentMille => "pcm",
        Quantities.PartsPerNotationUnit.PerMyriad => "\u2031",
        Quantities.PartsPerNotationUnit.PerMille => "\u2030",
        Quantities.PartsPerNotationUnit.Percent => "\u0025",
        Quantities.PartsPerNotationUnit.One => "pp1",
        _ => string.Empty,
      };

    /// <summary>Please note that not all units have an equivalent prefix.</summary>
    public static Quantities.MetricPrefix ToMetricMultiplicativePrefix(this Quantities.PartsPerNotationUnit unit)
      => (Quantities.MetricPrefix)(int)unit;
  }

  namespace Quantities
  {
    public enum PartsPerNotationUnit
    {
      /// <summary>This is the default unit for <see cref="PartsPerNotation"/>.</summary>
      Percent = 2,
      /// <summary>This represents a per one (i.e. so many to one).</summary>
      One = 1,
      /// <summary>Permille.</summary>
      PerMille = 3,
      /// <summary>Permyriad.</summary>
      PerMyriad = 4,
      /// <summary>Per cent mille, abbreviated "pcm".</summary>
      PerCentMille = 5,
      /// <summary>Abbreviated "ppm".</summary>
      PartsPerMillion = 6,
      /// <summary>Abbreviated "ppb".</summary>
      PartsPerBillion = 9,
      /// <summary>Abbreviated "ppt".</summary>
      PartsPerTrillion = 12,
      /// <summary>Abbreviated "ppq".</summary>
      PartsPerQuadrillion = 15,
    }

    /// <summary>
    /// <para>Parts per notation.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Parts-per_notation"/></para>
    /// </summary>
    /// <remarks>In science and engineering, the parts-per notation is a set of pseudo-units to describe small values of miscellaneous dimensionless quantities, e.g. mole fraction or mass fraction. Since these fractions are quantity-per-quantity measures, they are pure numbers with no associated units of measurement.</remarks>
    public readonly record struct PartsPerNotation
      : System.IComparable, System.IComparable<PartsPerNotation>, System.IFormattable, IUnitValueQuantifiable<double, PartsPerNotationUnit>
    {
      private readonly double m_parts;
      //private readonly PartsPerNotationUnit m_unit;

      /// <summary>Creates a new instance of this type.</summary>
      /// <param name="parts">The parts in parts per notation.</param>
      /// <param name="unit">The notation in parts per notation.</param>
      public PartsPerNotation(double parts, PartsPerNotationUnit unit = PartsPerNotationUnit.Percent)
      {
        m_parts = unit switch
        {
          PartsPerNotationUnit.One => parts,
          PartsPerNotationUnit.Percent => parts / 1e2,
          PartsPerNotationUnit.PerMille => parts / 1e3,
          PartsPerNotationUnit.PerMyriad => parts / 1e4,
          PartsPerNotationUnit.PerCentMille => parts / 1e5,
          PartsPerNotationUnit.PartsPerMillion => parts / 1e6,
          PartsPerNotationUnit.PartsPerBillion => parts / 1e9,
          PartsPerNotationUnit.PartsPerTrillion => parts / 1e12,
          PartsPerNotationUnit.PartsPerQuadrillion => parts / 1e15,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

        //m_unit = unit;
      }

      #region Static methods
      #endregion Static methods

      #region Overloaded operators

      public static bool operator <(PartsPerNotation a, PartsPerNotation b) => a.CompareTo(b) < 0;
      public static bool operator <=(PartsPerNotation a, PartsPerNotation b) => a.CompareTo(b) <= 0;
      public static bool operator >(PartsPerNotation a, PartsPerNotation b) => a.CompareTo(b) > 0;
      public static bool operator >=(PartsPerNotation a, PartsPerNotation b) => a.CompareTo(b) >= 0;

      public static PartsPerNotation operator -(PartsPerNotation v) => new(-v.m_parts);
      public static PartsPerNotation operator +(PartsPerNotation a, double b) => new(a.m_parts + b);
      public static PartsPerNotation operator +(PartsPerNotation a, PartsPerNotation b) => a + b.m_parts;
      public static PartsPerNotation operator /(PartsPerNotation a, double b) => new(a.m_parts / b);
      public static PartsPerNotation operator /(PartsPerNotation a, PartsPerNotation b) => a / b.m_parts;
      public static PartsPerNotation operator *(PartsPerNotation a, double b) => new(a.m_parts * b);
      public static PartsPerNotation operator *(PartsPerNotation a, PartsPerNotation b) => a * b.m_parts;
      public static PartsPerNotation operator %(PartsPerNotation a, double b) => new(a.m_parts % b);
      public static PartsPerNotation operator %(PartsPerNotation a, PartsPerNotation b) => a % b.m_parts;
      public static PartsPerNotation operator -(PartsPerNotation a, double b) => new(a.m_parts - b);
      public static PartsPerNotation operator -(PartsPerNotation a, PartsPerNotation b) => a - b.m_parts;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is PartsPerNotation o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(PartsPerNotation other) => m_parts.CompareTo(other.m_parts);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(PartsPerNotationUnit.Percent, format, formatProvider);

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="PartsPerNotation.Value"/> property is in <see cref="PartsPerNotationUnit.Percent"/>.</para>
      /// </summary>
      public double Value => m_parts;

      // IUnitQuantifiable<>
      public string GetUnitSymbol(PartsPerNotationUnit unit, bool preferUnicode, bool useFullName) => unit.GetUnitString(preferUnicode, useFullName);

      public double GetUnitValue(PartsPerNotationUnit unit)
        => unit switch
        {
          PartsPerNotationUnit.One => m_parts,
          PartsPerNotationUnit.Percent => m_parts * 1e2,
          PartsPerNotationUnit.PerMille => m_parts * 1e3,
          PartsPerNotationUnit.PerMyriad => m_parts * 1e4,
          PartsPerNotationUnit.PerCentMille => m_parts * 1e5,
          PartsPerNotationUnit.PartsPerMillion => m_parts * 1e6,
          PartsPerNotationUnit.PartsPerBillion => m_parts * 1e9,
          PartsPerNotationUnit.PartsPerTrillion => m_parts * 1e12,
          PartsPerNotationUnit.PartsPerQuadrillion => m_parts * 1e15,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(PartsPerNotationUnit unit = PartsPerNotationUnit.One, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
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