namespace Flux
{
  public static partial class Unicode
  {
    public static string ToStringEx(this char source)
      => $"{char.GetUnicodeCategory(source)} 0x{((int)source).ToString("X4")}";
  }
}
