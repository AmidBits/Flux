namespace Flux.Geometry.Geodesy
{
  /// <summary>
  /// <para>Azimuth, a.k.a. bearing, unit of degree.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Azimuth"/></para>
  /// </summary>
  /// <remarks>
  /// <para>Values are always wrapped within the range [0, +360).</para>
  /// </remarks>
  public readonly record struct Azimuth
    : System.IComparable, System.IComparable<Azimuth>, System.IFormattable, IValueQuantifiable<double>
  {
    /// <summary>MaxValue is an open (excluded) endpoint.</summary>
    public const double MaxValue = 360;
    /// <summary>MinValue is a closed (included) endpoint.</summary>
    public const double MinValue = 0;

    private readonly Units.Angle m_angle;

    /// <summary>
    /// <para>Creates a new <see cref="Azimuth"/> from the specified <paramref name="angle"/>.</para>
    /// </summary>
    public Azimuth(Units.Angle angle) => m_angle = new(angle.InDegrees.WrapAroundOpenEnd(MinValue, MaxValue), Units.AngleUnit.Degree);

    /// <summary>
    /// <para>Creates a new <see cref="Azimuth"/> from the specified <paramref name="angle"/> and <paramref name="unit"/>.</para>
    /// </summary>
    public Azimuth(double angle, Units.AngleUnit unit = Units.AngleUnit.Degree) : this(new Units.Angle(angle, unit)) { }

    /// <summary>
    /// <para>Gets the <see cref="Units.Angle"/> of the <see cref="Azimuth"/>.</para>
    /// </summary>
    public Units.Angle Angle { get => m_angle; }

    public CompassRose32Wind CompassAbbreviation => CompassPoint(m_angle.InDegrees, PointsOfTheCompass.ThirtytwoWinds, MidpointRounding.ToZero, out double _);

    public string CompassWords => string.Join(' ', CompassAbbreviation.ToWords());

    #region Static methods

    /// <summary>
    /// <para>Compass point (to given precision) for specified bearing.</para>
    /// </summary>
    /// <remarks>Precision = max length of compass point, 1 = the four cardinal directions, 2 = ; it could be extended to 4 for quarter-winds (eg NEbN), but I think they are little used.</remarks>
    /// <param name="azimuth">The direction in degrees.</param>
    /// <param name="precision">The precision, or resolution to adhere to, 4 = the four cardinal directions, 8 = the four cardinals and four intercardinal together (a.k.a. the eight principal winds) form the 8-wind compass rose, 16 = the eight principal winds and the eight half-winds together form the 16-wind compass rose, 32 = the eight principal winds, eight half-winds and sixteen quarter-winds form the 32-wind compass rose.</param>
    /// <param name="notch">The integer notch that is closest to the <paramref name="azimuth"/> scaled by <paramref name="precision"/>.</param>
    /// <returns></returns>
    public static CompassRose32Wind CompassPoint(double azimuth, PointsOfTheCompass precision, System.MidpointRounding roundingMethod, out double notch)
      => (CompassRose32Wind)(int)((notch = LatchNeedle(azimuth, (int)precision, roundingMethod)) * (32 / (int)precision));

    /// <summary>
    /// <para>Finding the angle between two bearings.</para>
    /// </summary>
    /// <param name="azimuth1"></param>
    /// <param name="azimuth2"></param>
    /// <returns></returns>
    public static double DeltaBearing(double azimuth1, double azimuth2)
      => new Units.Angle(azimuth2 - azimuth1, Units.AngleUnit.Degree).GetUnitValue(Units.AngleUnit.Degree);

    /// <summary>
    /// <para>Returns the bearing needle latched to one of the specified number of positions around the compass. For example, 4 positions will return an index [0, 3] (of four) for the latched bearing.</para>
    /// </summary>
    public static int LatchNeedle(double azimuth, int positions, System.MidpointRounding roundingMethod)
      => System.Convert.ToInt32(double.Round(new Units.Angle(azimuth, Units.AngleUnit.Degree).GetUnitValue(Units.AngleUnit.Degree) / (MaxValue / positions) % positions, roundingMethod));

    /// <summary>
    /// <para>Create a new <see cref="Azimuth"/> by parsing <paramref name="compassPointAbbreviated"/>.</para>
    /// </summary>
    /// <param name="compassPointAbbreviated"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static Azimuth ParseAbbreviation(string compassPointAbbreviated)
      => System.Enum.TryParse<CompassRose32Wind>(compassPointAbbreviated.Trim(), true, out var thirtytwoWindCompassRose)
      ? thirtytwoWindCompassRose.GetAzimuth()
      : throw new System.ArgumentOutOfRangeException(nameof(compassPointAbbreviated));

    /// <summary>
    /// <para>Create a new <see cref="Azimuth"/> by parsing <paramref name="compassPointInWords"/>.</para>
    /// </summary>
    /// <param name="compassPointInWords"></param>
    /// <returns></returns>
    public static Azimuth ParseWords(string compassPointInWords)
    {
      var wordsOfTheCompassPoints = System.Enum.GetNames<WordsOfTheCompass>();

      var initials = new System.Collections.Generic.List<char>();

      var sb = new System.Text.StringBuilder(compassPointInWords).RemoveAll(char.IsWhiteSpace);

      while (sb.Length > 0)
      {
        if (System.Array.FindIndex(wordsOfTheCompassPoints, w => sb.IsCommonPrefix(0, w, Flux.StringComparerEx.CurrentCultureIgnoreCase)) is var index && index > -1)
        {
          var word = wordsOfTheCompassPoints[index];

          initials.Add(word[0]);

          sb.Remove(0, word.Length);
        }
        else
          sb.Remove(0, 1);
      }

      return ParseAbbreviation(string.Concat(initials));
    }

    /// <summary>
    /// <para>Attempt to create a new <see cref="Azimuth"/> by parsing <paramref name="compassPointInWordsOrAbbreviation"/> into the out parameter <paramref name="result"/>.</para>
    /// </summary>
    /// <param name="compassPointInWordsOrAbbreviation"></param>
    /// <param name="result"></param>
    /// <returns>Whether the parsing succeeded.</returns>
    public static bool TryParse(string compassPointInWordsOrAbbreviation, out Azimuth result)
    {
      try
      {
        result = ParseAbbreviation(compassPointInWordsOrAbbreviation);
        return true;
      }
      catch { }

      try
      {
        result = ParseWords(compassPointInWordsOrAbbreviation);
        return true;
      }
      catch { }

      result = default;
      return false;
    }

    ///// <summary>An azimuth is wrapped over the half-open interval [<see cref="MinValue"/> = 0, <see cref="MaxValue"/> = 360). I.e. azimuth can be any value between <see cref="MinValue"/> (inclusive) but never <see cref="MaxValue"/> (exclusive).</summary>
    ////public static double WrapExtremum(double azimuth) => azimuth.Wrap(MinValue, MaxValue) % MaxValue;
    //public static Angle WrapExtremum(Angle angle) => new(angle.GetUnitValue(AngleUnit.Degree).Wrap(MinValue, MaxValue) % MaxValue, AngleUnit.Degree);
    ////=> (azimuth < MinValue // Closed side, allow MinValue.
    ////? MaxValue - (MinValue - azimuth) % (MaxValue - MinValue)
    ////: azimuth >= MaxValue // Half-open side, disallow MaxValue.
    ////? MinValue + (azimuth - MinValue) % (MaxValue - MinValue)
    ////: azimuth);

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Azimuth a, Azimuth b) => a.CompareTo(b) < 0;
    public static bool operator <=(Azimuth a, Azimuth b) => a.CompareTo(b) <= 0;
    public static bool operator >(Azimuth a, Azimuth b) => a.CompareTo(b) > 0;
    public static bool operator >=(Azimuth a, Azimuth b) => a.CompareTo(b) >= 0;

    public static Azimuth operator -(Azimuth v) => new(-v.m_angle.GetUnitValue(Units.AngleUnit.Degree));
    public static Azimuth operator +(Azimuth a, double b) => new(a.m_angle.GetUnitValue(Units.AngleUnit.Degree) + b);
    public static Azimuth operator +(Azimuth a, Azimuth b) => a + b.m_angle.GetUnitValue(Units.AngleUnit.Degree);
    public static Azimuth operator /(Azimuth a, double b) => new(a.m_angle.GetUnitValue(Units.AngleUnit.Degree) / b);
    public static Azimuth operator /(Azimuth a, Azimuth b) => a / b.m_angle.GetUnitValue(Units.AngleUnit.Degree);
    public static Azimuth operator *(Azimuth a, double b) => new(a.m_angle.GetUnitValue(Units.AngleUnit.Degree) * b);
    public static Azimuth operator *(Azimuth a, Azimuth b) => a * b.m_angle.GetUnitValue(Units.AngleUnit.Degree);
    public static Azimuth operator %(Azimuth a, double b) => new(a.m_angle.GetUnitValue(Units.AngleUnit.Degree) % b);
    public static Azimuth operator %(Azimuth a, Azimuth b) => a % b.m_angle.GetUnitValue(Units.AngleUnit.Degree);
    public static Azimuth operator -(Azimuth a, double b) => new(a.m_angle.GetUnitValue(Units.AngleUnit.Degree) - b);
    public static Azimuth operator -(Azimuth a, Azimuth b) => a - b.m_angle.GetUnitValue(Units.AngleUnit.Degree);

    #endregion // Overloaded operators

    #region Implemented interfaces

    // IComparable<>
    public int CompareTo(Azimuth other) => m_angle.CompareTo(other.m_angle);

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Azimuth o ? CompareTo(o) : -1;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => Angle.ToUnitString(Units.AngleUnit.Degree, format, formatProvider);

    #region IValueQuantifiable<>

    /// <summary>
    ///  <para>The unit of the <see cref="Azimuth.Value"/> property is in <see cref="AngleUnit.Degree"/>.</para>
    /// </summary>
    public double Value => m_angle.InDegrees;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(3.FormatUpToFractionalDigits(), null); // Up to three decimals.
  }
}
