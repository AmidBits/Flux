namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Creates a new array from the source sequence, adding the number of specified pre-slots and post-slots.</summary>
    public static T[] ToArray<T>(this System.Span<T> source, int preLength, int postLength)
    {
      var array = new T[preLength + source.Length + postLength];
      source.CopyTo(new System.Span<T>(array).Slice(preLength, source.Length));
      return array;
    }
  }
}
