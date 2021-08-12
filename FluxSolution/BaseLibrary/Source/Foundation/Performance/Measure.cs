namespace Flux.Services
{
  public struct MeasuredResult
    : System.IEquatable<MeasuredResult>
  {
    public static readonly MeasuredResult Empty;
    public bool IsEmpty => Equals(Empty);

    public System.TimeSpan AverageTime
      => new System.TimeSpan(TotalTime.Ticks / Iterations);

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
        var expectedResultTypeName = expectedResult?.GetType().Name ?? "null";
        var lastResultTypeName = LastResult?.GetType().Name ?? "null";

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

    #region Static members
    // Operators
    public static bool operator ==(MeasuredResult a, MeasuredResult b)
      => a.Equals(b);
    public static bool operator !=(MeasuredResult a, MeasuredResult b)
      => !a.Equals(b);
    #endregion Static members

    // System.IEquatable
    public bool Equals(MeasuredResult other)
      => Identifier == other.Identifier && Iterations == other.Iterations && LastResult == other.LastResult && TotalTime == other.TotalTime;

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is MeasuredResult o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Identifier, Iterations, LastResult, TotalTime);
    public override string ToString()
      => $"<{Identifier}, x {Iterations.ToGroupString()}, {TotalTime} (avg {new System.TimeSpan(TotalTime.Ticks / (Iterations > 0 ? Iterations : 1))}) {(LastResult is null ? @"null" : $"{LastResult}[{LastResult.GetType().Name}]")}>";
  }

  public static partial class Performance
  {
    /// <summary></summary>
    public static MeasuredResult Measure(System.Linq.Expressions.Expression<System.Func<object>> expression, int iterations = 1000000, string? name = null)
    {
      if (expression is null) throw new System.ArgumentNullException(nameof(expression));

      var compiledExpression = expression.Compile();

      var stopWatch = new System.Diagnostics.Stopwatch();

      System.GC.Collect();
      System.GC.WaitForPendingFinalizers();
      System.GC.Collect();

      var processorAffinity = System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity;
      var processPriorityClass = System.Diagnostics.Process.GetCurrentProcess().PriorityClass;
      var threadPriority = System.Threading.Thread.CurrentThread.Priority;

      System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity = new System.IntPtr(processorAffinity.ToInt64() & 0x7FFFFFFFFFFFFFFE);
      System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.High; // Prevent "Normal" processes from interrupting.
      System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Highest; // Prevent "Normal" threads from interrupting.

      object? lastResult = null;

      for (var iteration = 0; iteration < iterations; iteration = unchecked(iteration + 1))
      {
        stopWatch.Start();
        lastResult = compiledExpression();
        stopWatch.Stop();
      }

      System.Threading.Thread.CurrentThread.Priority = threadPriority;
      System.Diagnostics.Process.GetCurrentProcess().PriorityClass = processPriorityClass;
      System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity = processorAffinity;

      return new MeasuredResult(name ?? System.Text.RegularExpressions.Regex.Replace(expression.Body.ToString(), @"(^Convert\(|value\([^\)]+\)\.|, Object\)$)", string.Empty), iterations, lastResult, stopWatch.Elapsed);
    }
    /// <summary></summary>
    public static MeasuredResult Measure(System.Linq.Expressions.Expression<System.Action> expression, int iterations = 1000000, string? name = null)
    {
      if (expression is null) throw new System.ArgumentNullException(nameof(expression));

      var compiledExpression = expression.Compile();

      var stopWatch = new System.Diagnostics.Stopwatch();

      System.GC.Collect();
      System.GC.WaitForPendingFinalizers();
      System.GC.Collect();

      var processorAffinity = System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity;
      var processPriorityClass = System.Diagnostics.Process.GetCurrentProcess().PriorityClass;
      var threadPriority = System.Threading.Thread.CurrentThread.Priority;

      System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity = new System.IntPtr(processorAffinity.ToInt64() & 0x7FFFFFFFFFFFFFFE);
      System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.High; // Prevent "Normal" processes from interrupting.
      System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Highest; // Prevent "Normal" threads from interrupting.

      for (var iteration = 0; iteration < iterations; iteration = unchecked(iteration + 1))
      {
        stopWatch.Start();
        compiledExpression();
        stopWatch.Stop();
      }

      System.Threading.Thread.CurrentThread.Priority = threadPriority;
      System.Diagnostics.Process.GetCurrentProcess().PriorityClass = processPriorityClass;
      System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity = processorAffinity;

      return new MeasuredResult(name ?? System.Text.RegularExpressions.Regex.Replace(expression.Body.ToString(), @"(^Convert\(|value\([^\)]+\)\.|, Object\)$)", string.Empty), iterations, null, stopWatch.Elapsed);
    }
  }
}
