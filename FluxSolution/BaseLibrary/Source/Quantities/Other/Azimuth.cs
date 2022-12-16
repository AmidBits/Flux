namespace Flux.Quantities
{
  /// <summary>Azimuth unit of degree. The internal unit here is defined in the range [0, +360]. Values are always wrapped within that range.</summary>
  /// <remarks>It may seem unreasonable to perform arithmetic with what could be perceived as a compass quantity, but this really is just another angle quantity hardcoded to degrees and a range of [0, +360].</remarks>
  /// <see cref="https://en.wikipedia.org/wiki/Azimuth"/>
  public readonly record struct Azimuth
    : System.IComparable<Azimuth>, System.IConvertible, IQuantifiable<double>
  {
    public const double MaxValue = 360;
    public const double MinValue = 0;

    public static Azimuth Zero => new();

    private readonly double m_degAzimuth;

    /// <summary>Creates a new Azimuth from the specified number of degrees. The value is wrapped within the degree range [0, +360].</summary>
    public Azimuth(double degAzimuth)
      => m_degAzimuth = WrapAzimuth(degAzimuth);

    public Quantities.Angle ToAngle()
      => new(m_degAzimuth, Quantities.AngleUnit.Degree);

    public double ToRadians()
      => Quantities.Angle.ConvertDegreeToRadian(m_degAzimuth);

    #region Static methods
    /// <summary>Finding the angle between two bearings.</summary>

    public static double DeltaBearing(double degAzimuth1, double degAzimuth2)
      => (degAzimuth2 - degAzimuth1).Wrap(MinValue, MaxValue);

    public static Azimuth FromAbbreviation(string compassPointAbbreviated)
      => System.Enum.TryParse<ThirtytwoWindCompassRose>(compassPointAbbreviated, true, out var thirtytwoWindCompassRose) ? thirtytwoWindCompassRose.GetAzimuth() : throw new System.ArgumentOutOfRangeException(nameof(compassPointAbbreviated));


    private static string[] Words
      => new string[] { "North", "East", "South", "West", "By" };

    public static Azimuth FromRadians(double radAzimuth)
      => new(Quantities.Angle.ConvertRadianToDegree(radAzimuth));

    public static Azimuth FromWords(string compassPointInWords)
    {
      var words = new System.Collections.Generic.List<string>();

      var sb = compassPointInWords.ToSpanBuilder();

      sb.RemoveAll(char.IsWhiteSpace);

      while (sb.Length > 0)
      {
        var index = 0;

        for (; index < Words.Length; index++)
        {
          var word = Words[index];

          if (sb.AsReadOnlySpan().StartsWith(word, System.StringComparison.InvariantCultureIgnoreCase))
          {
            words.Add(word);

            sb.Remove(0, word.Length);

            break;
          }
        }

        if (index == Words.Length)
          sb.Remove(0, 1);
      }

      return FromAbbreviation(string.Concat(words.Select(s => s[0])));
    }

    /// <summary>Returns the bearing needle latched to one of the specified number of positions around the compass. For example, 4 positions will return an index [0, 3] (of four) for the latched bearing.</summary>

    public static int LatchNeedle(double degAzimuth, int positions)
      => (int)System.Math.Round(WrapAzimuth(degAzimuth) / (360d / positions) % positions);

    public static Azimuth Parse(string compassPointInWordsOrAbbreviation)
      => FromAbbreviation(compassPointInWordsOrAbbreviation) is Azimuth fromAbbreviation
      ? fromAbbreviation
      : FromWords(compassPointInWordsOrAbbreviation) is Azimuth fromWords
      ? fromWords
      : throw new System.ArgumentOutOfRangeException(nameof(compassPointInWordsOrAbbreviation));

    public static bool TryParse(string compassPointInWordsOrAbbreviation, out Azimuth result)
    {
      try
      {
        result = FromAbbreviation(compassPointInWordsOrAbbreviation);
        return true;
      }
      catch { }

      try
      {
        result = FromWords(compassPointInWordsOrAbbreviation);
        return true;
      }
      catch { }

      result = default;
      return false;
    }

    /// <summary>An azimuth is wrapped over the range [0, 360).</summary>
    public static double WrapAzimuth(double degAzimuth)
      => degAzimuth.Wrap(MinValue, MaxValue) % MaxValue;
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Azimuth v) => v.m_degAzimuth;
    public static explicit operator Azimuth(double v) => new(v);

    public static bool operator <(Azimuth a, Azimuth b) => a.CompareTo(b) < 0;
    public static bool operator <=(Azimuth a, Azimuth b) => a.CompareTo(b) <= 0;
    public static bool operator >(Azimuth a, Azimuth b) => a.CompareTo(b) > 0;
    public static bool operator >=(Azimuth a, Azimuth b) => a.CompareTo(b) >= 0;

    public static Azimuth operator -(Azimuth v) => new(-v.m_degAzimuth);
    public static Azimuth operator +(Azimuth a, double b) => new(a.m_degAzimuth + b);
    public static Azimuth operator +(Azimuth a, Azimuth b) => a + b.Value;
    public static Azimuth operator /(Azimuth a, double b) => new(a.m_degAzimuth / b);
    public static Azimuth operator /(Azimuth a, Azimuth b) => a / b.Value;
    public static Azimuth operator *(Azimuth a, double b) => new(a.m_degAzimuth * b);
    public static Azimuth operator *(Azimuth a, Azimuth b) => a * b.Value;
    public static Azimuth operator %(Azimuth a, double b) => new(a.m_degAzimuth % b);
    public static Azimuth operator %(Azimuth a, Azimuth b) => a % b.Value;
    public static Azimuth operator -(Azimuth a, double b) => new(a.m_degAzimuth - b);
    public static Azimuth operator -(Azimuth a, Azimuth b) => a - b.Value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<>
    public int CompareTo(Azimuth other) => m_degAzimuth.CompareTo(other.m_degAzimuth);
    // IComparable
    public int CompareTo(object? other) => other is not null && other is Azimuth o ? CompareTo(o) : -1;

    #region IConvertible
    public System.TypeCode GetTypeCode() => System.TypeCode.Object;
    public bool ToBoolean(System.IFormatProvider? provider) => m_degAzimuth != 0;
    public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(m_degAzimuth);
    public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(m_degAzimuth);
    public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(m_degAzimuth);
    public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(m_degAzimuth);
    public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(m_degAzimuth);
    public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(m_degAzimuth);
    public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(m_degAzimuth);
    public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(m_degAzimuth);
    [System.CLSCompliant(false)] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(m_degAzimuth);
    public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(m_degAzimuth);
    public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", m_degAzimuth);
    public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(m_degAzimuth, conversionType, provider);
    [System.CLSCompliant(false)] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(m_degAzimuth);
    [System.CLSCompliant(false)] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(m_degAzimuth);
    [System.CLSCompliant(false)] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(m_degAzimuth);
    #endregion IConvertible

    // IQuantifiable<>
    public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false)
      => new Quantities.Angle(m_degAzimuth, Quantities.AngleUnit.Degree).ToUnitString(Quantities.AngleUnit.Degree, format, preferUnicode, useFullName);

    public double Value { get => m_degAzimuth; init => m_degAzimuth = value; }
    #endregion Implemented interfaces

    #region Object overrides
    public override string ToString()
      => $"{GetType().Name} {{ {ToQuantityString()} }}";
    #endregion Object overrides
  }
}
