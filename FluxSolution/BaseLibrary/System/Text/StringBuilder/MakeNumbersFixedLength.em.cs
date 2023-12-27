namespace Flux
{
  public static partial class Reflection
  {
    public static System.Text.StringBuilder MakeNumbersFixedLength(this System.Text.StringBuilder source, int length)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      bool wasDigit = false;
      var digitCount = 0;

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var isDigit = char.IsDigit(source[index]);

        if (!isDigit && wasDigit && digitCount < length)
          source.Insert(index + 1, @"0", length - digitCount);
        else if (isDigit && !wasDigit)
          digitCount = 1;
        else
          digitCount++;

        wasDigit = isDigit;
      }

      if (wasDigit) source.Insert(0, @"0", length - digitCount);

      return source;
    }
  }
}
