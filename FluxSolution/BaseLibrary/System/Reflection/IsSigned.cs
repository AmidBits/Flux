using System.Reflection;
using System.Runtime.InteropServices;

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

    public static bool IsSignedNumber2<TSelf>(this TSelf source)
    {
      var type = source?.GetType();
      var property = type?.GetProperty("NegativeOne", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
      if (property is null) property = type?.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public).FirstOrDefault(dp => dp.Name.EndsWith(".NegativeOne"));
      var value = (property?.GetValue(null) ?? 0);
      return !0.Equals(value);
    }

    public static bool IsSignedNumber<TSelf>(this TSelf source)
      //=> !0.Equals(System.Array.Find(source?.GetType().GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public) ?? System.Array.Empty<System.Reflection.PropertyInfo>(), dp => dp.Name.Equals("NegativeOne") || dp.Name.EndsWith(".NegativeOne"))?.GetValue(null) ?? 0);
      => !0.Equals(source?.GetType().GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public).FirstOrDefault(dp => dp.Name.Equals("NegativeOne") || dp.Name.EndsWith(".NegativeOne"))?.GetValue(null) ?? 0);
    //=> !0.Equals(source?.GetType().GetTypeInfo()?.DeclaredProperties.FirstOrDefault(dp => dp.Name.Equals("NegativeOne") || dp.Name.EndsWith(".NegativeOne"))?.GetValue(null) ?? 0);

    //public static bool IsSignedNumber<TSelf>(this TSelf source)
    //  => source is System.Decimal
    //  || source is System.Double || source is System.Half
    //  || source is System.Int16 || source is System.Int32 || source is System.Int64 || source is System.Int128
    //  || source is System.IntPtr
    //  || source is System.Runtime.InteropServices.NFloat
    //  || source is System.SByte
    //  || source is System.Single;

    //public static bool IsUnsignedNumber<TSelf>(this TSelf source)
    //  => source is System.Byte
    //  || source is System.Char
    //  || source is System.UInt16 || source is System.UInt32 || source is System.UInt64 || source is System.UInt128
    //  || source is System.UIntPtr;
  }
}
