namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this Quantities.MagneticFluxDensityUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.MagneticFluxDensityUnit.Tesla => "T",
        Quantities.MagneticFluxDensityUnit.KilogramPerSquareSecond => "kg/s²",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum MagneticFluxDensityUnit
    {
      /// <summary>Tesla.</summary>
      Tesla,
      KilogramPerSquareSecond
    }

    /// <summary>Magnetic flux density unit of tesla.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Magnetic_flux_density"/>
    public readonly struct MagneticFluxDensity
      : System.IComparable, System.IComparable<MagneticFluxDensity>, System.IConvertible, System.IEquatable<MagneticFluxDensity>, System.IFormattable, IUnitQuantifiable<double, MagneticFluxDensityUnit>
    {
      public const MagneticFluxDensityUnit DefaultUnit = MagneticFluxDensityUnit.Tesla;

      private readonly double m_value;

      public MagneticFluxDensity(double value, MagneticFluxDensityUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          MagneticFluxDensityUnit.Tesla => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      
      public MetricMultiplicative ToMetricMultiplicative()
        => new(ToUnitValue(DefaultUnit), MetricMultiplicativePrefix.One);

      #region Overloaded operators
       public static explicit operator double(MagneticFluxDensity v) => v.m_value;
       public static explicit operator MagneticFluxDensity(double v) => new(v);

       public static bool operator <(MagneticFluxDensity a, MagneticFluxDensity b) => a.CompareTo(b) < 0;
       public static bool operator <=(MagneticFluxDensity a, MagneticFluxDensity b) => a.CompareTo(b) <= 0;
       public static bool operator >(MagneticFluxDensity a, MagneticFluxDensity b) => a.CompareTo(b) > 0;
       public static bool operator >=(MagneticFluxDensity a, MagneticFluxDensity b) => a.CompareTo(b) >= 0;

       public static bool operator ==(MagneticFluxDensity a, MagneticFluxDensity b) => a.Equals(b);
       public static bool operator !=(MagneticFluxDensity a, MagneticFluxDensity b) => !a.Equals(b);

       public static MagneticFluxDensity operator -(MagneticFluxDensity v) => new(-v.m_value);
       public static MagneticFluxDensity operator +(MagneticFluxDensity a, double b) => new(a.m_value + b);
       public static MagneticFluxDensity operator +(MagneticFluxDensity a, MagneticFluxDensity b) => a + b.m_value;
       public static MagneticFluxDensity operator /(MagneticFluxDensity a, double b) => new(a.m_value / b);
       public static MagneticFluxDensity operator /(MagneticFluxDensity a, MagneticFluxDensity b) => a / b.m_value;
       public static MagneticFluxDensity operator *(MagneticFluxDensity a, double b) => new(a.m_value * b);
       public static MagneticFluxDensity operator *(MagneticFluxDensity a, MagneticFluxDensity b) => a * b.m_value;
       public static MagneticFluxDensity operator %(MagneticFluxDensity a, double b) => new(a.m_value % b);
       public static MagneticFluxDensity operator %(MagneticFluxDensity a, MagneticFluxDensity b) => a % b.m_value;
       public static MagneticFluxDensity operator -(MagneticFluxDensity a, double b) => new(a.m_value - b);
       public static MagneticFluxDensity operator -(MagneticFluxDensity a, MagneticFluxDensity b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces
      // IComparable<>
       public int CompareTo(MagneticFluxDensity other) => m_value.CompareTo(other.m_value);
      // IComparable
       public int CompareTo(object? other) => other is not null && other is MagneticFluxDensity o ? CompareTo(o) : -1;

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
       public bool Equals(MagneticFluxDensity other) => m_value == other.m_value;

      // IFormattable
       public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // ISiDerivedUnitQuantifiable<>
      
      public double Value
        => m_value;
      
      public string ToUnitString(MagneticFluxDensityUnit unit = DefaultUnit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
      
      public double ToUnitValue(MagneticFluxDensityUnit unit = DefaultUnit)
        => unit switch
        {
          MagneticFluxDensityUnit.Tesla => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };
      #endregion Implemented interfaces

      #region Object overrides
       public override bool Equals(object? obj) => obj is MagneticFluxDensity o && Equals(o);
       public override int GetHashCode() => m_value.GetHashCode();
       public override string ToString() => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
      #endregion Object overrides
    }
  }
}
