#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Maths
  {
    public static TNumber Add<TNumber, TScalar>(this TNumber value, TScalar scalar) where TNumber : System.Numerics.INumberBase<TNumber> where TScalar : System.Numerics.INumber<TScalar> => value + TNumber.CreateChecked(scalar);
    public static TNumber Divide<TNumber, TScalar>(this TNumber value, TScalar scalar) where TNumber : System.Numerics.INumberBase<TNumber> where TScalar : System.Numerics.INumber<TScalar> => value / TNumber.CreateChecked(scalar);
    public static TNumber Multiply<TNumber, TScalar>(this TNumber value, TScalar scalar) where TNumber : System.Numerics.INumberBase<TNumber> where TScalar : System.Numerics.INumber<TScalar> => value * TNumber.CreateChecked(scalar);
    public static TNumber Subtract<TNumber, TScalar>(this TNumber value, TScalar scalar) where TNumber : System.Numerics.INumberBase<TNumber> where TScalar : System.Numerics.INumber<TScalar> => value - TNumber.CreateChecked(scalar);

    public static TNumber Remainder<TNumber, TScalar>(this TNumber value, TScalar scalar) where TNumber : System.Numerics.INumber<TNumber> where TScalar : System.Numerics.INumber<TScalar> => value % TNumber.CreateChecked(scalar);

    public static bool IsEqualTo<TNumber, TScalar>(this TNumber value, TScalar scalar) where TNumber : System.Numerics.INumber<TNumber> where TScalar : System.Numerics.INumber<TScalar> => value == TNumber.CreateChecked(scalar);
    public static bool IsGreaterThan<TNumber, TScalar>(this TNumber value, TScalar scalar) where TNumber : System.Numerics.INumber<TNumber> where TScalar : System.Numerics.INumber<TScalar> => value > TNumber.CreateChecked(scalar);
    public static bool IsGreaterThanOrEqualTo<TNumber, TScalar>(this TNumber value, TScalar scalar) where TNumber : System.Numerics.INumber<TNumber> where TScalar : System.Numerics.INumber<TScalar> => value >= TNumber.CreateChecked(scalar);
    public static bool IsLessThan<TNumber, TScalar>(this TNumber value, TScalar scalar) where TNumber : System.Numerics.INumber<TNumber> where TScalar : System.Numerics.INumber<TScalar> => value < TNumber.CreateChecked(scalar);
    public static bool IsLessThanOrEqualTo<TNumber, TScalar>(this TNumber value, TScalar scalar) where TNumber : System.Numerics.INumber<TNumber> where TScalar : System.Numerics.INumber<TScalar> => value <= TNumber.CreateChecked(scalar);
  }
}
#endif
