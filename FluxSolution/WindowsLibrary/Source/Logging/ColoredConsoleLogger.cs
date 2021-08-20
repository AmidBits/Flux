namespace LoggerApp
{
  public static class ColoredConsoleLoggerExtensions
  {
    [System.CLSCompliant(false)]
    public static Microsoft.Extensions.Logging.ILoggerFactory AddColoredConsoleLogger(this Microsoft.Extensions.Logging.ILoggerFactory loggerFactory, ColoredConsoleLoggerConfiguration config)
    {
      loggerFactory.AddProvider(new ColoredConsoleLoggerProvider(config));
      return loggerFactory;
    }
    [System.CLSCompliant(false)]
    public static Microsoft.Extensions.Logging.ILoggerFactory AddColoredConsoleLogger(this Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
    {
      var config = new ColoredConsoleLoggerConfiguration();
      return loggerFactory.AddColoredConsoleLogger(config);
    }
    [System.CLSCompliant(false)]
    public static Microsoft.Extensions.Logging.ILoggerFactory AddColoredConsoleLogger(this Microsoft.Extensions.Logging.ILoggerFactory loggerFactory, System.Action<ColoredConsoleLoggerConfiguration> configure)
    {
      var config = new ColoredConsoleLoggerConfiguration();
      configure(config);
      return loggerFactory.AddColoredConsoleLogger(config);
    }
  }

  public class ColoredConsoleLoggerConfiguration
  {
    [System.CLSCompliant(false)]
    public Microsoft.Extensions.Logging.LogLevel LogLevel { get; set; } = Microsoft.Extensions.Logging.LogLevel.Warning;
    public int EventId { get; set; } = 0;

    public System.ConsoleColor BackgroundColor { get; set; } = System.ConsoleColor.Black;
    public System.ConsoleColor ForegroundColor { get; set; } = System.ConsoleColor.Yellow;
  }

  public class ColoredConsoleLogger : Microsoft.Extensions.Logging.ILogger
  {
    private readonly string _name;
    private readonly ColoredConsoleLoggerConfiguration _config;

    public ColoredConsoleLogger(string name, ColoredConsoleLoggerConfiguration config)
    {
      _name = name;
      _config = config;
    }

    public System.IDisposable BeginScope<TState>(TState state) => null!;

    [System.CLSCompliant(false)]
    public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel) => logLevel == _config.LogLevel;

    [System.CLSCompliant(false)]
    public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, Microsoft.Extensions.Logging.EventId eventId, TState state, System.Exception exception, System.Func<TState, System.Exception, string> formatter)
    {
      if (IsEnabled(logLevel))
      {
        if (_config.EventId == 0 || _config.EventId == eventId.Id)
        {
          var backgroundColor = System.Console.BackgroundColor;
          var foregroundColor = System.Console.ForegroundColor;
          System.Console.BackgroundColor = _config.BackgroundColor;
          System.Console.ForegroundColor = _config.ForegroundColor;
          System.Console.WriteLine($"{logLevel} : {eventId.Id} : {_name} : {formatter?.Invoke(state, exception) ?? $"{state ?? default} {exception.Message}"}");
          System.Console.BackgroundColor = backgroundColor;
          System.Console.ForegroundColor = foregroundColor;
        }
      }
    }
  }

  public class ColoredConsoleLoggerProvider
    : Flux.Disposable, Microsoft.Extensions.Logging.ILoggerProvider
  {
    private readonly ColoredConsoleLoggerConfiguration _config;
    private readonly System.Collections.Concurrent.ConcurrentDictionary<string, ColoredConsoleLogger> _loggers = new System.Collections.Concurrent.ConcurrentDictionary<string, ColoredConsoleLogger>();

    public ColoredConsoleLoggerProvider(ColoredConsoleLoggerConfiguration config) => _config = config;

    [System.CLSCompliant(false)]
    public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName) => _loggers.GetOrAdd(categoryName, name => new ColoredConsoleLogger(name, _config));

    protected override void DisposeManaged()
      => _loggers.Clear();
  }
}
