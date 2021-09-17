//using System.Linq;

//namespace Flux.Model
//{
//  /// <summary>A specialized tokenization engine for math parsing.</summary>
//  public class MathTokenizer
//  {
//    #region Math Constants
//    public const string Label = nameof(Label);

//    public const string MathOperator = nameof(MathOperator);

//    public const string Number = nameof(Number);
//    public const string NumberDecimal = nameof(NumberDecimal);
//    public const string NumberInteger = nameof(NumberInteger);
//    public const string NumberScientific = nameof(NumberScientific);

//    public const string Parenthesis = nameof(Parenthesis);
//    public const string ParenthesisComma = @",";
//    public const string ParenthesisDepth = nameof(ParenthesisDepth);
//    public const string ParenthesisGroup = nameof(ParenthesisGroup);
//    public const string ParenthesisLeft = @"(";
//    public const string ParenthesisRight = @")";

//    public const string Whitespace = nameof(Whitespace);

//    public const string RegexLabel = @"^[\p{L}][\p{L}\p{Nd}_]*";

//    public const string RegexMathOperator4 = @"[\*\/\+\-]";
//    public const string RegexMathOperator6 = @"[\*\/\+\-\%\^]";

//    public const string RegexNumber = @"(" + RegexNumberScientific + @"|" + RegexNumberDecimal + @"|" + RegexNumberInteger + @")";
//    public const string RegexNumberDecimal = @"^-?(\d*\.\d+|\d+\.\d*)";
//    public const string RegexNumberInteger = @"^-?\d+";
//    public const string RegexNumberScientific = @"^(-?(\d*\.\d+|\d+\.\d*|\d+)[Ee][+-]\d+)";

//    public const string RegexParenthesisWithComma = @"^[\(\,\)]";
//    public const string RegexParenthesisWithoutComma = @"^[\(\)]";

//    public const string RegexWhitespace = @"^\s+";
//    #endregion

//    private Text.Tokenizer _tokenizer = new Text.Tokenizer();

//    public void SetRecognized(bool multipleParameters, bool extendedMathOperators)
//    {
//      _tokenizer.SetRecognized(new System.Collections.Generic.Dictionary<string, string>()
//      {
//        { Whitespace, RegexWhitespace },
//        { Label, RegexLabel },
//        { Number, RegexNumber },
//        { Parenthesis, multipleParameters?RegexParenthesisWithComma:RegexParenthesisWithoutComma },
//        { MathOperator, extendedMathOperators?RegexMathOperator6 :RegexMathOperator4}
//      });
//    }

//    public System.Collections.Generic.IEnumerable<Flux.Text.Token> GetTokens(string expression)
//    {
//      var parenthesisDepth = 0;
//      var parenthesisGroup = 0;
//      var parenthesisGroups = new System.Collections.Generic.Stack<int>();

//      foreach (var token in _tokenizer.GetTokens(expression))
//      {
//        if (token.Name.Equals(Parenthesis))
//        {
//          if (token.Text.Equals(ParenthesisLeft))
//          {
//            parenthesisGroups.Push(++parenthesisGroup);
//            token[ParenthesisGroup] = parenthesisGroups.Peek();
//            token[ParenthesisDepth] = ++parenthesisDepth;
//          }
//          else if (token.Text.Equals(ParenthesisRight))
//          {
//            token[ParenthesisGroup] = parenthesisGroups.Count > 0 ? parenthesisGroups.Pop() : -1;
//            token[ParenthesisDepth] = parenthesisDepth--;
//          }
//          else if (token.Text.Equals(ParenthesisComma))
//          {
//            token[ParenthesisGroup] = parenthesisGroups.Count > 0 ? parenthesisGroups.Peek() : 0;
//            token[ParenthesisDepth] = parenthesisDepth;
//          }
//        }

//        yield return token;
//      }
//    }

//    public static System.Collections.Generic.IEnumerable<Flux.Text.Token> GetUnbalancedParenthesis(System.Collections.Generic.IEnumerable<Text.Token> tokens)
//    {
//      if (tokens.Where(t => t.Name.Equals(Parenthesis)).ToList() is System.Collections.Generic.List<Text.Token> ps && ps.Any())
//      {
//        var psl = ps.Where(t => t.Text.Equals(ParenthesisLeft));
//        var psr = ps.Where(t => t.Text.Equals(ParenthesisRight));

//        var pslmm = psl.Where(p1 => !psr.Where(p2 => p2[ParenthesisGroup].Equals(p1[ParenthesisGroup])).Any());
//        var psrmm = psr.Where(p1 => !psl.Where(p2 => p2[ParenthesisGroup].Equals(p1[ParenthesisGroup])).Any());

//        foreach (var token in pslmm.Union(psrmm))
//        {
//          yield return token;
//        }
//      }
//    }
//  }
//}
