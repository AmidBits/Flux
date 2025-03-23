namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Determines whether the <typeparamref name="T"/> is a primitive (built-in) integer type.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsPrimitiveTypeOfInteger<T>(this T value)
      => (value?.GetType().IsPrimitive ?? false)
      && (value?.GetType().IsPrimitiveTypeOfInteger() ?? false);

    /// <summary>
    /// <para>Determines whether the <paramref name="type"/> is aprimitive (built-in) integer type.</para>
    /// </summary>
    public static bool IsPrimitiveTypeOfInteger(this System.Type type)
      => type == typeof(System.Byte)
      || type == typeof(System.Char)
      || type == typeof(System.Int128)
      || type == typeof(System.Int16)
      || type == typeof(System.Int32)
      || type == typeof(System.Int64)
      || type == typeof(System.IntPtr)
      || type == typeof(System.Numerics.BigInteger)
      || type == typeof(System.SByte)
      || type == typeof(System.UInt128)
      || type == typeof(System.UInt16)
      || type == typeof(System.UInt32)
      || type == typeof(System.UInt64)
      || type == typeof(System.UIntPtr);
  }
}
