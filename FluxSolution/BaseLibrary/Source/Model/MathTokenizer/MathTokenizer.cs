using System.Linq;
using Flux.Hashing;

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

    public System.Collections.Generic.IEnumerable<MathToken> GetTokens(string expression)
    {
      var tokens = new System.Collections.Generic.List<MathToken>();

      var parenthesisDepth = 0;
      var parenthesisGroup = 0;
      var parenthesisGroups = new System.Collections.Generic.Stack<int>();

      expression = UnifyExpressionOperators(expression);

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
                tokens.Add(new MathTokenLabel(kvp.Key, match.Value, index));
                break;
              case nameof(MathTokenNumber):
                tokens.Add(new MathTokenNumber(kvp.Key, match.Value, index));
                break;
              case nameof(MathTokenOperator):
                tokens.Add(new MathTokenOperator(kvp.Key, match.Value, index));
                break;
              case nameof(MathTokenParenthesis) when match.Value.Equals(MathTokenParenthesis.SymbolLeft, System.StringComparison.Ordinal):
                parenthesisGroups.Push(++parenthesisGroup);
                tokens.Add(new MathTokenParenthesis(kvp.Key, match.Value, index, ++parenthesisDepth, parenthesisGroups.Peek()));
                break;
              case nameof(MathTokenParenthesis) when match.Value.Equals(MathTokenParenthesis.SymbolRight, System.StringComparison.Ordinal):
                tokens.Add(new MathTokenParenthesis(kvp.Key, match.Value, index, parenthesisDepth--, parenthesisGroups.Count > 0 ? parenthesisGroups.Pop() : -1));
                break;
              case nameof(MathTokenParenthesis) when match.Value.Equals(MathTokenParenthesis.SymbolComma, System.StringComparison.Ordinal):
                tokens.Add(new MathTokenParenthesis(kvp.Key, match.Value, index, parenthesisDepth, parenthesisGroups.Count > 0 ? parenthesisGroups.Peek() : 0));
                break;
              case nameof(MathTokenWhitespace):
                tokens.Add(new MathTokenWhitespace(kvp.Key, match.Value, index));
                break;
              case nameof(MathTokenUnrecognized):
                tokens.Add(new MathTokenUnrecognized(kvp.Key, match.Value, index));
                break;
              default:
                throw new System.Exception();
            }

            index += match.Value.Length;

            break;
          }
        }
      }

      return tokens;
    }

    public System.Collections.Generic.IList<MathToken> FilterTokens(System.Collections.Generic.IList<MathToken> tokens, bool discardUnrecognized)
      => MergeUnaryNegativeOperator(tokens.Where(t => t is not MathTokenWhitespace).Where(t => !(t is MathTokenUnrecognized && discardUnrecognized)).Select(t => t is not MathTokenUnrecognized ? t : throw new System.ArgumentException($"Invalid token: {t.ToTokenString()}", nameof(tokens))).ToList());

    /// <summary>
    /// <para>Evaluate a sequence of tokens in prefix notation order (a.k.a. Normal Polish Notation, or NPN), to a value.</para>
    /// <see cref="https://en.wikipedia.org/wiki/Polish_notation"/>
    /// </summary>
    public static double EvaluateNPN(System.Collections.Generic.IEnumerable<MathToken> tokensNPN)
    {
      var stack = new System.Collections.Generic.Stack<MathToken>();

      foreach (var token in tokensNPN.Reverse())
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
    public static double EvaluateRPN(System.Collections.Generic.IEnumerable<MathToken> tokensRPN)
    {
      var stack = new System.Collections.Generic.Stack<MathToken>();

      foreach (var token in tokensRPN.ThrowOnNull(nameof(tokensRPN)))
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
    public static System.Collections.Generic.List<MathToken> GetTokensNPN(System.Collections.Generic.IEnumerable<MathToken> tokens)
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
    public static System.Collections.Generic.List<MathToken> GetTokensRPN(System.Collections.Generic.IEnumerable<MathToken> tokens)
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

    /// <summary>Creates a new list of <see cref="MathToken"/> from <paramref name="tokens"/>, modifying by merging where unary (not preceeded by a <see cref="MathTokenNumber"/>) negative <see cref="MathTokenOperator"/> are merged with the following <see cref="MathTokenNumber"/></summary>
    /// <param name="tokens"></param>
    /// <returns></returns>
    private static System.Collections.Generic.List<MathToken> MergeUnaryNegativeOperator(System.Collections.Generic.IList<MathToken> tokens)
    {
      var list = new System.Collections.Generic.List<MathToken>();

      for (var i = 0; i < tokens.Count; i++)
      {
        if (tokens[i] is MathTokenOperator mto && mto.Value == MathTokenOperator.SymbolSubtract)
        {
          if ((i == 0 || tokens[i - 1] is not MathTokenNumber) && (i <= tokens.Count - 1 && tokens[i + 1] is MathTokenNumber mtn))
          {
            list.Add(mtn.GetNegated());
            i++;
            continue;
          }
        }

        list.Add(tokens[i]);
      }

      return list;
    }

    public static string UnifyExpressionOperators(string expression)
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
