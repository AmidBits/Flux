namespace Flux
{
  public static partial class FxType
  {
    ///// <summary>
    ///// <para>Determines whether the <typeparamref name="T"/> is a primitive (built-in) floating-point type.</para>
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="value"></param>
    ///// <returns></returns>
    //public static bool IsPrimitiveTypeOfFloatingPoint<T>(this T value)
    //  => (value?.GetType().IsPrimitive ?? false)
    //  && (value?.GetType().IsPrimitiveTypeOfFloatingPoint() ?? false);

    /// <summary>
    /// <para>Determines whether the <paramref name="type"/> is a primitive (built-in) floating-point type.</para>
    /// </summary>
    public static bool IsPrimitiveTypeOfFloatingPoint(this System.Type type)
      => type.IsPrimitive
      && (
        type.Equals(typeof(System.Decimal))
        || type.Equals(typeof(System.Double))
        || type.Equals(typeof(System.Half))
        || type.Equals(typeof(System.Runtime.InteropServices.NFloat))
        || type.Equals(typeof(System.Single))
      );
  }
}
