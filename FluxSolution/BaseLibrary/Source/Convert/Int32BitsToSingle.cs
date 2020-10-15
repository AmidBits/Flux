namespace Flux
{
  public static partial class Convert
  {
    /// <summary>Converts the specified 32-bit signed integer to a single-precision floating point number. Note: the endianness of this converter does not affect the returned value.</summary>
    /// <param name="value">The number to convert. </param>
    /// <returns>A single-precision floating point number whose value is equivalent to value.</returns>
    public static float Int32BitsToSingle(int value)
      => new Int32BitsToSingleUnion(value).AsSingle;

    /// <summary>Union used solely for the equivalent of DoubleToInt64Bits and vice versa.</summary>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
    private struct Int32BitsToSingleUnion
    {
      [System.Runtime.InteropServices.FieldOffset(0)]
      private int m_int32;
      /// <summary>The value in System.Int32 form.</summary>
      internal int AsInt32
        => m_int32;

      [System.Runtime.InteropServices.FieldOffset(0)]
      private float m_single;
      /// <summary>The value in System.Single form.</summary>
      internal float AsSingle
        => m_single;

      /// <summary>Creates a new instance representing the specified System.Int32 value.</summary>
      internal Int32BitsToSingleUnion(int value)
      {
        m_single = 0f;
        m_int32 = value;
      }

      /// <summary>Creates a new instance representing the specified System.Single value.</summary>
      internal Int32BitsToSingleUnion(float value)
      {
        m_int32 = 0;
        m_single = value;
      }
    }
  }
}
