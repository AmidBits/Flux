namespace Flux
{
  namespace Quantities
  {
    /// <summary>
    /// <para>Azimuth, a.k.a. bearing, unit of degree. The internal unit here is defined in the range [0, +360). Values are always wrapped within that range.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Azimuth"/></para>
    /// </summary>
    /// <remarks>It may seem unreasonable to perform arithmetic with what could be perceived as a compass quantity, but this really is just another angle quantity hardcoded to degrees and a range of [0, +360).</remarks>
    public readonly record struct Azimuth
      : System.IComparable, System.IComparable<Azimuth>, System.IFormattable, IValueQuantifiable<double>
    {
      /// <summary>MaxValue is an open (excluded) endpoint.</summary>
      public const double MaxValue = 360;
      /// <summary>MinValue is a closed (included) endpoint.</summary>
      public const double MinValue = 0;

      private readonly Quantities.Angle m_angle;

      public Azimuth(Quantities.Angle angle) => m_angle = WrapExtremum(angle);

      /// <summary>Creates a new Azimuth from the specified number of degrees. The value is wrapped within the degree range [0, +360].</summary>
      public Azimuth(double angle, AngleUnit unit = AngleUnit.Degree) : this(new Angle(angle, unit)) { }

      public Angle Angle { get => m_angle; init => m_angle = WrapExtremum(value); }

      #region Static methods

      //public ThirtytwoWindCompassRose ToCompassPoint(PointsOfTheCompass precision, out double notch)
      //{
      //  notch = System.Math.Round(m_degrees / (MaxValue / (int)precision) % (int)precision);

      //  return (ThirtytwoWindCompassRose)(int)(notch * (32 / (int)precision));
      //}

      /// <summary>Compass point (to given precision) for specified bearing.</summary>
      /// <remarks>Precision = max length of compass point, 1 = the four cardinal directions, 2 = ; it could be extended to 4 for quarter-winds (eg NEbN), but I think they are little used.</remarks>
      /// <param name="azimuth">The direction in degrees.</param>
      /// <param name="precision">The precision, or resolution to adhere to, 4 = the four cardinal directions, 8 = the four cardinals and four intercardinal together (a.k.a. the eight principal winds) form the 8-wind compass rose, 16 = the eight principal winds and the eight half-winds together form the 16-wind compass rose, 32 = the eight principal winds, eight half-winds and sixteen quarter-winds form the 32-wind compass rose.</param>
      /// <param name="notch">The integer notch that is closest to the <paramref name="azimuth"/> scaled by <paramref name="precision"/>.</param>
      /// <returns></returns>
      public static CompassRose32Wind CompassPoint(double azimuth, PointsOfTheCompass precision, out double notch)
        => (CompassRose32Wind)(int)((notch = LatchNeedle(azimuth, (int)precision)) * (32 / (int)precision));

      /// <summary>Finding the angle between two bearings.</summary>
      public static double DeltaBearing(double azimuth1, double azimuth2)
        => WrapExtremum(new Angle(azimuth2 - azimuth1, AngleUnit.Degree)).GetUnitValue(AngleUnit.Degree);

      public static Azimuth FromAbbreviation(string compassPointAbbreviated)
        => System.Enum.TryParse<CompassRose32Wind>(compassPointAbbreviated, true, out var thirtytwoWindCompassRose) ? thirtytwoWindCompassRose.GetAzimuth() : throw new System.ArgumentOutOfRangeException(nameof(compassPointAbbreviated));

      public static Azimuth FromWords(string compassPointInWords)
      {
        var wordsOfTheCompassPoints = System.Enum.GetNames<WordsOfTheCompass>();

        var words = new System.Collections.Generic.List<string>();

        var sb = new System.Text.StringBuilder(compassPointInWords);

        sb.Replace(" ", string.Empty);

        while (sb.Length > 0)
        {
          var index = 0;

          for (; index < wordsOfTheCompassPoints.Length; index++)
          {
            var word = wordsOfTheCompassPoints[index];

            if (StartsWith(sb, word, System.Collections.Generic.EqualityComparer<char>.Default))
            {
              words.Add(word);

              sb.Remove(0, word.Length);

              break;
            }
          }

          if (index == wordsOfTheCompassPoints.Length)
            sb.Remove(0, 1);
        }

        return FromAbbreviation(string.Concat(words.Select(s => s[0])));

        static bool StartsWith(System.Text.StringBuilder source, System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char> equalityComparer)
        {
          System.ArgumentNullException.ThrowIfNull(source);
          System.ArgumentNullException.ThrowIfNull(equalityComparer);

          var sourceLength = source.Length;
          var targetLength = target.Length;

          if (sourceLength < targetLength)
            return false;

          for (var index = targetLength - 1; index >= 0; index--)
            if (!equalityComparer.Equals(source[index], target[index]))
              return false;

          return true;
        }
      }

      /// <summary>Returns the bearing needle latched to one of the specified number of positions around the compass. For example, 4 positions will return an index [0, 3] (of four) for the latched bearing.</summary>
      public static int LatchNeedle(double azimuth, int positions)
        => System.Convert.ToInt32(System.Math.Round(WrapExtremum(new Angle(azimuth)).GetUnitValue(AngleUnit.Degree) / (MaxValue / positions) % positions));

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

      /// <summary>An azimuth is wrapped over the half-open interval [<see cref="MinValue"/> = 0, <see cref="MaxValue"/> = 360). I.e. azimuth can be any value between <see cref="MinValue"/> (inclusive) but never <see cref="MaxValue"/> (exclusive).</summary>
      //public static double WrapExtremum(double azimuth) => azimuth.Wrap(MinValue, MaxValue) % MaxValue;
      public static Angle WrapExtremum(Angle angle) => new(angle.GetUnitValue(AngleUnit.Degree).Wrap(MinValue, MaxValue) % MaxValue, AngleUnit.Degree);
      //=> (azimuth < MinValue // Closed side, allow MinValue.
      //? MaxValue - (MinValue - azimuth) % (MaxValue - MinValue)
      //: azimuth >= MaxValue // Half-open side, disallow MaxValue.
      //? MinValue + (azimuth - MinValue) % (MaxValue - MinValue)
      //: azimuth);

      #endregion // Static methods

      #region Overloaded operators

      public static bool operator <(Azimuth a, Azimuth b) => a.CompareTo(b) < 0;
      public static bool operator <=(Azimuth a, Azimuth b) => a.CompareTo(b) <= 0;
      public static bool operator >(Azimuth a, Azimuth b) => a.CompareTo(b) > 0;
      public static bool operator >=(Azimuth a, Azimuth b) => a.CompareTo(b) >= 0;

      public static Azimuth operator -(Azimuth v) => new(-v.m_angle.GetUnitValue(AngleUnit.Degree));
      public static Azimuth operator +(Azimuth a, double b) => new(a.m_angle.GetUnitValue(AngleUnit.Degree) + b);
      public static Azimuth operator +(Azimuth a, Azimuth b) => a + b.m_angle.GetUnitValue(AngleUnit.Degree);
      public static Azimuth operator /(Azimuth a, double b) => new(a.m_angle.GetUnitValue(AngleUnit.Degree) / b);
      public static Azimuth operator /(Azimuth a, Azimuth b) => a / b.m_angle.GetUnitValue(AngleUnit.Degree);
      public static Azimuth operator *(Azimuth a, double b) => new(a.m_angle.GetUnitValue(AngleUnit.Degree) * b);
      public static Azimuth operator *(Azimuth a, Azimuth b) => a * b.m_angle.GetUnitValue(AngleUnit.Degree);
      public static Azimuth operator %(Azimuth a, double b) => new(a.m_angle.GetUnitValue(AngleUnit.Degree) % b);
      public static Azimuth operator %(Azimuth a, Azimuth b) => a % b.m_angle.GetUnitValue(AngleUnit.Degree);
      public static Azimuth operator -(Azimuth a, double b) => new(a.m_angle.GetUnitValue(AngleUnit.Degree) - b);
      public static Azimuth operator -(Azimuth a, Azimuth b) => a - b.m_angle.GetUnitValue(AngleUnit.Degree);

      #endregion // Overloaded operators

      #region Implemented interfaces

      // IComparable<>
      public int CompareTo(Azimuth other) => m_angle.CompareTo(other.m_angle);

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Azimuth o ? CompareTo(o) : -1;

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider) => Angle.ToUnitValueString(AngleUnit.Degree, format, formatProvider, preferUnicode: true);

      // IQuantifiable<>
      /// <summary>
      ///  <para>The unit of the <see cref="Azimuth.Value"/> property is in <see cref="AngleUnit.Degree"/>.</para>
      /// </summary>
      public double Value => m_angle.GetUnitValue(AngleUnit.Degree);

      #endregion // Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}