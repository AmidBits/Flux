namespace Flux
{
  public static partial class Convert
  {
    /// <summary>Converts the specified single-precision floating point number to a 32-bit signed integer. Note: the endianness of this converter does not affect the returned value.</summary>
    /// <param name="value">The number to convert. </param>
    /// <returns>A 32-bit signed integer whose value is equivalent to value.</returns>
    public static int SingleToInt32Bits(float value) 
      => new SingleToInt32BitsUnion(value).AsInt32;

    /// <summary>Union used solely for the equivalent of DoubleToInt64Bits and vice versa.</summary>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
    private struct SingleToInt32BitsUnion
    {
      [System.Runtime.InteropServices.FieldOffset(0)]
      private float m_single;
      /// <summary>The value in System.Single form.</summary>
      internal float AsSingle 
        => m_single;

      [System.Runtime.InteropServices.FieldOffset(0)]
      private int m_int32;
      /// <summary>The value in System.Int32 form.</summary>
      internal int AsInt32 
        => m_int32;

      /// <summary>Creates a new instance representing the specified System.Single value.</summary>
      internal SingleToInt32BitsUnion(float value)
      {
        m_int32 = 0;
        m_single = value;
      }

      /// <summary>Creates a new instance representing the specified System.Int32 value.</summary>
      internal SingleToInt32BitsUnion(int value)
      {
        m_single = 0F;
        m_int32 = value;
      }
    }
  }
}
