//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//// https://github.com/sprache/Sprache
//// https://nblumhardt.com/2010/01/building-an-external-dsl-in-c/
//// https://www.codeproject.com/Articles/795056/Sprache-Calc-building-yet-another-expression-evalu?msg=4858437#xx4858437xx

//namespace Flux
//{
//  /// <summary>
//  /// Represents a parser.
//  /// </summary>
//  /// <typeparam name="T">The type of the result.</typeparam>
//  /// <param name="input">The input to parse.</param>
//  /// <returns>The result of the parser.</returns>
//  public delegate IResult<T> Parser<out T>(IInput input);

//  /// <summary>
//  /// Contains some extension methods for <see cref="Parser&lt;T&gt;" />.
//  /// </summary>
//  public static class ParserExtensions
//  {
//    /// <summary>
//    /// Tries to parse the input without throwing an exception.
//    /// </summary>
//    /// <typeparam name="T">The type of the result.</typeparam>
//    /// <param name="parser">The parser.</param>
//    /// <param name="input">The input.</param>
//    /// <returns>The result of the parser</returns>
//    public static IResult<T> TryParse<T>(this Parser<T> parser, string input)
//    {
//      if (parser == null) throw new ArgumentNullException(nameof(parser));
//      if (input == null) throw new ArgumentNullException(nameof(input));

//      return parser(new Input(input));
//    }

//    /// <summary>
//    /// Parses the specified input string.
//    /// </summary>
//    /// <typeparam name="T">The type of the result.</typeparam>
//    /// <param name="parser">The parser.</param>
//    /// <param name="input">The input.</param>
//    /// <returns>The result of the parser.</returns>
//    /// <exception cref="Sprache.ParseException">It contains the details of the parsing error.</exception>
//    public static T Parse<T>(this Parser<T> parser, string input)
//    {
//      if (parser == null) throw new ArgumentNullException(nameof(parser));
//      if (input == null) throw new ArgumentNullException(nameof(input));

//      var result = parser.TryParse(input);

//      if (result.WasSuccessful)
//        return result.Value;

//      throw new ParseException(result.ToString(), Position.FromInput(result.Remainder));
//    }
//  }
//}
