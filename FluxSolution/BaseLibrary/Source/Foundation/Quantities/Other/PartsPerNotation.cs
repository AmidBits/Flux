namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this PartsPerNotationUnit source, bool useFullName = false, bool preferUnicode = false)
      => useFullName ? source.ToString() : source switch
      {
        PartsPerNotationUnit.PartsPerQuadrillion => "ppq",
        PartsPerNotationUnit.PartsPerTrillion => "ppt",
        PartsPerNotationUnit.PartsPerBillion => "ppb",
        PartsPerNotationUnit.PartsPerMillion => preferUnicode ? "\u33d9" : "ppm",
        PartsPerNotationUnit.PerCentMille => "pcm",
        PartsPerNotationUnit.PerMyriad => "\u2030",
        PartsPerNotationUnit.PerMille => "\u2030",
        PartsPerNotationUnit.Percent => preferUnicode ? "\u0025" : "pct",
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
    : System.IComparable, System.IComparable<PartsPerNotation>, System.IConvertible, System.IEquatable<PartsPerNotation>, IUnitQuantifiable<double, PartsPerNotationUnit>
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

    #region Static methods
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static explicit operator double(PartsPerNotation v) => v.Value;

    [System.Diagnostics.Contracts.Pure] public static bool operator <(PartsPerNotation a, PartsPerNotation b) => a.CompareTo(b) < 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator <=(PartsPerNotation a, PartsPerNotation b) => a.CompareTo(b) <= 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >(PartsPerNotation a, PartsPerNotation b) => a.CompareTo(b) > 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >=(PartsPerNotation a, PartsPerNotation b) => a.CompareTo(b) >= 0;

    [System.Diagnostics.Contracts.Pure] public static bool operator ==(PartsPerNotation a, PartsPerNotation b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(PartsPerNotation a, PartsPerNotation b) => !a.Equals(b);

    [System.Diagnostics.Contracts.Pure] public static PartsPerNotation operator -(PartsPerNotation v) => new(-v.m_parts);
    [System.Diagnostics.Contracts.Pure] public static PartsPerNotation operator +(PartsPerNotation a, double b) => new(a.m_parts + b);
    [System.Diagnostics.Contracts.Pure] public static PartsPerNotation operator +(PartsPerNotation a, PartsPerNotation b) => a + b.m_parts;
    [System.Diagnostics.Contracts.Pure] public static PartsPerNotation operator /(PartsPerNotation a, double b) => new(a.m_parts / b);
    [System.Diagnostics.Contracts.Pure] public static PartsPerNotation operator /(PartsPerNotation a, PartsPerNotation b) => a / b.m_parts;
    [System.Diagnostics.Contracts.Pure] public static PartsPerNotation operator *(PartsPerNotation a, double b) => new(a.m_parts * b);
    [System.Diagnostics.Contracts.Pure] public static PartsPerNotation operator *(PartsPerNotation a, PartsPerNotation b) => a * b.m_parts;
    [System.Diagnostics.Contracts.Pure] public static PartsPerNotation operator %(PartsPerNotation a, double b) => new(a.m_parts % b);
    [System.Diagnostics.Contracts.Pure] public static PartsPerNotation operator %(PartsPerNotation a, PartsPerNotation b) => a % b.m_parts;
    [System.Diagnostics.Contracts.Pure] public static PartsPerNotation operator -(PartsPerNotation a, double b) => new(a.m_parts - b);
    [System.Diagnostics.Contracts.Pure] public static PartsPerNotation operator -(PartsPerNotation a, PartsPerNotation b) => a - b.m_parts;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<>
    [System.Diagnostics.Contracts.Pure] public int CompareTo(PartsPerNotation other) => m_parts.CompareTo(other.m_parts);
    // IComparable
    [System.Diagnostics.Contracts.Pure] public int CompareTo(object? other) => other is not null && other is PartsPerNotation o ? CompareTo(o) : -1;

    #region IConvertible
    [System.Diagnostics.Contracts.Pure] public System.TypeCode GetTypeCode() => System.TypeCode.Object;
    [System.Diagnostics.Contracts.Pure] public bool ToBoolean(System.IFormatProvider? provider) => Value != 0;
    [System.Diagnostics.Contracts.Pure] public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(Value);
    [System.Diagnostics.Contracts.Pure] public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(Value);
    [System.Diagnostics.Contracts.Pure] public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(Value);
    [System.Diagnostics.Contracts.Pure] public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(Value);
    [System.Diagnostics.Contracts.Pure] public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(Value);
    [System.Diagnostics.Contracts.Pure] public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(Value);
    [System.Diagnostics.Contracts.Pure] public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(Value);
    [System.Diagnostics.Contracts.Pure] public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(Value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(Value);
    [System.Diagnostics.Contracts.Pure] public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(Value);
    [System.Diagnostics.Contracts.Pure] public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", Value);
    [System.Diagnostics.Contracts.Pure] public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(Value, conversionType, provider);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(Value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(Value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(Value);
    #endregion IConvertible

    // IEquatable<>
    [System.Diagnostics.Contracts.Pure] public bool Equals(PartsPerNotation other) => m_parts == other.m_parts;

    //// IMetricOneQuantifiable
    //[System.Diagnostics.Contracts.Pure]
    //public string ToMetricOneString(MetricMultiplicativePrefix prefix, string? format = null, bool useFullName = false, bool preferUnicode = false)
    //  => $"{new MetricMultiplicative(m_parts, MetricMultiplicativePrefix.One).ToUnitString(prefix, format, useFullName, preferUnicode)}{DefaultUnit.GetUnitString(useFullName, preferUnicode)}";

    // IQuantifiable<>
    [System.Diagnostics.Contracts.Pure]
    public double Value
      => m_parts;
    [System.Diagnostics.Contracts.Pure]
    public string ToUnitString(PartsPerNotationUnit unit = DefaultUnit, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString()}";
    [System.Diagnostics.Contracts.Pure]
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
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is PartsPerNotation o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_parts);
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
