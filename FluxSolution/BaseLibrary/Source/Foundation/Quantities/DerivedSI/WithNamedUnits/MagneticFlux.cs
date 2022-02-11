namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this MagneticFluxUnit unit, bool useNameInsteadOfSymbol = false, bool useUnicodeIfAvailable = false)
      => useNameInsteadOfSymbol ? unit.ToString() : unit switch
      {
        MagneticFluxUnit.Weber => useUnicodeIfAvailable ? "\u33dd" : "Wb",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum MagneticFluxUnit
  {
    /// <summary>Weber.</summary>
    Weber,
  }

  /// <summary>Magnetic flux unit of weber.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Magnetic_flux"/>
  public struct MagneticFlux
    : System.IComparable<MagneticFlux>, System.IConvertible, System.IEquatable<MagneticFlux>, ISiDerivedUnitQuantifiable<double, MagneticFluxUnit>
  {
    public const MagneticFluxUnit DefaultUnit = MagneticFluxUnit.Weber;

    private readonly double m_value;

    public MagneticFlux(double value, MagneticFluxUnit unit = MagneticFluxUnit.Weber)
      => m_value = unit switch
      {
        MagneticFluxUnit.Weber => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public string ToUnitString(MagneticFluxUnit unit = DefaultUnit, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{string.Format($"{{0:{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString()}";
    public double ToUnitValue(MagneticFluxUnit unit = MagneticFluxUnit.Weber)
      => unit switch
      {
        MagneticFluxUnit.Weber => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    public static explicit operator double(MagneticFlux v)
      => v.m_value;
    public static explicit operator MagneticFlux(double v)
      => new(v);

    public static bool operator <(MagneticFlux a, MagneticFlux b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(MagneticFlux a, MagneticFlux b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(MagneticFlux a, MagneticFlux b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(MagneticFlux a, MagneticFlux b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(MagneticFlux a, MagneticFlux b)
      => a.Equals(b);
    public static bool operator !=(MagneticFlux a, MagneticFlux b)
      => !a.Equals(b);

    public static MagneticFlux operator -(MagneticFlux v)
      => new(-v.m_value);
    public static MagneticFlux operator +(MagneticFlux a, double b)
      => new(a.m_value + b);
    public static MagneticFlux operator +(MagneticFlux a, MagneticFlux b)
      => a + b.m_value;
    public static MagneticFlux operator /(MagneticFlux a, double b)
      => new(a.m_value / b);
    public static MagneticFlux operator /(MagneticFlux a, MagneticFlux b)
      => a / b.m_value;
    public static MagneticFlux operator *(MagneticFlux a, double b)
      => new(a.m_value * b);
    public static MagneticFlux operator *(MagneticFlux a, MagneticFlux b)
      => a * b.m_value;
    public static MagneticFlux operator %(MagneticFlux a, double b)
      => new(a.m_value % b);
    public static MagneticFlux operator %(MagneticFlux a, MagneticFlux b)
      => a % b.m_value;
    public static MagneticFlux operator -(MagneticFlux a, double b)
      => new(a.m_value - b);
    public static MagneticFlux operator -(MagneticFlux a, MagneticFlux b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(MagneticFlux other)
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
    public bool Equals(MagneticFlux other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is MagneticFlux o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
