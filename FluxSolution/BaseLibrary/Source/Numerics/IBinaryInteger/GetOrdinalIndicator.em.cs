//#if NET7_0_OR_GREATER
//namespace Flux
//{
//  public static partial class ExtensionMethods
//  {
//    public static string GetOrdinalIndicator<TSelf>(this TSelf source)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//      => (source % 10 is var d && d < 4 && source % 100 is var dd && (dd < 11 || dd > 13) ? d : 0) switch
//      {
//        0 => "th",
//        1 => "st",
//        2 => "nd",
//        3 => "rd",
//        _ => throw new System.IndexOutOfRangeException()
//      };
//  }
//}
//#endif
