//namespace Flux
//{
//  public static partial class ExtensionMethodsSequenceBuilder
//  {
//    public static System.Collections.Generic.IEnumerable<Text.TextElementCluster> EnumerateTextElements(this SequenceBuilder<char> source)
//    {
//      var e = source.AsReadOnlySpan().EnumerateTextElements();

//      while (e.MoveNext())
//        yield return e.Current;

//      //var si = new System.Globalization.StringInfo(source.ToString());

//      //for (var index = 0; index < si.LengthInTextElements; index++)
//      //  yield return si.SubstringByTextElements(index);
//    }
//  }
//}
