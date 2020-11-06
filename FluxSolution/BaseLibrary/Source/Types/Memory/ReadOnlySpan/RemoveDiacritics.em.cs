namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Remove diacritical (latin) strokes which are not covered by the normalization forms in NET.</summary>
    public static char[] RemoveDiacriticalLatinStrokes(this System.ReadOnlySpan<char> source)
    {
      var target = source.ToArray();
      RemoveDiacriticalLatinStrokes(target);
      return target;
    }

    /// <summary>Remove diacritical marks and any optional replacements desired.</summary>
    public static System.ReadOnlySpan<char> RemoveDiacriticalMarks(this System.ReadOnlySpan<char> source, System.Func<char, char> additionalCharacterReplacements)
      => RemoveDiacriticalMarks(source.ToArray(), additionalCharacterReplacements);

    /// <summary>Remove diacritical marks and latin strokes (the latter are unaffected by normalization forms in NET).</summary>
    public static System.ReadOnlySpan<char> RemoveDiacriticalMarksAndLatinStrokes(this System.ReadOnlySpan<char> source)
      => RemoveDiacriticalMarks(source.ToArray(), RemoveDiacriticalLatinStroke);
  }
}
