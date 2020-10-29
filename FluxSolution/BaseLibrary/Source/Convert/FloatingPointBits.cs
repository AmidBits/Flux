namespace Flux
{
  public static partial class Convert
  {
    /// <summary>Converts the specified signed integer to a floating point number. Note: the endianness of this converter does not affect the returned value.</summary>
    /// <param name="value">The integer <see cref="System.Int32"/> to convert. </param>
    /// <returns>A single-precision floating point number whose value is equivalent to value.</returns>
    public static float BitsFromInt32ToSingle(int value)
      => new StructuresOfBits32(value).FloatingPoint32;
    /// <summary>Converts the specified floating point number to a signed integer. Note: the endianness of this converter does not affect the returned value.</summary>
    /// <param name="value">The floating point <see cref="System.Single"/> to convert. </param>
    /// <returns>A 32-bit signed integer whose value is equivalent to value.</returns>
    public static int BitsFromSingleToInt32(float value)
      => new StructuresOfBits32(value).Integer32;

    /// <summary>Converts the specified floating point number to a signed integer. Note: the endianness of this converter does not affect the returned value.</summary>
    /// <param name="value">The floating point <see cref="System.Single"/> to convert. </param>
    /// <returns>A 32-bit signed integer whose value is equivalent to value.</returns>
    public static long BitsFromDoubleToInt64(double value)
      => new StructuresOfBits64(value).Integer64;
    /// <summary>Converts the specified signed integer to a floating point number. Note: the endianness of this converter does not affect the returned value.</summary>
    /// <param name="value">The integer <see cref="System.Int32"/> to convert. </param>
    /// <returns>A single-precision floating point number whose value is equivalent to value.</returns>
    public static double BitsFromInt64ToDouble(long value)
      => new StructuresOfBits64(value).FloatingPoint64;
  }
}
