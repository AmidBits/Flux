namespace Flux
{
  public static partial class Number
  {
    extension<TNumber>(TNumber)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      /// <summary>
      /// <para>Internal failure shortcut for Try paradigm.</para>
      /// </summary>
      /// <param name="result">Contains <see langword="default"/>(<typeparamref name="TNumber"/>) on return.</param>
      /// <returns></returns>
      internal static bool FailWithDefault(out TNumber result)
      {
        result = default!;
        return false;
      }

      #region IsConsideredPlural

      /// <summary>
      /// <para>Determines whether the number is considered plural in terms of writing.</para>
      /// <para></para>
      /// </summary>
      /// <remarks>This function consider all numbers (e.g. 1.0, 2, etc.), except <c>integer</c> types equal to 1, to be plural.</remarks>
      /// <returns></returns>
      public static bool IsConsideredPlural(TNumber value, System.Globalization.CultureInfo? cultureInfo = null)
        => (cultureInfo!).GetGrammaticalNumber(value) == GrammaticalNumber.Other; // Only an integer 1 (not 1.0) is singular, otherwise a number is considered plural.

      #endregion

      #region NormalizeZero

      /// <summary>
      /// <para>Normalize any (including negative) zero to <see cref="TNumber.Zero"/>, e.g. <c>-0.0 normalizes to 0.0</c>.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static TNumber NormalizeZero(TNumber value)
        => TNumber.IsZero(value) ? TNumber.Zero : value;

      #endregion

      #region ToSingularOrPluralString

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="number"></param>
      /// <param name="singularWord"></param>
      /// <param name="pluralExtension"></param>
      /// <param name="offsetFromSingularEnd"></param>
      /// <returns></returns>
      public static string ToSingularOrPluralString(TNumber number, string singularWord, string pluralExtension, int offsetFromSingularEnd = 0)
        => IsConsideredPlural(number)
        ? singularWord[..^int.Abs(offsetFromSingularEnd)] + pluralExtension
        : singularWord;

      public static string ToSingularOrPluralString(TNumber number, string singularWord, char pluralExtension, int offsetFromSingularEnd = 0)
        => ToSingularOrPluralString(number, singularWord, pluralExtension.ToString(), offsetFromSingularEnd);

      #endregion

      #region IsNegativeOrZero

      /// <summary>
      /// Determines whether a value is negative or zero.
      /// </summary>
      public static bool IsNegativeOrZero(TNumber value) => value <= TNumber.Zero;

      #endregion

      #region IsPositiveOrZero

      /// <summary>
      /// Determines whether a value is positive or zero.
      /// </summary>
      public static bool IsPositiveOrZero(TNumber value) => TNumber.IsPositive(value);

      #endregion

      #region IsStrictlyNegative

      /// <summary>
      /// Determines whether a value is strictly negative (zero excluded).
      /// </summary>
      public static bool IsStrictlyNegative(TNumber value) => TNumber.IsNegative(value);

      #endregion

      #region IsStrictlyPositive

      /// <summary>
      /// Determines whether a value is strictly positive (zero excluded).
      /// </summary>
      public static bool IsStrictlyPositive(TNumber value) => value > TNumber.Zero;

      #endregion

      #region TryAdd

      public static bool TryAdd(TNumber left, TNumber right, out TNumber sum)
      {
        if (NumericTypeInfo<TNumber>.ImplementsIFloatingPoint)
        {
          sum = left + right;
          return TNumber.IsFinite(sum);
        }
        else if (NumericTypeInfo<TNumber>.IsSystemDecimal)
        {
          try
          {
            sum = left + right;
            return true;
          }
          catch (OverflowException)
          {
            return FailWithDefault(out sum);
          }
        }
        else if (NumericTypeInfo<TNumber>.IsUnsignedInteger)
        {
          var max = NumberBaseConstants<TNumber>.LargestInteger;

          if (left > max - right)
            return FailWithDefault(out sum);
        }
        else if (NumericTypeInfo<TNumber>.IsSignedInteger)
        {
          var max = NumberBaseConstants<TNumber>.LargestInteger;
          var min = NumberBaseConstants<TNumber>.SmallestInteger;

          if ((right > TNumber.Zero && left > max - right) || (right < TNumber.Zero && left < min - right))
            return FailWithDefault(out sum);
        }

        sum = left + right;
        return true;
      }

      #endregion

      #region TryDivide

      public static bool TryDivide(TNumber left, TNumber right, out TNumber quotient)
      {
        if (TNumber.IsZero(right))
          return FailWithDefault(out quotient);

        if (NumericTypeInfo<TNumber>.ImplementsIFloatingPoint)
        {
          quotient = left / right;
          return TNumber.IsFinite(quotient);
        }
        else if (NumericTypeInfo<TNumber>.IsSystemDecimal)
        {
          try
          {
            quotient = left / right;
            return true;
          }
          catch (OverflowException)
          {
            return FailWithDefault(out quotient);
          }
        }
        else if (NumericTypeInfo<TNumber>.IsSignedInteger)
        {
          var min = NumberBaseConstants<TNumber>.SmallestInteger;

          if ((left == min) && (right == -TNumber.One)) // MinValue / -1.
            return FailWithDefault(out quotient);
        }

        quotient = left / right;
        return true;
      }

      #endregion

      #region TryMultiply

      public static bool TryMultiply(TNumber left, TNumber right, out TNumber product)
      {
        if (TNumber.IsZero(left) || TNumber.IsZero(right))
        {
          product = TNumber.Zero;
          return true;
        }

        if (NumericTypeInfo<TNumber>.ImplementsIFloatingPoint)
        {
          product = left * right;
          return TNumber.IsFinite(product);
        }
        else if (NumericTypeInfo<TNumber>.IsSystemDecimal)
        {
          try
          {
            product = left * right;
            return true;
          }
          catch (OverflowException)
          {
            return FailWithDefault(out product);
          }
        }
        else if (NumericTypeInfo<TNumber>.IsUnsignedInteger)
        {
          var max = NumberBaseConstants<TNumber>.LargestInteger;

          if (left > max / right)
            return FailWithDefault(out product);
        }
        else if (NumericTypeInfo<TNumber>.IsSignedInteger)
        {
          var max = NumberBaseConstants<TNumber>.LargestInteger;
          var min = NumberBaseConstants<TNumber>.SmallestInteger;

          if ((left == min && right == -TNumber.One) || (right == min && left == -TNumber.One)) // MinValue * -1.
            return FailWithDefault(out product);

          if (left > TNumber.Zero
                ? (right > TNumber.Zero
                    ? left > max / right
                    : right < min / left)
                : (right > TNumber.Zero
                    ? left < min / right
                    : left < max / right))
            return FailWithDefault(out product);
        }

        product = left * right;
        return true;
      }

      #endregion

      #region TrySubtract

      public static bool TrySubtract(TNumber left, TNumber right, out TNumber difference)
      {
        if (NumericTypeInfo<TNumber>.ImplementsIFloatingPoint)
        {
          difference = left - right;
          return TNumber.IsFinite(difference);
        }
        else if (NumericTypeInfo<TNumber>.IsSystemDecimal)
        {
          try
          {
            difference = left - right;
            return true;
          }
          catch (OverflowException)
          {
            return FailWithDefault(out difference);
          }
        }
        else if (NumericTypeInfo<TNumber>.IsUnsignedInteger)
        {
          if (left < right)
            return FailWithDefault(out difference);
        }
        else if (NumericTypeInfo<TNumber>.IsSignedInteger)
        {
          var max = NumberBaseConstants<TNumber>.LargestInteger;
          var min = NumberBaseConstants<TNumber>.SmallestInteger;

          if ((right < TNumber.Zero && left > max + right) || (right > TNumber.Zero && left < min + right))
            return FailWithDefault(out difference);
        }

        difference = left - right;
        return true;
      }

      #endregion
    }
  }
}
