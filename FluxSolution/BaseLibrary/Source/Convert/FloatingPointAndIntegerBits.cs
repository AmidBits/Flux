namespace Flux
{
  public static partial class Convert
  {
    /// <summary>Converts the specified 32-bit signed integer to a single-precision floating point number. Note: the endianness of this converter does not affect the returned value.</summary>
    /// <param name="value">The integer <see cref="System.Int32"/> to convert. </param>
    /// <returns>A single-precision floating point number whose value is equivalent to value.</returns>
    public static float Int32BitsToSingle(int value)
      => new Union32BitFloatingPointInteger(value).AsSingle;
    /// <summary>Converts the specified single-precision floating point number to a 32-bit signed integer. Note: the endianness of this converter does not affect the returned value.</summary>
    /// <param name="value">The floating point <see cref="System.Single"/> to convert. </param>
    /// <returns>A 32-bit signed integer whose value is equivalent to value.</returns>
    public static int SingleToInt32Bits(float value)
      => new Union32BitFloatingPointInteger(value).AsInt32;

    /// <summary>Union used solely for the equivalent of DoubleToInt64Bits and vice versa.</summary>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
    private struct Union32BitFloatingPointInteger
    {
      [System.Runtime.InteropServices.FieldOffset(0)]
      private readonly int m_int32;
      [System.Runtime.InteropServices.FieldOffset(0)]
      private readonly float m_single;

      /// <summary>The value in System.Int32 form.</summary>
      internal int AsInt32
        => m_int32;
      /// <summary>The value in System.Single form.</summary>
      internal float AsSingle
        => m_single;

      /// <summary>Creates a new instance representing the specified System.Int32 value.</summary>
      internal Union32BitFloatingPointInteger(int value)
      {
        m_single = 0f;
        m_int32 = value;
      }

      /// <summary>Creates a new instance representing the specified System.Single value.</summary>
      internal Union32BitFloatingPointInteger(float value)
      {
        m_int32 = 0;
        m_single = value;
      }
    }
  }
}
