namespace Flux
{
  public static partial class Fx
  {
    public static void MakeNumbersFixedLength(this ref SpanMaker<char> source, int length, char padding)
    {
      var sm = source;

      bool wasDigit = false;
      var digitCount = 0;

      for (var index = sm.Count - 1; index >= 0; index--)
      {
        var isDigit = char.IsDigit(sm[index]);

        if (!isDigit && wasDigit && digitCount < length)
          sm = sm.Insert(index + 1, length - digitCount, padding);
        else if (isDigit && !wasDigit)
          digitCount = 1;
        else
          digitCount++;

        wasDigit = isDigit;
      }

      if (wasDigit)
        sm = sm.Insert(0, length - digitCount, padding);

      source = sm;
    }
  }
}
