//namespace Flux
//{
//  public static partial class Enumerable
//  {
//    /// <summary>Returns a specified number of contiguos elements at the end of a sequence.</summary>
//    public static System.Collections.Generic.IEnumerable<T> TakeLast<T>(this System.Collections.Generic.IEnumerable<T> source, int count)
//    {
//      var buffer = new System.Collections.Generic.Queue<T>(count + 1);

//      foreach (var element in source)
//      {
//        buffer.Enqueue(element);

//        if (buffer.Count > count)
//        {
//          buffer.Dequeue();
//        }
//      }

//      return buffer;
//    }
//  }
//}
