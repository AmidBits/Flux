namespace Flux
{
  public static partial class FxReflection
  {
    /// <summary>
    /// <para>Determines whether the <paramref name="source"/> type is a signed integer.</para>
    /// <see href="https://stackoverflow.com/a/13609383/3178666"/>
    /// </summary>
    /// <remarks>Constrained to generic math binary integers.</remarks>
    public static bool IsSigned<TSelf>(this TSelf source)
      where TSelf : System.Numerics.INumber<TSelf>
      => source.GetType() is var type && System.Convert.ToBoolean(type.GetField("MinValue")?.GetValue(null) ?? type.IsAssignableToGenericType(typeof(System.Numerics.ISignedNumber<>)));
  }
}
