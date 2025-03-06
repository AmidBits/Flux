namespace Flux.Units
{
  /// <summary>
  /// <para>Slope</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Slope"/></para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/Grade_(slope)"/></para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/Roof_pitch"/></para>
  /// </summary>
  public record struct Slope
    : System.IComparable, System.IComparable<Slope>, System.IFormattable, IValueQuantifiable<double>
  {
    /// <summary>
    /// <para>The slope 'm' which is equal to tan(angle).</para>
    /// </summary>
    private readonly double m_value;

    public Slope(double slope) => m_value = slope;
    public Slope(double rise, double run) : this(rise / run) { }

    /// <summary>
    /// <para><see href="https://en.wikipedia.org/wiki/Grade_(slope)"/></para>
    /// </summary>
    public Units.Angle AngleOfInclination => new(double.Atan(m_value));

    /// <summary>
    /// <para><see href="https://en.wikipedia.org/wiki/Grade_(slope)"/></para>
    /// </summary>
    public double GradePercentage => 100 * m_value;

    /// <summary>
    /// <para><see href="https://en.wikipedia.org/wiki/Grade_(slope)"/></para>
    /// </summary>
    public double GradePermillage => 1000 * m_value;

    /// <summary>
    /// <para><see href="https://en.wikipedia.org/wiki/Grade_(slope)"/></para>
    /// </summary>
    public Units.Ratio Ratio => new(1, 1 / m_value);

    /// <summary>
    /// <para><see href="https://en.wikipedia.org/wiki/Roof_pitch"/></para>
    /// </summary>
    public double RoofPitch => m_value * 12;

    public double Steepness => double.Abs(m_value);

    #region Static methods

    /// <summary>
    /// <para><see href="https://en.wikipedia.org/wiki/Grade_(slope)"/></para>
    /// </summary>
    /// <param name="angle"></param>
    /// <param name="unit"></param>
    /// <returns></returns>
    public static Slope FromAngleOfInclination(double angle, Units.AngleUnit unit) => new(double.Tan(Units.Angle.ConvertFromUnit(unit, angle)));

    /// <summary>
    /// <para><see href="https://en.wikipedia.org/wiki/Grade_(slope)"/></para>
    /// </summary>
    /// <param name="gradePercentage"></param>
    /// <returns></returns>
    public static Slope FromGradePercentage(double gradePercentage) => new(gradePercentage / 100);

    /// <summary>
    /// <para><see href="https://en.wikipedia.org/wiki/Grade_(slope)"/></para>
    /// </summary>
    /// <param name="gradePermillage"></param>
    /// <returns></returns>
    public static Slope FromGradePermillage(double gradePermillage) => new(gradePermillage / 1000);

    /// <summary>
    /// <para><see href="https://en.wikipedia.org/wiki/Grade_(slope)"/></para>
    /// </summary>
    /// <param name="ratio"></param>
    /// <returns></returns>
    public static Slope FromRatio(Units.Ratio ratio) => new(ratio.Value);

    /// <summary>
    /// <para><see href="https://en.wikipedia.org/wiki/Roof_pitch"/></para>
    /// </summary>
    /// <param name="angle"></param>
    /// <param name="unit"></param>
    /// <returns></returns>
    public static Slope FromRoofPitch(double pitch) => new(pitch, 12);

    #endregion // Static methods

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Slope o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Slope other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The <see cref="UnitInterval.Value"/> property is a value of the unit interval, between <see cref="MinValue"/> and <see cref="MaxValue"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion //Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
