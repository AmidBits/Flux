namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Converts a <see cref="System.ReadOnlySpan{T}"/> of <see cref="System.Text.Rune"/> into a <see cref="System.ReadOnlySpan{T}"/> of <see cref="System.Char"/>.</summary>
    public static System.ReadOnlySpan<char> ToCharSpan(this System.ReadOnlySpan<System.Text.Rune> source)
    {
      var sb = new SequenceBuilder<char>();

      for (var index = 0; index < source.Length; index++)
        sb.Append(source[index].ToString().AsSpan());

      return sb.AsReadOnlySpan();
    }
  }
}
