//namespace Flux
//{
//  public static partial class ExtensionMethods
//  {
//    public static void MakeNumbersFixedLength(ref this SpanBuilder<char> source, int length)
//    {
//      bool wasDigit = false;
//      var digitCount = 0;

//      for (var index = source.Length - 1; index >= 0; index--)
//      {
//        var isDigit = char.IsDigit(source[index]);

//        if (!isDigit && wasDigit && digitCount < length)
//          source.Insert(index + digitCount, '0', length - digitCount);
//        else if (isDigit && !wasDigit)
//          digitCount = 1;
//        else
//          digitCount++;

//        wasDigit = isDigit;
//      }

//      if (wasDigit) source.Insert(0, '0', length - digitCount);
//    }
//  }
//}
