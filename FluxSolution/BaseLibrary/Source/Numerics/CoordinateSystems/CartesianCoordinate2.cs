namespace Flux
{
  /// <summary>A 2D cartesian coordinate using integers.</summary>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly struct CartesianCoordinate2<TSelf>
    : ICartesianCoordinate2<TSelf>
    , System.IEquatable<CartesianCoordinate2<TSelf>>
    , System.Numerics.IAdditionOperators<CartesianCoordinate2<TSelf>, CartesianCoordinate2<TSelf>, CartesianCoordinate2<TSelf>>
    , System.Numerics.IAdditiveIdentity<CartesianCoordinate2<TSelf>, CartesianCoordinate2<TSelf>>
    , System.Numerics.IDecrementOperators<CartesianCoordinate2<TSelf>>
    , System.Numerics.IDivisionOperators<CartesianCoordinate2<TSelf>, CartesianCoordinate2<TSelf>, CartesianCoordinate2<TSelf>>
    , System.Numerics.IEqualityOperators<CartesianCoordinate2<TSelf>, CartesianCoordinate2<TSelf>, bool>
    , System.Numerics.IIncrementOperators<CartesianCoordinate2<TSelf>>
    , System.Numerics.IModulusOperators<CartesianCoordinate2<TSelf>, CartesianCoordinate2<TSelf>, CartesianCoordinate2<TSelf>>
    , System.Numerics.IMultiplicativeIdentity<CartesianCoordinate2<TSelf>, CartesianCoordinate2<TSelf>>
    , System.Numerics.IMultiplyOperators<CartesianCoordinate2<TSelf>, CartesianCoordinate2<TSelf>, CartesianCoordinate2<TSelf>>
    , System.Numerics.INumberBase<CartesianCoordinate2<TSelf>>
    , System.Numerics.ISubtractionOperators<CartesianCoordinate2<TSelf>, CartesianCoordinate2<TSelf>, CartesianCoordinate2<TSelf>>
    , System.Numerics.IUnaryNegationOperators<CartesianCoordinate2<TSelf>, CartesianCoordinate2<TSelf>>
    , System.Numerics.IUnaryPlusOperators<CartesianCoordinate2<TSelf>, CartesianCoordinate2<TSelf>>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    private readonly TSelf m_x;
    private readonly TSelf m_y;

    public CartesianCoordinate2(TSelf x, TSelf y)
    {
      m_x = x;
      m_y = y;
    }
    //public CartesianCoordinate2(TSelf value)
    //  : this(value, value)
    //{ }
    //public CartesianCoordinate2(TSelf[] array, int startIndex)
    //{
    //  if (array is null) throw new System.ArgumentNullException(nameof(array));

    //  if (array.Length < 2) throw new System.ArgumentOutOfRangeException(nameof(array));
    //  if (startIndex + 2 >= array.Length) throw new System.ArgumentOutOfRangeException(nameof(startIndex));

    //  m_x = array[startIndex++];
    //  m_y = array[startIndex];
    //}

    public TSelf X { get => m_x; init => m_x = value; }
    public TSelf Y { get => m_y; init => m_y = value; }

    ///// <summary>Compute the Chebyshev length of the source vector. To compute the Chebyshev distance between two vectors, ChebyshevLength(target - source).</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    //public TSelf ChebyshevLength(TSelf edgeLength = 1) => TSelf.Max(TSelf.Abs(m_x / edgeLength), TSelf.Abs(m_y / edgeLength));

    ///// <summary>Compute the Euclidean length of the vector.</summary>
    //public TSelf EuclideanLength() => TSelf.Sqrt(EuclideanLengthSquared());

    ///// <summary>Compute the Euclidean length squared of the vector.</summary>
    //public TSelf EuclideanLengthSquared() => m_x * m_x + m_y * m_y;

    ///// <summary>Compute the Manhattan length (or magnitude) of the vector. To compute the Manhattan distance between two vectors, ManhattanLength(target - source).</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    //public TSelf ManhattanLength(TSelf edgeLength = 1) => TSelf.Abs(m_x / edgeLength) + TSelf.Abs(m_y / edgeLength);

    //public CartesianCoordinate2<TSelf> Normalized() => EuclideanLength() is var m && m != 0 ? this / m : this;

    ///// <summary>Returns the orthant (quadrant) of the 2D vector using the specified center and orthant numbering.</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
    //[System.Diagnostics.Contracts.Pure]
    //public int OrthantNumber(CartesianCoordinate2<TSelf> center, OrthantNumbering numbering)
    //  => numbering switch
    //  {
    //    OrthantNumbering.Traditional => m_y >= center.m_y ? (m_x >= center.m_x ? 0 : 1) : (m_x >= center.m_x ? 3 : 2),
    //    OrthantNumbering.BinaryNegativeAs1 => (m_x >= center.m_x ? 0 : 1) + (m_y >= center.m_y ? 0 : 2),
    //    OrthantNumbering.BinaryPositiveAs1 => (m_x < center.m_x ? 0 : 1) + (m_y < center.m_y ? 0 : 2),
    //    _ => throw new System.ArgumentOutOfRangeException(nameof(numbering))
    //  };

    ///// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only X and Y.</summary>
    //public CartesianCoordinate2<TSelf> PerpendicularCcw() => new(-m_y, m_x);

    ///// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only X and Y.</summary>
    //public CartesianCoordinate2<TSelf> PerpendicularCw() => new(m_y, -m_x);

    ///// <summary>Converts the <see cref="Vector2"/> to a <see cref="PolarCoordinate"/>.</summary>
    //public PolarCoordinate<TSelf> ToPolarCoordinate()
    //  => new(
    //    TSelf.Sqrt(m_x * m_x + m_y * m_y),
    //    TSelf.Atan2(m_y, m_x)
    //  );

    ///// <summary>Return the rotation angle using the cartesian 2D coordinate (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a counter-clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    //public Angle ToRotationAngle() => (Angle)ConvertCartesianCoordinate2<TSelf>ToRotationAngle(m_x, m_y);

    ///// <summary>Convert the cartesian 2D coordinate (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    //public Angle ToRotationAngleEx() => (Angle)ConvertCartesianCoordinate2<TSelf>ToRotationAngleEx(m_x, m_y);

    ///// <summary>Creates a new intrinsic vector <see cref="System.Runtime.Intrinsics.Vector128"/> with the cartesian values as vector elements [X, Y].</summary>
    //public System.Runtime.Intrinsics.Vector128<TSelf> ToVector128() => System.Runtime.Intrinsics.Vector128.Create(m_x, m_y);

    ///// <summary>Creates a new intrinsic vector <see cref="System.Runtime.Intrinsics.Vector256"/> with the cartesian values as vector elements [X, Y, <paramref name="z"/>, <paramref name="w"/>].</summary>
    //public System.Runtime.Intrinsics.Vector256<TSelf> ToVector256(TSelf z, TSelf w) => System.Runtime.Intrinsics.Vector256.Create(m_x, m_y, z, w);

    ///// <summary>Creates a new intrinsic vector <see cref="System.Runtime.Intrinsics.Vector256"/> with the cartesian values as vector elements [X, Y, X, Y], i.e. the values are duplicated.</summary>
    //public System.Runtime.Intrinsics.Vector256<TSelf> ToVector256() => ToVector256(m_x, m_y);

    #region Static methods
    ///// <summary>For 2D vectors, the cross product of two vectors, is equivalent to DotProduct(a, CrossProduct(b)), which is consistent with the notion of a "perpendicular dot product", which this is known as.</summary>
    //public static TSelf CrossProduct(ICartesianCoordinate2<TSelf> a, ICartesianCoordinate2<TSelf> b)
    //  => a.X * b.Y - a.Y * b.X;

    ///// <summary>Returns the dot product of two 2D vectors.</summary>
    //public static TSelf DotProduct(ICartesianCoordinate2<TSelf> a, ICartesianCoordinate2<TSelf> b)
    //  => a.X * b.X + a.Y * b.Y;

    ///// <summary>Convert the cartesian 2D coordinate (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a counter-clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    //public static TSelf ConvertCartesianCoordinate2ToRotationAngle<TSelf>(TSelf x, TSelf y) => TSelf.Atan2(y, x) is var atan2 && atan2 < 0 ? Constants.PiX2 + atan2 : atan2;
    ///// <summary>Convert the cartesian 2D coordinate (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    //public static TSelf ConvertCartesianCoordinate2ToRotationAngleEx<TSelf>(TSelf x, TSelf y) => Constants.PiX2 - ConvertCartesianCoordinate2 < TSelf > ToRotationAngle(y, -x); // Pass the cartesian vector (x, y) rotated 90 degrees counter-clockwise.
    #endregion Static methods

    #region Implemented interfaces

    public static explicit operator CartesianCoordinate2<TSelf>(System.ValueTuple<TSelf, TSelf> vt3) => new(vt3.Item1, vt3.Item2);
    public static explicit operator System.ValueTuple<TSelf, TSelf>(CartesianCoordinate2<TSelf> cc3) => new(cc3.X, cc3.Y);

    public static explicit operator CartesianCoordinate2<TSelf>(TSelf[] v) => new(v[0], v[1]);
    public static explicit operator TSelf[](CartesianCoordinate2<TSelf> v) => new TSelf[] { v.m_x, v.m_y };

    // System.Numerics.INumberBase<>
    public static CartesianCoordinate2<TSelf> Zero => new(TSelf.Zero, TSelf.Zero);
    public static int Radix => 2;
    public static CartesianCoordinate2<TSelf> One => new(TSelf.One, TSelf.One);
    public static CartesianCoordinate2<TSelf> Abs(CartesianCoordinate2<TSelf> cc) => new(TSelf.Abs(cc.X), TSelf.Abs(cc.Y));
    public static CartesianCoordinate2<TSelf> CreateChecked<TOther>(TOther o)
      where TOther : System.Numerics.INumberBase<TOther>
    {
      if (o.IsINumber())
      {
        var v = TSelf.CreateChecked(o);
        return new(v, v);
      }
      else if (o is CartesianCoordinate2<TSelf> cc)
        return new(cc.X, cc.Y);

      throw new System.NotSupportedException();
    }
    public static CartesianCoordinate2<TSelf> CreateSaturating<TOther>(TOther o)
      where TOther : System.Numerics.INumberBase<TOther>
    {
      if (o.IsINumber())
      {
        var v = TSelf.CreateSaturating(o);
        return new(v, v);
      }
      else if (o is CartesianCoordinate2<TSelf> cc)
        return new(cc.X, cc.Y);

      throw new System.NotSupportedException();
    }
    public static CartesianCoordinate2<TSelf> CreateTruncating<TOther>(TOther o)
      where TOther : System.Numerics.INumberBase<TOther>
    {
      if (o.IsINumber())
      {
        var v = TSelf.CreateTruncating(o);
        return new(v, v);
      }
      else if (o is CartesianCoordinate2<TSelf> cc)
        return new(cc.X, cc.Y);

      throw new System.NotSupportedException();
    }
    public static bool IsCanonical(CartesianCoordinate2<TSelf> cc) => true;
    public static bool IsComplexNumber(CartesianCoordinate2<TSelf> cc) => false;
    public static bool IsEvenInteger(CartesianCoordinate2<TSelf> cc) => TSelf.IsEvenInteger(cc.m_x) && TSelf.IsEvenInteger(cc.m_y);
    public static bool IsFinite(CartesianCoordinate2<TSelf> cc) => !IsInfinity(cc);
    public static bool IsImaginaryNumber(CartesianCoordinate2<TSelf> cc) => false;
    public static bool IsInfinity(CartesianCoordinate2<TSelf> cc) => TSelf.IsInfinity(cc.m_x) || TSelf.IsInfinity(cc.m_y);
    public static bool IsInteger(CartesianCoordinate2<TSelf> cc) => TSelf.IsInteger(cc.m_x) && TSelf.IsInteger(cc.m_y);
    public static bool IsNaN(CartesianCoordinate2<TSelf> cc) => TSelf.IsNaN(cc.m_x) || TSelf.IsNaN(cc.m_y);
    public static bool IsNegative(CartesianCoordinate2<TSelf> cc) => TSelf.IsNegative(cc.m_x) || TSelf.IsNegative(cc.m_y);
    public static bool IsNegativeInfinity(CartesianCoordinate2<TSelf> cc) => TSelf.IsNegativeInfinity(cc.m_x) || TSelf.IsNegativeInfinity(cc.m_y);
    public static bool IsNormal(CartesianCoordinate2<TSelf> cc) => false;
    public static bool IsOddInteger(CartesianCoordinate2<TSelf> cc) => TSelf.IsOddInteger(cc.m_x) && TSelf.IsOddInteger(cc.m_y);
    public static bool IsPositive(CartesianCoordinate2<TSelf> cc) => TSelf.IsPositive(cc.m_x) || TSelf.IsPositive(cc.m_y);
    public static bool IsPositiveInfinity(CartesianCoordinate2<TSelf> cc) => TSelf.IsPositiveInfinity(cc.m_x) || TSelf.IsPositiveInfinity(cc.m_y);
    public static bool IsRealNumber(CartesianCoordinate2<TSelf> cc) => false;
    public static bool IsSubnormal(CartesianCoordinate2<TSelf> cc) => false;
    public static bool IsZero(CartesianCoordinate2<TSelf> cc) => cc.Equals(Zero);
    public static CartesianCoordinate2<TSelf> MaxMagnitude(CartesianCoordinate2<TSelf> cc1, CartesianCoordinate2<TSelf> cc2) => throw new System.NotImplementedException();
    public static CartesianCoordinate2<TSelf> MaxMagnitudeNumber(CartesianCoordinate2<TSelf> cc1, CartesianCoordinate2<TSelf> cc2) => throw new System.NotImplementedException();
    public static CartesianCoordinate2<TSelf> MinMagnitude(CartesianCoordinate2<TSelf> cc1, CartesianCoordinate2<TSelf> cc2) => throw new System.NotImplementedException();
    public static CartesianCoordinate2<TSelf> MinMagnitudeNumber(CartesianCoordinate2<TSelf> cc1, CartesianCoordinate2<TSelf> cc2) => throw new System.NotImplementedException();
    public static CartesianCoordinate2<TSelf> Parse(ReadOnlySpan<char> s, System.Globalization.NumberStyles style, IFormatProvider? provider)
    {
      throw new NotImplementedException();
    }
    public static CartesianCoordinate2<TSelf> Parse(string s, System.Globalization.NumberStyles style, IFormatProvider? provider)
    {
      throw new NotImplementedException();
    }
    static bool System.Numerics.INumberBase<CartesianCoordinate2<TSelf>>.TryConvertFromChecked<TOther>(TOther value, out CartesianCoordinate2<TSelf> result)
    {
      try
      {
        result = CreateChecked(value);
        return true;
      }
      catch
      {
        result = default;
        return false;
      }
    }
    static bool System.Numerics.INumberBase<CartesianCoordinate2<TSelf>>.TryConvertFromSaturating<TOther>(TOther value, out CartesianCoordinate2<TSelf> result)
    {
      try
      {
        result = CreateSaturating(value);
        return true;
      }
      catch
      {
        result = default;
        return false;
      }
    }
    static bool System.Numerics.INumberBase<CartesianCoordinate2<TSelf>>.TryConvertFromTruncating<TOther>(TOther value, out CartesianCoordinate2<TSelf> result)
    {
      try
      {
        result = CreateTruncating(value);
        return true;
      }
      catch
      {
        result = default;
        return false;
      }
    }
    static bool System.Numerics.INumberBase<CartesianCoordinate2<TSelf>>.TryConvertToChecked<TOther>(CartesianCoordinate2<TSelf> value, out TOther result)
    {
      result = default;
      return false;
    }
    static bool System.Numerics.INumberBase<CartesianCoordinate2<TSelf>>.TryConvertToSaturating<TOther>(CartesianCoordinate2<TSelf> value, out TOther result)
    {
      result = default;
      return false;
    }
    static bool System.Numerics.INumberBase<CartesianCoordinate2<TSelf>>.TryConvertToTruncating<TOther>(CartesianCoordinate2<TSelf> value, out TOther result)
    {
      result = default;
      return false;
    }
    public static bool TryParse(System.ReadOnlySpan<char> s, System.Globalization.NumberStyles style, System.IFormatProvider? provider, out CartesianCoordinate2<TSelf> result)
    {
      throw new NotImplementedException();
    }
    public static bool TryParse(string? s, System.Globalization.NumberStyles style, System.IFormatProvider? provider, out CartesianCoordinate2<TSelf> result)
    {
      throw new NotImplementedException();
    }
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, System.IFormatProvider? provider)
    {
      charsWritten = default;
      return true;
    }
    public static CartesianCoordinate2<TSelf> Parse(System.ReadOnlySpan<char> s, System.IFormatProvider? provider)
    {
      throw new NotImplementedException();
    }
    public static bool TryParse(ReadOnlySpan<char> s, System.IFormatProvider? provider, out CartesianCoordinate2<TSelf> result)
    {
      throw new NotImplementedException();
    }
    public static CartesianCoordinate2<TSelf> Parse(string s, System.IFormatProvider? provider)
    {
      throw new NotImplementedException();
    }
    public static bool TryParse(string? s, System.IFormatProvider? provider, out CartesianCoordinate2<TSelf> result)
    {
      throw new NotImplementedException();
    }

    // IEquatable<>, System.Numerics.IEqualityOperators<>
    public bool Equals(CartesianCoordinate2<TSelf> other) => m_x == other.m_x && m_y == other.m_y;
    public static bool operator ==(CartesianCoordinate2<TSelf> a, CartesianCoordinate2<TSelf> b) => a.Equals(b);
    public static bool operator !=(CartesianCoordinate2<TSelf> a, CartesianCoordinate2<TSelf> b) => !a.Equals(b);
    public override bool Equals([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] object? obj) => obj is CartesianCoordinate2<TSelf> o && Equals(o);
    public override int GetHashCode() => System.HashCode.Combine(m_x, m_y);

    //// IComparable<>, System.Numerics.INumber<>
    //public int CompareTo(CartesianCoordinate2<TSelf> other) => EuclideanLength() is var el && other.EuclideanLength() is var oel && el > oel ? 1 : el < oel ? -1 : 0;
    //public static bool operator >(CartesianCoordinate2<TSelf> a, CartesianCoordinate2<TSelf> b) => a.CompareTo(b) > 0;
    //public static bool operator >=(CartesianCoordinate2<TSelf> a, CartesianCoordinate2<TSelf> b) => a.CompareTo(b) >= 0;
    //public static bool operator <(CartesianCoordinate2<TSelf> a, CartesianCoordinate2<TSelf> b) => a.CompareTo(b) < 0;
    //public static bool operator <=(CartesianCoordinate2<TSelf> a, CartesianCoordinate2<TSelf> b) => a.CompareTo(b) <= 0;
    //public int CompareTo([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] object? obj) => obj is CartesianCoordinate2<TSelf> o ? CompareTo(o) : 1;

    // System.Numerics.IUnaryPlusOperators<>
    public static CartesianCoordinate2<TSelf> operator +(CartesianCoordinate2<TSelf> cc) => new(+cc.X, +cc.Y);

    // System.Numerics.IUnaryNegationOperators<>
    public static CartesianCoordinate2<TSelf> operator -(CartesianCoordinate2<TSelf> cc) => new(-cc.X, -cc.Y);

    // System.Numerics.IDecrementOperators<>
    public static CartesianCoordinate2<TSelf> operator --(CartesianCoordinate2<TSelf> cc) => cc - TSelf.One;

    // System.Numerics.IIncrementOperators<>
    public static CartesianCoordinate2<TSelf> operator ++(CartesianCoordinate2<TSelf> cc) => cc + TSelf.One;

    // System.Numerics.IAdditiveIdentity<>
    public static CartesianCoordinate2<TSelf> AdditiveIdentity => Zero;

    // System.Numerics.IAdditionOperators<>
    public static CartesianCoordinate2<TSelf> operator +(CartesianCoordinate2<TSelf> cc1, CartesianCoordinate2<TSelf> cc2) => new(cc1.X + cc2.X, cc1.Y + cc2.Y);
    public static CartesianCoordinate2<TSelf> operator +(CartesianCoordinate2<TSelf> cc, TSelf scalar) => new(cc.X + scalar, cc.Y + scalar);
    public static CartesianCoordinate2<TSelf> operator +(TSelf scalar, CartesianCoordinate2<TSelf> cc) => new(scalar + cc.X, scalar + cc.Y);

    // System.Numerics.ISubtractionOperators<>
    public static CartesianCoordinate2<TSelf> operator -(CartesianCoordinate2<TSelf> cc1, CartesianCoordinate2<TSelf> cc2) => new(cc1.X - cc2.X, cc1.Y - cc2.Y);
    public static CartesianCoordinate2<TSelf> operator -(CartesianCoordinate2<TSelf> cc, TSelf scalar) => new(cc.X - scalar, cc.Y - scalar);
    public static CartesianCoordinate2<TSelf> operator -(TSelf scalar, CartesianCoordinate2<TSelf> cc) => new(scalar - cc.X, scalar - cc.Y);

    // System.Numerics.IMultiplicativeIdentity<>
    public static CartesianCoordinate2<TSelf> MultiplicativeIdentity => One;

    // System.Numerics.IMultiplyOperators<>
    public static CartesianCoordinate2<TSelf> operator *(CartesianCoordinate2<TSelf> cc1, CartesianCoordinate2<TSelf> cc2) => new(cc1.X * cc2.X, cc1.Y * cc2.Y);
    public static CartesianCoordinate2<TSelf> operator *(CartesianCoordinate2<TSelf> cc, TSelf scalar) => new(cc.X * scalar, cc.Y * scalar);
    public static CartesianCoordinate2<TSelf> operator *(TSelf scalar, CartesianCoordinate2<TSelf> cc) => new(scalar * cc.X, scalar * cc.Y);

    // System.Numerics.IDivisionOperators<>
    public static CartesianCoordinate2<TSelf> operator /(CartesianCoordinate2<TSelf> cc1, CartesianCoordinate2<TSelf> cc2) => new(cc1.X / cc2.X, cc1.Y / cc2.Y);
    public static CartesianCoordinate2<TSelf> operator /(CartesianCoordinate2<TSelf> cc, TSelf scalar) => new(cc.X / scalar, cc.Y / scalar);
    public static CartesianCoordinate2<TSelf> operator /(TSelf scalar, CartesianCoordinate2<TSelf> cc) => new(scalar / cc.X, scalar / cc.Y);

    // System.Numerics.IModulusOperators<>
    public static CartesianCoordinate2<TSelf> operator %(CartesianCoordinate2<TSelf> cc1, CartesianCoordinate2<TSelf> cc2) => new(cc1.X % cc2.X, cc1.Y % cc2.Y);
    public static CartesianCoordinate2<TSelf> operator %(CartesianCoordinate2<TSelf> cc, TSelf scalar) => new(cc.X % scalar, cc.Y % scalar);
    public static CartesianCoordinate2<TSelf> operator %(TSelf scalar, CartesianCoordinate2<TSelf> cc) => new(scalar % cc.X, scalar % cc.Y);

    #endregion Implemented interfaces
  }
}
