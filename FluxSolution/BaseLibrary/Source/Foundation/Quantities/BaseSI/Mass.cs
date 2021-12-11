namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitSymbol(this Quantity.MassUnit unit)
      => unit switch
      {
        Quantity.MassUnit.Milligram => @" mg",
        Quantity.MassUnit.Gram => @" g",
        Quantity.MassUnit.Ounce => @" oz",
        Quantity.MassUnit.Pound => @" lb",
        Quantity.MassUnit.Kilogram => @" kg",
        Quantity.MassUnit.MetricTon => @" t",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantity
  {
    public enum MassUnit
    {
      Milligram,
      Gram,
      Ounce,
      Pound,
      Kilogram,
      MetricTon,
    }

    /// <summary>Mass. SI unit of kilogram. This is a base quantity.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Mass"/>
    public struct Mass
      : System.IComparable<Mass>, System.IEquatable<Mass>, IUnitValueStandardized<double>, IValueBaseUnitSI<double>
    {
      public static Mass ElectronMass
        => new(9.1093837015e-31);

      private readonly double m_value;

      public Mass(double value, MassUnit unit = MassUnit.Kilogram)
        => m_value = unit switch
        {
          MassUnit.Milligram => value / 1000000,
          MassUnit.Gram => value / 1000,
          MassUnit.Ounce => value / 35.27396195,
          MassUnit.Pound => value * 0.45359237,
          MassUnit.Kilogram => value,
          MassUnit.MetricTon => value * 1000,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public double BaseUnitValue
        => m_value;

      public double StandardUnitValue
        => m_value;

      public string ToUnitString(MassUnit unit = MassUnit.Kilogram, string? format = null)
        => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
      public double ToUnitValue(MassUnit unit = MassUnit.Kilogram)
        => unit switch
        {
          MassUnit.Milligram => m_value * 1000000,
          MassUnit.Gram => m_value * 1000,
          MassUnit.Ounce => m_value * 35.27396195,
          MassUnit.Pound => m_value / 0.45359237,
          MassUnit.Kilogram => m_value,
          MassUnit.MetricTon => m_value / 1000,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(Mass v)
        => v.m_value;
      public static explicit operator Mass(double v)
        => new(v);

      public static bool operator <(Mass a, Mass b)
        => a.CompareTo(b) < 0;
      public static bool operator <=(Mass a, Mass b)
        => a.CompareTo(b) <= 0;
      public static bool operator >(Mass a, Mass b)
        => a.CompareTo(b) > 0;
      public static bool operator >=(Mass a, Mass b)
        => a.CompareTo(b) >= 0;

      public static bool operator ==(Mass a, Mass b)
        => a.Equals(b);
      public static bool operator !=(Mass a, Mass b)
        => !a.Equals(b);

      public static Mass operator -(Mass v)
        => new(-v.m_value);
      public static Mass operator +(Mass a, double b)
        => new(a.m_value + b);
      public static Mass operator +(Mass a, Mass b)
        => a + b.m_value;
      public static Mass operator /(Mass a, double b)
        => new(a.m_value / b);
      public static Mass operator /(Mass a, Mass b)
        => a / b.m_value;
      public static Mass operator *(Mass a, double b)
        => new(a.m_value * b);
      public static Mass operator *(Mass a, Mass b)
        => a * b.m_value;
      public static Mass operator %(Mass a, double b)
        => new(a.m_value % b);
      public static Mass operator %(Mass a, Mass b)
        => a % b.m_value;
      public static Mass operator -(Mass a, double b)
        => new(a.m_value - b);
      public static Mass operator -(Mass a, Mass b)
        => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces
      // IComparable
      public int CompareTo(Mass other)
        => m_value.CompareTo(other.m_value);

      // IEquatable
      public bool Equals(Mass other)
        => m_value == other.m_value;
      #endregion Implemented interfaces

      #region Object overrides
      public override bool Equals(object? obj)
        => obj is Mass o && Equals(o);
      public override int GetHashCode()
        => m_value.GetHashCode();
      public override string ToString()
        => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
      #endregion Object overrides
    }
  }
}
