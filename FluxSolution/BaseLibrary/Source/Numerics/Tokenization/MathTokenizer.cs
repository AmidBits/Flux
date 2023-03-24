using System.Linq;

namespace Flux.Text
{
  // https://en.wikipedia.org/wiki/Lexical_analysis

  public sealed class MathTokenizer
    : ITokenizer<MathToken>
  {
    //public const string Unrecognized = nameof(Unrecognized);

    //public const string Whitespace = nameof(Whitespace);
    //public const string RegexWhitespace = @"^\s+";

    //    private Tokenizer _tokenizer = new Tokenizer();

    private readonly System.Collections.Generic.Dictionary<string, System.Text.RegularExpressions.Regex> m_recognized;

    public MathTokenizer(bool multipleArguments)
    {
      m_recognized = new System.Collections.Generic.Dictionary<string, System.Text.RegularExpressions.Regex>()
      {
        { nameof(MathTokenParenthesis), new System.Text.RegularExpressions.Regex(multipleArguments ? MathTokenParenthesis.RegexWithComma : MathTokenParenthesis.RegexWithoutComma) },
        { nameof(MathTokenOperator), new System.Text.RegularExpressions.Regex(MathTokenOperator.Regex) },
        { nameof(MathTokenNumber), new System.Text.RegularExpressions.Regex(MathTokenNumber.Regex) },
        { nameof(MathTokenLabel), new System.Text.RegularExpressions.Regex(MathTokenLabel.Regex) },
        { nameof(MathTokenWhitespace), new System.Text.RegularExpressions.Regex(MathTokenWhitespace.Regex) },
        { nameof(MathTokenUnrecognized), new System.Text.RegularExpressions.Regex(MathTokenUnrecognized.Regex) }
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

    /// <comment>Creates an infix sequence of tokens.</comment>
    public System.Collections.Generic.IEnumerable<MathToken> GetTokens(string expression)
    {
      var parenthesisDepth = 0;
      var parenthesisGroup = 0;
      var parenthesisGroups = new System.Collections.Generic.Stack<int>();

      expression = UnifyExpression(expression);

      var index = 0;

      while (index < expression.Length)
      {
        foreach (var kvp in m_recognized)
        {
          if (kvp.Value.Match(expression, index, expression.Length - index) is var match && match.Success)
          {
            switch (kvp.Key)
            {
              case nameof(MathTokenLabel):
                yield return new MathTokenLabel(kvp.Key, match.Value, index);
                break;
              case nameof(MathTokenNumber):
                yield return new MathTokenNumber(kvp.Key, match.Value, index);
                break;
              case nameof(MathTokenOperator):
                yield return new MathTokenOperator(kvp.Key, match.Value, index);
                break;
              case nameof(MathTokenParenthesis) when match.Value.Equals(MathTokenParenthesis.SymbolLeft, System.StringComparison.Ordinal):
                parenthesisGroups.Push(++parenthesisGroup);
                yield return new MathTokenParenthesis(kvp.Key, match.Value, index, ++parenthesisDepth, parenthesisGroups.Peek());
                break;
              case nameof(MathTokenParenthesis) when match.Value.Equals(MathTokenParenthesis.SymbolRight, System.StringComparison.Ordinal):
                yield return new MathTokenParenthesis(kvp.Key, match.Value, index, parenthesisDepth--, parenthesisGroups.Count > 0 ? parenthesisGroups.Pop() : -1);
                break;
              case nameof(MathTokenParenthesis) when match.Value.Equals(MathTokenParenthesis.SymbolComma, System.StringComparison.Ordinal):
                yield return new MathTokenParenthesis(kvp.Key, match.Value, index, parenthesisDepth, parenthesisGroups.Count > 0 ? parenthesisGroups.Peek() : 0);
                break;
              case nameof(MathTokenWhitespace):
                yield return new MathTokenWhitespace(kvp.Key, match.Value, index);
                break;
              case nameof(MathTokenUnrecognized):
                yield return new MathTokenUnrecognized(kvp.Key, match.Value, index);
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

    public static System.Collections.Generic.IEnumerable<MathToken> GetUnbalancedParenthesis(System.Collections.Generic.IEnumerable<MathToken> tokens)
    {
      if (tokens.Where(t => t.Name.Equals(nameof(MathTokenParenthesis), System.StringComparison.Ordinal)).ToList() is System.Collections.Generic.List<MathToken> ps && ps.Any())
      {
        var psl = ps.Where(t => t.Value.Equals(MathTokenParenthesis.SymbolLeft, System.StringComparison.Ordinal)).Cast<MathTokenParenthesis>();
        var psr = ps.Where(t => t.Value.Equals(MathTokenParenthesis.SymbolRight, System.StringComparison.Ordinal)).Cast<MathTokenParenthesis>();

        var pslmm = psl.Where(p1 => !psr.Where(p2 => p2.Group.Equals(p1.Group)).Any());
        var psrmm = psr.Where(p1 => !psl.Where(p2 => p2.Group.Equals(p1.Group)).Any());

        foreach (var token in pslmm.Union(psrmm))
          yield return token;
      }
    }

    /// <summary>
    /// <para>Convert an infix sequence of tokens to prefix notation order (a.k.a. Normal Polish Notation, or NPN), using a Shunting-yard algorithm.</para>
    /// <para><see cref="https://en.wikipedia.org/wiki/Shunting-yard_algorithm"/></para>
    /// <para><see cref="https://en.wikipedia.org/wiki/Polish_notation"/></para>
    /// </summary>
    /// <param name="tokens"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<MathToken> GetTokensNPN(System.Collections.Generic.IEnumerable<MathToken> tokens)
    {
      var output = new System.Collections.Generic.List<MathToken>();

      var stack = new System.Collections.Generic.Stack<MathToken>();

      foreach (var token in tokens.Reverse())
      {
        switch (token)
        {
          case MathTokenLabel t:
            output.Add(t);
            break;
          case MathTokenNumber t:
            output.Add(t);
            break;
          case MathTokenOperator t:
            while (stack.Count > 0 && stack.Peek() is MathTokenOperator mots && mots.Precedence is int sp && t.Precedence is int tp && (tp < sp || (tp == sp && t.Associativity == MathTokenOperator.AssociativityLeft)))
            {
              output.Add(stack.Pop());
            }
            stack.Push(token);
            break;
          case MathTokenParenthesis t when t.Value == MathTokenParenthesis.SymbolLeft:
            while (stack.Count > 0 && stack.Peek().Value != MathTokenParenthesis.SymbolRight)
            {
              output.Add(stack.Pop());
            }
            stack.Pop();
            break;
          case MathTokenParenthesis t when t.Value == MathTokenParenthesis.SymbolRight:
            stack.Push(token);
            break;
        }
      }

      output.AddRange(stack);

      output.Reverse();

      return output;
    }

    /// <summary>
    /// <para>Convert an infix sequence of tokens to postfix notation (a.k.a. Reverse Polish Notation, or RPN), using a Shunting-yard algorithm.</para>
    /// <para><see cref="https://en.wikipedia.org/wiki/Shunting-yard_algorithm"/></para>
    /// <para><see cref="https://en.wikipedia.org/wiki/Reverse_Polish_notation"/></para>
    /// </summary>
    /// <param name="tokens"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<MathToken> GetTokensRPN(System.Collections.Generic.IEnumerable<MathToken> tokens)
    {
      var output = new System.Collections.Generic.List<MathToken>();

      var stack = new System.Collections.Generic.Stack<MathToken>();

      foreach (var token in tokens ?? throw new System.ArgumentNullException(nameof(tokens)))
      {
        switch (token)
        {
          case MathTokenLabel t:
            output.Add(t);
            break;
          case MathTokenNumber t:
            output.Add(t);
            break;
          case MathTokenOperator t:
            while (stack.Count > 0 && stack.Peek().Name == nameof(MathTokenOperator) && t.Precedence is int tp && stack.Peek() is MathTokenOperator mots && mots.Precedence is int sp && (tp < sp || (tp == sp && t.Associativity == MathTokenOperator.AssociativityLeft)))
            {
              output.Add(stack.Pop());
            }
            stack.Push(token);
            break;
          case MathTokenParenthesis t when t.Value == MathTokenParenthesis.SymbolLeft:
            stack.Push(token);
            break;
          case MathTokenParenthesis t when t.Value == MathTokenParenthesis.SymbolRight:
            while (stack.Count > 0 && stack.Peek().Value != MathTokenParenthesis.SymbolLeft)
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

    /// <summary>
    /// <para>Evaluate a sequence of tokens in prefix notation order (a.k.a. Normal Polish Notation, or NPN), to a value.</para>
    /// <see cref="https://en.wikipedia.org/wiki/Polish_notation"/>
    /// </summary>
    public static double EvaluateNPN(System.Collections.Generic.IEnumerable<MathToken> tokens)
    {
      var stack = new System.Collections.Generic.Stack<MathToken>();

      foreach (var token in tokens.Reverse())
      {
        switch (token)
        {
          case MathTokenOperator t:
            var t1 = stack.Pop();
            var t2 = stack.Pop();
            stack.Push(t.Evaluate(t1, t2));
            break;
          case MathTokenNumber t:
            stack.Push(t);
            break;
        }
      }

      return double.Parse(stack.Pop().Value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture);
    }
    /// <summary>
    /// <para>Evaluate a sequence of tokens in postfix notation order (a.k.a. Reverse Polish Notation, or RPN), to a value.</para>
    /// <see cref="https://en.wikipedia.org/wiki/Reverse_Polish_notation"/>
    /// </summary>
    public static double EvaluateRPN(System.Collections.Generic.IEnumerable<MathToken> tokens)
    {
      var stack = new System.Collections.Generic.Stack<MathToken>();

      foreach (var token in tokens.ThrowIfNull(nameof(tokens)))
      {
        switch (token)
        {
          case MathTokenOperator t:
            var t2 = stack.Pop();
            var t1 = stack.Pop();
            stack.Push(t.Evaluate(t1, t2));
            break;
          case MathTokenNumber t:
            stack.Push(t);
            break;
        }
      }

      return double.Parse(stack.Pop().Value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture);
    }

    public static string UnifyExpression(string expression)
    {
      if (expression is null) throw new System.ArgumentNullException(nameof(expression));

      expression = expression.Replace(@"+", MathTokenOperator.SymbolAdd, System.StringComparison.Ordinal);
      expression = expression.Replace(@"/", MathTokenOperator.SymbolDivide, System.StringComparison.Ordinal);
      expression = expression.Replace(@"*", MathTokenOperator.SymbolMultiply, System.StringComparison.Ordinal);
      expression = expression.Replace(@"-", MathTokenOperator.SymbolSubtract, System.StringComparison.Ordinal);

      return expression;
    }
  }
}
