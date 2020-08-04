using System.Linq;

namespace Flux.Text.Tokenization.Math
{
  // https://en.wikipedia.org/wiki/Lexical_analysis

  public class Token
    : IToken<string>
  {
    public int Index { get; }
    public string Name { get; }
    public string Value { get; }

    public Token(string name, string textElement, int textIndex = -1)
    {
      Name = name;
      Value = textElement;
      Index = textIndex;
    }

    public override string ToString() => $"{this.GetType().Name}.{Name}=\"{Value}\",#{Index}";
  }

  public class TokenLabel
    : Token
  {
    public const string Regex = @"^[\p{L}][\p{L}\p{Nd}_]*";

    public TokenLabel(string name, string text, int index)
      : base(name, text, index)
    {
    }
  }

  public class TokenNumber
    : Token
  {
    public const string Regex = @"^(?=\d|\.\d)\d*(\.\d*)?([Ee]([+-]?\d+))?";

    public double NumericalValue { get; set; }

    public TokenNumber(string name, string text, int index)
      : base(name, text, index)
    {
      NumericalValue = double.Parse(Value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture);
    }

    public override string ToString() => $"{base.ToString()},{nameof(NumericalValue)}=\"{NumericalValue}\"";
  }

  public class TokenOperator
    : Token
  {
    public const string AssociativityLeft = @"Left";
    public const string AssociativityRight = @"Right";

    public const string Regex = @"^[\u002B\u00F7\u00D7\u2212]";

    public const string SymbolAdd = "\u002B";
    public const string SymbolDivide = "\u00F7";
    public const string SymbolMultiply = "\u00D7";
    public const string SymbolSubtract = "\u2212";

    public string Associativity { get; set; }
    public int Precedence { get; set; }

    public TokenOperator(string name, string text, int index)
      : base(name, text, index)
    {
      switch (Value)
      {
        case SymbolDivide:
        case SymbolMultiply:
          Associativity = TokenOperator.AssociativityLeft;
          Precedence = 3;
          break;
        case SymbolAdd:
        case SymbolSubtract:
          Associativity = TokenOperator.AssociativityLeft;
          Precedence = 2;
          break;
        default:
          Associativity = TokenOperator.AssociativityLeft;
          Precedence = 0;
          break;
      }
    }

    public Token Evaluate(Token left, Token right)
    {
      if (left is null) throw new System.ArgumentNullException(nameof(left));
      if (right is null) throw new System.ArgumentNullException(nameof(right));

      return Value switch
      {
        SymbolAdd => new TokenNumber(nameof(TokenNumber), (double.Parse(left.Value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) + double.Parse(right.Value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture)).ToString(System.Globalization.CultureInfo.CurrentCulture), left.Index),
        SymbolDivide => new TokenNumber(nameof(TokenNumber), (double.Parse(left.Value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) / double.Parse(right.Value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture)).ToString(System.Globalization.CultureInfo.CurrentCulture), left.Index),
        SymbolMultiply => new TokenNumber(nameof(TokenNumber), (double.Parse(left.Value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) * double.Parse(right.Value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture)).ToString(System.Globalization.CultureInfo.CurrentCulture), left.Index),
        SymbolSubtract => new TokenNumber(nameof(TokenNumber), (double.Parse(left.Value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) - double.Parse(right.Value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture)).ToString(System.Globalization.CultureInfo.CurrentCulture), left.Index),
        _ => throw new System.ArithmeticException(),
      };
    }

    public override string ToString()
      => $"{base.ToString()},{nameof(Associativity)}=\"{Associativity}\",{nameof(Precedence)}=\"{Precedence}\"";

    public static string Unify(string expression)
    {
      if (expression is null) throw new System.ArgumentNullException(nameof(expression));

      expression = expression.Replace(@"+", TokenOperator.SymbolAdd, System.StringComparison.Ordinal);
      expression = expression.Replace(@"/", TokenOperator.SymbolDivide, System.StringComparison.Ordinal);
      expression = expression.Replace(@"*", TokenOperator.SymbolMultiply, System.StringComparison.Ordinal);
      expression = expression.Replace(@"-", TokenOperator.SymbolSubtract, System.StringComparison.Ordinal);

      return expression;
    }
  }

  public class TokenParenthesis
    : Token
  {
    public const string RegexWithComma = @"^[\(\,\)]";
    public const string RegexWithoutComma = @"^[\(\)]";

    public const string SymbolComma = @",";
    public const string SymbolLeft = @"(";
    public const string SymbolRight = @")";

    public int Depth { get; set; }
    public int Group { get; set; }

    public TokenParenthesis(string name, string text, int index, int depth, int group)
      : base(name, text, index)
    {
      Depth = depth;
      Group = group;
    }

    public override string ToString()
      => $"{base.ToString()},{nameof(Depth)}=\"{Depth}\",{nameof(Group)}=\"{Group}\"";
  }

  public class TokenUnrecognized
    : Token
  {
    public const string Regex = @"^.";

    public TokenUnrecognized(string name, string text, int index)
      : base(name, text, index)
    {
    }
  }

  public class TokenWhitespace
    : Token
  {
    public const string Regex = @"^\s+";

    public TokenWhitespace(string name, string text, int index)
      : base(name, text, index)
    {
    }
  }

  public class Tokenizer
    : ITokenizer<Token>
  {
    //public const string Unrecognized = nameof(Unrecognized);

    //public const string Whitespace = nameof(Whitespace);
    //public const string RegexWhitespace = @"^\s+";

    //    private Tokenizer _tokenizer = new Tokenizer();

    private readonly System.Collections.Generic.Dictionary<string, System.Text.RegularExpressions.Regex> m_recognized;

    public Tokenizer(bool multipleArguments)
    {
      m_recognized = new System.Collections.Generic.Dictionary<string, System.Text.RegularExpressions.Regex>()
      {
        { nameof(TokenParenthesis), new System.Text.RegularExpressions.Regex(multipleArguments ? TokenParenthesis.RegexWithComma : TokenParenthesis.RegexWithoutComma) },
        { nameof(TokenOperator), new System.Text.RegularExpressions.Regex(TokenOperator.Regex) },
        { nameof(TokenNumber), new System.Text.RegularExpressions.Regex(TokenNumber.Regex) },
        { nameof(TokenLabel), new System.Text.RegularExpressions.Regex(TokenLabel.Regex) },
        { nameof(TokenWhitespace), new System.Text.RegularExpressions.Regex(TokenWhitespace.Regex) },
        { nameof(TokenUnrecognized), new System.Text.RegularExpressions.Regex(TokenUnrecognized.Regex) }
      };
    }

    /// <comment>Create an infix sequence of tokens.</comment>
    //public System.Collections.Generic.IEnumerable<Token> GetTokens(string expression, bool includeWhitespace)
    //{
    //  var parenthesisDepth = 0;
    //  var parenthesisGroup = 0;
    //  var parenthesisGroups = new System.Collections.Generic.Stack<int>();

    //  expression = UnifyExpression(expression.NormalizeAll(' ', char.IsWhiteSpace));

    //  foreach (var token in GetTokens(expression))
    //  {
    //    switch (token)
    //    {
    //      case Token t when t.Name.Equals(LabelToken.Label):
    //        var tokenLabel = new LabelToken(t);
    //        yield return tokenLabel;
    //        break;
    //      case Token t when t.Name.Equals(NumberToken.Number):
    //        yield return new NumberToken(t);
    //        break;
    //      case Token t when t.Name.Equals(OperatorToken.Operator):
    //        yield return new OperatorToken(t);
    //        break;
    //      case Token t when t.Name.Equals(ParenthesisToken.Parenthesis) && t.Text == ParenthesisToken.SymbolLeft:
    //        parenthesisGroups.Push(++parenthesisGroup);
    //        yield return new ParenthesisToken(t, ++parenthesisDepth, parenthesisGroups.Peek());
    //        break;
    //      case Token t when t.Name.Equals(ParenthesisToken.Parenthesis) && t.Text == ParenthesisToken.SymbolRight:
    //        yield return new ParenthesisToken(t, parenthesisDepth--, parenthesisGroups.Count > 0 ? parenthesisGroups.Pop() : -1);
    //        break;
    //      case Token t when t.Name.Equals(ParenthesisToken.Parenthesis) && t.Text == ParenthesisToken.SymbolComma:
    //        yield return new ParenthesisToken(t, parenthesisDepth, parenthesisGroups.Count > 0 ? parenthesisGroups.Peek() : 0);
    //        break;
    //      default:
    //        if (includeWhitespace && token.Name == Whitespace)
    //        {
    //          yield return new Token(token);
    //        }
    //        else yield return token;
    //        break;
    //    }
    //  }

    //  System.Collections.Generic.IEnumerable<Token> GetTokens(string subExpression)
    //  {
    //    var index = 0;

    //    while (index < subExpression.Length)
    //    {
    //      foreach (var kvp in m_recognized)
    //      {
    //        if (kvp.Value.Match(subExpression, index, subExpression.Length - index) is var match && match.Success)
    //        {
    //          var token = new Token(kvp.Key, match.Value, index);

    //          yield return token;

    //          index += token.Text.Length;
    //        }
    //      }
    //    }
    //  }
    //}

    public System.Collections.Generic.IEnumerable<Token> GetTokens(string text)
    {
      var parenthesisDepth = 0;
      var parenthesisGroup = 0;
      var parenthesisGroups = new System.Collections.Generic.Stack<int>();

      text = UnifyExpression(text);

      var index = 0;

      while (index < text.Length)
      {
        foreach (var kvp in m_recognized)
        {
          if (kvp.Value.Match(text, index, text.Length - index) is var match && match.Success)
          {
            switch (kvp.Key)
            {
              case nameof(TokenLabel):
                yield return new TokenLabel(kvp.Key, match.Value, index);
                break;
              case nameof(TokenNumber):
                yield return new TokenNumber(kvp.Key, match.Value, index);
                break;
              case nameof(TokenOperator):
                yield return new TokenOperator(kvp.Key, match.Value, index);
                break;
              case nameof(TokenParenthesis) when match.Value.Equals(TokenParenthesis.SymbolLeft, System.StringComparison.Ordinal):
                parenthesisGroups.Push(++parenthesisGroup);
                yield return new TokenParenthesis(kvp.Key, match.Value, index, ++parenthesisDepth, parenthesisGroups.Peek());
                break;
              case nameof(TokenParenthesis) when match.Value.Equals(TokenParenthesis.SymbolRight, System.StringComparison.Ordinal):
                yield return new TokenParenthesis(kvp.Key, match.Value, index, parenthesisDepth--, parenthesisGroups.Count > 0 ? parenthesisGroups.Pop() : -1);
                break;
              case nameof(TokenParenthesis) when match.Value.Equals(TokenParenthesis.SymbolComma, System.StringComparison.Ordinal):
                yield return new TokenParenthesis(kvp.Key, match.Value, index, parenthesisDepth, parenthesisGroups.Count > 0 ? parenthesisGroups.Peek() : 0);
                break;
              case nameof(TokenWhitespace):
                yield return new TokenWhitespace(kvp.Key, match.Value, index);
                break;
              case nameof(TokenUnrecognized):
                yield return new TokenUnrecognized(kvp.Key, match.Value, index);
                break;
              default:
                throw new System.Exception();
            }

            index += match.Value.Length;

            break;
          }
        }
      }
    }

    public static System.Collections.Generic.IEnumerable<Token> GetUnbalancedParenthesis(System.Collections.Generic.IEnumerable<Token> tokens)
    {
      if (tokens.Where(t => t.Name.Equals(nameof(TokenParenthesis), System.StringComparison.Ordinal)).ToList() is System.Collections.Generic.List<Token> ps && ps.Any())
      {
        var psl = ps.Where(t => t.Value.Equals(TokenParenthesis.SymbolLeft, System.StringComparison.Ordinal)).Cast<TokenParenthesis>();
        var psr = ps.Where(t => t.Value.Equals(TokenParenthesis.SymbolRight, System.StringComparison.Ordinal)).Cast<TokenParenthesis>();

        var pslmm = psl.Where(p1 => !psr.Where(p2 => p2.Group.Equals(p1.Group)).Any());
        var psrmm = psr.Where(p1 => !psl.Where(p2 => p2.Group.Equals(p1.Group)).Any());

        foreach (var token in pslmm.Union(psrmm))
        {
          yield return token;
        }
      }
    }

    /// <comment>Convert an infix sequence of tokens to prefix (or Normal Polish) notation (NPN), using a Shunting-yard algorithm.</comment>
    /// <see cref="https://en.wikipedia.org/wiki/Shunting-yard_algorithm"/>
    /// <see cref="https://en.wikipedia.org/wiki/Polish_notation"/>
    public static System.Collections.Generic.IEnumerable<Token> GetTokensNPN(System.Collections.Generic.IEnumerable<Token> tokens)
    {
      var output = new System.Collections.Generic.List<Token>();

      var stack = new System.Collections.Generic.Stack<Token>();

      foreach (var token in tokens.Reverse())
      {
        switch (token)
        {
          case TokenLabel t:
            output.Add(t);
            break;
          case TokenNumber t:
            output.Add(t);
            break;
          case TokenOperator t:
            while (stack.Count > 0 && stack.Peek() is TokenOperator mots && mots.Precedence is int sp && t.Precedence is int tp && (tp < sp || (tp == sp && t.Associativity == TokenOperator.AssociativityLeft)))
            {
              output.Add(stack.Pop());
            }
            stack.Push(token);
            break;
          case TokenParenthesis t when t.Value == TokenParenthesis.SymbolLeft:
            while (stack.Count > 0 && stack.Peek().Value != TokenParenthesis.SymbolRight)
            {
              output.Add(stack.Pop());
            }
            stack.Pop();
            break;
          case TokenParenthesis t when t.Value == TokenParenthesis.SymbolRight:
            stack.Push(token);
            break;
        }
      }

      output.AddRange(stack);

      output.Reverse();

      return output;
    }
    /// <comment>Convert an infix sequence of tokens to postfix (or Reverse Polish) notation (RPN), using a Shunting-yard algorithm..</comment>
    /// <see cref="https://en.wikipedia.org/wiki/Shunting-yard_algorithm"/>
    /// <see cref="https://en.wikipedia.org/wiki/Reverse_Polish_notation"/>
    public static System.Collections.Generic.IEnumerable<Token> GetTokensRPN(System.Collections.Generic.IEnumerable<Token> tokens)
    {
      var output = new System.Collections.Generic.List<Token>();

      var stack = new System.Collections.Generic.Stack<Token>();

      foreach (var token in tokens ?? throw new System.ArgumentNullException(nameof(tokens)))
      {
        switch (token)
        {
          case TokenLabel t:
            output.Add(t);
            break;
          case TokenNumber t:
            output.Add(t);
            break;
          case TokenOperator t:
            while (stack.Count > 0 && stack.Peek().Name == nameof(TokenOperator) && t.Precedence is int tp && stack.Peek() is TokenOperator mots && mots.Precedence is int sp && (tp < sp || (tp == sp && t.Associativity == TokenOperator.AssociativityLeft)))
            {
              output.Add(stack.Pop());
            }
            stack.Push(token);
            break;
          case TokenParenthesis t when t.Value == TokenParenthesis.SymbolLeft:
            stack.Push(token);
            break;
          case TokenParenthesis t when t.Value == TokenParenthesis.SymbolRight:
            while (stack.Count > 0 && stack.Peek().Value != TokenParenthesis.SymbolLeft)
            {
              output.Add(stack.Pop());
            }
            stack.Pop();
            break;
        }
      }

      output.AddRange(stack);

      return output;
    }

    /// <comment>Evaluate a prefix (or Normal Polish) notation sequence to a value.</comment>
    /// <see cref="https://en.wikipedia.org/wiki/Polish_notation"/>
    public static double EvaluateNPN(System.Collections.Generic.IEnumerable<Token> tokens)
    {
      var stack = new System.Collections.Generic.Stack<Token>();

      foreach (var token in tokens.Reverse())
      {
        switch (token)
        {
          case TokenOperator t:
            var t1 = stack.Pop();
            var t2 = stack.Pop();
            stack.Push(t.Evaluate(t1, t2));
            break;
          case TokenNumber t:
            stack.Push(t);
            break;
        }
      }

      return double.Parse(stack.Pop().Value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture);
    }
    /// <comment>Evaluate a postfix (or Reverse Polish) notation sequence to a value.</comment>
    /// <see cref="https://en.wikipedia.org/wiki/Reverse_Polish_notation"/>
    public static double EvaluateRPN(System.Collections.Generic.IEnumerable<Token> tokens)
    {
      var stack = new System.Collections.Generic.Stack<Token>();

      foreach (var token in tokens ?? throw new System.ArgumentNullException(nameof(tokens)))
      {
        switch (token)
        {
          case TokenOperator t:
            var t2 = stack.Pop();
            var t1 = stack.Pop();
            stack.Push(t.Evaluate(t1, t2));
            break;
          case TokenNumber t:
            stack.Push(t);
            break;
        }
      }

      return double.Parse(stack.Pop().Value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture);
    }

    public static string UnifyExpression(string expression)
    {
      if (expression is null) throw new System.ArgumentNullException(nameof(expression));

      expression = expression.Replace(@"+", TokenOperator.SymbolAdd, System.StringComparison.Ordinal);
      expression = expression.Replace(@"/", TokenOperator.SymbolDivide, System.StringComparison.Ordinal);
      expression = expression.Replace(@"*", TokenOperator.SymbolMultiply, System.StringComparison.Ordinal);
      expression = expression.Replace(@"-", TokenOperator.SymbolSubtract, System.StringComparison.Ordinal);

      return expression;
    }
  }
}
