namespace Flux.Quantity
{
  public interface IValuedUnit
  {
    /// <summary>The unit Value.</summary>
    double Value { get; }

    /// <summary>The unit Value squared.</summary>
    double ValueSquared
      => Value * Value;
    /// <summary>The unit Value cubed.</summary>
    double ValueCubed
      => Value * Value * Value;
  }
}
