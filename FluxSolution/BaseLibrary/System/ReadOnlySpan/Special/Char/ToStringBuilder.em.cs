namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new <see cref="System.Text.StringBuilder"/> with the {<paramref name="prepend"/> + <paramref name="source"/>} as content.</summary>
    public static System.Text.StringBuilder ToStringBuilder(this System.ReadOnlySpan<char> source, string? prepend = null) => new System.Text.StringBuilder(prepend).Append(source);
  }
}
