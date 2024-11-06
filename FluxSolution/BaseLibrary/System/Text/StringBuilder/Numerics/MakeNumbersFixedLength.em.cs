namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Make all numeric groups be of at least <paramref name="fixedLength"/> in <paramref name="source"/> from <paramref name="startIndex"/> and <paramref name="count"/> characters.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="fixedLength"></param>
    /// <param name="startIndex"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public static System.Text.StringBuilder MakeNumbersFixedLength(this System.Text.StringBuilder source, int fixedLength, int startIndex, int count)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      bool wasDigit = false;
      var digitCount = 0;

      for (var i = startIndex + count - 1; i >= startIndex; i--)
      {
        var isDigit = char.IsDigit(source[i]);

        if (!isDigit && wasDigit && digitCount < fixedLength)
          source.Insert(i + 1, @"0", fixedLength - digitCount);
        else if (isDigit && !wasDigit)
          digitCount = 1;
        else
          digitCount++;

        wasDigit = isDigit;
      }

      if (wasDigit) source.Insert(0, @"0", fixedLength - digitCount);

      return source;
    }
  }
}
