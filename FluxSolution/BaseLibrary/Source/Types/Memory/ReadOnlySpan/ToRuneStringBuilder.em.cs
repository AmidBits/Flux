namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Creates a new <see cref="RuneStringBuilder"/> from the source.</summary>
    public static RuneStringBuilder ToCharStringBuilder(this System.ReadOnlySpan<System.Text.Rune> source)
      => (RuneStringBuilder)new RuneStringBuilder().Append(source);
  }
}
