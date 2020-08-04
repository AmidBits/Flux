//namespace Flux
//{
//  public static partial class ExtensionMethodsReadOnlySpan
//  {
//    /// <summary>Loops through each element and performs an action.</summary>
//    public static void ForEach<T>(this System.ReadOnlySpan<T> source, System.Action<T> action)
//    {
//      var sourceLength = source.Length;

//      for (var index = 0; index < sourceLength; index++)
//        action(source[index]);
//    }
//    /// <summary>Loops through each element and performs an action.</summary>
//    public static void ForEach<T>(this System.ReadOnlySpan<T> source, System.Action<T, int> action)
//    {
//      var sourceLength = source.Length;

//      for (var index = 0; index < sourceLength; index++)
//        action(source[index], index);
//    }
//  }
//}
