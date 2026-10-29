namespace Flux
{
  public static partial class NumberBase
  {
    extension<TNumberBase>(TNumberBase)
    where TNumberBase : System.Numerics.INumberBase<TNumberBase>
    {
      /// <summary>
      /// <para>Gets the radix, or base, for the type.</para>
      /// </summary>
      /// <returns></returns>
      public static int Radix => TNumberBase.Radix;
    }

    extension<TNumberBase>(TNumberBase value)
      where TNumberBase : System.Numerics.INumberBase<TNumberBase>
    {
      public TNumberBase Cube()
        => value * value * value;

      public TNumberBase DistanceTo(TNumberBase other)
        => TNumberBase.Abs(other - value);

      public bool IsSignedInteger()
        => value is System.Int128 or System.Int16 or System.Int32 or System.Int64 or System.IntPtr or System.SByte or System.Numerics.BigInteger;

      public bool IsUnsignedInteger()
        => value is System.Byte or System.UInt128 or System.UInt16 or System.UInt32 or System.UInt64 or System.UIntPtr;

      public TNumberBase NormalizeZero()
        => TNumberBase.IsZero(value) ? TNumberBase.Zero : value;

      public TNumberBase Square()
        => value * value;
    }
  }
}

//public bool IsBigInteger => numberBase is System.Numerics.BigInteger;
//public bool IsComplex => numberBase is System.Numerics.Complex;

//public bool IsDouble => numberBase is System.Double || (numberBase is System.Runtime.InteropServices.NFloat && System.Runtime.InteropServices.NFloat.Size == 8);
//public bool IsSingle => numberBase is System.Single || (numberBase is System.Runtime.InteropServices.NFloat && System.Runtime.InteropServices.NFloat.Size == 4);

//      public bool IsIBinaryFloatingPointIeee754 => typeof(TNumberBase).ImplementsIBinaryFloatingPointIeee754;
//      public bool IsIBinaryInteger => typeof(TNumberBase).ImplementsIBinaryInteger;
//      public bool IsIBinaryNumber => typeof(TNumberBase).ImplementsIBinaryNumber;
//#if NET11_0_OR_GREATER
//      public bool IsIDecimalFloatingPointIeee754 => typeof(TNumberBase).ImplementsIDecimalFloatingPointIeee754;
//#endif
//      public bool IsIFloatingPoint => typeof(TNumberBase).ImplementsIFloatingPoint;
//      public bool IsIFloatingPointIeee754 => typeof(TNumberBase).ImplementsIFloatingPointIeee754;
//      public bool IsIMinMaxValue => typeof(TNumberBase).ImplementsIMinMaxValue;
//      public bool IsINumber => typeof(TNumberBase).ImplementsINumber;
//      public bool IsISignedNumber => typeof(TNumberBase).ImplementsISignedNumber;
//      public bool IsIUnsignedNumber => typeof(TNumberBase).ImplementsIUnsignedNumber;

//      public bool IsSignedInteger => typeof(TNumberBase).IsSignedInteger;
//      public bool IsUnsignedInteger => typeof(TNumberBase).IsUnsignedInteger;
