using System.Linq;

namespace Flux
{
  public static partial class SystemTextStringBuilderEm
  {
    /// <summary>Returns the string builder with the specified characters doubled throughout. If no characters are specified, all characters are doubled.</summary>
    public static System.Text.StringBuilder Geminate(this System.Text.StringBuilder source, params char[] characters)
      => Replicate(source, 1, characters);
  }
}
