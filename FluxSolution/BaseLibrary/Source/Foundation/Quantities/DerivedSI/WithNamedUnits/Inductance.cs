namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this InductanceUnit unit, bool useNameInsteadOfSymbol = false, bool useUnicodeIfAvailable = false)
      => useNameInsteadOfSymbol ? unit.ToString() : unit switch
      {
        InductanceUnit.Henry => "H",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum InductanceUnit
  {
    /// <summary>Henry.</summary>
    Henry,
  }

  /// <summary>Electrical inductance unit of Henry.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Inductance"/>
  public struct Inductance
    : System.IComparable<Inductance>, System.IConvertible, System.IEquatable<Inductance>, IValueSiDerivedUnit<double>
  {
    public const InductanceUnit DefaultUnit = InductanceUnit.Henry;

    private readonly double m_value;

    public Inductance(double value, InductanceUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        InductanceUnit.Henry => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public string ToUnitString(InductanceUnit unit = DefaultUnit, string? format = null)
      => $"{string.Format($"{{0:{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString()}";
    public double ToUnitValue(InductanceUnit unit = DefaultUnit)
      => unit switch
      {
        InductanceUnit.Henry => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    public static explicit operator double(Inductance v)
      => v.m_value;
    public static explicit operator Inductance(double v)
      => new(v);

    public static bool operator <(Inductance a, Inductance b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Inductance a, Inductance b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Inductance a, Inductance b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Inductance a, Inductance b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Inductance a, Inductance b)
      => a.Equals(b);
    public static bool operator !=(Inductance a, Inductance b)
      => !a.Equals(b);

    public static Inductance operator -(Inductance v)
      => new(-v.m_value);
    public static Inductance operator +(Inductance a, double b)
      => new(a.m_value + b);
    public static Inductance operator +(Inductance a, Inductance b)
      => a + b.m_value;
    public static Inductance operator /(Inductance a, double b)
      => new(a.m_value / b);
    public static Inductance operator /(Inductance a, Inductance b)
      => a / b.m_value;
    public static Inductance operator *(Inductance a, double b)
      => new(a.m_value * b);
    public static Inductance operator *(Inductance a, Inductance b)
      => a * b.m_value;
    public static Inductance operator %(Inductance a, double b)
      => new(a.m_value % b);
    public static Inductance operator %(Inductance a, Inductance b)
      => a % b.m_value;
    public static Inductance operator -(Inductance a, double b)
      => new(a.m_value - b);
    public static Inductance operator -(Inductance a, Inductance b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Inductance other)
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
    public bool Equals(Inductance other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Inductance o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
