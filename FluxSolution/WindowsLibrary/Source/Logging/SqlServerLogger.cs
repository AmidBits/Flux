namespace Flux.Logging
{
  public static class SqlServerLoggerExtensions
  {
    [System.CLSCompliant(false)]
    public static Microsoft.Extensions.Logging.ILoggerFactory AddSqlServerLogger(this Microsoft.Extensions.Logging.ILoggerFactory loggerFactory, SqlServerLoggerConfiguration config)
    {
      loggerFactory.AddProvider(new SqlServerLoggerProvider(config));
      return loggerFactory;
    }
    [System.CLSCompliant(false)]
    public static Microsoft.Extensions.Logging.ILoggerFactory AddSqlServerLogger(this Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
    {
      var config = new SqlServerLoggerConfiguration();
      return loggerFactory.AddSqlServerLogger(config);
    }
    [System.CLSCompliant(false)]
    public static Microsoft.Extensions.Logging.ILoggerFactory AddSqlServerLogger(this Microsoft.Extensions.Logging.ILoggerFactory loggerFactory, System.Action<SqlServerLoggerConfiguration> configure)
    {
      var config = new SqlServerLoggerConfiguration();
      configure(config);
      return loggerFactory.AddSqlServerLogger(config);
    }
  }

  public class SqlServerLoggerConfiguration
  {
    [System.CLSCompliant(false)]
    public Microsoft.Extensions.Logging.LogLevel LogLevel { get; set; } = Microsoft.Extensions.Logging.LogLevel.Information;
    public int EventId { get; set; } = 0;
    public string FormatString { get; set; } = @"{0}|{1}|{2}|{3}|{4}";
  }

  public class SqlServerLoggerConfigurationXml
  {
    public string FormatString { get; set; } = @"<Log><Level>{0}</Level><Event><Id>{1}</Id><Name>{2}</Name></Event><State>{3}</State><Exeption><Message>{4}</Message></Exception></Log>";
  }

  public class SqlServerLogger : Microsoft.Extensions.Logging.ILogger
  {
    private readonly string _name;
    private readonly SqlServerLoggerConfiguration _config;

    public SqlServerLogger(string name, SqlServerLoggerConfiguration config)
    {
      _name = name;
      _config = config;
    }

    public System.IDisposable BeginScope<TState>(TState state) => null!;

    [System.CLSCompliant(false)]
    public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel) => logLevel == _config.LogLevel;

    /*
     * LogLevel [nvarchar](16),
     * EventId [int],
     * EventName [nvarchar](128)
     * Entry [nvarchar](MAX)
     */
    [System.CLSCompliant(false)]
    public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, Microsoft.Extensions.Logging.EventId eventId, TState state, System.Exception? exception, System.Func<TState, System.Exception, string> formatter)
    {
      exception ??= new System.Exception(@"No exception available.");

      if (IsEnabled(logLevel))
      {
        if (_config.EventId == 0 || _config.EventId == eventId.Id)
        {
          System.Console.WriteLine($"{logLevel} : {eventId.Id} : {_name} : {formatter(state, exception)}");
        }
      }
    }

    [System.CLSCompliant(false)]
    public void LogXml<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, Microsoft.Extensions.Logging.EventId eventId, TState state, System.Exception exception, System.Func<TState, System.Exception, string> formatter)
    {
      if (IsEnabled(logLevel))
      {
        if (_config.EventId == 0 || _config.EventId == eventId.Id)
        {
          var xeLog = new System.Xml.Linq.XElement(@"LogEntry");

          var xeLogLevel = new System.Xml.Linq.XElement(@"LogLevel");
          xeLogLevel.SetAttributeValue(@"Name", logLevel.ToString());
          xeLogLevel.SetAttributeValue(@"Value", ((int)logLevel).ToString());
          xeLog.Add(xeLogLevel);

          var xeEventId = new System.Xml.Linq.XElement(@"Event");
          xeEventId.SetAttributeValue(@"Id", $"{eventId.Id}");
          xeEventId.SetAttributeValue(@"Name", eventId.Name);
          xeLog.Add(xeEventId);

          System.Console.WriteLine($"{logLevel} : {eventId.Id} : {_name} : {formatter(state, exception)}");
        }
      }
    }
  }

  public class SqlServerLoggerProvider
    : Flux.Disposable, Microsoft.Extensions.Logging.ILoggerProvider
  {
    private readonly SqlServerLoggerConfiguration _config;
    private readonly System.Collections.Concurrent.ConcurrentDictionary<string, SqlServerLogger> _loggers = new System.Collections.Concurrent.ConcurrentDictionary<string, SqlServerLogger>();

    public SqlServerLoggerProvider(SqlServerLoggerConfiguration config) => _config = config;

    [System.CLSCompliant(false)]
    public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName) => _loggers.GetOrAdd(categoryName, name => new SqlServerLogger(name, _config));

    protected override void DisposeManaged()
      => _loggers.Clear();
  }

}
