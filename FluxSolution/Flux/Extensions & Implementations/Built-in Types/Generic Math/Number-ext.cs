namespace Flux
{
  public static partial class Number
  {
    extension<TNumber>(TNumber)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      #region IsConsideredPlural

      /// <summary>
      /// <para>Determines whether the number is considered plural in terms of writing.</para>
      /// <para></para>
      /// </summary>
      /// <remarks>This function consider all numbers (e.g. 1.0, 2, etc.), except <c>integer</c> types equal to 1, to be plural.</remarks>
      /// <returns></returns>
      public static bool IsConsideredPlural(TNumber value)
        => !(value == TNumber.One && NumericTypeInfo<TNumber>.ImplementsIBinaryInteger); // Only an integer 1 (not 1.0) is singular, otherwise a number is considered plural.

      #endregion

      #region NormalizeZero

      /// <summary>
      /// <para>Normalize any (including negative) zero to <see cref="TNumber.Zero"/>.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static TNumber NormalizeZero(TNumber value)
        => TNumber.IsZero(value) ? TNumber.Zero : value;

      #endregion

      public static string ToPlural(string text, TNumber value)
      {
        if (!IsConsideredPlural(value))
          return text;

        var sb = new System.Text.StringBuilder(text);

        if (text.EndsWith('y'))
          sb.Append("ies");
        else if (text.EndsWith('x'))
          sb.Append("es");
        else if (!text.EndsWith('s'))
          sb.Append('s');

        return sb.ToString();
      }

      public static bool TryMultiply(TNumber left, TNumber right, out TNumber product)
      {
        if (left == TNumber.Zero || right == TNumber.Zero)
        {
          product = TNumber.Zero;
          return true;
        }

        if (NumericTypeInfo<TNumber>.IsUnsignedInteger && left > NumberBaseConstants<TNumber>.LargestInteger / right)
        {
          product = default!;
          return false;
        }

        if (NumericTypeInfo<TNumber>.ImplementsIBinaryInteger || NumericTypeInfo<TNumber>.IsSystemDecimal)
        {
          var max = NumberBaseConstants<TNumber>.LargestInteger;
          var min = NumberBaseConstants<TNumber>.SmallestInteger;

          if (left > TNumber.Zero)
          {
            if (right > TNumber.Zero)
            {
              if (left > max / right)
              {
                product = default!;
                return false;
              }
            }
            else
            {
              if (right < min / left)
              {
                product = default!;
                return false;
              }
            }
          }
          else
          {
            if (right > TNumber.Zero)
            {
              if (left < min / right)
              {
                product = default!;
                return false;
              }
            }
            else
            {
              if (left < max / right)
              {
                product = default!;
                return false;
              }
            }
          }
        }

        if (NumericTypeInfo<TNumber>.ImplementsIFloatingPoint)
        {
          product = left * right;

          return !TNumber.IsNaN(product) && !TNumber.IsInfinity(product);
        }

        product = left * right;
        return true;
      }
    }
  }
}
