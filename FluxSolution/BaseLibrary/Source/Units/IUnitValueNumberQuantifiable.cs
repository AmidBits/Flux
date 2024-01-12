namespace Flux
{
  /// <summary>An interface representing a quantifiable value number that can be converted to other units.</summary>
  /// <typeparam name="TValue">The value number type.</typeparam>
  /// <typeparam name="TUnit">The unit enum.</typeparam>
  public interface IUnitValueNumberQuantifiable<TValue, TUnit>
    : IUnitValueQuantifiable<TValue, TUnit>
    where TValue : struct, System.Numerics.INumber<TValue>
    where TUnit : notnull, System.Enum
  {
  }
}
