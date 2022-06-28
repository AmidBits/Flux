//namespace Flux
//{
//  public static partial class ExtensionMethods
//  {
//    /// <summary>Returns the source replicated (copied) the specified number of times.</summary>
//    public static T[] Replicate<T>(this System.ReadOnlySpan<T> source, int count)
//    {
//      var target = new T[source.Length * (count + 1)];

//      for (var index = 0; index < count; index++)
//        source.CopyTo(target, count * index);

//      return target;
//    }
//  }
//}
