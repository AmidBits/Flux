namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new <see cref="System.Text.StringBuilder"/> with the <paramref name="source"/> as content.</summary>
    public static System.Text.StringBuilder ToStringBuilder(this string source, int additionalCapacity = 0) => new(source, source.Length + additionalCapacity);
  }
}
