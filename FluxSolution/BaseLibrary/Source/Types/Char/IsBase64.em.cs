namespace Flux
{
  public static partial class SystemCharEm
  {
    public static bool IsBase64(this char source, bool acceptPadding)
      => (source >= '0' && source <= '9') || (source >= 'A' && source <= 'Z') || (source >= 'a' && source <= 'z') || source == '+' || source == '/' || (acceptPadding && source == '=');
  }
}
