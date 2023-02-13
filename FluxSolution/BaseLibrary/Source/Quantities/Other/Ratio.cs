namespace Flux
{
  namespace Quantities
  {
    public enum RatioFormat
    {
      AtoB,
      AcolonB,
    }

    /// <summary>A ratio indicates how many times one number contains another. It is two related quantities measured with the same unit (here System.Double), and is a dimensionless number (value). This struct stores both constituting numbers of the ratio (numerator and denominator) and returns the quotient as a value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Ratio"/>
    public readonly record struct Ratio
      : System.IConvertible, IQuantifiable<double>
    {
      private readonly double m_numerator;
      private readonly double m_denominator;

      public Ratio(double numerator, double denominator)
      {
        m_numerator = numerator;
        m_denominator = denominator;
      }

      public double Numerator
        => m_numerator;
      public double Denominator
        => m_denominator;

      public string ToRatioString(RatioFormat format)
        => format switch
        {
          RatioFormat.AcolonB => $"{m_numerator}\u2236{m_denominator}",
          RatioFormat.AtoB => $"{m_numerator} to {m_denominator}",
          _ => throw new System.ArgumentOutOfRangeException(nameof(format))
        };

      #region Overloaded operators
      public static explicit operator double(Ratio v) => v.Value;
      #endregion Overloaded operators

      #region Implemented interfaces

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

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{m_numerator} / {m_denominator} ({Value})";

      public double Value => m_numerator / m_denominator;
      #endregion Implemented interfaces

      #region Object overrides
      public override string ToString() => $"{GetType().Name} {{ {ToQuantityString()} }}";
      #endregion Object overrides
    }
  }
}
