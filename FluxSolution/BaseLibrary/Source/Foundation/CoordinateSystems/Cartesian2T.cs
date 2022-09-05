#if NET7_0_OR_GREATER
namespace Flux
{
  public readonly struct Cartesian2<T>
    : System.Numerics.INumberBase<Cartesian2<T>>
    where T : System.Numerics.INumber<T>
  {
    private readonly T m_x;
    private readonly T m_y;

    public Cartesian2(T x, T y)
    {
      m_x = x;
      m_y = y;
    }

    public T X => m_x;
    public T Y => m_y;

    public static Cartesian2<T> One => new(T.One, T.One);

    public static int Radix => 2;

    public static Cartesian2<T> Zero => new(T.Zero, T.Zero);

    public static Cartesian2<T> AdditiveIdentity => Zero;

    public static Cartesian2<T> MultiplicativeIdentity => One;

    public static Cartesian2<T> Abs(Cartesian2<T> value) => new(T.Abs(value.m_x), T.Abs(value.m_y));

    public static bool IsCanonical(Cartesian2<T> value) => true;
    public static bool IsComplexNumber(Cartesian2<T> value) => false;
    public static bool IsEvenInteger(Cartesian2<T> value) => T.IsEvenInteger(value.m_x) || T.IsEvenInteger(value.m_y);
    public static bool IsFinite(Cartesian2<T> value) => T.IsFinite(value.m_x) || T.IsFinite(value.m_y);
    public static bool IsImaginaryNumber(Cartesian2<T> value) => false;
    public static bool IsInfinity(Cartesian2<T> value) => T.IsInfinity(value.m_x) || T.IsInfinity(value.m_y);
    public static bool IsInteger(Cartesian2<T> value) => false;
    public static bool IsNaN(Cartesian2<T> value) => T.IsNaN(value.m_x) || T.IsNaN(value.m_y);
    public static bool IsNegative(Cartesian2<T> value) => T.IsNegative(value.m_x) || T.IsNegative(value.m_y);
    public static bool IsNegativeInfinity(Cartesian2<T> value) => T.IsNegativeInfinity(value.m_x) || T.IsNegativeInfinity(value.m_y);
    public static bool IsNormal(Cartesian2<T> value) => T.IsNormal(value.m_x) || T.IsNormal(value.m_y);
    public static bool IsOddInteger(Cartesian2<T> value) => T.IsOddInteger(value.m_x) || T.IsOddInteger(value.m_y);
    public static bool IsPositive(Cartesian2<T> value) => T.IsPositive(value.m_x) || T.IsPositive(value.m_y);
    public static bool IsPositiveInfinity(Cartesian2<T> value) => T.IsPositiveInfinity(value.m_x) || T.IsPositiveInfinity(value.m_y);
    public static bool IsRealNumber(Cartesian2<T> value) => false;
    public static bool IsSubnormal(Cartesian2<T> value) => T.IsSubnormal(value.m_x) || T.IsSubnormal(value.m_y);

    public static bool IsZero(Cartesian2<T> value) => value == Zero;

    public static Cartesian2<T> MaxMagnitude(Cartesian2<T> x, Cartesian2<T> y) => throw new NotImplementedException();
    public static Cartesian2<T> MaxMagnitudeNumber(Cartesian2<T> x, Cartesian2<T> y) => throw new NotImplementedException();
    public static Cartesian2<T> MinMagnitude(Cartesian2<T> x, Cartesian2<T> y) => throw new NotImplementedException();
    public static Cartesian2<T> MinMagnitudeNumber(Cartesian2<T> x, Cartesian2<T> y) => throw new NotImplementedException();

    public static Cartesian2<T> Parse(ReadOnlySpan<char> s, System.Globalization.NumberStyles style, IFormatProvider? provider) => throw new NotImplementedException();
    public static Cartesian2<T> Parse(string s, System.Globalization.NumberStyles style, IFormatProvider? provider) => throw new NotImplementedException();
    public static Cartesian2<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider) => throw new NotImplementedException();
    public static Cartesian2<T> Parse(string s, IFormatProvider? provider) => throw new NotImplementedException();

    static bool System.Numerics.INumberBase<Cartesian2<T>>.TryConvertFromChecked<TOther>(TOther value, out Cartesian2<T> result) => throw new NotImplementedException();
    static bool System.Numerics.INumberBase<Cartesian2<T>>.TryConvertFromSaturating<TOther>(TOther value, out Cartesian2<T> result) => throw new NotImplementedException();
    static bool System.Numerics.INumberBase<Cartesian2<T>>.TryConvertFromTruncating<TOther>(TOther value, out Cartesian2<T> result) => throw new NotImplementedException();
    static bool System.Numerics.INumberBase<Cartesian2<T>>.TryConvertToChecked<TOther>(Cartesian2<T> value, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out TOther result) => throw new NotImplementedException();
    static bool System.Numerics.INumberBase<Cartesian2<T>>.TryConvertToSaturating<TOther>(Cartesian2<T> value, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out TOther result) => throw new NotImplementedException();
    static bool System.Numerics.INumberBase<Cartesian2<T>>.TryConvertToTruncating<TOther>(Cartesian2<T> value, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out TOther result) => throw new NotImplementedException();

    public static bool TryParse(ReadOnlySpan<char> s, System.Globalization.NumberStyles style, IFormatProvider? provider, out Cartesian2<T> result) => throw new NotImplementedException();
    public static bool TryParse([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] string? s, System.Globalization.NumberStyles style, IFormatProvider? provider, out Cartesian2<T> result) => throw new NotImplementedException();
    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out Cartesian2<T> result) => throw new NotImplementedException();
    public static bool TryParse([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] string? s, IFormatProvider? provider, out Cartesian2<T> result) => throw new NotImplementedException();

    public bool Equals(Cartesian2<T> other) => m_x == other.m_x && m_y == other.m_y;

    public string ToString(string? format, IFormatProvider? formatProvider) => throw new NotImplementedException();

    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider) => throw new NotImplementedException();

    public static Cartesian2<T> operator +(Cartesian2<T> value) => value;
    public static Cartesian2<T> operator +(Cartesian2<T> left, Cartesian2<T> right) => new(left.m_x + right.m_x, left.m_y + right.m_y);
    public static Cartesian2<T> operator +(Cartesian2<T> left, T right) => new(left.m_x + right, left.m_y + right);
    public static Cartesian2<T> operator +(T left, Cartesian2<T> right) => new(left + right.m_x, left + right.m_y);
    public static Cartesian2<T> operator -(Cartesian2<T> value) => new(-value.m_x, -value.m_y);
    public static Cartesian2<T> operator -(Cartesian2<T> left, Cartesian2<T> right) => new(left.m_x - right.m_x, left.m_y - right.m_y);
    public static Cartesian2<T> operator -(Cartesian2<T> left, T right) => new(left.m_x - right, left.m_y - right);
    public static Cartesian2<T> operator -(T left, Cartesian2<T> right) => new(left - right.m_x, left - right.m_y);
    public static Cartesian2<T> operator ++(Cartesian2<T> value) => new(value.m_x + T.One, value.m_y + T.One);
    public static Cartesian2<T> operator --(Cartesian2<T> value) => new(value.m_x - T.One, value.m_y - T.One);
    public static Cartesian2<T> operator *(Cartesian2<T> left, Cartesian2<T> right) => new(left.m_x * right.m_x, left.m_y * right.m_y);
    public static Cartesian2<T> operator *(Cartesian2<T> left, T right) => new(left.m_x * right, left.m_y * right);
    public static Cartesian2<T> operator *(T left, Cartesian2<T> right) => new(left * right.m_x, left * right.m_y);
    public static Cartesian2<T> operator /(Cartesian2<T> left, Cartesian2<T> right) => new(left.m_x / right.m_x, left.m_y / right.m_y);
    public static Cartesian2<T> operator /(Cartesian2<T> left, T right) => new(left.m_x / right, left.m_y / right);
    public static Cartesian2<T> operator /(T left, Cartesian2<T> right) => new(left / right.m_x, left / right.m_y);

    public static bool operator ==(Cartesian2<T> left, Cartesian2<T> right) => left.Equals(right);
    public static bool operator !=(Cartesian2<T> left, Cartesian2<T> right) => !left.Equals(right);

    public override bool Equals([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] object? obj)
      => obj is Cartesian2<T> o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_x, m_y);
    public override string ToString()
      => $"{GetType().Name} {{ X = {m_x}, Y = {m_y} }}";
  }
}
#endif
