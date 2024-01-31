namespace Flux.Services
{
  public record struct PerformanceResult
  {
    public readonly System.TimeSpan AverageTime
      => new(TotalTime.Ticks / Iterations);

    public string Identifier { get; private set; }
    public int Iterations { get; private set; }
    public object? LastResult { get; private set; }
    public System.TimeSpan TotalTime { get; private set; }

    public PerformanceResult(string identifier, int iterations, object? lastResult, System.TimeSpan totalTime)
    {
      Identifier = identifier;
      Iterations = iterations;
      LastResult = lastResult;
      TotalTime = totalTime;
    }

    private readonly void Assert(object? expectedResult)
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
    public readonly void Assert(object expectedResult, double maxTotalSeconds)
    {
      Assert(expectedResult);
      Assert(maxTotalSeconds);
    }
    public readonly void Assert(object expectedResult, System.TimeSpan maxTotalTime)
    {
      Assert(expectedResult);
      Assert(maxTotalTime);
    }
    public readonly void Assert(double maxTotalSeconds)
    {
      if (TotalTime.TotalSeconds > maxTotalSeconds)
        throw new System.ArgumentException($"Assertion expected less than <{maxTotalSeconds}> but actual was <{TotalTime.TotalSeconds}> ({LastResult?.GetType().Name ?? @"null"})", nameof(maxTotalSeconds));
    }
    public readonly void Assert(System.TimeSpan maxTotalTime)
    {
      if (TotalTime > maxTotalTime)
        throw new System.ArgumentException($"Assertion expected less than <{maxTotalTime}> but actual was <{TotalTime}> ({LastResult?.GetType().Name ?? @"null"})", nameof(maxTotalTime));
    }

    public readonly override string ToString()
      => $"{GetType().Name} {{ {Identifier}, x {Iterations:#,###0}, {TotalTime} (avg {new System.TimeSpan(TotalTime.Ticks / (Iterations > 0 ? Iterations : 1))}) {(LastResult is null ? @"null" : $"{LastResult}[{LastResult.GetType().Name}]")} }}";
  }
}
