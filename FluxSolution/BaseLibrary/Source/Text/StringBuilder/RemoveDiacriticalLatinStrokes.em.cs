namespace Flux
{
  public static partial class SystemTextStringBuilderEm
  {
    /// <summary>Remove diacritical (latin) strokes which are not covered by the normalization forms in NET.</summary>
    public static System.Text.StringBuilder RemoveDiacriticalLatinStrokes(this System.Text.StringBuilder source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      for (var index = 0; index < source.Length; index++)
        source[index] = SystemCharEm.RemoveDiacriticalLatinStroke(source[index]);

      return source;
    }
  }
}
