namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Creates a new <see cref="Flux.SequenceBuilder{T}"/> of <see cref="char"/> with the data in <paramref name="source"/>.</summary>
    public static Flux.SequenceBuilder<char> ToSequenceBuilder(this System.ReadOnlySpan<char> source)
      => new Flux.SequenceBuilder<char>().Append(source);

    /// <summary>Creates a new <see cref="Flux.SequenceBuilder{T}"/> of <see cref="System.Text.Rune"/> with the data in <paramref name="source"/>.</summary>
    public static Flux.SequenceBuilder<System.Text.Rune> ToSequenceBuilder(this System.ReadOnlySpan<System.Text.Rune> source)
      => new Flux.SequenceBuilder<System.Text.Rune>().Append(source);
  }
}
