namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new <see cref="System.Text.StringBuilder"/> from the source.</summary>
    public static System.Text.StringBuilder ToStringBuilder(this System.ReadOnlySpan<char> source)
      => new System.Text.StringBuilder().Append(source);
  }
}
