namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string ToStringEx(this char source)
      => $"{char.GetUnicodeCategory(source)} 0x{(int)source:X4}";
  }
}
