using System.Linq;
using Flux;

namespace Wormhole
{
	public static class ExtenstionMethods
	{
		public static string EscapeTsql(this string source)
			=> source.Replace(@"'", @"''");
	}
	public class Core
	{
		public enum FlowType
		{
			Parallel,
			Serial
		}

		public interface IDataSource
		{
			System.Data.IDataReader Read(System.Xml.Linq.XElement source, Log log);
		}

		public interface IDataTarget
		{
			void Write(System.Data.IDataReader reader, System.Xml.Linq.XElement target, Log log);
		}

		public const string ApplicationName = @"Wormhole.NET";

		public const string MaxThreads = nameof(MaxThreads);

		//  public interface IDataExport
		//  {
		//    /*
		//SELECT '<Export Type="SqlClient" Entity="[' + @@SERVERNAME + '].[' + DB_NAME() + '].[' + S.[name] + '].[' + T.[name] + ']" Command="SELECT ' + STUFF((
		//SELECT ',' + '['+C.[name]+']' AS [text()]
		//FROM sys.columns C (NOLOCK)
		//WHERE  C.object_id = T.object_id
		//ORDER BY column_id
		//FOR XML PATH('')
		//), 1, 1, '') + ' FROM [' + DB_NAME() + '].[' + S.[name] + '].[' + T.[name] + '] (NOLOCK)" />'
		//FROM sys.schemas S (NOLOCK)
		//INNER JOIN sys.tables T (NOLOCK)
		//ON (S.schema_id = T.schema_id)
		//--WHERE T.[name] NOT LIKE '%[_]DE'
		//ORDER BY S.[name], T.[name]
		//*/
		//    /// <summary>The required Export method for the IDataExport interface.</summary>
		//    /// <param name="export">The XML element for the source.</param>
		//    /// <param name="writer">This is the output stream for the </param>
		//    /// <param name="log"></param>
		//    void Export(System.Xml.Linq.XElement export, Flux.IO.TabularWriter writer, Log log);
		//  }

		//  public interface IDataImport
		//  {
		//    /*
		//SELECT '<Target Type="SqlClient" Entity="[STBIDB].[' + DB_NAME() + '].[' + S.[name] + '].[' + SUBSTRING(T.[name], 1, LEN(T.[name]) -3) + '_DE]" Columns="' + STUFF((
		//SELECT ',' + '['+C.[name]+']' AS [text()]
		//FROM sys.columns C (NOLOCK)
		//WHERE  C.object_id = T.object_id
		//ORDER BY C.column_id
		//FOR XML PATH('')
		//), 1, 1, '') + '"  SourceEntity="[SDBIDB].[' + DB_NAME() + '].[' + S.[name] + '].[' + T.[name] + ']" />'
		//FROM sys.schemas S (NOLOCK)
		//INNER JOIN sys.tables T (NOLOCK)
		//ON (S.schema_id = T.schema_id)
		//WHERE T.[name] LIKE '%[_]DE'
		//ORDER BY S.[name], T.[name]
		//*/
		//    /// <summary>The required Import method for the IDataImport interface.</summary>
		//    /// <param name="import">The XML element for the source.</param>
		//    /// <param name="reader">This is the output stream for the </param>
		//    /// <param name="log"></param>
		//    void Import(System.Xml.Linq.XElement target, Flux.IO.TabularReader reader, Log log);
		//  }

		//  public interface IDataTransfer
		//  {
		//    void Transfer(System.Xml.Linq.XElement transfer, Log log);
		//  }

		private static System.IO.DirectoryInfo WorkDirectory;

		public static void Create(System.Xml.Linq.XElement xmlWork, string pathWork)
		{
			WriteEventLogInformation($"Wormhole is opening.");

			try
			{
				WorkDirectory = new System.IO.DirectoryInfo(pathWork);

				if (!WorkDirectory.Exists)
				{
					WorkDirectory.Create();
				}

				Proxy(xmlWork);
			}
			catch (System.Exception ex)
			{
				WriteEventLogError($"Wormhole Create: {ex.Message}, {ex.StackTrace}");
			}

			Clean(xmlWork, WorkDirectory);

			WriteEventLogInformation($"Wormhole is closing.");
		}

		public static void Clean(System.Xml.Linq.XElement xmlWork, System.IO.DirectoryInfo workDirectory)
		{
			try
			{
				foreach (var xExport in xmlWork.DescendantsAndSelf(@"Export"))
				{
					workDirectory.DeleteFiles(xExport.Attribute(@"Entity").Value + @".*", System.IO.SearchOption.TopDirectoryOnly);
				}

				foreach (var xExport in xmlWork.DescendantsAndSelf(@"Import"))
				{
					workDirectory.DeleteFiles(xExport.Attribute(@"Entity").Value + @".import", System.IO.SearchOption.TopDirectoryOnly);
				}
			}
			catch (System.Exception ex)
			{
				WriteEventLogError($"Wormhole Clean: {ex.Message}, {ex.StackTrace}");
			}
		}

		public static void Proxy(object data)
		{
			try
			{
				var xElement = (System.Xml.Linq.XElement)data;

				switch (xElement.Name.LocalName)
				{
					case nameof(Parallel):
						Parallel(xElement);
						break;
					case nameof(Serial):
						Serial(xElement);
						break;
					case nameof(Payload):
						Payload(xElement);
						break;
					default:
						throw new System.Exception($"Unrecognized element '{xElement.Name.LocalName}'.");
				}
			}
			catch (System.Exception ex)
			{
				WriteEventLogError($"Wormhole Proxy: {ex.Message}, {ex.StackTrace}");
			}
		}

		public static void Parallel(System.Xml.Linq.XElement xParallel)
		{
			try
			{
				var maxThreads = Flux.Maths.Min(int.Parse(xParallel.Attribute(MaxThreads)?.Value ?? @"7"), 17);

				var threads = new System.Collections.Generic.List<System.Threading.Thread>();

				var xParallelChildren = xParallel.Elements().ToList();

				foreach (var xParallelChild in xParallelChildren)
				{
				}

				do
				{
					if (threads.Count < maxThreads && xParallelChildren.Count > 0)
					{
						var xChild = xParallelChildren.First();
						xParallelChildren.RemoveAt(0);

						var thread = new System.Threading.Thread(Proxy) { Name = xChild.Name.LocalName, Priority = System.Threading.ThreadPriority.Lowest };
						thread.Start(xChild);

						threads.Add(thread);
					}

					threads.RemoveAll(t => t.ThreadState == System.Threading.ThreadState.Stopped);

					System.Threading.Thread.Sleep(499);
				}
				while (threads.Count > 0 || xParallelChildren.Count > 0);
			}
			catch (System.Exception ex)
			{
				WriteEventLogError($"Wormhole Process Parallel: {ex.Message}, {ex.StackTrace}");
			}
		}

		public static void Serial(System.Xml.Linq.XElement xSerial)
		{
			try
			{
				foreach (var xSerialChild in xSerial.Elements())
				{
					Proxy(xSerialChild);

					System.Threading.Thread.Sleep(293);
				}
			}
			catch (System.Exception ex)
			{
				WriteEventLogError($"Wormhole Process Serial: {ex.Message}, {ex.StackTrace}");
			}
		}

		public static void Payload(System.Xml.Linq.XElement xPayload)
		{
			try
			{
				var log = new Log(WorkDirectory.FullName, xPayload);

				try
				{
					var xSource = xPayload.Element(@"Export");
					var xTarget = xPayload.Element(@"Import");

					using System.Data.IDataReader source = xSource.Attribute(@"Type").Value switch
					{
						nameof(System.DirectoryServices) => new DirectoryServices().Read(xSource, log),
						nameof(System.Data.SqlClient) => new SqlClient().Read(xSource, log),
						_ => null
					};

					//switch (xSource.Attribute(@"Type").Value)
					//{
					//  case nameof(DirectoryServices):
					//    try { source = new DirectoryServices().Read(xSource, log); }
					//    catch (System.Exception ex) { log.Write(ex); throw; }
					//    break;
					//  case nameof(SqlClient):
					//    try { source = new SqlClient().Read(xSource, log); }
					//    catch (System.Exception ex) { log.Write(ex); throw; }
					//    break;
					//}

					switch (xTarget.Attribute(@"Type").Value)
					{
						case nameof(SqlClient):
							new SqlClient().Write(source, xTarget, log);
							break;
					}

					for (var index = 3; index > 0; index--)
					{
						log.Write(@"Done");

						System.Threading.Thread.Sleep(499);
					}
				}
				catch (System.Exception ex) { log.Write(ex); throw; }

				System.Threading.Thread.Sleep(1499);
			}
			catch (System.Exception ex)
			{
				WriteEventLogError($"Wormhole Process Export: {ex.Message}, {ex.StackTrace}");
			}
		}

		public static bool TryFindOrCreateDirectory(string path, out System.IO.DirectoryInfo result, bool createIfNotExist)
		{
			try
			{
				result = new System.IO.DirectoryInfo(path);

				if (!result.Exists && createIfNotExist)
				{
					result.Create();
				}

				return result.Exists;
			}
			catch { }

			result = null;
			return false;
		}

		public static bool TryLoadOrParseXmlManifest(string fileNameOrXml, out System.Xml.Linq.XElement result, FlowType multiManifestFlowType)
		{
			if (new System.IO.DirectoryInfo(fileNameOrXml) is var directoryInfo && directoryInfo.Exists)
			{
				try
				{
					result = new System.Xml.Linq.XElement(multiManifestFlowType.ToString());

					foreach (var enumeratedFileInfo in directoryInfo.EnumerateFiles(@"*.xml"))
					{
						if (System.Xml.Linq.XElement.Load(enumeratedFileInfo.FullName) is var xe && xe != null)
						{
							result.Add(xe);
						}
					}

					return true;
				}
				catch { }
			}

			if (new System.IO.FileInfo(fileNameOrXml) is var fileInfo && fileInfo.Exists)
			{
				result = System.Xml.Linq.XElement.Load(fileInfo.FullName);

				if (result != null)
				{
					return true;
				}
			}

			if (!string.IsNullOrWhiteSpace(fileNameOrXml) && fileNameOrXml.StartsWith(@"<") && fileNameOrXml.EndsWith(@">"))
			{
				result = System.Xml.Linq.XElement.Parse(fileNameOrXml);

				if (result != null)
				{
					return true;
				}
			}

			result = null;
			return true;
		}

#pragma warning disable CA1416 // Validate platform compatibility

		#region Windows EventLog functionality
		public const string EventSource = nameof(Wormhole) + @" Service";
		public const string EventLogName = @"Application";

		public static void WriteEventLog(string message, System.Diagnostics.EventLogEntryType type, int eventID = 0, short category = 0)
		{
			try
			{
				if (!System.Diagnostics.EventLog.SourceExists(EventSource))
				{
					System.Diagnostics.EventLog.CreateEventSource(EventSource, EventLogName);
				}

				using var el = new System.Diagnostics.EventLog(EventLogName);
				el.Source = EventSource;
				el.WriteEntry(message, type, eventID, category);
			}
			catch { }
		}
		public static void WriteEventLogError(string message, int eventID = 0, short category = 0)
			=> WriteEventLog(message, System.Diagnostics.EventLogEntryType.Error, eventID, category);
		public static void WriteEventLogInformation(string message, int eventID = 0, short category = 0)
			=> WriteEventLog(message, System.Diagnostics.EventLogEntryType.Information, eventID, category);
		#endregion Windows EventLog functionality

#pragma warning restore CA1416 // Validate platform compatibility

		#region File/Sql Log functionality
		public class Log
		{
			public string BatchID => m_xePayload.Attribute(nameof(System.Guid)).Value;
			public string Category => m_xePayload.Name.LocalName;
			public string Location { get; private set; }
			public string Name => string.Join(@"-", m_xePayload.Elements().Select(c => c.Attribute(@"Entity").Value));

			private readonly string m_logFileName;

			private readonly System.Xml.Linq.XElement m_xePayload;

			public Log(string workFolder, System.Xml.Linq.XElement xePayload)
			{
				m_xePayload = xePayload;

				if (m_xePayload.Attribute(nameof(System.Guid)) is null)
				{
					m_xePayload.SetAttributeValue(nameof(System.Guid), System.Guid.NewGuid().ToString());
				}

				if (m_xePayload.Attribute("Name") is null)
				{
					m_xePayload.SetAttributeValue(@"Name", m_xePayload.Element(@"Export").Attribute(@"Entity").Value + @"-" + m_xePayload.Element(@"Import").Attribute(@"Entity").Value);
				}

				Location = workFolder;

				SetupLog();

				m_logFileName = System.IO.Path.Combine(workFolder, $"{Name}.{m_xePayload.Name.LocalName.ToLower()}");

				if (System.IO.File.Exists(m_logFileName))
				{
					System.IO.File.Delete(m_logFileName);
				}

				//ExecuteNonQuery($"INSERT [tools].[dbo].[ActionLog] ([BatchID], [Category], [Location], [Message], [Name], [Provider], [Text], [Timestamp], [Xml]) SELECT '{_batchID}', '{Category}', '{workFolder.EscapeTsql()}', 'Commenced', '{Name.EscapeTsql()}', '{ApplicationName}', NULL, GETDATE(), '{xml.ToString().EscapeTsql()}';");
				//ExecuteNonQuery($"EXECUTE [tools].[dbo].[LogActivity] '{_batchID}', '{Category}', '{workFolder.EscapeTsql()}', 'Commenced', '{Name.EscapeTsql()}', '{ApplicationName}', NULL, '{xml.ToString().EscapeTsql()}'");
				ExecuteNonQuery(@"Reset", string.Empty, m_xePayload.ToString().EscapeTsql());
			}

			public void Write(params string[] message)
			{
				if (message.Length == 0)
				{
					Core.WriteEventLogError($"Log.Write: \"{nameof(message)}[] is empty.\"");

					return;
				}

				var messages = message.ToCopy(1, message.Length - 1).Prepend($"{System.DateTime.Now:yyyy-MM-ddTHH:mm:ss.fffffff} {message[0]}").ToArray();

				try
				{
					System.IO.File.AppendAllLines(m_logFileName, messages);
				}
				catch (System.Exception ex)
				{
					Core.WriteEventLogError($"Log.Write(FILE={m_logFileName}): {ex.Message} \"{messages}\"; {ex.StackTrace}");
				}

				try
				{
					ExecuteNonQuery(message[0].EscapeTsql(), (message.Length > 1 ? string.Join(@", ", message, 1, message.Length - 1) : string.Empty).EscapeTsql(), string.Empty);
				}
				catch (System.Exception ex)
				{
					Core.WriteEventLogError($"Log.Write(SQL={LogTableName}): {ex.Message} \"{messages}\"; {ex.StackTrace}");
				}
			}
			public void Write(System.Exception exception)
			{
				Write($"ERROR: {exception.Message}", exception.StackTrace);
			}

			public void ExecuteNonQuery(string message, string text, string xml)
			{
				ExecuteNonQuery(string.Format(LogScript, LogTableName.QualifiedNameQuoted(3), BatchID, Category, Location, message, Name, ApplicationName, text, xml));
			}

			private static string[] m_logTableColumnDefinitions;
			public static string[] LogTableColumnDefinitions
				=> m_logTableColumnDefinitions ??= System.Text.RegularExpressions.Regex.Replace(System.Configuration.ConfigurationManager.AppSettings[nameof(LogTableColumnDefinitions)] ?? throw new System.ArgumentOutOfRangeException(nameof(LogTableColumnDefinitions)), @"\s{2,}", string.Empty).Split(',');

			private static Flux.Data.TsqlName _logTableName;
			public static Flux.Data.TsqlName LogTableName
				=> _logTableName == Flux.Data.TsqlName.Empty ? (_logTableName = Flux.Data.TsqlName.Parse(System.Configuration.ConfigurationManager.AppSettings[nameof(LogTableName)] ?? throw new System.ArgumentOutOfRangeException(nameof(LogTableName)))) : _logTableName;

			private static string m_logScript;
			public static string LogScript
				=> m_logScript ??= System.Configuration.ConfigurationManager.AppSettings[nameof(LogScript)]?.Trim() ?? throw new System.ArgumentOutOfRangeException(nameof(LogScript));

			public static void ExecuteNonQuery(string statement)
			{
				var rng = new System.Random(System.DateTime.Now.Minute * System.DateTime.Now.Second * System.DateTime.Now.Millisecond);

				System.Threading.Thread.Sleep(rng.Next(101, 251));

				try
				{
					using var connection = new System.Data.SqlClient.SqlConnection($"Data Source={LogTableName.ServerName};Initial Catalog={LogTableName.DatabaseName};Integrated Security=SSPI;");
					connection.Open();

					using var command = connection.CreateCommand();
					command.CommandText = statement;

					command.ExecuteNonQuery();
				}
				catch (System.Exception ex)
				{
					WriteEventLogError($"ActivityLog: {ex.Message} \"{statement}\"; {ex.StackTrace}");
				}

				//System.Threading.Thread.Sleep(rng.Next(499, 1499));
			}

			private static void SetupLog()
			{
				ExecuteNonQuery($"{Flux.Data.Tsql.If(Flux.Data.Tsql.Not(Flux.Data.Tsql.TableExists(LogTableName)))} {Flux.Data.Tsql.CreateTable(LogTableName, LogTableColumnDefinitions)}");
			}
		}
		#endregion File/Sql Log functionality
	}
}
