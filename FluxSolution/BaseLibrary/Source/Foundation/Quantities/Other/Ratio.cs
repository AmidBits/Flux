namespace Flux
{
  public enum RatioFormat
  {
    AtoB,
    AcolonB,
  }

  /// <summary>A ratio indicates how many times one number contains another. It is two related quantities measured with the same unit (here System.Double), and is a dimensionless number (value). This struct stores both constituting numbers of the ratio (numerator and denominator) and returns the quotient as a value.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Ratio"/>
  public readonly struct Ratio
    : System.IConvertible, System.IEquatable<Ratio>, IQuantifiable<double>
  {
    private readonly double m_numerator;
    private readonly double m_denominator;

    public Ratio(double numerator, double denominator)
    {
      m_numerator = numerator;
      m_denominator = denominator;
    }

    [System.Diagnostics.Contracts.Pure]
    public double Numerator
      => m_numerator;
    [System.Diagnostics.Contracts.Pure]
    public double Denominator
      => m_denominator;

    [System.Diagnostics.Contracts.Pure]
    public string ToRatioString(RatioFormat format)
      => format switch
      {
        RatioFormat.AcolonB => $"{m_numerator}\u2236{m_denominator}",
        RatioFormat.AtoB => $"{m_numerator} to {m_denominator}",
        _ => throw new System.ArgumentOutOfRangeException(nameof(format))
      };

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static explicit operator double(Ratio v) => v.Value;

    [System.Diagnostics.Contracts.Pure] public static bool operator ==(Ratio a, Ratio b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(Ratio a, Ratio b) => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces

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
    [System.Diagnostics.Contracts.Pure] public bool Equals(Ratio other) => m_numerator == other.m_numerator && m_denominator == other.m_denominator;

    // IQuantifiable<>
    [System.Diagnostics.Contracts.Pure] public double Value => m_numerator / m_denominator;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is Ratio o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_numerator, m_denominator);
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Numerator = {m_numerator}, Denominator = {m_denominator} ({Value}) }}";
    #endregion Object overrides
  }
}
