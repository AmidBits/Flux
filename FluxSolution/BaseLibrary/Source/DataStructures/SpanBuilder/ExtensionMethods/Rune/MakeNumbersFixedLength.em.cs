namespace Flux
{
  public static partial class Fx
  {
    public static void MakeNumbersFixedLength(this ref SpanMaker<System.Text.Rune> source, int length, System.Text.Rune padding)
    {
      bool wasDigit = false;
      var digitCount = 0;

      for (var index = source.Count - 1; index >= 0; index--)
      {
        var isDigit = System.Text.Rune.IsDigit(source[index]);

        if (!isDigit && wasDigit && digitCount < length)
          source.Insert(index + 1, length - digitCount, padding);
        else if (isDigit && !wasDigit)
          digitCount = 1;
        else
          digitCount++;

        wasDigit = isDigit;
      }

      if (wasDigit) source.Insert(0, length - digitCount, padding);
    }
  }
}
