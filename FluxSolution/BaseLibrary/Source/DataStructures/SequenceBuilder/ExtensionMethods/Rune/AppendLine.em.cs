namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    public static SequenceBuilder<System.Text.Rune> AppendLine(this SequenceBuilder<System.Text.Rune> source)
      => source.Append(System.Environment.NewLine.Select(c => (System.Text.Rune)c));
    public static SequenceBuilder<System.Text.Rune> AppendLine(this SequenceBuilder<System.Text.Rune> source, System.Text.Rune value)
      => source.Append(value).AppendLine();
    public static SequenceBuilder<System.Text.Rune> AppendLine(this SequenceBuilder<System.Text.Rune> source, System.ReadOnlySpan<System.Text.Rune> value)
      => source.Append(value).AppendLine();
  }
}
