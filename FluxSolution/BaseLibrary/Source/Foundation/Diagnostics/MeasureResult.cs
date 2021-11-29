namespace Flux.Services
{
#if NET5_0
  public struct MeasuredResult
    : System.IEquatable<MeasuredResult>
#else
  public record struct MeasuredResult
#endif
  {
    public static readonly MeasuredResult Empty;

    public System.TimeSpan AverageTime
      => new(TotalTime.Ticks / Iterations);

    public string Identifier { get; private set; }
    public int Iterations { get; private set; }
    public object? LastResult { get; private set; }
    public System.TimeSpan TotalTime { get; private set; }

    public MeasuredResult(string identifier, int iterations, object? lastResult, System.TimeSpan totalTime)
    {
      Identifier = identifier;
      Iterations = iterations;
      LastResult = lastResult;
      TotalTime = totalTime;
    }

    private void Assert(object? expectedResult)
    {
      if ((LastResult == null && expectedResult != null) || (LastResult != null && !LastResult.Equals(expectedResult)))
      {
        var expectedResultTypeName = expectedResult?.GetType().Name ?? @"null";
        var lastResultTypeName = LastResult?.GetType().Name ?? @"null";

        if (!expectedResultTypeName.Equals(lastResultTypeName, System.StringComparison.Ordinal))
          throw new System.Exception($"Assertion expected({expectedResult}) <{expectedResultTypeName}> != actual({LastResult}) <{lastResultTypeName}>");
        else
          throw new System.ArgumentException($"Assertion expected({expectedResultTypeName}) <{expectedResult}> != actual({lastResultTypeName}) <{LastResult}>", nameof(expectedResult));
      }
    }
    public void Assert(object expectedResult, double maxTotalSeconds)
    {
      Assert(expectedResult);
      Assert(maxTotalSeconds);
    }
    public void Assert(object expectedResult, System.TimeSpan maxTotalTime)
    {
      Assert(expectedResult);
      Assert(maxTotalTime);
    }
    public void Assert(double maxTotalSeconds)
    {
      if (TotalTime.TotalSeconds > maxTotalSeconds)
        throw new System.ArgumentException($"Assertion expected less than <{maxTotalSeconds}> but actual was <{TotalTime.TotalSeconds}> ({LastResult?.GetType().Name ?? @"null"})", nameof(maxTotalSeconds));
    }
    public void Assert(System.TimeSpan maxTotalTime)
    {
      if (TotalTime > maxTotalTime)
        throw new System.ArgumentException($"Assertion expected less than <{maxTotalTime}> but actual was <{TotalTime}> ({LastResult?.GetType().Name ?? @"null"})", nameof(maxTotalTime));
    }

    #region Overloaded operators
#if NET5_0
    public static bool operator ==(MeasuredResult a, MeasuredResult b)
      => a.Equals(b);
    public static bool operator !=(MeasuredResult a, MeasuredResult b)
      => !a.Equals(b);
#endif
    #endregion Overloaded operators

    #region Implemented interfaces
#if NET5_0
    // System.IEquatable
    public bool Equals(MeasuredResult other)
      => Identifier == other.Identifier && Iterations == other.Iterations && LastResult == other.LastResult && TotalTime == other.TotalTime;
#endif
    #endregion Implemented interfaces

    #region Object overrides
#if NET5_0
    public override bool Equals(object? obj)
      => obj is MeasuredResult o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Identifier, Iterations, LastResult, TotalTime);
#endif
    public override string ToString()
      => $"{GetType().Name} {{ {Identifier}, x {Iterations.ToGroupString()}, {TotalTime} (avg {new System.TimeSpan(TotalTime.Ticks / (Iterations > 0 ? Iterations : 1))}) {(LastResult is null ? @"null" : $"{LastResult}[{LastResult.GetType().Name}]")} }}";
    #endregion Object overrides
  }
}
