namespace Flux
{
  public static partial class Xtensions
  {
    public static System.Text.StringBuilder MakeIntegersFixedLength(this System.Text.StringBuilder source, int minLength)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      bool wasDigit = false;
      var digitCount = 0;

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var isDigit = char.IsDigit(source[index]);

        if (!isDigit && wasDigit && digitCount < minLength)
          source.Insert(index + 1, @"0", minLength - digitCount);
        else if (isDigit && !wasDigit)
          digitCount = 1;
        else
          digitCount++;

        wasDigit = isDigit;
      }

      if (wasDigit) source.Insert(0, @"0", minLength - digitCount);

      return source;
    }
  }
}
