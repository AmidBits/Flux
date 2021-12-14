namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static PartsPerNotation Create(this PartsPerNotationUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this PartsPerNotationUnit unit)
      => unit switch
      {
        PartsPerNotationUnit.Quadrillion => @" ppq",
        PartsPerNotationUnit.Trillion => @" ppt",
        PartsPerNotationUnit.Billion => @" ppb",
        PartsPerNotationUnit.Million => @" ppm",
        PartsPerNotationUnit.HundredThousand => @" pcm",
        PartsPerNotationUnit.TenThousand => ((char)unit).ToString(),
        PartsPerNotationUnit.Thousand => ((char)unit).ToString(),
        PartsPerNotationUnit.Hundred => ((char)unit).ToString(),
        _ => string.Empty,
      };
  }

  public enum PartsPerNotationUnit
  {
    /// <summary>Percent. This is also the actual Unicode char value of the notation unit.</summary>
    Hundred = '\u0025',
    /// <summary>Per mille. This is also the actual Unicode char value of the notation unit.</summary>
    Thousand = '\u2030',
    /// <summary>Permyriad. This is also the actual Unicode char value of the notation unit.</summary>
    TenThousand = '\u2031',
    /// <summary>Per cent mille, abbreviated "pcm".</summary>
    HundredThousand,
    /// <summary>Abbreviated "ppm".</summary>
    Million,
    /// <summary>Abbreviated "ppb".</summary>
    Billion,
    /// <summary>Abbreviated "ppt".</summary>
    Trillion,
    /// <summary>Abbreviated "ppq".</summary>
    Quadrillion,
  }

  /// <summary>Parts per notation. In science and engineering, the parts-per notation is a set of pseudo-units to describe small values of miscellaneous dimensionless quantities, e.g. mole fraction or mass fraction. Since these fractions are quantity-per-quantity measures, they are pure numbers with no associated units of measurement.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Parts-per_notation"/>
  public struct PartsPerNotation
    : System.IComparable<PartsPerNotation>, System.IEquatable<PartsPerNotation>, IValueGeneralizedUnit<double>
  {
    public const PartsPerNotationUnit DefaultUnit = PartsPerNotationUnit.Hundred;

    private readonly double m_parts;
    //private readonly PartsPerNotationUnit m_unit;

    /// <summary>Creates a new instance of this type.</summary>
    /// <param name="parts">The parts in parts per notation.</param>
    /// <param name="unit">The notation in parts per notation.</param>
    public PartsPerNotation(double parts, PartsPerNotationUnit unit = DefaultUnit)
    {
      m_parts = unit switch
      {
        PartsPerNotationUnit.Hundred => parts / 1e2,
        PartsPerNotationUnit.Thousand => parts / 1e3,
        PartsPerNotationUnit.TenThousand => parts / 1e4,
        PartsPerNotationUnit.HundredThousand => parts / 1e5,
        PartsPerNotationUnit.Million => parts / 1e6,
        PartsPerNotationUnit.Billion => parts / 1e9,
        PartsPerNotationUnit.Trillion => parts / 1e12,
        PartsPerNotationUnit.Quadrillion => parts / 1e15,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

      //m_unit = unit;
    }

    public double Value
      => m_parts;

    public string ToUnitString(PartsPerNotationUnit unit = DefaultUnit, string? format = null)
      => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
    public double ToUnitValue(PartsPerNotationUnit unit = DefaultUnit)
      => unit switch
      {
        PartsPerNotationUnit.Hundred => m_parts * 1e2,
        PartsPerNotationUnit.Thousand => m_parts * 1e3,
        PartsPerNotationUnit.TenThousand => m_parts * 1e4,
        PartsPerNotationUnit.HundredThousand => m_parts * 1e5,
        PartsPerNotationUnit.Million => m_parts * 1e6,
        PartsPerNotationUnit.Billion => m_parts * 1e9,
        PartsPerNotationUnit.Trillion => m_parts * 1e12,
        PartsPerNotationUnit.Quadrillion => m_parts * 1e15,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(PartsPerNotation v)
      => v.Value;

    public static bool operator <(PartsPerNotation a, PartsPerNotation b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(PartsPerNotation a, PartsPerNotation b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(PartsPerNotation a, PartsPerNotation b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(PartsPerNotation a, PartsPerNotation b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(PartsPerNotation a, PartsPerNotation b)
      => a.Equals(b);
    public static bool operator !=(PartsPerNotation a, PartsPerNotation b)
      => !a.Equals(b);

    public static PartsPerNotation operator -(PartsPerNotation v)
      => new(-v.m_parts);
    public static PartsPerNotation operator +(PartsPerNotation a, double b)
      => new(a.m_parts + b);
    public static PartsPerNotation operator +(PartsPerNotation a, PartsPerNotation b)
      => a + b.m_parts;
    public static PartsPerNotation operator /(PartsPerNotation a, double b)
      => new(a.m_parts / b);
    public static PartsPerNotation operator /(PartsPerNotation a, PartsPerNotation b)
      => a / b.m_parts;
    public static PartsPerNotation operator *(PartsPerNotation a, double b)
      => new(a.m_parts * b);
    public static PartsPerNotation operator *(PartsPerNotation a, PartsPerNotation b)
      => a * b.m_parts;
    public static PartsPerNotation operator %(PartsPerNotation a, double b)
      => new(a.m_parts % b);
    public static PartsPerNotation operator %(PartsPerNotation a, PartsPerNotation b)
      => a % b.m_parts;
    public static PartsPerNotation operator -(PartsPerNotation a, double b)
      => new(a.m_parts - b);
    public static PartsPerNotation operator -(PartsPerNotation a, PartsPerNotation b)
      => a - b.m_parts;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(PartsPerNotation other)
      => m_parts.CompareTo(other.m_parts);

    // IEquatable
    public bool Equals(PartsPerNotation other)
      => m_parts == other.m_parts;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is PartsPerNotation o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_parts);
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
