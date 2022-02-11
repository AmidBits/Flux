namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this PartsPerNotationUnit source, bool useNameInsteadOfSymbol = false, bool useUnicodeIfAvailable = false)
      => useNameInsteadOfSymbol ? source.ToString() : source switch
      {
        PartsPerNotationUnit.PartsPerQuadrillion => "ppq",
        PartsPerNotationUnit.PartsPerTrillion => "ppt",
        PartsPerNotationUnit.PartsPerBillion => "ppb",
        PartsPerNotationUnit.PartsPerMillion => useUnicodeIfAvailable ? "\u33d9" : "ppm",
        PartsPerNotationUnit.PerCentMille => "pcm",
        PartsPerNotationUnit.PerMyriad => "\u2030",
        PartsPerNotationUnit.PerMille => "\u2030",
        PartsPerNotationUnit.Percent => useUnicodeIfAvailable ? "\u0025" : "pct",
        PartsPerNotationUnit.One => "pp1",
        _ => string.Empty,
      };

    public static MetricMultiplicativePrefix ToMetricMultiplicativePrefix(this PartsPerNotationUnit unit)
    {
      return unit switch
      {
        PartsPerNotationUnit.Percent => MetricMultiplicativePrefix.Hecto,
        PartsPerNotationUnit.PerMille => MetricMultiplicativePrefix.Kilo,
        PartsPerNotationUnit.PartsPerMillion => MetricMultiplicativePrefix.Mega,
        PartsPerNotationUnit.PartsPerBillion => MetricMultiplicativePrefix.Giga,
        PartsPerNotationUnit.PartsPerTrillion => MetricMultiplicativePrefix.Tera,
        PartsPerNotationUnit.PartsPerQuadrillion => MetricMultiplicativePrefix.Peta,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
    }
  }

  public enum PartsPerNotationUnit
  {
    One,
    /// <summary>Percent. This is also the actual Unicode char value of the notation unit.</summary>
    Percent,
    /// <summary>Per mille. This is also the actual Unicode char value of the notation unit.</summary>
    PerMille,
    /// <summary>Permyriad. This is also the actual Unicode char value of the notation unit.</summary>
    PerMyriad,
    /// <summary>Per cent mille, abbreviated "pcm".</summary>
    PerCentMille,
    /// <summary>Abbreviated "ppm".</summary>
    PartsPerMillion,
    /// <summary>Abbreviated "ppb".</summary>
    PartsPerBillion,
    /// <summary>Abbreviated "ppt".</summary>
    PartsPerTrillion,
    /// <summary>Abbreviated "ppq".</summary>
    PartsPerQuadrillion,
  }

  /// <summary>Parts per notation. In science and engineering, the parts-per notation is a set of pseudo-units to describe small values of miscellaneous dimensionless quantities, e.g. mole fraction or mass fraction. Since these fractions are quantity-per-quantity measures, they are pure numbers with no associated units of measurement.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Parts-per_notation"/>
  public struct PartsPerNotation
    : System.IComparable<PartsPerNotation>, System.IConvertible, System.IEquatable<PartsPerNotation>, IQuantifiable<double>
  {
    public const PartsPerNotationUnit DefaultUnit = PartsPerNotationUnit.Percent;

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

    public double Value
      => m_parts;

    public string ToUnitString(PartsPerNotationUnit unit = DefaultUnit, string? format = null)
      => $"{string.Format($"{{0:{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString()}";
    public double ToUnitValue(PartsPerNotationUnit unit = DefaultUnit)
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
