namespace Flux
{
  /// <summary>Azimuth unit of degree. The unit here is defined in the range [0, +360]. Arithmetic results are wrapped around the range.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Azimuth"/>
  public readonly struct Azimuth
    : System.IComparable<Azimuth>, System.IConvertible, System.IEquatable<Azimuth>, IQuantifiable<double>
  {
    public const double MaxValue = 360;
    public const double MinValue = 0;

    private readonly double m_degAzimuth;

    /// <summary>Creates a new Azimuth from the specified number of degrees. The value is wrapped within the degree range [0, +360].</summary>
    public Azimuth(double degAzimuth)
      => m_degAzimuth = WrapAzimuth(degAzimuth);
    /// <summary>Creates a new Azimuth from the specfied Angle instance. The value is wrapped within the degree range [0, +360].</summary>
    public Azimuth(Angle azimuth)
      : this(azimuth.ToUnitValue(AngleUnit.Degree)) // Call this to ensure value is between min/max.
    { }

    [System.Diagnostics.Contracts.Pure]
    public double Radian
      => Angle.ConvertDegreeToRadian(m_degAzimuth);

    [System.Diagnostics.Contracts.Pure]
    public Angle ToAngle()
      => new(m_degAzimuth, AngleUnit.Degree);

    #region Static methods
    /// <summary>Finding the angle between two bearings.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static double DeltaBearing(double degAzimuth1, double degAzimuth2)
      => Flux.Maths.Wrap(degAzimuth2 - degAzimuth1, MinValue, MaxValue);

    /// <summary>Returns the bearing needle latched to one of the specified number of positions around the compass. For example, 4 positions will return an index [0, 3] (of four) for the latched bearing.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static int LatchNeedle(double radAzimuth, int positions)
      => (int)System.Math.Round(Maths.Wrap(radAzimuth, 0, Maths.PiX2) / (Maths.PiX2 / positions) % positions);

    /// <summary>An azimuth is wrapped over the range [0, 360).</summary>
    [System.Diagnostics.Contracts.Pure]
    public static double WrapAzimuth(double degAzimuth)
      => Maths.Wrap(degAzimuth, MinValue, MaxValue) % MaxValue;
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static explicit operator double(Azimuth v) => v.m_degAzimuth;
    [System.Diagnostics.Contracts.Pure] public static explicit operator Azimuth(double v) => new(v);

    [System.Diagnostics.Contracts.Pure] public static bool operator <(Azimuth a, Azimuth b) => a.CompareTo(b) < 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator <=(Azimuth a, Azimuth b) => a.CompareTo(b) <= 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >(Azimuth a, Azimuth b) => a.CompareTo(b) > 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >=(Azimuth a, Azimuth b) => a.CompareTo(b) >= 0;

    [System.Diagnostics.Contracts.Pure] public static bool operator ==(Azimuth a, Azimuth b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(Azimuth a, Azimuth b) => !a.Equals(b);

    [System.Diagnostics.Contracts.Pure] public static Azimuth operator -(Azimuth v) => new(-v.m_degAzimuth);
    [System.Diagnostics.Contracts.Pure] public static Azimuth operator +(Azimuth a, double b) => new(WrapAzimuth(a.m_degAzimuth + b));
    [System.Diagnostics.Contracts.Pure] public static Azimuth operator +(Azimuth a, Azimuth b) => a + b.Value;
    [System.Diagnostics.Contracts.Pure] public static Azimuth operator /(Azimuth a, double b) => new(WrapAzimuth(a.m_degAzimuth / b));
    [System.Diagnostics.Contracts.Pure] public static Azimuth operator /(Azimuth a, Azimuth b) => a / b.Value;
    [System.Diagnostics.Contracts.Pure] public static Azimuth operator *(Azimuth a, double b) => new(WrapAzimuth(a.m_degAzimuth * b));
    [System.Diagnostics.Contracts.Pure] public static Azimuth operator *(Azimuth a, Azimuth b) => a * b.Value;
    [System.Diagnostics.Contracts.Pure] public static Azimuth operator %(Azimuth a, double b) => new(WrapAzimuth(a.m_degAzimuth % b));
    [System.Diagnostics.Contracts.Pure] public static Azimuth operator %(Azimuth a, Azimuth b) => a % b.Value;
    [System.Diagnostics.Contracts.Pure] public static Azimuth operator -(Azimuth a, double b) => new(WrapAzimuth(a.m_degAzimuth - b));
    [System.Diagnostics.Contracts.Pure] public static Azimuth operator -(Azimuth a, Azimuth b) => a - b.Value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<>
    [System.Diagnostics.Contracts.Pure] public int CompareTo(Azimuth other) => m_degAzimuth.CompareTo(other.m_degAzimuth);
    // IComparable
    [System.Diagnostics.Contracts.Pure] public int CompareTo(object? other) => other is not null && other is Azimuth o ? CompareTo(o) : -1;

    #region IConvertible
    [System.Diagnostics.Contracts.Pure] public System.TypeCode GetTypeCode() => System.TypeCode.Object;
    [System.Diagnostics.Contracts.Pure] public bool ToBoolean(System.IFormatProvider? provider) => m_degAzimuth != 0;
    [System.Diagnostics.Contracts.Pure] public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(m_degAzimuth);
    [System.Diagnostics.Contracts.Pure] public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(m_degAzimuth);
    [System.Diagnostics.Contracts.Pure] public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(m_degAzimuth);
    [System.Diagnostics.Contracts.Pure] public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(m_degAzimuth);
    [System.Diagnostics.Contracts.Pure] public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(m_degAzimuth);
    [System.Diagnostics.Contracts.Pure] public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(m_degAzimuth);
    [System.Diagnostics.Contracts.Pure] public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(m_degAzimuth);
    [System.Diagnostics.Contracts.Pure] public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(m_degAzimuth);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(m_degAzimuth);
    [System.Diagnostics.Contracts.Pure] public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(m_degAzimuth);
    [System.Diagnostics.Contracts.Pure] public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", m_degAzimuth);
    [System.Diagnostics.Contracts.Pure] public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(m_degAzimuth, conversionType, provider);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(m_degAzimuth);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(m_degAzimuth);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(m_degAzimuth);
    #endregion IConvertible

    // IEquatable<>
    [System.Diagnostics.Contracts.Pure] public bool Equals(Azimuth other) => m_degAzimuth == other.m_degAzimuth;

    // IQuantifiable<>
    [System.Diagnostics.Contracts.Pure] public double Value { get => m_degAzimuth; init => m_degAzimuth = value; }
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is Azimuth o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => m_degAzimuth.GetHashCode();
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Value = {m_degAzimuth}\u00B0 }}";
    #endregion Object overrides
  }
}
