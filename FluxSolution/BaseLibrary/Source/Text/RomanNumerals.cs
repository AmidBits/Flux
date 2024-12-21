//namespace Flux
//{
//  namespace Text
//  {
//    /// <summary>
//    /// <para>Provides functionality to help convert numbers to and from roman numerals.</para>
//    /// <see href="https://en.wikipedia.org/wiki/Roman_numerals"/>
//    /// </summary>
//    public static partial class RomanNumerals
//    {
//      public static readonly System.Collections.Generic.List<char> UpperLatinNumerals = new() { 'I', 'V', 'X', 'L', 'C', 'D', 'M' };

//      public static string ConvertIndicesToSymbols(System.Collections.Generic.List<int> indices, System.Collections.Generic.List<char> numerals) => string.Concat(indices.Select(i => numerals[i]));

//      public static System.Collections.Generic.List<int> ConvertSymbolsToIndices(string symbols, System.Collections.Generic.List<char> numerals) => symbols.Select(s => numerals.IndexOf(s)).ToList();

//      public static readonly System.Collections.Generic.List<int> RomanValues = new() { 1, 5, 10, 50, 100, 500, 1000 };

//      /// <summary>
//      /// <para>Convert a whole <paramref name="number"/> to a list of <paramref name="indices"/> in the interval [0, 6]. The indices represents the roman sign (symbols) values {1, 5, 10, 50, 100, 500, 1000] (respectively).</para>
//      /// <see href="https://en.wikipedia.org/wiki/Roman_numerals"/>
//      /// <code><![CDATA[
//      /// var RomanNumerals = new System.Collections.Generic.List<char>() { 'I', 'V', 'X', 'L', 'C', 'D', 'M' };
//      /// var sourceNumber = 1912;
//      /// sourceNumber.TryConvertNumberToIndices(out var sourceIndices);
//      /// var sourceString = sourceIndices.Select(v => RomanNumerals[v]).ToList().AsSpan().ToString();
//      /// var targetIndices = sourceString.Select(c => RomanNumerals.IndexOf(c)).ToList();
//      /// targetIndices.TryConvertIndicesToNumber(out int targetNumber);
//      /// ]]></code>
//      /// </summary>
//      /// <typeparam name="TSelf"></typeparam>
//      /// <param name="number"></param>
//      /// <param name="indices"></param>
//      /// <returns></returns>
//      public static bool TryConvertNumberToIndices<TSelf>(this TSelf number, out System.Collections.Generic.List<int> indices)
//        where TSelf : System.Numerics.IBinaryInteger<TSelf>
//      {
//        try
//        {
//          indices = new System.Collections.Generic.List<int>();

//          while (number >= TSelf.CreateChecked(4))
//          {
//            for (var index = RomanValues.Count - 1; index > 0; index--)
//            {
//              if (TSelf.CreateChecked(RomanValues[index]) is var rvi && number >= rvi)
//              {
//                indices.Add(index);
//                number -= rvi;
//                break;
//              }
//              else
//              {
//                var q = rvi / TSelf.CreateChecked(RomanValues[index - 1]);

//                if (q == TSelf.CreateChecked(2)) // 2-boundary.
//                {
//                  rvi -= rvi / TSelf.CreateChecked(10);

//                  if (number >= rvi) // rvi is now 9, 90 or 900.
//                  {
//                    indices.Add(index - 2);
//                    indices.Add(index);
//                    number -= rvi;
//                    break;
//                  }
//                }
//                else if (q == TSelf.CreateChecked(5)) // 5-boundary.
//                {
//                  rvi -= rvi / q; // Since q was just compared equal to 5 above, let's re-use it.

//                  if (number >= rvi) // rvi is now 4, 40 or 400.
//                  {
//                    indices.Add(index - 1);
//                    indices.Add(index);
//                    number -= rvi;
//                    break;
//                  }
//                }
//              }
//            }
//          }

//          while (number > TSelf.Zero)
//          {
//            indices.Add(0);
//            number--;
//          }

//          return true;
//        }
//        catch { }

//        indices = default!;
//        return false;
//      }

//      /// <summary>
//      /// <para>Convert a list of <paramref name="indices"/> in the interval [0, 6] to a whole <paramref name="number"/>. The indices represents the roman sign (symbols) values {1, 5, 10, 50, 100, 500, 1000] (respectively).</para>
//      /// <see href="https://en.wikipedia.org/wiki/Roman_numerals"/>
//      /// <code><![CDATA[
//      /// var RomanNumerals = new System.Collections.Generic.List<char>() { 'I', 'V', 'X', 'L', 'C', 'D', 'M' };
//      /// var sourceNumber = 1912;
//      /// sourceNumber.TryConvertNumberToIndices(out var sourceIndices);
//      /// var sourceString = sourceIndices.Select(v => RomanNumerals[v]).ToList().AsSpan().ToString();
//      /// var targetIndices = sourceString.Select(c => RomanNumerals.IndexOf(c)).ToList();
//      /// targetIndices.TryConvertIndicesToNumber(out int targetNumber);
//      /// ]]></code>
//      /// </summary>
//      /// <typeparam name="TSelf"></typeparam>
//      /// <param name="indices"></param>
//      /// <param name="number"></param>
//      /// <returns></returns>
//      public static bool TryConvertIndicesToNumber<TSelf>(this System.Collections.Generic.List<int> indices, out TSelf number)
//        where TSelf : System.Numerics.IBinaryInteger<TSelf>
//      {
//        try
//        {
//          number = TSelf.Zero;

//          var maxIndex = indices.Count - 1;

//          for (var index = 0; index < indices.Count; index++)
//          {
//            var value = RomanValues[indices[index]];
//            var valueP1 = index < maxIndex ? RomanValues[indices[index + 1]] : 0;

//            if (value < valueP1) // If the value is less than the next value, we have a subtractive notation.
//            {
//              number += TSelf.CreateChecked(valueP1 - value); // Adjust for subtractive notation, i.e. 900 = CM ("one hundred less than a thousand"). 
//              index++; // Index has to be adjusted too, since there are two indices (values) used for subtractive notation, e.g. CM.
//            }
//            else // Otherwise it's a simple add.
//              number += TSelf.CreateChecked(value);
//          }

//          return true;
//        }
//        catch { }

//        number = default!;
//        return false;
//      }
//    }
//  }
//}
