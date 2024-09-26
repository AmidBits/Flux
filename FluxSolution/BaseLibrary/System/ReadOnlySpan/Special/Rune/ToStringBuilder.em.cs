namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new <see cref="System.Text.StringBuilder"/> from the <paramref name="source"/> as the content.</summary>
    public static System.Text.StringBuilder ToStringBuilder(this System.ReadOnlySpan<System.Text.Rune> source, string? prepend = null)
    {
      var sb = new System.Text.StringBuilder(prepend);

      for (var index = 0; index < source.Length; index++)
        sb.Append(source[index].ToString());

      return sb;
    }
  }
}
