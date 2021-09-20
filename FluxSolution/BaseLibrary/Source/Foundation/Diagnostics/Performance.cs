namespace Flux.Services
{
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
