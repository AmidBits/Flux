namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static Area Create(this AreaUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this AreaUnit unit)
      => unit switch
      {
        AreaUnit.SquareMeter => @" m²",
        AreaUnit.Hectare => @" ha",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum AreaUnit
  {
    SquareMeter,
    Hectare,
  }

  /// <summary>Area, unit of square meter. This is an SI derived quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Area"/>
  public struct Area
    : System.IComparable<Area>, System.IEquatable<Area>, IValueGeneralizedUnit<double>, IValueDerivedUnitSI<double>
  {
    public const AreaUnit DefaultUnit = AreaUnit.SquareMeter;

    private readonly double m_value;

    public Area(double value, AreaUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        AreaUnit.SquareMeter => value,
        AreaUnit.Hectare => value * 10000,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double DerivedUnitValue
      => m_value;

    public double GeneralUnitValue
      => m_value;

    public string ToUnitString(AreaUnit unit = DefaultUnit, string? format = null)
      => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
    public double ToUnitValue(AreaUnit unit = DefaultUnit)
      => unit switch
      {
        AreaUnit.SquareMeter => m_value,
        AreaUnit.Hectare => m_value / 10000,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    /// <summary>Creates a new Area instance from the specified rectangular length and width.</summary>
    /// <param name="length"></param>
    /// <param name="width"></param>
    public static Area From(Length length, Length width)
      => new(length.GeneralUnitValue * width.GeneralUnitValue);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Area v)
      => v.m_value;
    public static explicit operator Area(double v)
      => new(v);

    public static bool operator <(Area a, Area b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Area a, Area b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Area a, Area b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Area a, Area b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Area a, Area b)
      => a.Equals(b);
    public static bool operator !=(Area a, Area b)
      => !a.Equals(b);

    public static Area operator -(Area v)
      => new(-v.m_value);
    public static Area operator +(Area a, double b)
      => new(a.m_value + b);
    public static Area operator +(Area a, Area b)
      => a + b.m_value;
    public static Area operator /(Area a, double b)
      => new(a.m_value / b);
    public static Area operator /(Area a, Area b)
      => a / b.m_value;
    public static Area operator *(Area a, double b)
      => new(a.m_value * b);
    public static Area operator *(Area a, Area b)
      => a * b.m_value;
    public static Area operator %(Area a, double b)
      => new(a.m_value % b);
    public static Area operator %(Area a, Area b)
      => a % b.m_value;
    public static Area operator -(Area a, double b)
      => new(a.m_value - b);
    public static Area operator -(Area a, Area b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Area other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Area other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Area o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
