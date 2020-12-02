namespace Flux
{
  public static partial class EnumEm
  {
    /// <summary>Attempts to convert the source name string to an enum type (T).</summary>
    public static TEnum ToEnumFromString<TEnum>(this string source)
      where TEnum : System.Enum
      => (TEnum)new System.ComponentModel.EnumConverter(typeof(TEnum)).ConvertFromString(source);
  }
}
