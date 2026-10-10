namespace Flux
{
  public static partial class NumberBase
  {
    extension<TNumberBase>(TNumberBase numberBase)
      where TNumberBase : System.Numerics.INumberBase<TNumberBase>
    {
      public bool IsIBinaryFloatingPointIeee754() => typeof(TNumberBase).ImplementsIBinaryFloatingPointIeee754();
      public bool IsIBinaryInteger() => typeof(TNumberBase).ImplementsIBinaryInteger();
      public bool IsIBinaryIntegerSigned() => typeof(TNumberBase).ImplementsIBinaryIntegerSigned();
      public bool IsIBinaryIntegerUnsigned() => typeof(TNumberBase).ImplementsIBinaryIntegerUnsigned();
      public bool IsIBinaryNumber() => typeof(TNumberBase).ImplementsIBinaryNumber();
#if NET11_0_OR_GREATER
      public bool IsIDecimalFloatingPointIeee754() => typeof(TNumberBase).ImplementsIDecimalFloatingPointIeee754();
#endif
      public bool IsIFloatingPoint() => typeof(TNumberBase).ImplementsIFloatingPoint();
      public bool IsIFloatingPointIeee754() => typeof(TNumberBase).ImplementsIFloatingPointIeee754();
      public bool IsIMinMaxValue() => typeof(TNumberBase).ImplementsIMinMaxValue();
      public bool IsINumber() => typeof(TNumberBase).ImplementsINumber();
      public bool IsISignedNumber() => typeof(TNumberBase).ImplementsISignedNumber();
      public bool IsIUnsignedNumber() => typeof(TNumberBase).ImplementsIUnsignedNumber();
    }
  }
}
