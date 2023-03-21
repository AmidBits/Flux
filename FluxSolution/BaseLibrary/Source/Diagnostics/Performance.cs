namespace Flux.Services
{
  public static partial class Performance
  {
    [System.Text.RegularExpressions.GeneratedRegex(@"(^Convert\(|value\([^\)]+\)\.|, Object\)$)")]
    private static partial System.Text.RegularExpressions.Regex ReplacerRegex();

    /// <summary></summary>
    public static PerformanceResult Measure(System.Linq.Expressions.Expression<System.Func<object>> expression, int iterations = 1000000, string? name = null)
    {
      if (expression is null) throw new System.ArgumentNullException(nameof(expression));

      var compiledExpression = expression.Compile();

      var stopWatch = new System.Diagnostics.Stopwatch();

      System.GC.Collect();
      System.GC.WaitForPendingFinalizers();
      System.GC.Collect();

      System.IntPtr processorAffinity = default;
      if (System.OperatingSystem.IsWindows() || System.OperatingSystem.IsLinux())
      {
        processorAffinity = System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity;
        System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity = new System.IntPtr(processorAffinity.ToInt64() & 0x7FFFFFFFFFFFFFFE);
      }

      var processPriorityClass = System.Diagnostics.Process.GetCurrentProcess().PriorityClass;
      var threadPriority = System.Threading.Thread.CurrentThread.Priority;

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

      if (System.OperatingSystem.IsWindows() || System.OperatingSystem.IsLinux())
        System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity = processorAffinity;

      return new PerformanceResult(name ?? ReplacerRegex().Replace(expression.Body.ToString(), string.Empty), iterations, lastResult, stopWatch.Elapsed);
    }

    /// <summary></summary>
    public static PerformanceResult Measure(System.Linq.Expressions.Expression<System.Action> expression, int iterations = 1000000, string? name = null)
    {
      if (expression is null) throw new System.ArgumentNullException(nameof(expression));

      var compiledExpression = expression.Compile();

      var stopWatch = new System.Diagnostics.Stopwatch();

      System.GC.Collect();
      System.GC.WaitForPendingFinalizers();
      System.GC.Collect();

      System.IntPtr processorAffinity = default;
      if (System.OperatingSystem.IsWindows() || System.OperatingSystem.IsLinux())
      {
        processorAffinity = System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity;
        System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity = new System.IntPtr(processorAffinity.ToInt64() & 0x7FFFFFFFFFFFFFFE);
      }

      var processPriorityClass = System.Diagnostics.Process.GetCurrentProcess().PriorityClass;
      var threadPriority = System.Threading.Thread.CurrentThread.Priority;

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

      if (System.OperatingSystem.IsWindows() || System.OperatingSystem.IsLinux())
        System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity = processorAffinity;

      return new PerformanceResult(name ?? ReplacerRegex().Replace(expression.Body.ToString(), string.Empty), iterations, null, stopWatch.Elapsed);
    }
  }
}
