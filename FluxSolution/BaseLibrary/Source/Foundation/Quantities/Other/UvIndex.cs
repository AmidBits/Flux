namespace Flux
{
  /// <summary>UV index, unit of itself.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Ultraviolet_index"/>
  public struct UvIndex
    : System.IComparable<UvIndex>, System.IConvertible, System.IEquatable<UvIndex>, IQuantifiable<double>
  {
    private readonly double m_value;

    public UvIndex(double value)
      => m_value = value > 0 ? value : throw new System.ArgumentOutOfRangeException(nameof(value));

    public double Value
      => m_value;

    public string ToString(string? format = null)
      => string.Format($"UV Index {{0:{format ?? "N1"}}}", m_value);

    #region Static methods
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(UvIndex v)
      => v.m_value;
    public static explicit operator UvIndex(double v)
      => new(v);

    public static bool operator <(UvIndex a, UvIndex b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(UvIndex a, UvIndex b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(UvIndex a, UvIndex b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(UvIndex a, UvIndex b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(UvIndex a, UvIndex b)
      => a.Equals(b);
    public static bool operator !=(UvIndex a, UvIndex b)
      => !a.Equals(b);

    public static UvIndex operator -(UvIndex v)
      => new(-v.m_value);
    public static UvIndex operator +(UvIndex a, double b)
      => new(a.m_value + b);
    public static UvIndex operator +(UvIndex a, UvIndex b)
      => a + b.m_value;
    public static UvIndex operator /(UvIndex a, double b)
      => new(a.m_value / b);
    public static UvIndex operator /(UvIndex a, UvIndex b)
      => a / b.m_value;
    public static UvIndex operator *(UvIndex a, double b)
      => new(a.m_value * b);
    public static UvIndex operator *(UvIndex a, UvIndex b)
      => a * b.m_value;
    public static UvIndex operator %(UvIndex a, double b)
      => new(a.m_value % b);
    public static UvIndex operator %(UvIndex a, UvIndex b)
      => a % b.m_value;
    public static UvIndex operator -(UvIndex a, double b)
      => new(a.m_value - b);
    public static UvIndex operator -(UvIndex a, UvIndex b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(UvIndex other)
      => m_value.CompareTo(other.m_value);

    #region IConvertible
    [System.Diagnostics.Contracts.Pure] public System.TypeCode GetTypeCode() => System.TypeCode.Object;
    [System.Diagnostics.Contracts.Pure] public bool ToBoolean(System.IFormatProvider? provider) => m_value != 0;
    [System.Diagnostics.Contracts.Pure] public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(m_value);
    [System.Diagnostics.Contracts.Pure] public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(m_value);
    [System.Diagnostics.Contracts.Pure] public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(m_value);
    [System.Diagnostics.Contracts.Pure] public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(m_value);
    [System.Diagnostics.Contracts.Pure] public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(m_value);
    [System.Diagnostics.Contracts.Pure] public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(m_value);
    [System.Diagnostics.Contracts.Pure] public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(m_value);
    [System.Diagnostics.Contracts.Pure] public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(m_value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(m_value);
    [System.Diagnostics.Contracts.Pure] public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(m_value);
    [System.Diagnostics.Contracts.Pure] public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", m_value);
    [System.Diagnostics.Contracts.Pure] public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(m_value, conversionType, provider);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(m_value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(m_value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(m_value);
    #endregion IConvertible

    // IEquatable
    public bool Equals(UvIndex other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is UvIndex o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {m_value} }}";
    #endregion Object overrides
  }
}
