//using System.Linq;

//namespace Flux.Text.Tokenizer
//{
//  // https://en.wikipedia.org/wiki/Lexical_analysis

//  /// <summary>A generic implementation of a demarcated and classified section of characters.</summary>
//  /// <see cref="https://en.wikipedia.org/wiki/Lexical_analysis#Tokenization"/>
//  public class Token
//  {
//    public int Index;

//    public string Name, Text;

//    public Token(string name, string text, int index = -1)
//    {
//      Index = index;

//      Name = name;
//      Text = text;
//    }
//    public Token(Token token)
//      : this(token.Name, token.Text, token.Index) { }

//    public override string ToString() => $"{Name}=\"{Text}\"@{Index}";
//  }

//  /// <summary>A generic implementation of a regex tokenization engine to demarcate and classify sections of a string of input characters.</summary>
//  /// <see cref="https://en.wikipedia.org/wiki/Lexical_analysis#Tokenization"/>
//  public class Tokenizer
//  {
//    public const string Unrecognized = nameof(Unrecognized);

//    public System.Collections.Generic.Dictionary<string, System.Text.RegularExpressions.Regex> Recognized { get; set; } = new System.Collections.Generic.Dictionary<string, System.Text.RegularExpressions.Regex>();

//    public virtual System.Collections.Generic.IEnumerable<Token> GetTokens(string expression)
//    {
//      for (var next = GetNextToken(expression); next.token != null; next = GetNextToken(expression.Substring(next.token.Index + next.token.Text.Length)))
//      {
//        yield return next.token;
//      }

//      (Token token, string remainder) GetNextToken(string remainder)
//      {
//        foreach (var kvp in Recognized)
//        {
//          if (kvp.Value.IsMatch(remainder))
//          {
//            return (new Token(kvp.Key, kvp.Value.Match(remainder).Value, expression.Length - remainder.Length), kvp.Value.Replace(remainder, string.Empty));
//          }
//        }

//        if (remainder.Length > 0)
//        {
//          return (new Token(Unrecognized, remainder.Substring(0, 1), expression.Length - remainder.Length), remainder.Substring(1));
//        }

//        return (null, null);
//      }
//    }

//    public void SetRecognized(System.Collections.Generic.Dictionary<string, string> recognized)
//    {
//      foreach (var kvp in recognized)
//      {
//        Recognized.Add(kvp.Key, new System.Text.RegularExpressions.Regex(kvp.Value[0] == '^' ? kvp.Value : '^' + kvp.Value));
//      }
//    }
//  }

//  #region UnicodeCategoryTokenizer functionality
//  public class UnicodeCategoryToken : Token
//  {
//    public UnicodeCategoryToken(string name, string text, int index = -1)
//      : base(name, text, index) { }
//    public UnicodeCategoryToken(Token token)
//      : base(token) { }
//  }

//  public class UnicodeCategoryLetterToken : UnicodeCategoryToken
//  {
//    public UnicodeCategoryLetterToken(Token token)
//      : base(token) { }
//  }

//  public class UnicodeCategoryMarkToken : UnicodeCategoryToken
//  {
//    public UnicodeCategoryMarkToken(Token token)
//      : base(token) { }
//  }

//  public class UnicodeCategoryNumberToken : UnicodeCategoryToken
//  {
//    public UnicodeCategoryNumberToken(Token token)
//      : base(token) { }
//  }

//  public class UnicodeCategoryOtherToken : UnicodeCategoryToken
//  {
//    public UnicodeCategoryOtherToken(Token token)
//      : base(token) { }
//  }

//  public class UnicodeCategoryPunctuationToken : UnicodeCategoryToken
//  {
//    public int Depth;
//    public int Group;

//    public UnicodeCategoryPunctuationToken(Token token, int depth, int group)
//      : base(token)
//    {
//      Depth = depth;
//      Group = group;
//    }

//    public override string ToString() { return $"{{{base.ToString()},{nameof(Depth)}=\"{Depth}\",{nameof(Group)}=\"{Group}\"}}"; }
//  }

//  public class UnicodeCategorySeparatorToken : UnicodeCategoryToken
//  {
//    public UnicodeCategorySeparatorToken(Token token)
//      : base(token) { }
//  }

//  public class UnicodeCategorySymbolToken : UnicodeCategoryToken
//  {
//    public UnicodeCategorySymbolToken(Token token)
//      : base(token) { }
//  }

//  /// <summary>A specialized tokenization engine by using regex unicode sub-categories as a token breakdown.</summary>
//  /// <see cref="http://www.regular-expressions.info/unicode.html"/>
//  public class UnicodeCategoryTokenizer
//  {
//    #region Unicode Category Constants
//    public const string LowercaseLetter = @"^\p{Ll}";
//    public const string UppercaseLetter = @"^\p{Lu}";
//    public const string TitlecaseLetter = @"^\p{Lt}";
//    //public const string CasedLetter = @"^\p{L&}";
//    public const string ModifierLetter = @"^\p{Lm}";
//    public const string OtherLetter = @"^\p{Lo}";
//    public const string Letter = @"^\p{L}";

//    public const string NonSpacingMark = @"^\p{Mn}";
//    public const string SpacingCombiningMark = @"^\p{Mc}";
//    public const string EnclosingMark = @"^\p{Me}";
//    public const string Mark = @"^\p{M}";

//    public const string DecimalDigitNumber = @"^\p{Nd}";
//    public const string LetterNumber = @"^\p{Nl}";
//    public const string OtherNumber = @"^\p{No}";
//    public const string Number = @"^\p{N}";

//    public const string ControlOther = @"^\p{Cc}";
//    public const string FormatOther = @"^\p{Cf}";
//    public const string PrivateUseOther = @"^\p{Co}";
//    public const string SurrogateOther = @"^\p{Cs}";
//    public const string UnassignedOther = @"^\p{Cn}";
//    public const string Other = @"^\p{C}";

//    public const string DashPunctuation = @"^\p{Pd}";
//    public const string OpenPunctuation = @"^\p{Ps}";
//    public const string ClosePunctuation = @"^\p{Pe}";
//    public const string InitialPunctuation = @"^\p{Pi}";
//    public const string FinalPunctuation = @"^\p{Pf}";
//    public const string ConnectorPunctuation = @"^\p{Pc}";
//    public const string OtherPunctuation = @"^\p{Po}";
//    public const string Punctuation = @"^\p{P}";

//    public const string SpaceSeparator = @"^\p{Zs}";
//    public const string LineSeparator = @"^\p{Zl}";
//    public const string ParagraphSeparator = @"^\p{Zp}";
//    public const string Separator = @"^\p{Z}";

//    public const string MathSymbol = @"^\p{Sm}";
//    public const string CurrencySymbol = @"^\p{Sc}";
//    public const string ModifierSymbol = @"^\p{Sk}";
//    public const string OtherSymbol = @"^\p{So}";
//    public const string Symbol = @"^\p{S}";
//    #endregion

//    private Tokenizer _tokenizer;

//    public UnicodeCategoryTokenizer()
//    {
//      _tokenizer = new Tokenizer();

//      _tokenizer.SetRecognized(new System.Collections.Generic.Dictionary<string, string>()
//      {
//        { nameof(LowercaseLetter), LowercaseLetter },
//        { nameof(UppercaseLetter), UppercaseLetter },
//        { nameof(TitlecaseLetter), TitlecaseLetter },
//				//{ nameof(CasedLetter), CasedLetter },
//				{ nameof(ModifierLetter), ModifierLetter },
//        { nameof(OtherLetter), OtherLetter },
//        { nameof(Letter), Letter },

//        { nameof(NonSpacingMark), NonSpacingMark },
//        { nameof(SpacingCombiningMark), SpacingCombiningMark },
//        { nameof(EnclosingMark), EnclosingMark },
//        { nameof(Mark),Mark },

//        { nameof(SpaceSeparator), SpaceSeparator },
//        { nameof(LineSeparator), LineSeparator },
//        { nameof(ParagraphSeparator), ParagraphSeparator },
//        { nameof(Separator), Separator },

//        { nameof(MathSymbol), MathSymbol },
//        { nameof(CurrencySymbol), CurrencySymbol },
//        { nameof(ModifierSymbol), ModifierSymbol },
//        { nameof(OtherSymbol), OtherSymbol },
//        { nameof(Symbol), Symbol },

//        { nameof(DecimalDigitNumber), DecimalDigitNumber },
//        { nameof(LetterNumber), LetterNumber },
//        { nameof(OtherNumber), OtherNumber },
//        { nameof(Number), Number },

//        { nameof(DashPunctuation), DashPunctuation },
//        { nameof(OpenPunctuation), OpenPunctuation },
//        { nameof(ClosePunctuation), ClosePunctuation },
//        { nameof(InitialPunctuation), InitialPunctuation },
//        { nameof(FinalPunctuation), FinalPunctuation },
//        { nameof(ConnectorPunctuation), ConnectorPunctuation },
//        { nameof(OtherPunctuation), OtherPunctuation },
//        { nameof(Punctuation), Punctuation },

//        { nameof(ControlOther), ControlOther },
//        { nameof(FormatOther), FormatOther },
//        { nameof(PrivateUseOther), PrivateUseOther },
//        { nameof(SurrogateOther), SurrogateOther },
//        { nameof(UnassignedOther), UnassignedOther },
//        { nameof(Other), Other }
//      });
//    }

//    public virtual System.Collections.Generic.IEnumerable<UnicodeCategoryToken> GetTokens(string expression)
//    {
//      var punctuationDepth = 0;
//      var punctuationGroup = 0;
//      var punctuationGroups = new System.Collections.Generic.Stack<int>();

//      foreach (var token in _tokenizer.GetTokens(expression))
//      {
//        switch (token)
//        {
//          case Token t when t.Name.EndsWith(nameof(Letter)):
//            yield return new UnicodeCategoryLetterToken(t);
//            break;
//          case Token t when t.Name.EndsWith(nameof(Mark)):
//            yield return new UnicodeCategoryMarkToken(t);
//            break;
//          case Token t when t.Name.EndsWith(nameof(Number)):
//            yield return new UnicodeCategorySeparatorToken(t);
//            break;
//          case Token t when t.Name.EndsWith(nameof(Other)):
//            yield return new UnicodeCategorySymbolToken(t);
//            break;
//          case Token t when t.Name.Equals(nameof(OpenPunctuation)):
//            punctuationGroups.Push(++punctuationGroup);
//            yield return new UnicodeCategoryPunctuationToken(t, ++punctuationDepth, punctuationGroups.Peek());
//            break;
//          case Token t when t.Name.Equals(nameof(ClosePunctuation)):
//            yield return new UnicodeCategoryPunctuationToken(t, punctuationDepth--, punctuationGroups.Count > 0 ? punctuationGroups.Pop() : -1);
//            break;
//          case Token t when t.Name.EndsWith(nameof(Punctuation)):
//            yield return new UnicodeCategoryPunctuationToken(t, punctuationDepth, punctuationGroups.Count > 0 ? punctuationGroups.Peek() : 0);
//            break;
//          case Token t when t.Name.EndsWith(nameof(Separator)):
//            yield return new UnicodeCategorySeparatorToken(t);
//            break;
//          case Token t when t.Name.EndsWith(nameof(Symbol)):
//            yield return new UnicodeCategorySymbolToken(t);
//            break;
//          default:
//            yield return new UnicodeCategoryToken(token.Name, token.Text);
//            break;
//        }
//      }
//    }
//  }
//  #endregion UnicodeCategoryTokenizer functionality

//  #region MathTokenizer functionality
//  public class MathToken : Token
//  {
//    public MathToken(string name, string text, int index = -1)
//      : base(name, text, index) { }
//    public MathToken(Token token)
//      : base(token) { }
//  }

//  public class MathLabelToken : MathToken
//  {
//    public const string Label = nameof(Label);
//    public const string Regex = @"^[\p{L}][\p{L}\p{Nd}_]*";

//    public MathLabelToken(Token token)
//      : base(token) { }
//  }

//  public class MathNumberToken : MathToken
//  {
//    public const string Number = nameof(Number);
//    public const string Regex = @"^(?=\d|\.\d)\d*(\.\d*)?([Ee]([+-]?\d+))?";

//    public double Value;

//    public MathNumberToken(string name, string text, int index)
//      : base(name, text, index)
//    {
//      Value = double.Parse(Text);
//    }
//    public MathNumberToken(Token token)
//      : this(token.Name, token.Text, token.Index) { }

//    public override string ToString() => $"{base.ToString()},{nameof(Value)}=\"{Value}\"";
//  }

//  public class MathOperatorToken : MathToken
//  {
//    public const string AssociativityLeft = @"Left";
//    public const string AssociativityRight = @"Right";
//    public const string Operator = nameof(Operator);
//    public const string Regex4 = @"^[\u00F7\u2212\u00D7\u002B]";
//    public const string Regex6 = @"^[\u00F7\u2212\%\u00D7\u002B\^]";
//    public const string SymbolAdd = "\u002B";
//    public const string SymbolDivide = "\u00F7";
//    public const string SymbolModulo = @"%";
//    public const string SymbolMultiply = "\u00D7";
//    public const string SymbolPower = @"^";
//    public const string SymbolSubtract = "\u2212";

//    public string Associativity;
//    public int Precedence;

//    public MathOperatorToken(Token token)
//      : base(token)
//    {
//      switch (Text)
//      {
//        case SymbolPower:
//          Associativity = MathOperatorToken.AssociativityRight;
//          Precedence = 4;
//          break;
//        case SymbolDivide:
//        case SymbolModulo:
//        case SymbolMultiply:
//          Associativity = MathOperatorToken.AssociativityLeft;
//          Precedence = 3;
//          break;
//        case SymbolAdd:
//        case SymbolSubtract:
//          Associativity = MathOperatorToken.AssociativityLeft;
//          Precedence = 2;
//          break;
//        default:
//          Associativity = MathOperatorToken.AssociativityLeft;
//          Precedence = 0;
//          break;
//      }
//    }

//    public MathToken Evaluate(MathToken left, MathToken right)
//    {
//      switch (Text)
//      {
//        case SymbolAdd:
//          return new MathNumberToken(MathNumberToken.Number, (double.Parse(left.Text) + double.Parse(right.Text)).ToString(), left.Index);
//        case SymbolDivide:
//          return new MathNumberToken(MathNumberToken.Number, (double.Parse(left.Text) / double.Parse(right.Text)).ToString(), left.Index);
//        case SymbolMultiply:
//          return new MathNumberToken(MathNumberToken.Number, (double.Parse(left.Text) * double.Parse(right.Text)).ToString(), left.Index);
//        case SymbolModulo:
//          return new MathNumberToken(MathNumberToken.Number, (double.Parse(left.Text) % double.Parse(right.Text)).ToString(), left.Index);
//        case SymbolPower:
//          return new MathNumberToken(MathNumberToken.Number, System.Math.Pow(double.Parse(left.Text), double.Parse(right.Text)).ToString(), left.Index);
//        case SymbolSubtract:
//          return new MathNumberToken(MathNumberToken.Number, (double.Parse(left.Text) - double.Parse(right.Text)).ToString(), left.Index);
//        default:
//          throw new System.ArithmeticException();
//      }
//    }

//    public override string ToString() => $"{base.ToString()},{nameof(Associativity)}=\"{Associativity}\",{nameof(Precedence)}=\"{Precedence}\"";
//  }

//  public class MathParenthesisToken : MathToken
//  {
//    public const string Parenthesis = nameof(Parenthesis);
//    public const string RegexWithComma = @"^[\(\,\)]";
//    public const string RegexWithoutComma = @"^[\(\)]";
//    public const string SymbolComma = @",";
//    public const string SymbolLeft = @"(";
//    public const string SymbolRight = @")";

//    public int Depth;
//    public int Group;

//    public MathParenthesisToken(Token token, int depth, int group)
//      : base(token)
//    {
//      Depth = depth;
//      Group = group;
//    }

//    public override string ToString() => $"{base.ToString()},{nameof(Depth)}=\"{Depth}\",{nameof(Group)}=\"{Group}\"";
//  }

//  public class MathTokenizer
//  {
//    public const string Whitespace = nameof(Whitespace);
//    public const string RegexWhitespace = @"^\s+";

//    private Tokenizer _tokenizer = new Tokenizer();

//    public MathTokenizer(bool multipleArguments, bool extendedMathOperators)
//    {
//      _tokenizer.SetRecognized(new System.Collections.Generic.Dictionary<string, string>()
//      {
//        { MathParenthesisToken.Parenthesis, multipleArguments ? MathParenthesisToken.RegexWithComma : MathParenthesisToken.RegexWithoutComma },
//        { MathOperatorToken.Operator, extendedMathOperators ? MathOperatorToken.Regex6 : MathOperatorToken.Regex4 },
//        { MathNumberToken.Number, MathNumberToken.Regex },
//        { MathLabelToken.Label,MathLabelToken.Regex },
//        { Whitespace, RegexWhitespace },
//      });
//    }

//    /// <comment>Create an infix sequence of tokens.</comment>
//    public System.Collections.Generic.IEnumerable<MathToken> GetTokens(string expression, bool includeWhitespace = false)
//    {
//      var parenthesisDepth = 0;
//      var parenthesisGroup = 0;
//      var parenthesisGroups = new System.Collections.Generic.Stack<int>();

//      foreach (var token in _tokenizer.GetTokens(UnifyExpression(expression.NormalizeAll(' ', char.IsWhiteSpace))))
//      {
//        switch (token)
//        {
//          case Token t when t.Name.Equals(MathLabelToken.Label):
//            var tokenLabel = new MathLabelToken(t);
//            //  switch (token.Text)
//            //  {
//            //    case nameof(System.Math.PI):
//            //      yield return new Token() { Name = Number, Text = System.Math.PI.ToString() };
//            //      continue;
//            //    case @"GR":
//            //    case nameof(Flux.Math.GoldenRatio):
//            //      yield return new Token() { Name = Number, Text = Flux.Math.GoldenRatio.ToString() };
//            //      continue;
//            //  }
//            yield return tokenLabel;
//            break;
//          case Token t when t.Name.Equals(MathNumberToken.Number):
//            yield return new MathNumberToken(t);
//            break;
//          case Token t when t.Name.Equals(MathOperatorToken.Operator):
//            yield return new MathOperatorToken(t);
//            break;
//          case Token t when t.Name.Equals(MathParenthesisToken.Parenthesis) && t.Text == MathParenthesisToken.SymbolLeft:
//            parenthesisGroups.Push(++parenthesisGroup);
//            yield return new MathParenthesisToken(t, ++parenthesisDepth, parenthesisGroups.Peek());
//            break;
//          case Token t when t.Name.Equals(MathParenthesisToken.Parenthesis) && t.Text == MathParenthesisToken.SymbolRight:
//            yield return new MathParenthesisToken(t, parenthesisDepth--, parenthesisGroups.Count > 0 ? parenthesisGroups.Pop() : -1);
//            break;
//          case Token t when t.Name.Equals(MathParenthesisToken.Parenthesis) && t.Text == MathParenthesisToken.SymbolComma:
//            yield return new MathParenthesisToken(t, parenthesisDepth, parenthesisGroups.Count > 0 ? parenthesisGroups.Peek() : 0);
//            break;
//          default:
//            if (includeWhitespace && token.Name == Whitespace)
//            {
//              yield return new MathToken(token);
//            }
//            break;
//        }
//      }
//    }

//    public static System.Collections.Generic.IEnumerable<MathToken> GetUnbalancedParenthesis(System.Collections.Generic.IEnumerable<MathToken> tokens)
//    {
//      if (tokens.Where(t => t.Name.Equals(MathParenthesisToken.Parenthesis)).ToList() is System.Collections.Generic.List<MathToken> ps && ps.Any())
//      {
//        var psl = ps.Where(t => t.Text.Equals(MathParenthesisToken.SymbolLeft)).Cast<MathParenthesisToken>();
//        var psr = ps.Where(t => t.Text.Equals(MathParenthesisToken.SymbolRight)).Cast<MathParenthesisToken>();

//        var pslmm = psl.Where(p1 => !psr.Where(p2 => p2.Group.Equals(p1.Group)).Any());
//        var psrmm = psr.Where(p1 => !psl.Where(p2 => p2.Group.Equals(p1.Group)).Any());

//        foreach (var token in pslmm.Union(psrmm))
//        {
//          yield return token;
//        }
//      }
//    }

//    /// <comment>Convert an infix sequence of tokens to prefix (or Normal Polish) notation (NPN), using a Shunting-yard algorithm.</comment>
//    /// <see cref="https://en.wikipedia.org/wiki/Shunting-yard_algorithm"/>
//    /// <see cref="https://en.wikipedia.org/wiki/Polish_notation"/>
//    public static System.Collections.Generic.IEnumerable<MathToken> GetTokensNPN(System.Collections.Generic.IEnumerable<MathToken> tokens)
//    {
//      var output = new System.Collections.Generic.List<MathToken>();

//      var stack = new System.Collections.Generic.Stack<MathToken>();

//      foreach (var token in tokens.Reverse())
//      {
//        switch (token)
//        {
//          case MathLabelToken t:
//            output.Add(t);
//            break;
//          case MathNumberToken t:
//            output.Add(t);
//            break;
//          case MathOperatorToken t:
//            while (stack.Count > 0 && stack.Peek() is MathOperatorToken mots && mots.Precedence is int sp && t.Precedence is int tp && (tp < sp || (tp == sp && t.Associativity == MathOperatorToken.AssociativityLeft)))
//            {
//              output.Add(stack.Pop());
//            }
//            stack.Push(token);
//            break;
//          case MathParenthesisToken t when t.Text == MathParenthesisToken.SymbolLeft:
//            while (stack.Count > 0 && stack.Peek().Text != MathParenthesisToken.SymbolRight)
//            {
//              output.Add(stack.Pop());
//            }
//            stack.Pop();
//            break;
//          case MathParenthesisToken t when t.Text == MathParenthesisToken.SymbolRight:
//            stack.Push(token);
//            break;
//        }
//      }

//      output.AddRange(stack);

//      output.Reverse();

//      return output;
//    }
//    /// <comment>Convert an infix sequence of tokens to postfix (or Reverse Polish) notation (RPN), using a Shunting-yard algorithm..</comment>
//    /// <see cref="https://en.wikipedia.org/wiki/Shunting-yard_algorithm"/>
//    /// <see cref="https://en.wikipedia.org/wiki/Reverse_Polish_notation"/>
//    public static System.Collections.Generic.IEnumerable<MathToken> GetTokensRPN(System.Collections.Generic.IEnumerable<MathToken> tokens)
//    {
//      var output = new System.Collections.Generic.List<MathToken>();

//      var stack = new System.Collections.Generic.Stack<MathToken>();

//      foreach (var token in tokens)
//      {
//        switch (token)
//        {
//          case MathLabelToken t:
//            output.Add(t);
//            break;
//          case MathNumberToken t:
//            output.Add(t);
//            break;
//          case MathOperatorToken t:
//            while (stack.Count > 0 && stack.Peek().Name == MathOperatorToken.Operator && t.Precedence is int tp && stack.Peek() is MathOperatorToken mots && mots.Precedence is int sp && (tp < sp || (tp == sp && t.Associativity == MathOperatorToken.AssociativityLeft)))
//            {
//              output.Add(stack.Pop());
//            }
//            stack.Push(token);
//            break;
//          case MathParenthesisToken t when t.Text == MathParenthesisToken.SymbolLeft:
//            stack.Push(token);
//            break;
//          case MathParenthesisToken t when t.Text == MathParenthesisToken.SymbolRight:
//            while (stack.Count > 0 && stack.Peek().Text != MathParenthesisToken.SymbolLeft)
//            {
//              output.Add(stack.Pop());
//            }
//            stack.Pop();
//            break;
//        }
//      }

//      output.AddRange(stack);

//      return output;
//    }

//    /// <comment>Evaluate a prefix (or Normal Polish) notation sequence to a value.</comment>
//    /// <see cref="https://en.wikipedia.org/wiki/Polish_notation"/>
//    public static double EvaluateNPN(System.Collections.Generic.IEnumerable<MathToken> tokens)
//    {
//      var stack = new System.Collections.Generic.Stack<MathToken>();

//      foreach (var token in tokens.Reverse())
//      {
//        switch (token)
//        {
//          case MathOperatorToken t:
//            var t1 = stack.Pop();
//            var t2 = stack.Pop();
//            stack.Push(t.Evaluate(t1, t2));
//            break;
//          case MathNumberToken t:
//            stack.Push(t);
//            break;
//        }
//      }

//      return double.Parse(stack.Pop().Text);
//    }
//    /// <comment>Evaluate a postfix (or Reverse Polish) notation sequence to a value.</comment>
//    /// <see cref="https://en.wikipedia.org/wiki/Reverse_Polish_notation"/>
//    public static double EvaluateRPN(System.Collections.Generic.IEnumerable<MathToken> tokens)
//    {
//      var stack = new System.Collections.Generic.Stack<MathToken>();

//      foreach (var token in tokens)
//      {
//        switch (token)
//        {
//          case MathOperatorToken t:
//            var t2 = stack.Pop();
//            var t1 = stack.Pop();
//            stack.Push(t.Evaluate(t1, t2));
//            break;
//          case MathNumberToken t:
//            stack.Push(t);
//            break;
//        }
//      }

//      return double.Parse(stack.Pop().Text);
//    }

//    public static string UnifyExpression(string expression)
//    {
//      expression = expression.Replace(@"+", MathOperatorToken.SymbolAdd);
//      expression = expression.Replace(@"/", MathOperatorToken.SymbolDivide);
//      expression = expression.Replace(@"*", MathOperatorToken.SymbolMultiply);
//      expression = expression.Replace(@"-", MathOperatorToken.SymbolSubtract);

//      return expression;
//    }
//  }
//  #endregion MathTokenizer functionality
//}
