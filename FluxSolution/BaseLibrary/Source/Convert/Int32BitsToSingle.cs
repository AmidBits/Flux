namespace Flux
{
  public static partial class Convert
  {
    /// <summary>Converts the specified 32-bit signed integer to a single-precision floating point number. Note: the endianness of this converter does not affect the returned value.</summary>
    /// <param name="value">The number to convert. </param>
    /// <returns>A single-precision floating point number whose value is equivalent to value.</returns>
    public static float Int32BitsToSingle(int value) => new Int32BitsToSingleUnion(value).AsSingle;

    /// <summary>Union used solely for the equivalent of DoubleToInt64Bits and vice versa.</summary>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
    private struct Int32BitsToSingleUnion
    {
      [System.Runtime.InteropServices.FieldOffset(0)]
      private int _in32;
      /// <summary>The value in System.Int32 form.</summary>
      internal int AsInt32 => _in32;

      [System.Runtime.InteropServices.FieldOffset(0)]
      private float _single;
      /// <summary>The value in System.Single form.</summary>
      internal float AsSingle => _single;

      /// <summary>Creates a new instance representing the specified System.Int32 value.</summary>
      internal Int32BitsToSingleUnion(int value)
      {
        _single = 0F;
        _in32 = value;
      }

      /// <summary>Creates a new instance representing the specified System.Single value.</summary>
      internal Int32BitsToSingleUnion(float value)
      {
        _in32 = 0;
        _single = value;
      }
   }
  }
}
