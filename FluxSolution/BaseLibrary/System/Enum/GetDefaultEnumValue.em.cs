using System.Reflection;

namespace Flux
{
  public static partial class ExtensionMethodsDateTime
  {
    public static TEnum GetDefaultEnumValue<TEnum>()
      where TEnum : System.Enum
    {
      return (TEnum)GetDefaultEnumValue(typeof(TEnum));

      static object GetDefaultEnumValue(System.Type enumType)
      {
        if (enumType.GetCustomAttribute<System.ComponentModel.DefaultValueAttribute>(inherit: false) is var customAttribute && customAttribute != null)
          return customAttribute.Value!;

        if (System.Activator.CreateInstance(enumType.GetEnumUnderlyingType()) is var underlyingType && enumType.IsEnumDefined(underlyingType!))
          return underlyingType!;

        return enumType.GetEnumValues().GetValue(0)!;
      }
    }
  }
}
