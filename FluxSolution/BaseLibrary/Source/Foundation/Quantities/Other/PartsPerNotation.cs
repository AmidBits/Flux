namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitSymbol(this Quantity.PartsPerNotationUnit unit)
    {
      switch (unit)
      {
        case Quantity.PartsPerNotationUnit.Hundred:
          return Quantity.PartsPerNotation.PercentSymbol.ToString();
        case Quantity.PartsPerNotationUnit.Thousand:
          return Quantity.PartsPerNotation.PermilleSymbol.ToString();
        case Quantity.PartsPerNotationUnit.TenThousand:
          return Quantity.PartsPerNotation.PermyriadSymbol.ToString();
        case Quantity.PartsPerNotationUnit.HundredThousand:
          return @" pcm";
        case Quantity.PartsPerNotationUnit.Million:
          return @" ppm";
        case Quantity.PartsPerNotationUnit.Billion:
          return @" ppb";
        case Quantity.PartsPerNotationUnit.Trillion:
          return @" ppt";
        case Quantity.PartsPerNotationUnit.Quadrillion:
          return @" ppq";
        default:
          return string.Empty;
      }
    }
  }

  namespace Quantity
  {
    public enum PartsPerNotationUnit
    {
      /// <summary>Percent.</summary>
      Hundred,
      /// <summary>Permille.</summary>
      Thousand,
      /// <summary>Permyriad.</summary>
      TenThousand,
      /// <summary>Per cent mille, abbreviated "pcm".</summary>
      HundredThousand,
      /// <summary>Abbreviated "ppm".</summary>
      Million,
      /// <summary>Abbreviated "ppb".</summary>
      Billion,
      /// <summary>Abbreviated "ppt".</summary>
      Trillion,
      /// <summary>Abbreviated "ppq".</summary>
      Quadrillion,
    }

    /// <summary>Parts per notation. In science and engineering, the parts-per notation is a set of pseudo-units to describe small values of miscellaneous dimensionless quantities, e.g. mole fraction or mass fraction. Since these fractions are quantity-per-quantity measures, they are pure numbers with no associated units of measurement.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Parts-per_notation"/>
    public struct PartsPerNotation
      : System.IComparable<PartsPerNotation>, System.IEquatable<PartsPerNotation>, IValuedUnit
    {
      public const char PercentSymbol = '\u0025';
      public const char PermilleSymbol = '\u2030';
      public const char PermyriadSymbol = '\u2031';

      private readonly double m_parts;
      private readonly PartsPerNotationUnit m_unit;

      /// <summary>Creates a new instance of this type.</summary>
      /// <param name="parts">The parts in parts per notation.</param>
      /// <param name="unit">The notation in parts per notation.</param>
      public PartsPerNotation(double parts, PartsPerNotationUnit unit = PartsPerNotationUnit.Hundred)
      {
        switch (unit)
        {
          case PartsPerNotationUnit.Hundred:
            m_parts = parts / 1e2;
            break;
          case PartsPerNotationUnit.Thousand:
            m_parts = parts / 1e3;
            break;
          case PartsPerNotationUnit.TenThousand:
            m_parts = parts / 1e4;
            break;
          case PartsPerNotationUnit.HundredThousand:
            m_parts = parts / 1e5;
            break;
          case PartsPerNotationUnit.Million:
            m_parts = parts / 1e6;
            break;
          case PartsPerNotationUnit.Billion:
            m_parts = parts / 1e9;
            break;
          case PartsPerNotationUnit.Trillion:
            m_parts = parts / 1e12;
            break;
          case PartsPerNotationUnit.Quadrillion:
            m_parts = parts / 1e15;
            break;
          default:
            throw new System.ArgumentOutOfRangeException(nameof(unit));
        }

        m_unit = unit;
      }

      public double Value
        => m_parts;

      public double ToUnitValue(PartsPerNotationUnit unit = PartsPerNotationUnit.Hundred)
      {
        switch (unit)
        {
          case PartsPerNotationUnit.Hundred:
            return m_parts * 1e2;
          case PartsPerNotationUnit.Thousand:
            return m_parts * 1e3;
          case PartsPerNotationUnit.TenThousand:
            return m_parts * 1e4;
          case PartsPerNotationUnit.HundredThousand:
            return m_parts * 1e5;
          case PartsPerNotationUnit.Million:
            return m_parts * 1e6;
          case PartsPerNotationUnit.Billion:
            return m_parts * 1e9;
          case PartsPerNotationUnit.Trillion:
            return m_parts * 1e12;
          case PartsPerNotationUnit.Quadrillion:
            return m_parts * 1e15;
          default:
            throw new System.ArgumentOutOfRangeException(nameof(unit));
        }
      }

      #region Static methods
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(PartsPerNotation v)
        => v.Value;

      public static bool operator <(PartsPerNotation a, PartsPerNotation b)
        => a.CompareTo(b) < 0;
      public static bool operator <=(PartsPerNotation a, PartsPerNotation b)
        => a.CompareTo(b) <= 0;
      public static bool operator >(PartsPerNotation a, PartsPerNotation b)
        => a.CompareTo(b) > 0;
      public static bool operator >=(PartsPerNotation a, PartsPerNotation b)
        => a.CompareTo(b) >= 0;

      public static bool operator ==(PartsPerNotation a, PartsPerNotation b)
        => a.Equals(b);
      public static bool operator !=(PartsPerNotation a, PartsPerNotation b)
        => !a.Equals(b);

      public static PartsPerNotation operator -(PartsPerNotation v)
        => new(-v.m_parts);
      public static PartsPerNotation operator +(PartsPerNotation a, double b)
        => new(a.m_parts + b);
      public static PartsPerNotation operator +(PartsPerNotation a, PartsPerNotation b)
        => a + b.m_parts;
      public static PartsPerNotation operator /(PartsPerNotation a, double b)
        => new(a.m_parts / b);
      public static PartsPerNotation operator /(PartsPerNotation a, PartsPerNotation b)
        => a / b.m_parts;
      public static PartsPerNotation operator *(PartsPerNotation a, double b)
        => new(a.m_parts * b);
      public static PartsPerNotation operator *(PartsPerNotation a, PartsPerNotation b)
        => a * b.m_parts;
      public static PartsPerNotation operator %(PartsPerNotation a, double b)
        => new(a.m_parts % b);
      public static PartsPerNotation operator %(PartsPerNotation a, PartsPerNotation b)
        => a % b.m_parts;
      public static PartsPerNotation operator -(PartsPerNotation a, double b)
        => new(a.m_parts - b);
      public static PartsPerNotation operator -(PartsPerNotation a, PartsPerNotation b)
        => a - b.m_parts;
      #endregion Overloaded operators

      #region Implemented interfaces
      // IComparable
      public int CompareTo(PartsPerNotation other)
        => m_parts.CompareTo(other.m_parts);

      // IEquatable
      public bool Equals(PartsPerNotation other)
        => m_parts == other.m_parts;
      #endregion Implemented interfaces

      #region Object overrides
      public override bool Equals(object? obj)
        => obj is PartsPerNotation o && Equals(o);
      public override int GetHashCode()
        => System.HashCode.Combine(m_parts);
      public override string ToString()
        => $"<{GetType().Name}: {ToUnitValue(m_unit)}{m_unit.GetUnitSymbol()}>";
      #endregion Object overrides
    }
  }
}
