namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    /// <summary>Creates a new builder from the source.</summary>
    public static SequenceBuilder<T> ToSequenceBuilder<T>(this System.ReadOnlySpan<T> source)
      => new(source);

    /// <summary>Creates a new builder from the source.</summary>
    public static SequenceBuilder<T> ToSequenceBuilder<T>(this System.Span<T> source)
      => new(source);
  }
}
