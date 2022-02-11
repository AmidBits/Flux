namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this EnplethyUnit unit, bool useNameInstead = false, bool useUnicodeIfAvailable = false)
      => useNameInstead ? unit.ToString() : unit switch
      {
        EnplethyUnit.Mole => useUnicodeIfAvailable ? "\u33d6" : "mol",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum EnplethyUnit
  {
    Mole,
  }

  /// <summary>Enplethy, or amount of substance. SI unit of mole. This is a base quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Amount_of_substance"/>
  public struct Enplethy
    : System.IComparable<Enplethy>, System.IConvertible, System.IEquatable<Enplethy>, ISiBaseUnitQuantifiable<double, EnplethyUnit>
  {
    public const EnplethyUnit DefaultUnit = EnplethyUnit.Mole;

    // The unit of the Avagadro constant is the reciprocal mole, i.e. "per" mole.
    public static Enplethy AvagadroConstant
      => new(6.02214076e23);

    private readonly double m_value;

    public Enplethy(double value, EnplethyUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        EnplethyUnit.Mole => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public string ToUnitString(EnplethyUnit unit = DefaultUnit, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{string.Format($"{{0:{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString()}";
    public double ToUnitValue(EnplethyUnit unit = DefaultUnit)
      => unit switch
      {
        EnplethyUnit.Mole => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Enplethy v)
      => v.m_value;
    public static explicit operator Enplethy(double v)
      => new(v);

    public static bool operator <(Enplethy a, Enplethy b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Enplethy a, Enplethy b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Enplethy a, Enplethy b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Enplethy a, Enplethy b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Enplethy a, Enplethy b)
      => a.Equals(b);
    public static bool operator !=(Enplethy a, Enplethy b)
      => !a.Equals(b);

    public static Enplethy operator -(Enplethy v)
      => new(-v.Value);
    public static Enplethy operator +(Enplethy a, double b)
      => new(a.m_value + b);
    public static Enplethy operator +(Enplethy a, Enplethy b)
      => a + b.m_value;
    public static Enplethy operator /(Enplethy a, double b)
      => new(a.m_value / b);
    public static Enplethy operator /(Enplethy a, Enplethy b)
      => a / b.m_value;
    public static Enplethy operator *(Enplethy a, double b)
      => new(a.m_value * b);
    public static Enplethy operator *(Enplethy a, Enplethy b)
      => a * b.m_value;
    public static Enplethy operator %(Enplethy a, double b)
      => new(a.m_value % b);
    public static Enplethy operator %(Enplethy a, Enplethy b)
      => a % b.m_value;
    public static Enplethy operator -(Enplethy a, double b)
      => new(a.m_value - b);
    public static Enplethy operator -(Enplethy a, Enplethy b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Enplethy other)
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
    public bool Equals(Enplethy other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Enplethy o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
