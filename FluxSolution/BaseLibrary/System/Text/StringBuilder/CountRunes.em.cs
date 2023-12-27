namespace Flux
{
  public static partial class Reflection
  {
    public static int CountRunes(this System.Text.StringBuilder source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var count = 0;

      for (var index = source.Length - 1; index >= 0; index--)
      {
        count++;

        var c = source[index];

        if (char.IsLowSurrogate(c))
          index--;
        else if (char.IsHighSurrogate(c))
          throw new System.InvalidOperationException();
      }

      return count;
    }
  }
}
