namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Determines whether the source collection contains all of the elements in the specified target sequence.</summary>
    public static bool ContainsAll<T>(this System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
    {
      foreach (var t in target)
        if (!source.Contains(t))
          return false;

      return true;
    }
  }
}