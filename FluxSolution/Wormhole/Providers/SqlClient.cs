using System.Linq;
using Flux;

namespace Wormhole
{
  public static partial class ExtensionMethods
  {
    public static int TotalRowsCopied(this System.Data.SqlClient.SqlBulkCopy bulkCopy) => (int)typeof(System.Data.SqlClient.SqlBulkCopy).GetField(@"_rowsCopied", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance).GetValue(bulkCopy);
  }

  public class SqlClient : Core.IDataSource, Core.IDataTarget
  {
    //public void Export(System.Xml.Linq.XElement export, Flux.IO.TabularWriter writer, Wormhole.Log log)
    //{
    //  var entity = Flux.Data.Sql.QName.Parse(export.Attribute("Entity").Value);

    //  var commandText = export.Attribute("Command")?.Value ?? $"SELECT * FROM {entity.QualifiedNameQuoted(3)} (NOLOCK)";

    //  using (var connection = new System.Data.SqlClient.SqlConnection(export.Attribute("Connection")?.Value ?? entity.TrustedConnectionString(Wormhole.ApplicationName)))
    //  {
    //    connection.Open();

    //    using (var command = connection.CreateCommand())
    //    {
    //      command.CommandTimeout = 3600;

    //      command.CommandText = $"{Flux.Data.Sql.SelectCount()} {Flux.Data.Sql.FromSubquery(commandText)}";
    //      log.Write($"Export table COUNT={((int)command.ExecuteScalar())}", command.CommandText);

    //      command.CommandText = commandText;
    //      using (var sdr = command.ExecuteReader())
    //      {
    //        do
    //        {
    //          writer.WriteFields(sdr.GetNames());
    //          writer.WriteFields(sdr.GetFieldTypeNames());
    //          //writer.WriteFields(sdr.GetSchemaTable().ExtendSchemaTable().ToXDocument().Root.XPathSelectElements(@"SchemaTable/TSqlDefinition/.").Select(xe => xe.Value));
    //          writer.WriteFields(sdr.GetTsqlDefinitions().ToArray());

    //          if (sdr.Read())
    //          {
    //            do
    //            {
    //              writer.WriteRecord(sdr.GetValues());

    //              if (writer.TimeToNotify)
    //              {
    //                log.Write($"{writer.RecordIndex} rows exported so far");
    //              }
    //            }
    //            while (sdr.Read());
    //          }
    //        }
    //        while (sdr.NextResult());

    //        log.Write($"Export process COUNT={writer.RecordIndex}");
    //      }
    //    }
    //  }
    //}

    //public void Import(System.Xml.Linq.XElement import, Flux.IO.TabularReader reader, Wormhole.Log log)
    //{
    //  var importEntity = Flux.Data.Sql.QName.Parse(import.Attribute(@"Entity").Value);

    //  var importOptions = System.Text.RegularExpressions.Regex.Split(import.Attribute(@"Options")?.Value ?? string.Empty, @",\s*");

    //  reader.Initialize(import.Attribute(@"HeaderCount")?.Value, import.Attribute(nameof(reader.FieldNames))?.Value, import.Attribute(nameof(reader.FieldTypes))?.Value, import.Attribute(nameof(reader.FieldProviderTypes))?.Value);

    //  using (var connection = new System.Data.SqlClient.SqlConnection(import.Attribute("Connection")?.Value ?? importEntity.TrustedConnectionString(Wormhole.ApplicationName)))
    //  {
    //    connection.Open();

    //    using (var command = connection.CreateCommand())
    //    {
    //      command.CommandTimeout = 3600;

    //      if (importOptions.Contains("CREATE"))
    //      {
    //        command.CommandText = $"{Flux.Data.Sql.Ansi.If(Flux.Data.Sql.Ansi.Not(Flux.Data.Sql.Ansi.Exists(Flux.Data.Sql.Ansi.InformationSchema.SelectTable(importEntity))))} {Flux.Data.Sql.CreateTable(importEntity, reader.FieldProviderTypes)}";
    //        log.Write($"Import table CREATE", command.CommandText);
    //        command.ExecuteNonQuery();
    //      }

    //      command.CommandText = $"{Flux.Data.Sql.SelectCount()} {Flux.Data.Sql.From(importEntity)}";
    //      log.Write($"Import table COUNT={((int)command.ExecuteScalar())} (before)", command.CommandText);

    //      if (importOptions.Contains("TRUNCATE"))
    //      {
    //        command.CommandText = Flux.Data.Sql.TruncateTable(importEntity);
    //        log.Write($"Import table TRUNCATE", command.CommandText);
    //        command.ExecuteNonQuery();
    //      }

    //      using (var sbc = new System.Data.SqlClient.SqlBulkCopy(connection))
    //      {
    //        sbc.NotifyAfter = 10;
    //        sbc.SqlRowsCopied += (object sender, System.Data.SqlClient.SqlRowsCopiedEventArgs e) =>
    //        {
    //          log.Write($"{((int)e.RowsCopied)} rows imported so far");

    //          if (e.RowsCopied <= 1000000)
    //          {
    //            sbc.NotifyAfter = (int)(e.RowsCopied * 10 - e.RowsCopied);
    //          }
    //        };
    //        sbc.BatchSize = 10000;
    //        sbc.BulkCopyTimeout = 3600;
    //        for (var i = 0; i < reader.FieldCount; i++) { sbc.ColumnMappings.Add(new System.Data.SqlClient.SqlBulkCopyColumnMapping(reader.FieldNames[i], reader.FieldNames[i])); }
    //        sbc.DestinationTableName = importEntity.QualifiedNameQuoted(3);
    //        sbc.WriteToServer(reader);

    //        log.Write($"Import process COUNT={sbc.TotalRowsCopied()}");
    //      }

    //      command.CommandText = $"{Flux.Data.Sql.SelectCount()} {Flux.Data.Sql.From(importEntity)}";
    //      log.Write($"Import table COUNT={((int)command.ExecuteScalar())} (after)", command.CommandText);

    //      const string Merge = @"MERGE";
    //      const string MergePlus = @"MERGE+";
    //      const string ToolsMerge = @"ToolsMERGE";
    //      const string ToolsMergePlus = @"ToolsMERGE+";

    //      if (importOptions.Contains(ToolsMerge) || importOptions.Contains(ToolsMergePlus))
    //      {
    //        var mergeEntity = importEntity.DefaultMergeName;

    //        var options = new System.Collections.Generic.List<string>();

    //        if (importOptions.Contains(ToolsMergePlus))
    //        {
    //          options.Add(@"HighFrequency");
    //        }

    //        if (options.Count > 0)
    //        {
    //          log.Write($"Merge table OPTIONS", string.Join(@",", options));
    //        }

    //        command.CommandText = $"EXECUTE [tools].[EffectiveDating].[MergeTable] '{mergeEntity}', {(options.Count > 0 ? $"'{string.Join(@",", options)}'" : @"NULL")}";
    //        log.Write($"Merge table {mergeEntity}", command.CommandText);
    //        command.ExecuteNonQuery();
    //      }
    //      else if (importOptions.Contains(Merge) || importOptions.Contains(MergePlus))
    //      {
    //        var mergeEntity = importEntity.DefaultMergeName;

    //        var mergeDense = importOptions.Contains(MergePlus);

    //        command.CommandText = Flux.Data.Sql.Ansi.If(Flux.Data.Sql.Ansi.Not(Flux.Data.Sql.Ansi.Exists(Flux.Data.Sql.Ansi.InformationSchema.SelectTable(mergeEntity))), Flux.Data.Sql.CreateMergeTable(mergeEntity, reader.FieldProviderTypes, mergeDense));
    //        log.Write($"Merge table CREATE", command.CommandText);
    //        command.ExecuteNonQuery();

    //        command.CommandText = $"{Flux.Data.Sql.SelectCount()} {Flux.Data.Sql.From(mergeEntity)}";
    //        log.Write($"Merge table COUNT={((int)command.ExecuteScalar())} (before)", command.CommandText);

    //        command.CommandText = Flux.Data.Sql.MergeTable(mergeDense, mergeEntity, importEntity, reader.FieldNames, reader.FieldProviderTypes);
    //        log.Write($"MERGE into {mergeEntity.QualifiedNameQuoted(4)} using {importEntity.QualifiedNameQuoted(4)}", command.CommandText);
    //        log.Write($"Merge table OUTPUT", $"{command.ExecuteScalar()}");

    //        command.CommandText = $"{Flux.Data.Sql.SelectCount()} {Flux.Data.Sql.From(mergeEntity)}";
    //        log.Write($"Merge table COUNT={((int)command.ExecuteScalar())} (after)", command.CommandText);
    //      }
    //    }
    //  }
    //}

    //public void Transfer(System.Xml.Linq.XElement transfer, Wormhole.Log log)
    //{
    //  var exportEntity = Flux.Data.Sql.QName.Parse(transfer.Attribute(@"ExportEntity").Value);

    //  var importEntity = Flux.Data.Sql.QName.Parse(transfer.Attribute(@"ImportEntity").Value);

    //  var commandText = transfer.Attribute("Command")?.Value ?? $"SELECT * FROM {exportEntity.QualifiedNameQuoted(3)} (NOLOCK)";

    //  using (var exportConnection = new System.Data.SqlClient.SqlConnection(transfer.Attribute("Connection")?.Value ?? exportEntity.TrustedConnectionString(Wormhole.ApplicationName)))
    //  {
    //    exportConnection.Open();

    //    using (var exportCommand = exportConnection.CreateCommand())
    //    {
    //      exportCommand.CommandTimeout = 3600;

    //      exportCommand.CommandText = $"{Flux.Data.Sql.SelectCount()} {Flux.Data.Sql.FromSubquery(commandText)}";
    //      log.Write($"Export table COUNT={((int)exportCommand.ExecuteScalar())}", exportCommand.CommandText);

    //      exportCommand.CommandText = commandText;
    //      using (var reader = exportCommand.ExecuteReader())
    //      {
    //        using (var importConnection = new System.Data.SqlClient.SqlConnection(transfer.Attribute("Connection")?.Value ?? importEntity.TrustedConnectionString(Wormhole.ApplicationName)))
    //        {
    //          importConnection.Open();

    //          using (var importCommand = importConnection.CreateCommand())
    //          {
    //            importCommand.CommandTimeout = 3600;

    //            var tsqlDefinitions = reader.GetTsqlDefinitions().ToArray(); 
    //            //var fieldProviderTypes = reader.GetSchemaTable().ExtendSchemaTable().ToXDocument().Root.XPathSelectElements(@"SchemaTable/TSqlDefinition/.").Select(xe => xe.Value).ToArray();

    //            var importOptions = System.Text.RegularExpressions.Regex.Split(transfer.Attribute(@"Options")?.Value ?? string.Empty, @",\s*");

    //            if (importOptions.Contains("CREATE"))
    //            {
    //              importCommand.CommandText = $"{Flux.Data.Sql.Ansi.If(Flux.Data.Sql.Ansi.Not(Flux.Data.Sql.Ansi.Exists(Flux.Data.Sql.Ansi.InformationSchema.SelectTable(importEntity))))} {Flux.Data.Sql.CreateTable(importEntity, tsqlDefinitions)}";
    //              log.Write($"Import table CREATE", importCommand.CommandText);
    //              importCommand.ExecuteNonQuery();
    //            }

    //            importCommand.CommandText = $"{Flux.Data.Sql.SelectCount()} {Flux.Data.Sql.From(importEntity)}";
    //            log.Write($"Import table COUNT={((int)importCommand.ExecuteScalar())} (before)", importCommand.CommandText);

    //            if (importOptions.Contains("TRUNCATE"))
    //            {
    //              importCommand.CommandText = Flux.Data.Sql.TruncateTable(importEntity);
    //              log.Write($"Import table TRUNCATE", importCommand.CommandText);
    //              importCommand.ExecuteNonQuery();
    //            }

    //            using (var sbc = new System.Data.SqlClient.SqlBulkCopy(importConnection))
    //            {
    //              sbc.NotifyAfter = 10;
    //              sbc.SqlRowsCopied += (object sender, System.Data.SqlClient.SqlRowsCopiedEventArgs e) =>
    //              {
    //                log.Write($"{((int)e.RowsCopied)} rows imported so far");

    //                if (e.RowsCopied <= 1000000)
    //                {
    //                  sbc.NotifyAfter = (int)(e.RowsCopied * 10 - e.RowsCopied);
    //                }
    //              };
    //              sbc.BatchSize = 20000;
    //              sbc.BulkCopyTimeout = 3600;
    //              for (var i = 0; i < reader.FieldCount; i++) { sbc.ColumnMappings.Add(new System.Data.SqlClient.SqlBulkCopyColumnMapping(reader.GetName(i), reader.GetName(i))); }
    //              sbc.DestinationTableName = importEntity.QualifiedNameQuoted(3);
    //              sbc.WriteToServer(reader);

    //              log.Write($"Import process COUNT={sbc.TotalRowsCopied()}");
    //            }

    //            importCommand.CommandText = $"{Flux.Data.Sql.SelectCount()} {Flux.Data.Sql.From(importEntity)}";
    //            log.Write($"Import table COUNT={((int)importCommand.ExecuteScalar())} (after)", importCommand.CommandText);

    //          }
    //        }
    //      }
    //    }
    //  }
    //}

    /// <summary></summary>
    /// <remarks></remarks>
    // <Source Entity="[].[].[].[]" {Command=""} {Connection=""} SkipCount="true" />
    public System.Data.IDataReader Read(System.Xml.Linq.XElement source, Core.Log log)
    {
      var sourceLocalName = source.Name.LocalName;
      var sourceEntity = Flux.Data.TsqlName.Parse(source.Attribute(@"Entity").Value);
      var sourceCommand = source.Attribute(@"Command")?.Value ?? $"SELECT * FROM {sourceEntity.QualifiedNameQuoted(3)} (NOLOCK)";
      var sourceConnection = source.Attribute(@"Connection")?.Value ?? sourceEntity.TrustedConnectionStringSqlClient;

      var connection = new System.Data.SqlClient.SqlConnection(sourceConnection + ";Connection Timeout=15");
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandTimeout = 3600;

      //try
      //{
      //  command.CommandText = $"{Flux.Data.Sql.SelectCount()} {Flux.Data.Sql.FromSubquery(sourceCommand)}";
      //  log.Write($"Source table COUNT={((int)command.ExecuteScalar())}", command.CommandText);
      //}
      //catch { }

      command.CommandText = sourceCommand;

      var reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

      reader.GetSchemaTableEx(out var tsqlAdaptedDefinitions, out var tsqlCurrentDefinitions, out var tsqlDefaultDefinitions);

      log.Write($"Source schema current SourceColumnCount={tsqlCurrentDefinitions.Length}", string.Join(@", ", tsqlCurrentDefinitions));
      log.Write($"Source schema default SourceColumnCount={tsqlDefaultDefinitions.Length}", string.Join(@", ", tsqlDefaultDefinitions));

      return reader;
    }

    //public class Target
    //{
    //  public Flux.Data.Sql.QName Entity { get; set; }

    //  //public string Option { get; set; } // DROP,CREATE,TRUNCATE,MERGE|MERGE+

    //  public bool Drop { get; set; }
    //  public bool Create { get; set; }
    //  public bool Truncate { get; set; }

    //  //public bool Merge { get; set; } // Normal|HighFrequency

    //  public string ColumnDefinitions { get; set; }
    //}

    /// <summary></summary>
    /// <remarks></remarks>
    // <Target Entity="[].[].[].[]" Options="DROP,CREATE,TRUNCATE" {MapSource="Ordinal|Name"} {MapTarget="Ordinal|Name"} {Names=""} {ColumnNames=","} {TsqlDefinitions=","} {Connection=""} />
    public void Write(System.Data.IDataReader source, System.Xml.Linq.XElement target, Core.Log log)
    {
      var stex = source.GetSchemaTableEx(out var tsqlAdaptedDefinitions, out var tsqlCurrentDefinitions, out var tsqlDefaultDefinitions);

      var targetLocalName = target.Name.LocalName;
      var targetEntity = Flux.Data.TsqlName.Parse(target.Attribute(@"Entity").Value);
      var targetOptions = System.Text.RegularExpressions.Regex.Split(target.Attribute(@"Options")?.Value.ToUpper() ?? string.Empty, @"\s*,\s*");
      var targetConnection = target.Attribute(@"Connection")?.Value ?? targetEntity.TrustedConnectionStringSqlClient;
      string[] targetTsqlDefinitions = null;
      if (target.Attribute(@"TsqlDefinitions") is null) targetTsqlDefinitions = tsqlAdaptedDefinitions ?? tsqlCurrentDefinitions ?? tsqlDefaultDefinitions;
      else targetTsqlDefinitions = System.Text.RegularExpressions.Regex.Split(target.Attribute(@"TsqlDefinitions").Value, @"(?<!\d)\s*?,\s*?(?!\d)");
      //var targetTsqlDefinitions = target.Attribute(@"TsqlDefinitions")?.Value.Split(',') ?? tsqlCurrentDefinitions ?? tsqlDefaultDefinitions;

      var sourceFieldNames = source.GetNames().ToArray();
      var targetFieldNames = target.Attribute(@"ColumnNames")?.Value.Split(',') ?? targetTsqlDefinitions.Select(cd => Flux.Data.TsqlColumnDefinition.Parse(cd).ColumnName).ToArray();

      Flux.Data.Tsql.MergeTable(false, targetEntity.DefaultMergeName, targetEntity, targetFieldNames, targetTsqlDefinitions);

      using (var connection = new System.Data.SqlClient.SqlConnection(targetConnection))
      {
        connection.Open();

        using (var command = connection.CreateCommand())
        {
          command.CommandTimeout = 3600;

          var commandText = new System.Collections.Generic.Dictionary<string, string>();

          if (targetOptions.Contains(@"DROP"))
          {
            commandText.Add(@"DROP", $"{Flux.Data.Tsql.If(Flux.Data.Tsql.Exists(Flux.Data.TsqlInformationSchema.SelectTable(targetEntity)))} {Flux.Data.Tsql.DropTable(targetEntity)}");
          }

          if (targetOptions.Contains(@"CREATE"))
          {
            if (targetTsqlDefinitions == null || targetTsqlDefinitions.Length != sourceFieldNames.Length)
            {
              throw new System.ArgumentNullException("TsqlDefinitions");
            }

            commandText.Add(@"CREATE", $"{Flux.Data.Tsql.If(Flux.Data.Tsql.Not(Flux.Data.Tsql.Exists(Flux.Data.TsqlInformationSchema.SelectTable(targetEntity))))} {Flux.Data.Tsql.CreateTable(targetEntity, targetTsqlDefinitions)}");
          }

          if (targetOptions.Contains(@"TRUNCATE"))
          {
            commandText.Add(@"TRUNCATE", $"{Flux.Data.Tsql.TruncateTable(targetEntity)}");
          }

          command.CommandText = $"SELECT COUNT(*) AS ColumnCount FROM ( {Flux.Data.TsqlInformationSchema.SelectColumns(targetEntity, @"COLUMN_NAME")} ) AS CC;";
          log.Write($"Target table pre-processing{(int.TryParse(command.ExecuteScalar().ToString(), out var preColumns) ? $" TargetColumnCount={preColumns}" : string.Empty)} ({string.Join(@",", commandText.Keys)})", command.CommandText);

          command.CommandText = $"{Flux.Data.Tsql.If(Flux.Data.Tsql.Exists(Flux.Data.TsqlInformationSchema.SelectTable(targetEntity)))} {Flux.Data.Tsql.SelectCount()} {Flux.Data.Tsql.From(targetEntity)} ELSE SELECT 'NoTable'; {string.Join(@"; ", commandText.Values)}";
          log.Write($"Target table pre-processing{(int.TryParse(command.ExecuteScalar().ToString(), out var preCount) ? $" TargetRowCount={preCount}" : string.Empty)} ({string.Join(@",", commandText.Keys)})", command.CommandText);

          using (var sbc = new System.Data.SqlClient.SqlBulkCopy(connection)
          {
            BatchSize = (int)((-Flux.Maths.Logistic(System.Math.Clamp(Flux.Maths.ISqrt(sourceFieldNames.Length), 1, 20), 0.3, 5, 20) + 30) * 1000),
            BulkCopyTimeout = (int)System.TimeSpan.FromHours(3).TotalSeconds,
            DestinationTableName = targetEntity.QualifiedNameQuoted(3),
            EnableStreaming = true,
            NotifyAfter = 10000
          })
          {
            sbc.SqlRowsCopied += (object sender, System.Data.SqlClient.SqlRowsCopiedEventArgs e) =>
            {
              log.Write($"{((int)e.RowsCopied)} rows copied");

              if (e.RowsCopied < 1000000)
              {
                sbc.NotifyAfter = (int)(e.RowsCopied * 10 - e.RowsCopied);
              }
              else if (e.RowsCopied == 1000000)
              {
                sbc.NotifyAfter = 1000000;
              }
            };

            for (var i = 0; i < sourceFieldNames.Length; i++) { sbc.ColumnMappings.Add(new System.Data.SqlClient.SqlBulkCopyColumnMapping(sourceFieldNames[i], targetFieldNames[i])); }
            sbc.WriteToServer(source);

            log.Write($"Target table TotalRowsCopied={sbc.TotalRowsCopied()}", $"BatchSize={sbc.BatchSize}");
          }

          command.CommandText = $"{Flux.Data.Tsql.SelectCount()} {Flux.Data.Tsql.From(targetEntity)}";
          log.Write($"Target table post-processing TargetRowCount={(int)command.ExecuteScalar()}", command.CommandText);

          const string Merge = @"MERGE";
          const string MergePlus = @"MERGE+";
          const string ToolsMerge = @"ToolsMERGE";
          const string ToolsMergePlus = @"ToolsMERGE+";

          if (targetOptions.Contains(ToolsMerge) || targetOptions.Contains(ToolsMergePlus))
          {
            var mergeEntity = targetEntity.DefaultMergeName;

            var options = new System.Collections.Generic.List<string>();

            if (targetOptions.Contains(ToolsMergePlus))
            {
              options.Add(@"HighFrequency");
            }

            if (options.Count > 0)
            {
              log.Write($"Merge table OPTIONS", string.Join(@",", options));
            }

            command.CommandText = $"EXECUTE [tools].[EffectiveDating].[MergeTable] '{mergeEntity}', {(options.Count > 0 ? $"'{string.Join(@",", options)}'" : @"NULL")}";
            log.Write($"Merge table {mergeEntity}", command.CommandText);
            command.ExecuteNonQuery();
          }
          else if (targetOptions.Contains(Merge) || targetOptions.Contains(MergePlus))
          {
            var mergeEntity = targetEntity.DefaultMergeName;

            var mergeDense = targetOptions.Contains(MergePlus);

            if (targetTsqlDefinitions.Length == 0)
            {
              targetTsqlDefinitions = System.Linq.Enumerable.Range(0, source.FieldCount).Select(i => source.GetDefaultTsqlDefinition(i)).ToArray();
            }

            command.CommandText = Flux.Data.Tsql.If(Flux.Data.Tsql.Not(Flux.Data.Tsql.Exists(Flux.Data.TsqlInformationSchema.SelectTable(mergeEntity))), Flux.Data.Tsql.CreateMergeTable(mergeEntity, targetTsqlDefinitions, mergeDense));
            log.Write($"Merge table CREATE", command.CommandText);
            command.ExecuteNonQuery();

            command.CommandText = $"{Flux.Data.Tsql.SelectCount()} {Flux.Data.Tsql.From(mergeEntity)}";
            log.Write($"Merge table COUNT={((int)command.ExecuteScalar())} (before)", command.CommandText);

            command.CommandText = Flux.Data.Tsql.MergeTable(mergeDense, mergeEntity, targetEntity, sourceFieldNames, targetTsqlDefinitions);
            log.Write($"MERGE into {mergeEntity.QualifiedNameQuoted(4)} using {targetEntity.QualifiedNameQuoted(4)}", command.CommandText);
            log.Write($"Merge table OUTPUT", $"{command.ExecuteScalar()}");

            command.CommandText = $"{Flux.Data.Tsql.SelectCount()} {Flux.Data.Tsql.From(mergeEntity)}";
            log.Write($"Merge table COUNT={((int)command.ExecuteScalar())} (after)", command.CommandText);
          }
        }
      }
    }
  }
}
