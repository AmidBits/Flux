namespace Flux
{
  public static partial class ExtensionMethods
  {
#if !NETCOREAPP
    /// <summary>Returns all elements in a sequence except a specified number of elements at the end.</summary>
    internal static System.Collections.Generic.IEnumerable<T> SkipLast<T>(this System.Collections.Generic.IEnumerable<T> source, int count)
    {
      var buffer = new System.Collections.Generic.Queue<T>(count + 1);

      foreach (var element in source)
      {
        buffer.Enqueue(element);

        if (buffer.Count > count)
        {
          yield return buffer.Dequeue();
        }
      }
    }
#endif
  }
}
