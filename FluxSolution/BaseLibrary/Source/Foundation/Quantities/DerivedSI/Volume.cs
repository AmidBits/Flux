namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static Volume Create(this VolumeUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this VolumeUnit unit)
      => unit switch
      {
        VolumeUnit.Millilitre => @" ml",
        VolumeUnit.Centilitre => @" cl",
        VolumeUnit.Decilitre => @" dl",
        VolumeUnit.Litre => @" l",
        VolumeUnit.ImperialGallon => @" gal (imp)",
        VolumeUnit.ImperialQuart => @" qt (imp)",
        VolumeUnit.USGallon => @" gal (US)",
        VolumeUnit.USQuart => @" qt (US)",
        VolumeUnit.CubicFeet => " ft\u00B2",
        VolumeUnit.CubicYard => " yd\u00B2",
        VolumeUnit.CubicMeter => " m\u00B2",
        VolumeUnit.CubicMile => " mi\u00B2",
        VolumeUnit.CubicKilometer => " km\u00B2",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum VolumeUnit
  {
    Millilitre,
    Centilitre,
    Decilitre,
    Litre,
    /// <summary>British unit.</summary>
    ImperialGallon,
    /// <summary>British unit.</summary>
    ImperialQuart,
    /// <summary>US unit.</summary>
    USGallon,
    /// <summary>US unit.</summary>
    USQuart,
    CubicFeet,
    CubicYard,
    CubicMeter,
    CubicMile,
    CubicKilometer,
  }

  /// <summary>Volume, unit of cubic meter. This is an SI derived quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Volume"/>
  public struct Volume
    : System.IComparable<Volume>, System.IEquatable<Volume>, IValueGeneralizedUnit<double>, IValueDerivedUnitSI<double>
  {
    public const VolumeUnit DefaultUnit = VolumeUnit.CubicMeter;

    private readonly double m_value;

    public Volume(double value, VolumeUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        VolumeUnit.Millilitre => value / 1000000,
        VolumeUnit.Centilitre => value / 100000,
        VolumeUnit.Decilitre => value / 10000,
        VolumeUnit.Litre => value / 1000,
        VolumeUnit.ImperialGallon => value * 0.004546,
        VolumeUnit.ImperialQuart => value / 879.87699319635,
        VolumeUnit.USGallon => value * 0.003785,
        VolumeUnit.USQuart => value / 1056.68821,// Approximate.
          VolumeUnit.CubicFeet => value / (1953125000.0 / 55306341.0),
        VolumeUnit.CubicYard => value / (1953125000.0 / 1493271207.0),
        VolumeUnit.CubicMeter => value,
        VolumeUnit.CubicMile => value * (8140980127813632.0 / 1953125.0),// 
          VolumeUnit.CubicKilometer => value * 1e9,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double DerivedUnitValue
      => m_value;

    public double GeneralUnitValue
      => m_value;

    public string ToUnitString(VolumeUnit unit = DefaultUnit, string? format = null)
      => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
    public double ToUnitValue(VolumeUnit unit = DefaultUnit)
      => unit switch
      {
        VolumeUnit.Millilitre => m_value * 1000000,
        VolumeUnit.Centilitre => m_value * 100000,
        VolumeUnit.Decilitre => m_value * 10000,
        VolumeUnit.Litre => m_value * 1000,
        VolumeUnit.ImperialGallon => m_value / 0.004546,
        VolumeUnit.ImperialQuart => m_value * 879.87699319635,
        VolumeUnit.USGallon => m_value / 0.003785,
        VolumeUnit.USQuart => m_value * 1056.68821,// Approximate.
          VolumeUnit.CubicFeet => m_value * (1953125000.0 / 55306341.0),
        VolumeUnit.CubicYard => m_value * (1953125000.0 / 1493271207.0),
        VolumeUnit.CubicMeter => m_value,
        VolumeUnit.CubicMile => m_value / (8140980127813632.0 / 1953125.0),
        VolumeUnit.CubicKilometer => m_value / 1e9,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    /// <summary>Creates a new Volumne instance from the specified rectangular length, width and height.</summary>
    /// <param name="length"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public static Volume From(Length length, Length width, Length height)
      => new(length.GeneralUnitValue * width.GeneralUnitValue * height.GeneralUnitValue);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Volume v)
      => v.m_value;
    public static explicit operator Volume(double v)
      => new(v);

    public static bool operator <(Volume a, Volume b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Volume a, Volume b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Volume a, Volume b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Volume a, Volume b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Volume a, Volume b)
      => a.Equals(b);
    public static bool operator !=(Volume a, Volume b)
      => !a.Equals(b);

    public static Volume operator -(Volume v)
      => new(-v.m_value);
    public static Volume operator +(Volume a, double b)
      => new(a.m_value + b);
    public static Volume operator +(Volume a, Volume b)
      => a + b.m_value;
    public static Volume operator /(Volume a, double b)
      => new(a.m_value / b);
    public static Volume operator /(Volume a, Volume b)
      => a / b.m_value;
    public static Volume operator *(Volume a, double b)
      => new(a.m_value * b);
    public static Volume operator *(Volume a, Volume b)
      => a * b.m_value;
    public static Volume operator %(Volume a, double b)
      => new(a.m_value % b);
    public static Volume operator %(Volume a, Volume b)
      => a % b.m_value;
    public static Volume operator -(Volume a, double b)
      => new(a.m_value - b);
    public static Volume operator -(Volume a, Volume b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Volume other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Volume other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Volume o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}