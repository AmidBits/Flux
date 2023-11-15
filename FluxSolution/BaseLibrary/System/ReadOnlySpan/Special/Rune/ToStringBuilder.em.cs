namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>Creates a new <see cref="System.Text.StringBuilder"/> from the source.</summary>
    public static System.Text.StringBuilder ToStringBuilder(this System.ReadOnlySpan<System.Text.Rune> source)
    {
      var sb = new System.Text.StringBuilder();

      for (var index = 0; index < source.Length; index++)
        sb.Append(source[index].ToString());

      return sb;
    }
  }
}
