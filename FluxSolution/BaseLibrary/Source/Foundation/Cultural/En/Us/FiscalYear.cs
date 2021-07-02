namespace Flux.Cultural.EnUs
{
  public static partial class FiscalYear
  {
    public enum Scope
    {
      FederalGovernment,
      OtherStates,
      OtherTerritories,
      StateOfAlabama,
      StateOfMichigan,
      StateOfNewYork,
      StateOfTexas,
      TerritoryOfPuertoRico,
    }

    /// <summary>Gets the fiscal year of the specified scope for the datetime.</summary>
    public static int Get(System.DateTime source, Scope scope)
    {
      switch (scope)
      {
        case Scope.OtherStates:
        case Scope.TerritoryOfPuertoRico:
          return source.Month >= 7 ? source.Year + 1 : source.Year;
        case Scope.FederalGovernment:
        case Scope.OtherTerritories:
        case Scope.StateOfAlabama:
        case Scope.StateOfMichigan:
          return source.Month >= 10 ? source.Year + 1 : source.Year;
        case Scope.StateOfTexas:
          return source.Month >= 9 ? source.Year + 1 : source.Year;
        case Scope.StateOfNewYork:
          return source.Month >= 3 ? source.Year + 1 : source.Year;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(scope));
      }
    }
  }
}
