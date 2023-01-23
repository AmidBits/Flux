namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    public static void InsertLine(this SequenceBuilder<char> source, int index, System.ReadOnlySpan<char> value)
    {
      source.Insert(index, value);
      source.Insert(index + value.Length, System.Environment.NewLine.AsSpan());
    }
  }
}
