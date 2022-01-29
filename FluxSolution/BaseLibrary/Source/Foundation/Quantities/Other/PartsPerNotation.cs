namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static PartsPerNotation Create(this PartsPerNotationUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this PartsPerNotationUnit unit)
      => unit switch
      {
        PartsPerNotationUnit.Quadrillion => "ppq",
        PartsPerNotationUnit.Trillion => "ppt",
        PartsPerNotationUnit.Billion => "ppb",
        PartsPerNotationUnit.Million => "ppm",
        PartsPerNotationUnit.HundredThousand => "pcm",
        PartsPerNotationUnit.TenThousand => ((char)unit).ToString(),
        PartsPerNotationUnit.Thousand => ((char)unit).ToString(),
        PartsPerNotationUnit.Hundred => ((char)unit).ToString(),
        PartsPerNotationUnit.One => "pp1",
        _ => string.Empty,
      };

    public static MetricMultiplicativeUnit ToMetricPrefixUnit(this PartsPerNotationUnit unit)
    {
      return unit switch
      {
        PartsPerNotationUnit.Hundred => MetricMultiplicativeUnit.Hecto,
        PartsPerNotationUnit.Thousand => MetricMultiplicativeUnit.Kilo,
        PartsPerNotationUnit.Million => MetricMultiplicativeUnit.Mega,
        PartsPerNotationUnit.Billion => MetricMultiplicativeUnit.Giga,
        PartsPerNotationUnit.Trillion => MetricMultiplicativeUnit.Tera,
        PartsPerNotationUnit.Quadrillion => MetricMultiplicativeUnit.Peta,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
    }
  }

  public enum PartsPerNotationUnit
  {
    One,
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
    : System.IComparable<PartsPerNotation>, System.IConvertible, System.IEquatable<PartsPerNotation>, IValueGeneralizedUnit<double>
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
        PartsPerNotationUnit.One => parts,
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
      => $"{string.Format($"{{0:{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitSymbol()}";
    public double ToUnitValue(PartsPerNotationUnit unit = DefaultUnit)
      => unit switch
      {
        PartsPerNotationUnit.One => m_parts,
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

    #region IConvertible
    public System.TypeCode GetTypeCode() => System.TypeCode.Object;
    public bool ToBoolean(System.IFormatProvider? provider) => Value != 0;
    public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(Value);
    public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(Value);
    public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(Value);
    public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(Value);
    public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(Value);
    public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(Value);
    public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(Value);
    public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(Value);
    [System.CLSCompliant(false)] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(Value);
    public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(Value);
    public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", Value);
    public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(Value, conversionType, provider);
    [System.CLSCompliant(false)] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(Value);
    [System.CLSCompliant(false)] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(Value);
    [System.CLSCompliant(false)] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(Value);
    #endregion IConvertible

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
