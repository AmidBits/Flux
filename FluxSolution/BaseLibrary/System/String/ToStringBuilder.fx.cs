namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new <see cref="System.Text.StringBuilder"/> with the <paramref name="source"/> as content.</summary>
    public static System.Text.StringBuilder ToStringBuilder(this string source) => new System.Text.StringBuilder(source);
  }
}
