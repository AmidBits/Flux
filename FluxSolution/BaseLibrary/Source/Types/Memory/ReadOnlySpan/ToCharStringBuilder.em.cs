namespace Flux
{
  public static partial class ReadOnlySpanEm
  {
    /// <summary>Creates a new <see cref="CharStringBuilder"/> from the source.</summary>
    public static CharStringBuilder ToCharStringBuilder(this System.ReadOnlySpan<char> source)
      => (CharStringBuilder)new CharStringBuilder().Append(source);
  }
}