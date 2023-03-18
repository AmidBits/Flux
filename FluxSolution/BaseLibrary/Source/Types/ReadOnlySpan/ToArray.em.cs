namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>Creates a new array from the source sequence, adding the number of specified pre-slots and post-slots.</summary>
    public static T[] ToArray<T>(this System.ReadOnlySpan<T> source, int preLength, int postLength)
    {
      var target = new T[preLength + source.Length + postLength];
      source.CopyTo(new System.Span<T>(target).Slice(preLength, source.Length));
      return target;
    }
  }
}
