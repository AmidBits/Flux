namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this Quantities.PressureUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.PressureUnit.Millibar => "mbar",
        Quantities.PressureUnit.Bar => preferUnicode ? "\u3374" : "bar",
        Quantities.PressureUnit.HectoPascal => preferUnicode ? "\u3371" : "hPa",
        Quantities.PressureUnit.KiloPascal => preferUnicode ? "\u33AA" : "kPa",
        Quantities.PressureUnit.Pascal => preferUnicode ? "\u33A9" : "Pa",
        Quantities.PressureUnit.Psi => "psi",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum PressureUnit
    {
      Pascal, // DefaultUnit first for actual instatiation defaults.
      Millibar,
      Bar,
      HectoPascal,
      KiloPascal,
      Psi,
    }

    /// <summary>Pressure, unit of Pascal. This is an SI derived quantity.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Pressure"/>
    public readonly struct Pressure
      : System.IComparable, System.IComparable<Pressure>, System.IConvertible, System.IEquatable<Pressure>, System.IFormattable, IUnitQuantifiable<double, PressureUnit>
    {
      public const PressureUnit DefaultUnit = PressureUnit.Pascal;

      public static Pressure StandardAtmosphere
        => new(101325);
      public static Pressure StandardStatePressure
        => new(100000);

      private readonly double m_value;

      public Pressure(double value, PressureUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          PressureUnit.Millibar => value * 100,
          PressureUnit.Bar => value / 100000,
          PressureUnit.HectoPascal => value * 100,
          PressureUnit.KiloPascal => value * 1000,
          PressureUnit.Pascal => value,
          PressureUnit.Psi => value * (8896443230521.0 / 1290320000.0),
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods
      #endregion Static methods

      #region Overloaded operators
       public static explicit operator double(Pressure v) => v.m_value;
       public static explicit operator Pressure(double v) => new(v);

       public static bool operator <(Pressure a, Pressure b) => a.CompareTo(b) < 0;
       public static bool operator <=(Pressure a, Pressure b) => a.CompareTo(b) <= 0;
       public static bool operator >(Pressure a, Pressure b) => a.CompareTo(b) > 0;
       public static bool operator >=(Pressure a, Pressure b) => a.CompareTo(b) >= 0;

       public static bool operator ==(Pressure a, Pressure b) => a.Equals(b);
       public static bool operator !=(Pressure a, Pressure b) => !a.Equals(b);

       public static Pressure operator -(Pressure v) => new(-v.m_value);
       public static Pressure operator +(Pressure a, double b) => new(a.m_value + b);
       public static Pressure operator +(Pressure a, Pressure b) => a + b.m_value;
       public static Pressure operator /(Pressure a, double b) => new(a.m_value / b);
       public static Pressure operator /(Pressure a, Pressure b) => a / b.m_value;
       public static Pressure operator *(Pressure a, double b) => new(a.m_value * b);
       public static Pressure operator *(Pressure a, Pressure b) => a * b.m_value;
       public static Pressure operator %(Pressure a, double b) => new(a.m_value % b);
       public static Pressure operator %(Pressure a, Pressure b) => a % b.m_value;
       public static Pressure operator -(Pressure a, double b) => new(a.m_value - b);
       public static Pressure operator -(Pressure a, Pressure b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces
      // IComparable<>
       public int CompareTo(Pressure other) => m_value.CompareTo(other.m_value);
      // IComparable
       public int CompareTo(object? other) => other is not null && other is Pressure o ? CompareTo(o) : -1;

      #region IConvertible
       public System.TypeCode GetTypeCode() => System.TypeCode.Object;
       public bool ToBoolean(System.IFormatProvider? provider) => m_value != 0;
       public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(m_value);
       public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(m_value);
       public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(m_value);
       public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(m_value);
       public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(m_value);
       public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(m_value);
       public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(m_value);
       public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(m_value);
      [System.CLSCompliant(false)] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(m_value);
       public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(m_value);
       public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", m_value);
       public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(m_value, conversionType, provider);
      [System.CLSCompliant(false)] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(m_value);
      [System.CLSCompliant(false)] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(m_value);
      [System.CLSCompliant(false)] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(m_value);
      #endregion IConvertible

      // IEquatable<>
       public bool Equals(Pressure other) => m_value == other.m_value;

      // IFormattable
       public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
       public double Value { get => m_value; init => m_value = value; }
      // IUnitQuantifiable<>
      
      public string ToUnitString(PressureUnit unit = DefaultUnit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
      
      public double ToUnitValue(PressureUnit unit = DefaultUnit)
        => unit switch
        {
          PressureUnit.Millibar => m_value / 100,
          PressureUnit.Bar => m_value / 100000,
          PressureUnit.HectoPascal => m_value / 100,
          PressureUnit.KiloPascal => m_value / 1000,
          PressureUnit.Pascal => m_value,
          PressureUnit.Psi => m_value * (1290320000.0 / 8896443230521.0),
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };
      #endregion Implemented interfaces

      #region Object overrides
       public override bool Equals(object? obj) => obj is Pressure o && Equals(o);
       public override int GetHashCode() => m_value.GetHashCode();
       public override string ToString() => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
      #endregion Object overrides
    }
  }
}
