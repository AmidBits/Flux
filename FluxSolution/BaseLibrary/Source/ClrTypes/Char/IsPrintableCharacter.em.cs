namespace Flux
{
  public static partial class XtensionsChar
  {
    /// <summary>Indicates whether the char is a printable character.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/ASCII#Printable_characters"/>
    public static bool IsPrintableCharacter(this char source)
      => source >= '\u0020' && source <= '\u007E';
  }
}
