namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Creates a new builder from the source.</summary>
    public static SequenceBuilder<T> ToSequenceBuilder<T>(this System.ReadOnlySpan<T> source)
      where T : notnull
      => new SequenceBuilder<T>().Append(source);

    /// <summary>Creates a new builder from the source.</summary>
    public static SequenceBuilder<T> ToSequenceBuilder<T>(this System.Span<T> source)
      where T : notnull
      => new SequenceBuilder<T>().Append(source);
  }
}
