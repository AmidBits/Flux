namespace Flux.Numerics
{
  /// <summary>A 2D cartesian coordinate using integers.</summary>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly partial record struct CartesianCoordinate2<TSelf>
    : ICartesianCoordinate2<TSelf>
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

    ///// <summary>Creates a new intrinsic vector <see cref="System.Runtime.Intrinsics.Vector128"/> with the cartesian values as vector elements [X, Y].</summary>
    //public System.Runtime.Intrinsics.Vector128<TSelf> ToVector128() => System.Runtime.Intrinsics.Vector128.Create(m_x, m_y);

    ///// <summary>Creates a new intrinsic vector <see cref="System.Runtime.Intrinsics.Vector256"/> with the cartesian values as vector elements [X, Y, <paramref name="z"/>, <paramref name="w"/>].</summary>
    //public System.Runtime.Intrinsics.Vector256<TSelf> ToVector256(TSelf z, TSelf w) => System.Runtime.Intrinsics.Vector256.Create(m_x, m_y, z, w);

    ///// <summary>Creates a new intrinsic vector <see cref="System.Runtime.Intrinsics.Vector256"/> with the cartesian values as vector elements [X, Y, X, Y], i.e. the values are duplicated.</summary>
    //public System.Runtime.Intrinsics.Vector256<TSelf> ToVector256() => ToVector256(m_x, m_y);


    /// <summary>Convert a 'mapped' unique index to a <see cref="CartesianCoordinate2{TSelf}"/>.</summary>
    /// <remarks>An index can be uniquely mapped to 2D cartesian coordinates using a <paramref name="gridWidth"/>. The 2D cartesian coordinates can also be converted back to a unique index with the same grid width value.</remarks>
    public static CartesianCoordinate2<TSelf> ConvertFromUniqueIndex(TSelf uniqueIndex, TSelf gridWidth)
      => new(
        uniqueIndex % gridWidth,
        uniqueIndex / gridWidth
      );

    /// <summary>Converts the <see cref="CartesianCoordinate2{TSelf}"/> to a 'mapped' unique index.</summary>
    /// <remarks>A 2D cartesian coordinate can be uniquely indexed using a <paramref name="gridWidth"/>. The unique index can also be converted back to a 2D cartesian coordinate with the same grid width value.</remarks>
    public static TSelf ConvertToUniqueIndex(TSelf x, TSelf y, TSelf gridWidth)
      => x + (y * gridWidth);

    [System.Text.RegularExpressions.GeneratedRegex(@"^[^\d]*(?<X>\d+)[^\d]+(?<Y>\d+)[^\d]*$", System.Text.RegularExpressions.RegexOptions.Compiled)]
    private static partial System.Text.RegularExpressions.Regex ParsingRegex();
    public static CartesianCoordinate2<TSelf> Parse(string pointAsString)
      => ParsingRegex().Match(pointAsString) is var m && m.Success && m.Groups["X"] is var gX && gX.Success && TSelf.TryParse(gX.Value, System.Globalization.NumberStyles.Number, null, out var x) && m.Groups["Y"] is var gY && gY.Success && TSelf.TryParse(gY.Value, System.Globalization.NumberStyles.Number, null, out var y)
      ? new CartesianCoordinate2<TSelf>(x, y)
      : throw new System.ArgumentOutOfRangeException(nameof(pointAsString));
    public static bool TryParse(string pointAsString, out CartesianCoordinate2<TSelf> point)
    {
      try
      {
        point = Parse(pointAsString);
        return true;
      }
      catch
      {
        point = default!;
        return false;
      }
    }


    #region Implemented interfaces

    public static explicit operator CartesianCoordinate2<TSelf>(System.ValueTuple<TSelf, TSelf> vt) => new(vt.Item1, vt.Item2);
    public static explicit operator System.ValueTuple<TSelf, TSelf>(CartesianCoordinate2<TSelf> cc) => new(cc.X, cc.Y);

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
      result = default!;
      return false;
    }
    static bool System.Numerics.INumberBase<CartesianCoordinate2<TSelf>>.TryConvertToSaturating<TOther>(CartesianCoordinate2<TSelf> value, out TOther result)
    {
      result = default!;
      return false;
    }
    static bool System.Numerics.INumberBase<CartesianCoordinate2<TSelf>>.TryConvertToTruncating<TOther>(CartesianCoordinate2<TSelf> value, out TOther result)
    {
      result = default!;
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

    //// IEquatable<>, System.Numerics.IEqualityOperators<>
    //public bool Equals(CartesianCoordinate2<TSelf> other) => m_x == other.m_x && m_y == other.m_y;
    //public static bool operator ==(CartesianCoordinate2<TSelf> a, CartesianCoordinate2<TSelf> b) => a.Equals(b);
    //public static bool operator !=(CartesianCoordinate2<TSelf> a, CartesianCoordinate2<TSelf> b) => !a.Equals(b);
    //public override bool Equals([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] object? obj) => obj is CartesianCoordinate2<TSelf> o && Equals(o);
    //public override int GetHashCode() => System.HashCode.Combine(m_x, m_y);

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

    public override string ToString()
      => ((ICartesianCoordinate2<TSelf>)this).ToString(string.Empty, null);
  }
}