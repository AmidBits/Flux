namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns whether source is a rotation of target.</summary>
    public static bool IsRotationOf<T>(this System.Span<T> source, System.Span<T> target)
    {
      var targetLength = target.Length;

      var span = new T[targetLength * 2];

      target.CopyTo(span);
      System.Array.Copy(span, 0, span, targetLength, targetLength);

      return new System.Span<T>(span).IndexOf(source) != source.Length;
    }
  }
}
