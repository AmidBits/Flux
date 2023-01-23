namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    public static void AppendLine(this SequenceBuilder<char> source, System.ReadOnlySpan<char> value)
    {
      source.Append(value);
      source.Append(System.Environment.NewLine.AsSpan());
    }
  }
}
