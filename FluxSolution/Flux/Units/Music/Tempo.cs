namespace Flux.Units
{
  public class Tempo
    : Units.IUnitValueQuantifiable<double, TempoUnit>
  {
    private readonly double m_value;

    public Tempo(double value, TempoUnit unit = TempoUnit.BeatsPerMinute) => m_value = ConvertFromUnit(unit, value);

    #region Static methods

    public static double ConvertTempoToMilliseconds(double bpm)
      => 60000.0 / bpm;

    public static double ConvertTempoToSamples(double bpm, double noteDivision, double sampleRate)
      => 60000.0 / bpm * noteDivision * sampleRate / 1000.0;

    #endregion

    #region IFormattable

    public string ToString(string? format, IFormatProvider? formatProvider) => ToUnitString(TempoUnit.BeatsPerMinute, format, formatProvider);

    #endregion

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(TempoUnit unit, double value)
      => unit switch
      {
        TempoUnit.BeatsPerMinute => value,
        TempoUnit.BeatsPerSecond => value * 60,

        _ => unit.GetUnitFactor() * value,
      };
    public static double ConvertToUnit(TempoUnit unit, double value)
      => unit switch
      {
        TempoUnit.BeatsPerMinute => value,
        TempoUnit.BeatsPerSecond => value / 60,

        _ => value / unit.GetUnitFactor(),
      };
    public static double ConvertUnit(double value, TempoUnit from, TempoUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));
    public double GetUnitValue(TempoUnit unit) => ConvertToUnit(unit, m_value);
    public string ToUnitString(TempoUnit unit = TempoUnit.BeatsPerMinute, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider)
        + spacing.ToSpacingString()
        + (fullName ? unit.GetUnitName() : unit.GetUnitSymbol(false));
    }

    #endregion

    #region IValueQuantifiable<>

    public double Value => m_value;

    #endregion

    public override string ToString() => ToString(null, null);
  }
}
