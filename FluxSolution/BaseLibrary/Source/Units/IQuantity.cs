//namespace Flux
//{
//  /// <summary>An interface representing a comparable quantity.</summary>
//  /// <typeparam name="TValue">The value type.</typeparam>
//  public interface IQuantityComparable<TValue>
//    where TValue : System.IComparable<TValue>
//  {
//  }

//  public readonly record struct Cartesian3<TSelf>
//    : System.Numerics.INumberBase<Cartesian3<TSelf>>, IQuantityComparable<TSelf>
//    //, System.Numerics.IModulusOperators<Cartesian2<TSelf>, Cartesian2<TSelf>, Cartesian2<TSelf>>
//    where TSelf : System.Numerics.INumber<TSelf>
//  {
//    private readonly TSelf m_x;
//    private readonly TSelf m_y;
//    private readonly TSelf m_z;

//    public Cartesian3(TSelf x, TSelf y, TSelf z)
//    {
//      m_x = x;
//      m_y = y;
//      m_z = z;
//    }

//    //public TSelf Length() => System.Numerics.Vector.(new System.Numerics.Vector<TSelf>(new TSelf[] { m_x, m_y, m_z }));
//    public TSelf LengthSquared() => m_x * m_x + m_y * m_y + m_z * m_z;

//    public static Cartesian3<TSelf> One => new(TSelf.One, TSelf.One, TSelf.One);

//    public static int Radix => TSelf.Radix;

//    public static Cartesian3<TSelf> Zero => new(TSelf.Zero, TSelf.Zero, TSelf.Zero);

//    public static Cartesian3<TSelf> AdditiveIdentity => new(TSelf.AdditiveIdentity, TSelf.AdditiveIdentity, TSelf.AdditiveIdentity);

//    public static Cartesian3<TSelf> MultiplicativeIdentity => new(TSelf.MultiplicativeIdentity, TSelf.MultiplicativeIdentity, TSelf.MultiplicativeIdentity);

//    public static Cartesian3<TSelf> Abs(Cartesian3<TSelf> value) => new(TSelf.Abs(value.m_x), TSelf.Abs(value.m_y), TSelf.Abs(value.m_z));
//    public static bool IsCanonical(Cartesian3<TSelf> value) => false;
//    public static bool IsComplexNumber(Cartesian3<TSelf> value) => false;
//    public static bool IsEvenInteger(Cartesian3<TSelf> value) => false;
//    public static bool IsFinite(Cartesian3<TSelf> value) => TSelf.IsFinite(value.m_x) || TSelf.IsFinite(value.m_y) || TSelf.IsFinite(value.m_z);
//    public static bool IsImaginaryNumber(Cartesian3<TSelf> value) => false;
//    public static bool IsInfinity(Cartesian3<TSelf> value) => TSelf.IsInfinity(value.m_x) || TSelf.IsInfinity(value.m_y) || TSelf.IsInfinity(value.m_z);
//    public static bool IsInteger(Cartesian3<TSelf> value) => false;
//    public static bool IsNaN(Cartesian3<TSelf> value) => TSelf.IsNaN(value.m_x) || TSelf.IsNaN(value.m_y) || TSelf.IsNaN(value.m_z);
//    public static bool IsNegative(Cartesian3<TSelf> value) => false;
//    public static bool IsNegativeInfinity(Cartesian3<TSelf> value) => TSelf.IsNegativeInfinity(value.m_x) || TSelf.IsNegativeInfinity(value.m_y) || TSelf.IsNegativeInfinity(value.m_z);
//    public static bool IsNormal(Cartesian3<TSelf> value) => false;
//    public static bool IsOddInteger(Cartesian3<TSelf> value) => false;
//    public static bool IsPositive(Cartesian3<TSelf> value) => IsZero(value);
//    public static bool IsPositiveInfinity(Cartesian3<TSelf> value) => TSelf.IsPositiveInfinity(value.m_x) || TSelf.IsPositiveInfinity(value.m_y) || TSelf.IsPositiveInfinity(value.m_z);
//    public static bool IsRealNumber(Cartesian3<TSelf> value) => false;
//    public static bool IsSubnormal(Cartesian3<TSelf> value) => false;
//    public static bool IsZero(Cartesian3<TSelf> value) => TSelf.IsZero(value.m_x) && TSelf.IsZero(value.m_y) && TSelf.IsZero(value.m_z);
//    public static Cartesian3<TSelf> MaxMagnitude(Cartesian3<TSelf> x, Cartesian3<TSelf> y) => MaxMagnitudeNumber(x, y);
//    public static Cartesian3<TSelf> MaxMagnitudeNumber(Cartesian3<TSelf> x, Cartesian3<TSelf> y) => x.LengthSquared() >= y.LengthSquared() ? x : y;
//    public static Cartesian3<TSelf> MinMagnitude(Cartesian3<TSelf> x, Cartesian3<TSelf> y) => MinMagnitudeNumber(x, y);
//    public static Cartesian3<TSelf> MinMagnitudeNumber(Cartesian3<TSelf> x, Cartesian3<TSelf> y) => x.LengthSquared() <= y.LengthSquared() ? x : y;
//    public static Cartesian3<TSelf> Parse(ReadOnlySpan<char> s, System.Globalization.NumberStyles style, IFormatProvider? provider) => throw new NotImplementedException();
//    public static Cartesian3<TSelf> Parse(string s, System.Globalization.NumberStyles style, IFormatProvider? provider) => throw new NotImplementedException();
//    public static Cartesian3<TSelf> Parse(ReadOnlySpan<char> s, IFormatProvider? provider) => throw new NotImplementedException();
//    public static Cartesian3<TSelf> Parse(string s, IFormatProvider? provider) => throw new NotImplementedException();
//    public static bool TryParse(ReadOnlySpan<char> s, System.Globalization.NumberStyles style, IFormatProvider? provider, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out Cartesian3<TSelf> result) => throw new NotImplementedException();
//    public static bool TryParse([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] string? s, System.Globalization.NumberStyles style, IFormatProvider? provider, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out Cartesian3<TSelf> result) => throw new NotImplementedException();
//    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out Cartesian3<TSelf> result) => throw new NotImplementedException();
//    public static bool TryParse([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] string? s, IFormatProvider? provider, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out Cartesian3<TSelf> result) => throw new NotImplementedException();
//    static bool System.Numerics.INumberBase<Cartesian3<TSelf>>.TryConvertFromChecked<TOther>(TOther value, out Cartesian3<TSelf> result) => throw new NotImplementedException();
//    static bool System.Numerics.INumberBase<Cartesian3<TSelf>>.TryConvertFromSaturating<TOther>(TOther value, out Cartesian3<TSelf> result) => throw new NotImplementedException();
//    static bool System.Numerics.INumberBase<Cartesian3<TSelf>>.TryConvertFromTruncating<TOther>(TOther value, out Cartesian3<TSelf> result) => throw new NotImplementedException();
//    static bool System.Numerics.INumberBase<Cartesian3<TSelf>>.TryConvertToChecked<TOther>(Cartesian3<TSelf> value, out TOther result) => throw new NotImplementedException();
//    static bool System.Numerics.INumberBase<Cartesian3<TSelf>>.TryConvertToSaturating<TOther>(Cartesian3<TSelf> value, out TOther result) => throw new NotImplementedException();
//    static bool System.Numerics.INumberBase<Cartesian3<TSelf>>.TryConvertToTruncating<TOther>(Cartesian3<TSelf> value, out TOther result) => throw new NotImplementedException();
//    public string ToString(string? format, IFormatProvider? formatProvider) => throw new NotImplementedException();
//    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider) => throw new NotImplementedException();

//    public static Cartesian3<TSelf> operator +(Cartesian3<TSelf> value) => new(+value.m_x, +value.m_y, +value.m_z);
//    public static Cartesian3<TSelf> operator +(Cartesian3<TSelf> left, Cartesian3<TSelf> right) => new(left.m_x + right.m_x, left.m_y + right.m_y, left.m_z + right.m_z);
//    public static Cartesian3<TSelf> operator -(Cartesian3<TSelf> value) => new(-value.m_x, -value.m_y, -value.m_z);
//    public static Cartesian3<TSelf> operator -(Cartesian3<TSelf> left, Cartesian3<TSelf> right) => new(left.m_x - right.m_x, left.m_y - right.m_y, left.m_z - right.m_z);
//    public static Cartesian3<TSelf> operator ++(Cartesian3<TSelf> value) => new(value.m_x + TSelf.One, value.m_y + TSelf.One, value.m_z + TSelf.One);
//    public static Cartesian3<TSelf> operator --(Cartesian3<TSelf> value) => new(value.m_x - TSelf.One, value.m_y - TSelf.One, value.m_z - TSelf.One);
//    public static Cartesian3<TSelf> operator *(Cartesian3<TSelf> left, Cartesian3<TSelf> right) => new(left.m_x * right.m_x, left.m_y * right.m_y, left.m_z * right.m_z);
//    public static Cartesian3<TSelf> operator /(Cartesian3<TSelf> left, Cartesian3<TSelf> right) => new(left.m_x / right.m_x, left.m_y / right.m_y, left.m_z / right.m_z);
//  }
//}
