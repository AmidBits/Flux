namespace Flux
{
  #region Extension methods.
  public static partial class ExtensionMethods
  {
    public static void AppendLine(this SequenceBuilder<char> source, System.ReadOnlySpan<char> value)
    {
      source.Append(value);
      source.Append(System.Environment.NewLine);
    }

    public static void InsertLine(this SequenceBuilder<char> source, int index, System.ReadOnlySpan<char> value)
    {
      source.Insert(index, value);
      source.Insert(index + value.Length, System.Environment.NewLine);
    }

    /// <summary>Creates a new builder from the source.</summary>
    public static SequenceBuilder<T> ToSequenceBuilder<T>(this System.ReadOnlySpan<T> source)
      => new(source);

    /// <summary>Creates a new builder from the source.</summary>
    public static SequenceBuilder<T> ToSequenceBuilder<T>(this System.Span<T> source)
      => new(source);

    public static SequenceBuilder<char> ToSequenceBuilderOfChar(this SequenceBuilder<System.Text.Rune> source)
    {
      var target = new SequenceBuilder<char>();

      for (var index = 0; index < source.Length; index++)
        target.Append(source[index].ToString());

      return target;
    }

    public static SequenceBuilder<System.Text.Rune> ToSequenceBuilderOfRune(this SequenceBuilder<char> source)
    {
      var target = new SequenceBuilder<System.Text.Rune>();

      foreach (var rune in source.AsReadOnlySpan().EnumerateRunes())
        target.Append(rune);

      return target;
    }

    public static string ToString(this SequenceBuilder<System.Text.Rune> source, int startAt, int count)
      => source.AsReadOnlySpan().ToString(startAt, count);

    public static string ToString(this SequenceBuilder<char> source, int startAt, int count)
      => source.AsReadOnlySpan().ToString(startAt, count);
  }
  #endregion Extension methods.
}
