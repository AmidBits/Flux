namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    public static SpanBuilder<System.Text.Rune> MakeNumbersFixedLength(this SpanBuilder<System.Text.Rune> source, int length, System.Text.Rune padding)
    {
      bool wasDigit = false;
      var digitCount = 0;

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var isDigit = System.Text.Rune.IsDigit(source[index]);

        if (!isDigit && wasDigit && digitCount < length)
          source.Insert(index + digitCount, padding, length - digitCount);
        else if (isDigit && !wasDigit)
          digitCount = 1;
        else
          digitCount++;

        wasDigit = isDigit;
      }

      if (wasDigit) source.Insert(0, padding, length - digitCount);

      return source;
    }
  }
}
