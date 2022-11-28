namespace Flux.AmbOps
{
  public sealed class Choices<T>
     : IChoices, IValue<T>
  {
    private readonly T[] Values;

    public Choices(params T[] values) => Values = values;

    [System.Diagnostics.Contracts.Pure]
    public T Value
      => Values[Index];

    #region Implemented interfaces
    // IChoices
    public int Index
    { get; set; }
    [System.Diagnostics.Contracts.Pure]
    public int Length
      => Values.Length;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure]
    public override string ToString()
      => Value?.ToString() ?? string.Empty;
    #endregion Object overrides
  }
}
