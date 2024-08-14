namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Converts a <paramref name="number"/> to a list of <paramref name="positionalNotationIndices"/>, based on the specified <paramref name="radix"/>.</summary>
    public static bool TryConvertNumberToPositionalNotationIndices<TValue, TRadix>(this TValue number, TRadix radix, out System.Collections.Generic.List<int> positionalNotationIndices)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      if (TValue.IsNegative(number))
        return TryConvertNumberToPositionalNotationIndices(TValue.Abs(number), radix, out positionalNotationIndices);

      positionalNotationIndices = new();

      try
      {
        Quantities.Radix.AssertMember(radix);

        while (!TValue.IsZero(number))
        {
          (number, var remainder) = TValue.DivRem(number, TValue.CreateChecked(radix));

          positionalNotationIndices.Insert(0, int.CreateChecked(remainder));
        }

        return true;
      }
      catch { }

      return false;
    }

    /// <summary>Converts a list of <paramref name="positionalNotationIndices"/> to a <paramref name="number"/>, based on the specified <paramref name="radix"/>.</summary>
    public static bool TryConvertPositionalNotationIndicesToNumber<TRadix, TValue>(this System.Collections.Generic.IList<int> positionalNotationIndices, TRadix radix, out TValue number)
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      number = TValue.Zero;

      try
      {
        Quantities.Radix.AssertMember(radix);

        for (var index = 0; index < positionalNotationIndices.Count; index++)
        {
          number *= TValue.CreateChecked(radix);

          number += TValue.CreateChecked(positionalNotationIndices[index]);
        }

        return true;
      }
      catch { }

      return false;
    }
  }
}
