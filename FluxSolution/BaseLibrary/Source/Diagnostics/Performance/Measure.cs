namespace Flux.Diagnostics
{
  public static partial class Performance
  {
    public struct MeasuredResult
    {
      public System.TimeSpan AverageTime => new System.TimeSpan(TotalTime.Ticks / Iterations);

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

          if (!expectedResultTypeName.Equals(lastResultTypeName, System.StringComparison.Ordinal)) throw new System.Exception($"Assertion expected({expectedResult}) <{expectedResultTypeName}> != actual({LastResult}) <{lastResultTypeName}>");
          else throw new System.ArgumentException($"Assertion expected({expectedResultTypeName}) <{expectedResult}> != actual({lastResultTypeName}) <{LastResult}>", nameof(expectedResult));
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
        {
          throw new System.ArgumentException($"Assertion expected less than <{maxTotalSeconds}> but actual was <{TotalTime.TotalSeconds}> ({LastResult?.GetType().Name ?? @"null"})", nameof(maxTotalSeconds));
        }
      }
      public void Assert(System.TimeSpan maxTotalTime)
      {
        if (TotalTime > maxTotalTime)
        {
          throw new System.ArgumentException($"Assertion expected less than <{maxTotalTime}> but actual was <{TotalTime}> ({LastResult?.GetType().Name ?? @"null"})", nameof(maxTotalTime));
        }
      }

      public override string ToString() => $"<{Identifier}, x {Iterations.ToGroupString()}, {TotalTime} (avg {new System.TimeSpan(TotalTime.Ticks / (Iterations > 0 ? Iterations : 1))}) {(LastResult is null ? @"null" : $"{LastResult}[{LastResult.GetType().Name}]")}>";
    }

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

    //public static (string expression, int iterations, System.TimeSpan totalTime, System.TimeSpan averageTime, object lastResult, string text) Measure<T>(System.Linq.Expressions.Expression<System.Func<T>> expression, int iterations = 1000000, string name = null)
    //{
    //  var expressionText = name ?? System.Text.RegularExpressions.Regex.Replace(expression.Body.ToString(), @"(^Convert\(|value\([^\)]+\)\.|, Object\)$)", string.Empty);

    //  var compiledExpression = expression.Compile();

    //  var stopWatch = new System.Diagnostics.Stopwatch();

    //  var elapsedTime = System.TimeSpan.Zero;

    //  System.GC.Collect();
    //  System.GC.WaitForPendingFinalizers();
    //  System.GC.Collect();

    //  var processorAffinity = System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity;
    //  var processPriorityClass = System.Diagnostics.Process.GetCurrentProcess().PriorityClass;
    //  var threadPriority = System.Threading.Thread.CurrentThread.Priority;

    //  System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity = new System.IntPtr(processorAffinity.ToInt64() & 0x7FFFFFFFFFFFFFFE);
    //  System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.High; // Prevent "Normal" processes from interrupting.
    //  System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Highest; // Prevent "Normal" threads from interrupting.

    //  var lastReturnValue = compiledExpression();

    //  for (var iteration = 0; iteration < iterations; iteration = unchecked(iteration + 1))
    //  {
    //    stopWatch.Reset();
    //    stopWatch.Start();

    //    lastReturnValue = compiledExpression();

    //    stopWatch.Stop();

    //    elapsedTime += stopWatch.Elapsed;
    //  }

    //  System.Threading.Thread.CurrentThread.Priority = threadPriority;
    //  System.Diagnostics.Process.GetCurrentProcess().PriorityClass = processPriorityClass;
    //  System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity = processorAffinity;

    //  var averageTime = new System.TimeSpan(elapsedTime.Ticks / iterations);

    //  //System.Diagnostics.Debug.WriteLine($"{expressionText} (x{iterations.ToGroupString()}) {elapsedTime} ({averageTime})");

    //  var text = $"<{expressionText}, {elapsedTime} ({new System.TimeSpan(elapsedTime.Ticks / iterations)} x {iterations.ToGroupString()}), [{lastReturnValue.GetType().Name}]={lastReturnValue}>";

    //  return (expressionText, iterations, elapsedTime, new System.TimeSpan(elapsedTime.Ticks / iterations), lastReturnValue, text);
    //}
    //[System.Diagnostics.ConditionalAttribute("DEBUG")]
    //public static void MeasureDebug<T>(System.Linq.Expressions.Expression<System.Func<T>> expression, int iterations = 1000000, string name = null)
    //{
    //  var expressionText = name ?? System.Text.RegularExpressions.Regex.Replace(expression.Body.ToString(), @"(^Convert\(|value\([^\)]+\)\.|, Object\)$)", string.Empty); ;

    //  var result = Measure<T>(expression, iterations, name);

    //  System.Diagnostics.Debug.Write($"{expressionText} (x {iterations.ToGroupString()}) {result.totalTime} ({result.averageTime})");
    //  System.Diagnostics.Debug.WriteLine($": \"{result.lastResult}\"");
    //}

    //public static (string expression, int iterations, System.TimeSpan totalTime, System.TimeSpan averageTime, string text) Measure(System.Linq.Expressions.Expression<System.Action> expression, int iterations = 1000000, string name = null)
    //{
    //  var expressionText = name ?? System.Text.RegularExpressions.Regex.Replace(expression.Body.ToString(), @"(^Convert\(|value\([^\)]+\)\.|, Object\)$)", string.Empty); ;

    //  var compiledExpression = expression.Compile();

    //  var stopWatch = new System.Diagnostics.Stopwatch();

    //  var elapsedTime = System.TimeSpan.Zero;

    //  System.GC.Collect();
    //  System.GC.WaitForPendingFinalizers();
    //  System.GC.Collect();

    //  var processorAffinity = System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity;
    //  var processPriorityClass = System.Diagnostics.Process.GetCurrentProcess().PriorityClass;
    //  var threadPriority = System.Threading.Thread.CurrentThread.Priority;

    //  System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity = new System.IntPtr(processorAffinity.ToInt64() & 0x7FFFFFFFFFFFFFFE);
    //  System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.High; // Prevent "Normal" processes from interrupting.
    //  System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Highest; // Prevent "Normal" threads from interrupting.

    //  compiledExpression();

    //  for (var iteration = 1; iteration < iterations; iteration = unchecked(iteration + 1))
    //  {
    //    stopWatch.Reset();
    //    stopWatch.Start();

    //    compiledExpression();

    //    stopWatch.Stop();

    //    elapsedTime += stopWatch.Elapsed;
    //  }

    //  System.Threading.Thread.CurrentThread.Priority = threadPriority;
    //  System.Diagnostics.Process.GetCurrentProcess().PriorityClass = processPriorityClass;
    //  System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity = processorAffinity;

    //  var averageTime = new System.TimeSpan(elapsedTime.Ticks / iterations);

    //  //System.Diagnostics.Debug.WriteLine($"{expressionText} (x{iterations.ToGroupString()}) {elapsedTime} ({averageTime})");

    //  var text = $"<{expressionText}, {elapsedTime} ({new System.TimeSpan(elapsedTime.Ticks / iterations)} x {iterations.ToGroupString()})>";

    //  return (expressionText, iterations, elapsedTime, averageTime, text);
    //}
    //[System.Diagnostics.ConditionalAttribute("DEBUG")]
    //public static void MeasureDebug(System.Linq.Expressions.Expression<System.Action> expression, int iterations = 1000000)
    //{
    //  var expressionText = System.Text.RegularExpressions.Regex.Replace(expression.Body.ToString(), @"(^Convert\(|value\([^\)]+\)\.|, Object\)$)", string.Empty); ;

    //  var result = Measure(expression, iterations);

    //  System.Diagnostics.Debug.WriteLine($"{expressionText} (x{iterations.ToGroupString()}) {result.totalTime} ({result.averageTime})");
    //}

    //public class Test
    //{
    //  private System.Func<object> m_compiledExpression;

    //  public System.TimeSpan AverageTime { get; private set; }

    //  public System.TimeSpan ElapsedTime { get; private set; }

    //  public string ExpressionText { get; private set; }

    //  public int Iterations { get; private set; }

    //  public object LastReturnValue { get; private set; }

    //  public Test(System.Linq.Expressions.Expression<System.Func<object>> expression, int iterations, bool autoRun)
    //  {
    //    m_compiledExpression = expression.Compile();

    //    ExpressionText = System.Text.RegularExpressions.Regex.Replace(expression.Body.ToString(), @"(^Convert\(|value\([^\)]+\)\.|, Object\)$)", string.Empty); ;

    //    Iterations = iterations;

    //    if (autoRun) Run();
    //  }
    //  public Test(System.Linq.Expressions.Expression<System.Func<object>> expression, int iterations)
    //    : this(expression, iterations, false)
    //  {
    //  }
    //  public Test(System.Linq.Expressions.Expression<System.Func<object>> expression)
    //    : this(expression, 1000000, false)
    //  {
    //  }

    //  [System.Diagnostics.ConditionalAttribute("DEBUG")]
    //  public void Run()
    //  {
    //    var stopWatch = new System.Diagnostics.Stopwatch();

    //    ElapsedTime = System.TimeSpan.Zero;

    //    System.GC.Collect();
    //    System.GC.WaitForPendingFinalizers();
    //    System.GC.Collect();

    //    var processorAffinity = System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity;
    //    var processPriorityClass = System.Diagnostics.Process.GetCurrentProcess().PriorityClass;
    //    var threadPriority = System.Threading.Thread.CurrentThread.Priority;

    //    System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity = new System.IntPtr(processorAffinity.ToInt64() & 0x7FFFFFFFFFFFFFFE);
    //    System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.High; // Prevent "Normal" processes from interrupting.
    //    System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Highest; // Prevent "Normal" threads from interrupting.

    //    LastReturnValue = m_compiledExpression();

    //    for (var iteration = 0; iteration < Iterations; iteration = unchecked(iteration + 1))
    //    {
    //      stopWatch.Reset();
    //      stopWatch.Start();

    //      LastReturnValue = m_compiledExpression();

    //      stopWatch.Stop();

    //      ElapsedTime += stopWatch.Elapsed;
    //    }

    //    System.Threading.Thread.CurrentThread.Priority = threadPriority;
    //    System.Diagnostics.Process.GetCurrentProcess().PriorityClass = processPriorityClass;
    //    System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity = processorAffinity;

    //    AverageTime = new System.TimeSpan(ElapsedTime.Ticks / Iterations);

    //    System.Diagnostics.Debug.WriteLine($"{ExpressionText} (x{Iterations.ToGroupString()}) {ElapsedTime} ({AverageTime})");
    //    System.Diagnostics.Debug.WriteLine($"= {LastReturnValue}");
    //  }
    //}
  }
}
