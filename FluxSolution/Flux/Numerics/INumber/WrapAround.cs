





/*

namespace Flux
{
    public static partial class Fx
    {

    
    
    public static System.Decimal WrapAround(this System.Decimal number, System.Decimal minValue, System.Decimal maxValue)
        => (System.Decimal)(number < minValue
        ? maxValue - (minValue - number) % (maxValue - minValue)
        : number > maxValue
        ? minValue + (number - minValue) % (maxValue - minValue)
        : number);

    
    
    public static System.Double WrapAround(this System.Double number, System.Double minValue, System.Double maxValue)
        => (System.Double)(number < minValue
        ? maxValue - (minValue - number) % (maxValue - minValue)
        : number > maxValue
        ? minValue + (number - minValue) % (maxValue - minValue)
        : number);

    
    
    public static System.Int16 WrapAround(this System.Int16 number, System.Int16 minValue, System.Int16 maxValue)
        => (System.Int16)(number < minValue
        ? maxValue - (minValue - number) % (maxValue - minValue)
        : number > maxValue
        ? minValue + (number - minValue) % (maxValue - minValue)
        : number);

    
    
    public static System.Int32 WrapAround(this System.Int32 number, System.Int32 minValue, System.Int32 maxValue)
        => (System.Int32)(number < minValue
        ? maxValue - (minValue - number) % (maxValue - minValue)
        : number > maxValue
        ? minValue + (number - minValue) % (maxValue - minValue)
        : number);

    
    
    public static System.Int64 WrapAround(this System.Int64 number, System.Int64 minValue, System.Int64 maxValue)
        => (System.Int64)(number < minValue
        ? maxValue - (minValue - number) % (maxValue - minValue)
        : number > maxValue
        ? minValue + (number - minValue) % (maxValue - minValue)
        : number);

    
    
    public static System.IntPtr WrapAround(this System.IntPtr number, System.IntPtr minValue, System.IntPtr maxValue)
        => (System.IntPtr)(number < minValue
        ? maxValue - (minValue - number) % (maxValue - minValue)
        : number > maxValue
        ? minValue + (number - minValue) % (maxValue - minValue)
        : number);

    
    
    public static System.Numerics.BigInteger WrapAround(this System.Numerics.BigInteger number, System.Numerics.BigInteger minValue, System.Numerics.BigInteger maxValue)
        => (System.Numerics.BigInteger)(number < minValue
        ? maxValue - (minValue - number) % (maxValue - minValue)
        : number > maxValue
        ? minValue + (number - minValue) % (maxValue - minValue)
        : number);

    
    
    public static System.SByte WrapAround(this System.SByte number, System.SByte minValue, System.SByte maxValue)
        => (System.SByte)(number < minValue
        ? maxValue - (minValue - number) % (maxValue - minValue)
        : number > maxValue
        ? minValue + (number - minValue) % (maxValue - minValue)
        : number);

    
    
    public static System.Single WrapAround(this System.Single number, System.Single minValue, System.Single maxValue)
        => (System.Single)(number < minValue
        ? maxValue - (minValue - number) % (maxValue - minValue)
        : number > maxValue
        ? minValue + (number - minValue) % (maxValue - minValue)
        : number);

    
      [System.CLSCompliant(false)] 
    public static System.Byte WrapAround(this System.Byte number, System.Byte minValue, System.Byte maxValue)
        => (System.Byte)(number < minValue
        ? maxValue - (minValue - number) % (maxValue - minValue)
        : number > maxValue
        ? minValue + (number - minValue) % (maxValue - minValue)
        : number);

    
      [System.CLSCompliant(false)] 
    public static System.Char WrapAround(this System.Char number, System.Char minValue, System.Char maxValue)
        => (System.Char)(number < minValue
        ? maxValue - (minValue - number) % (maxValue - minValue)
        : number > maxValue
        ? minValue + (number - minValue) % (maxValue - minValue)
        : number);

    
      [System.CLSCompliant(false)] 
    public static System.UInt16 WrapAround(this System.UInt16 number, System.UInt16 minValue, System.UInt16 maxValue)
        => (System.UInt16)(number < minValue
        ? maxValue - (minValue - number) % (maxValue - minValue)
        : number > maxValue
        ? minValue + (number - minValue) % (maxValue - minValue)
        : number);

    
      [System.CLSCompliant(false)] 
    public static System.UInt32 WrapAround(this System.UInt32 number, System.UInt32 minValue, System.UInt32 maxValue)
        => (System.UInt32)(number < minValue
        ? maxValue - (minValue - number) % (maxValue - minValue)
        : number > maxValue
        ? minValue + (number - minValue) % (maxValue - minValue)
        : number);

    
      [System.CLSCompliant(false)] 
    public static System.UInt64 WrapAround(this System.UInt64 number, System.UInt64 minValue, System.UInt64 maxValue)
        => (System.UInt64)(number < minValue
        ? maxValue - (minValue - number) % (maxValue - minValue)
        : number > maxValue
        ? minValue + (number - minValue) % (maxValue - minValue)
        : number);

    
      [System.CLSCompliant(false)] 
    public static System.UIntPtr WrapAround(this System.UIntPtr number, System.UIntPtr minValue, System.UIntPtr maxValue)
        => (System.UIntPtr)(number < minValue
        ? maxValue - (minValue - number) % (maxValue - minValue)
        : number > maxValue
        ? minValue + (number - minValue) % (maxValue - minValue)
        : number);

    
    
    
    public static System.Byte Fold(this System.Byte value, System.Byte min, System.Byte max)
    {
      if (value > max)
        return (System.Byte)(System.Byte.DivRem((System.Byte)(value - max), (System.Byte)(max - min)) is var (q, remainder) && (q & 1) == 0 ? max - remainder : min + remainder);
      else if (value < min)
        return (System.Byte)(System.Byte.DivRem((System.Byte)(min - value), (System.Byte)(max - min)) is var (q, remainder) && (q & 1) == 0 ? min + remainder : max - remainder);
      return value;
    }

    
    
    public static System.Int16 Fold(this System.Int16 value, System.Int16 min, System.Int16 max)
    {
      if (value > max)
        return (System.Int16)(System.Int16.DivRem((System.Int16)(value - max), (System.Int16)(max - min)) is var (q, remainder) && (q & 1) == 0 ? max - remainder : min + remainder);
      else if (value < min)
        return (System.Int16)(System.Int16.DivRem((System.Int16)(min - value), (System.Int16)(max - min)) is var (q, remainder) && (q & 1) == 0 ? min + remainder : max - remainder);
      return value;
    }

    
    
    public static System.Int32 Fold(this System.Int32 value, System.Int32 min, System.Int32 max)
    {
      if (value > max)
        return (System.Int32)(System.Int32.DivRem((System.Int32)(value - max), (System.Int32)(max - min)) is var (q, remainder) && (q & 1) == 0 ? max - remainder : min + remainder);
      else if (value < min)
        return (System.Int32)(System.Int32.DivRem((System.Int32)(min - value), (System.Int32)(max - min)) is var (q, remainder) && (q & 1) == 0 ? min + remainder : max - remainder);
      return value;
    }

    
    
    public static System.Int64 Fold(this System.Int64 value, System.Int64 min, System.Int64 max)
    {
      if (value > max)
        return (System.Int64)(System.Int64.DivRem((System.Int64)(value - max), (System.Int64)(max - min)) is var (q, remainder) && (q & 1) == 0 ? max - remainder : min + remainder);
      else if (value < min)
        return (System.Int64)(System.Int64.DivRem((System.Int64)(min - value), (System.Int64)(max - min)) is var (q, remainder) && (q & 1) == 0 ? min + remainder : max - remainder);
      return value;
    }

    
    
    public static System.IntPtr Fold(this System.IntPtr value, System.IntPtr min, System.IntPtr max)
    {
      if (value > max)
        return (System.IntPtr)(System.IntPtr.DivRem((System.IntPtr)(value - max), (System.IntPtr)(max - min)) is var (q, remainder) && (q & 1) == 0 ? max - remainder : min + remainder);
      else if (value < min)
        return (System.IntPtr)(System.IntPtr.DivRem((System.IntPtr)(min - value), (System.IntPtr)(max - min)) is var (q, remainder) && (q & 1) == 0 ? min + remainder : max - remainder);
      return value;
    }

    
    
    public static System.Numerics.BigInteger Fold(this System.Numerics.BigInteger value, System.Numerics.BigInteger min, System.Numerics.BigInteger max)
    {
      if (value > max)
        return (System.Numerics.BigInteger)(System.Numerics.BigInteger.DivRem((System.Numerics.BigInteger)(value - max), (System.Numerics.BigInteger)(max - min)) is var (q, remainder) && (q & 1) == 0 ? max - remainder : min + remainder);
      else if (value < min)
        return (System.Numerics.BigInteger)(System.Numerics.BigInteger.DivRem((System.Numerics.BigInteger)(min - value), (System.Numerics.BigInteger)(max - min)) is var (q, remainder) && (q & 1) == 0 ? min + remainder : max - remainder);
      return value;
    }

    
      [System.CLSCompliant(false)] 
    public static System.SByte Fold(this System.SByte value, System.SByte min, System.SByte max)
    {
      if (value > max)
        return (System.SByte)(System.SByte.DivRem((System.SByte)(value - max), (System.SByte)(max - min)) is var (q, remainder) && (q & 1) == 0 ? max - remainder : min + remainder);
      else if (value < min)
        return (System.SByte)(System.SByte.DivRem((System.SByte)(min - value), (System.SByte)(max - min)) is var (q, remainder) && (q & 1) == 0 ? min + remainder : max - remainder);
      return value;
    }

    
      [System.CLSCompliant(false)] 
    public static System.UInt16 Fold(this System.UInt16 value, System.UInt16 min, System.UInt16 max)
    {
      if (value > max)
        return (System.UInt16)(System.UInt16.DivRem((System.UInt16)(value - max), (System.UInt16)(max - min)) is var (q, remainder) && (q & 1) == 0 ? max - remainder : min + remainder);
      else if (value < min)
        return (System.UInt16)(System.UInt16.DivRem((System.UInt16)(min - value), (System.UInt16)(max - min)) is var (q, remainder) && (q & 1) == 0 ? min + remainder : max - remainder);
      return value;
    }

    
      [System.CLSCompliant(false)] 
    public static System.UInt32 Fold(this System.UInt32 value, System.UInt32 min, System.UInt32 max)
    {
      if (value > max)
        return (System.UInt32)(System.UInt32.DivRem((System.UInt32)(value - max), (System.UInt32)(max - min)) is var (q, remainder) && (q & 1) == 0 ? max - remainder : min + remainder);
      else if (value < min)
        return (System.UInt32)(System.UInt32.DivRem((System.UInt32)(min - value), (System.UInt32)(max - min)) is var (q, remainder) && (q & 1) == 0 ? min + remainder : max - remainder);
      return value;
    }

    
      [System.CLSCompliant(false)] 
    public static System.UInt64 Fold(this System.UInt64 value, System.UInt64 min, System.UInt64 max)
    {
      if (value > max)
        return (System.UInt64)(System.UInt64.DivRem((System.UInt64)(value - max), (System.UInt64)(max - min)) is var (q, remainder) && (q & 1) == 0 ? max - remainder : min + remainder);
      else if (value < min)
        return (System.UInt64)(System.UInt64.DivRem((System.UInt64)(min - value), (System.UInt64)(max - min)) is var (q, remainder) && (q & 1) == 0 ? min + remainder : max - remainder);
      return value;
    }

    
      [System.CLSCompliant(false)] 
    public static System.UIntPtr Fold(this System.UIntPtr value, System.UIntPtr min, System.UIntPtr max)
    {
      if (value > max)
        return (System.UIntPtr)(System.UIntPtr.DivRem((System.UIntPtr)(value - max), (System.UIntPtr)(max - min)) is var (q, remainder) && (q & 1) == 0 ? max - remainder : min + remainder);
      else if (value < min)
        return (System.UIntPtr)(System.UIntPtr.DivRem((System.UIntPtr)(min - value), (System.UIntPtr)(max - min)) is var (q, remainder) && (q & 1) == 0 ? min + remainder : max - remainder);
      return value;
    }

    
    
    public static System.Decimal Fold(this System.Decimal value, System.Decimal min, System.Decimal max)
    {
      System.Decimal magnitude, range;

      if (value > max)
      {
        magnitude = value - max;
        range = max - min;

        return ((int)(magnitude / range) & 1) == 0 ? max - (magnitude % range) : min + (magnitude % range);
      }
      else if (value < min)
      {
        magnitude = min - value;
        range = max - min;

        return ((int)(magnitude / range) & 1) == 0 ? min + (magnitude % range) : max - (magnitude % range);
      }

      return value;
    }

    
    public static System.Double Fold(this System.Double value, System.Double min, System.Double max)
    {
      System.Double magnitude, range;

      if (value > max)
      {
        magnitude = value - max;
        range = max - min;

        return ((int)(magnitude / range) & 1) == 0 ? max - (magnitude % range) : min + (magnitude % range);
      }
      else if (value < min)
      {
        magnitude = min - value;
        range = max - min;

        return ((int)(magnitude / range) & 1) == 0 ? min + (magnitude % range) : max - (magnitude % range);
      }

      return value;
    }

    
    public static System.Single Fold(this System.Single value, System.Single min, System.Single max)
    {
      System.Single magnitude, range;

      if (value > max)
      {
        magnitude = value - max;
        range = max - min;

        return ((int)(magnitude / range) & 1) == 0 ? max - (magnitude % range) : min + (magnitude % range);
      }
      else if (value < min)
      {
        magnitude = min - value;
        range = max - min;

        return ((int)(magnitude / range) & 1) == 0 ? min + (magnitude % range) : max - (magnitude % range);
      }

      return value;
    }

    
    }
}

*/
