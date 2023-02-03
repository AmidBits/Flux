namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    public static SequenceBuilder<System.Text.Rune> InsertLine(this SequenceBuilder<System.Text.Rune> source, int index)
      => source.Insert(index, System.Environment.NewLine.AsSpan().ToSpanRune());

    public static SequenceBuilder<System.Text.Rune> InsertLine(this SequenceBuilder<System.Text.Rune> source, int index, System.Text.Rune value)
      => source.InsertLine(index).Insert(index, value);
  }
}
