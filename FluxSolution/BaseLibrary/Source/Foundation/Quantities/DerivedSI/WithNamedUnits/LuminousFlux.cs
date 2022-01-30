namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitSymbol(this LuminousFluxUnit unit)
      => unit switch
      {
        LuminousFluxUnit.Lumen => "lm",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum LuminousFluxUnit
  {
    Lumen,
  }

  /// <summary>Luminous flux unit of lumen.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Luminous_flux"/>
  public struct LuminousFlux
    : System.IComparable<LuminousFlux>, System.IConvertible, System.IEquatable<LuminousFlux>, IValueSiDerivedUnit<double>
  {
    public const LuminousFluxUnit DefaultUnit = LuminousFluxUnit.Lumen;

    private readonly double m_value;

    public LuminousFlux(double value, LuminousFluxUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        LuminousFluxUnit.Lumen => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public string ToUnitString(LuminousFluxUnit unit = DefaultUnit, string? format = null)
      => $"{string.Format($"{{0:{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitSymbol()}";
    public double ToUnitValue(LuminousFluxUnit unit = DefaultUnit)
      => unit switch
      {
        LuminousFluxUnit.Lumen => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    public static explicit operator double(LuminousFlux v)
      => v.m_value;
    public static explicit operator LuminousFlux(double v)
      => new(v);

    public static bool operator <(LuminousFlux a, LuminousFlux b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(LuminousFlux a, LuminousFlux b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(LuminousFlux a, LuminousFlux b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(LuminousFlux a, LuminousFlux b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(LuminousFlux a, LuminousFlux b)
      => a.Equals(b);
    public static bool operator !=(LuminousFlux a, LuminousFlux b)
      => !a.Equals(b);

    public static LuminousFlux operator -(LuminousFlux v)
      => new(-v.m_value);
    public static LuminousFlux operator +(LuminousFlux a, double b)
      => new(a.m_value + b);
    public static LuminousFlux operator +(LuminousFlux a, LuminousFlux b)
      => a + b.m_value;
    public static LuminousFlux operator /(LuminousFlux a, double b)
      => new(a.m_value / b);
    public static LuminousFlux operator /(LuminousFlux a, LuminousFlux b)
      => a / b.m_value;
    public static LuminousFlux operator *(LuminousFlux a, double b)
      => new(a.m_value * b);
    public static LuminousFlux operator *(LuminousFlux a, LuminousFlux b)
      => a * b.m_value;
    public static LuminousFlux operator %(LuminousFlux a, double b)
      => new(a.m_value % b);
    public static LuminousFlux operator %(LuminousFlux a, LuminousFlux b)
      => a % b.m_value;
    public static LuminousFlux operator -(LuminousFlux a, double b)
      => new(a.m_value - b);
    public static LuminousFlux operator -(LuminousFlux a, LuminousFlux b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(LuminousFlux other)
      => m_value.CompareTo(other.m_value);

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
    public bool Equals(LuminousFlux other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is LuminousFlux o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {m_value} lm }}";
    #endregion Object overrides
  }
}
