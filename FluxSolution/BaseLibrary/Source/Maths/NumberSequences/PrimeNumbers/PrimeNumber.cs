namespace Flux.NumberSequences
{
  public sealed partial class PrimeNumber
  {
#if NET7_0_OR_GREATER

    public static (TSelf potentialPrimeTowardZero, TSelf potentialPrimeAwayFromZero) RoundToPotentialPrime<TSelf>(TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TSelf.CreateChecked(3) is var three && value <= three)
        return (TSelf.CreateChecked(2), three);
      else if (TSelf.CreateChecked(5) is var five && value < five)
        return (three, five);

      var (potentialPrimeMultipleTowardsZero, potentialPrimeMultipleAwayFromZero) = Flux.Maths.RoundToMultipleOf(value, TSelf.CreateChecked(6), false);

      if (potentialPrimeMultipleTowardsZero - TSelf.One is var tzTz && potentialPrimeMultipleAwayFromZero + TSelf.One is var afzAfz && potentialPrimeMultipleTowardsZero == potentialPrimeMultipleAwayFromZero)
        return (tzTz, afzAfz);
      else if (potentialPrimeMultipleTowardsZero + TSelf.One is var tzAfz && value <= tzAfz)
        return (tzTz, tzAfz);
      else if (potentialPrimeMultipleAwayFromZero - TSelf.One is var afzTz && value >= afzTz)
        return (afzTz, afzAfz);
      else
        return (tzAfz, afzTz);
    }

    public static TSelf RoundToPotentialPrime<TSelf>(TSelf value, RoundingMode mode, out TSelf potentialPrimeTowardZero, out TSelf potentialPrimeAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      (potentialPrimeTowardZero, potentialPrimeAwayFromZero) = RoundToPotentialPrime(value);

      return value.RoundToBoundaries(mode, potentialPrimeTowardZero, potentialPrimeAwayFromZero);
    }

#else
#endif
  }
}
