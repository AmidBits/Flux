namespace Flux
{
  public static partial class ExtensionMethodsStringBuilder
  {
    public static int CountRunes(this System.Text.StringBuilder source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

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
