//using System.Linq;

///// <summary>A rune is a Unicode code point.</summary>
//namespace Flux
//{
//  namespace Text
//  {
//    public readonly record struct RuneTokenPuncuationQuote
//      : IToken<System.Text.Rune>, System.IFormattable
//    {
//      public int Index { get; }
//      public System.Text.Rune Value { get; }

//      public int? Depth { get; }
//      public int? Group { get; }

//      public RuneTokenPuncuationQuote(int index, System.Text.Rune rune, int depth, int group)
//      {
//        Index = index;
//        Value = rune;

//        Depth = depth;
//        Group = group;
//      }

//      public string ToString(string? format, IFormatProvider? formatProvider)
//        => string.Format(formatProvider, $"{{0}}{(format is null ? string.Empty : $":{format}")}", Value);

//      public string ToTokenString()
//        => $"{GetType().Name} {{ \"{Value}\", Index = {Index}, Chars = {Value.Utf16SequenceLength}:[{string.Join(", ", Value.ToString().Select(c => $"0x{(int)c:x4}"))}], Rune = {Value.ToStringEx()}, Depth = {Depth}, Group = {Group} }}";

//      public override string ToString()
//        => ToString(null, null);
//    }
//  }
//}