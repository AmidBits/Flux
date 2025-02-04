//using System.Linq;
//using System.Xml.Linq;
//using System.Xml.XPath;

//namespace Flux.Data
//{
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
//    /// <param name="metaData">The XML element for the source.</param>
//    /// <param name="writer">This is the output stream for the data.</param>
//    /// <param name="log"></param>
//    void Export(System.Xml.Linq.XElement metaData, IO.TabularStreamWriter writer, Services.FileLog log);
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
//    /// <param name="metaData">The XML element for the source.</param>
//    /// <param name="reader">This is the input stream for the data.</param>
//    /// <param name="log"></param>
//    void Import(System.Xml.Linq.XElement metaData, IO.TabularStreamReader reader, Services.FileLog log);
//  }

//  //class Program
//  //{
//  //  static void LaunchSession(string metaPath, string dataPath)
//  //  {
//  //    //new System.IO.DirectoryInfo(dataPath).DeleteContent();

//  //    #region Generate NodePool
//  //    var xeNodePool = new System.Xml.Linq.XElement("NodePool");
//  //    foreach (var file in System.IO.Directory.EnumerateFiles(metaPath, "*.xml").OrderBy(s => s))
//  //    {
//  //      var xeFile = System.Xml.Linq.XElement.Load(file);

//  //      xeNodePool.Add(xeFile.XPathSelectElements("//Export[@Entity]"));
//  //      xeNodePool.Add(xeFile.XPathSelectElements("//Import[@Entity]"));
//  //    }
//  //    xeNodePool.Save(System.IO.Path.Combine(dataPath, $"#NodePool.xml"));
//  //    #endregion

//  //    #region Generate DataFlow
//  //    //var xeDataFlow = new System.Xml.Linq.XElement("DataFlow");
//  //    //foreach(var xeExport in xeNodePool.XPathSelectElements("Export[@Entity]"))
//  //    //{
//  //    //	xeExport.Add(xeNodePool.XPathSelectElements($"Import[@ExportEntity='{xeExport.Attribute("Entity").Value}']"));

//  //    //	xeDataFlow.Add(xeExport);
//  //    //}
//  //    //xeDataFlow.Save(System.IO.Path.Combine(dataPath, $"#DataFlow.xml"));
//  //    #endregion

//  //    System.Collections.Generic.List<System.Threading.Thread> exportThreadPool = new System.Collections.Generic.List<System.Threading.Thread>();
//  //    System.Collections.Generic.List<System.Threading.Thread> importThreadPool = new System.Collections.Generic.List<System.Threading.Thread>();

//  //    foreach (var xeExport in xeNodePool.XPathSelectElements("Export[@Entity]").Shuffle())
//  //    {
//  //      new System.IO.DirectoryInfo(dataPath).DeleteFiles($"{xeExport.Attribute("Entity").Value}.*", System.IO.SearchOption.TopDirectoryOnly);

//  //      switch (xeExport.Attribute("Type").Value)
//  //      {
//  //        case "SqlClient":
//  //          var thread = new System.Threading.Thread(() =>
//  //          {
//  //            var fileName = System.IO.Path.Combine(dataPath, $"{xeExport.Attribute("Entity").Value}.partial");

//  //            using (var writer = new Flux.IO.TabularWriter(fileName))
//  //            {
//  //              new Pipeline.SqlClient().Export(xeExport, writer, new Flux.Data.Pipeline.LogFile(dataPath, "export", xeExport.Attribute("Entity").Value));
//  //            }

//  //            System.IO.File.Move(fileName, System.IO.Path.ChangeExtension(fileName, "data"));

//  //            xeExport.SetAttributeValue("Completed", System.DateTime.Now.ToString("o"));
//  //          });

//  //          thread.Name = $"{xeExport.Attribute("Entity").Value}";
//  //          thread.Priority = System.Threading.ThreadPriority.Lowest;

//  //          exportThreadPool.Add(thread);
//  //          break;
//  //      }
//  //    }

//  //    foreach (var xeImport in xeNodePool.XPathSelectElements("Import[@Entity and @ExportEntity]").Shuffle())
//  //    {
//  //      new System.IO.DirectoryInfo(dataPath).DeleteFiles($"{xeImport.Attribute("Entity").Value}.*", System.IO.SearchOption.TopDirectoryOnly);

//  //      switch (xeImport.Attribute("Type").Value)
//  //      {
//  //        case "SqlClient":
//  //          var thread = new System.Threading.Thread(() =>
//  //          {
//  //            var exportEntityName = xeImport.Attribute("ExportEntity").Value;
//  //            var importEntityName = xeImport.Attribute("Entity").Value;

//  //            using (var reader = new Flux.IO.TabularReader(System.IO.Path.Combine(dataPath, $"{exportEntityName}.data")))
//  //            {
//  //              new Pipeline.SqlClient().Import(xeImport, reader, new Flux.Data.Pipeline.LogFile(dataPath, "import", importEntityName));
//  //            }
//  //          });

//  //          thread.Name = $"{xeImport.Attribute("Entity").Value}";
//  //          thread.Priority = System.Threading.ThreadPriority.Lowest;

//  //          importThreadPool.Add(thread);
//  //          break;
//  //      }
//  //    }

//  //    var started = System.DateTime.Now;

//  //    while (exportThreadPool.Where(t => t.ThreadState != System.Threading.ThreadState.Stopped).Any() || importThreadPool.Where(t => t.ThreadState != System.Threading.ThreadState.Stopped).Any())
//  //    {
//  //      if (exportThreadPool.Any(t => t.ThreadState == System.Threading.ThreadState.Unstarted) && exportThreadPool.Count(t => t.ThreadState == System.Threading.ThreadState.Running) < 17)
//  //      {
//  //        var thread = exportThreadPool.Where(t => t.ThreadState == System.Threading.ThreadState.Unstarted).First();

//  //        System.Console.SetCursorPosition(0, 1);
//  //        System.Console.WriteLine($"          Timing: {started.ToStringISO8601Full()}");

//  //        thread.Start();
//  //      }

//  //      foreach (var thread in importThreadPool.Where(t => t.ThreadState == System.Threading.ThreadState.Unstarted).Where(importThread =>
//  //       {
//  //         var entityNames = xeNodePool.XPathSelectElement($"Import[@Entity='{importThread.Name}']").Attributes().Where(a => a.Name.LocalName.EqualsAny("ExportEntity", "AwaitEntity")).SelectMany(a => a.Value.Split(',')).Distinct().ToArray();
//  //         if (exportThreadPool.Where(te => te.Name.EqualsAny(entityNames)).All(te => te.ThreadState == System.Threading.ThreadState.Stopped) && importThreadPool.Where(te => te.Name.EqualsAny(entityNames)).All(te => te.ThreadState == System.Threading.ThreadState.Stopped))
//  //         {
//  //           return true;
//  //         }
//  //         return false;
//  //       }).Take(17 - importThreadPool.Count(t => t.ThreadState == System.Threading.ThreadState.Running)))
//  //      {
//  //        thread.Start();
//  //      }

//  //      System.Console.SetCursorPosition(0, 2);
//  //      System.Console.WriteLine($"         Aborted: {exportThreadPool.Count(t => t.ThreadState == System.Threading.ThreadState.Aborted)}   ");
//  //      System.Console.WriteLine($"  AbortRequested: {exportThreadPool.Count(t => t.ThreadState == System.Threading.ThreadState.AbortRequested)}   ");
//  //      System.Console.WriteLine($"      Background: {exportThreadPool.Count(t => t.ThreadState == System.Threading.ThreadState.Background)}   ");
//  //      System.Console.WriteLine($"         Running: {exportThreadPool.Count(t => t.ThreadState == System.Threading.ThreadState.Running)}   ");
//  //      System.Console.WriteLine($"         Stopped: {exportThreadPool.Count(t => t.ThreadState == System.Threading.ThreadState.Stopped)}   ");
//  //      System.Console.WriteLine($"   StopRequested: {exportThreadPool.Count(t => t.ThreadState == System.Threading.ThreadState.StopRequested)}   ");
//  //      System.Console.WriteLine($"       Suspended: {exportThreadPool.Count(t => t.ThreadState == System.Threading.ThreadState.Suspended)}   ");
//  //      System.Console.WriteLine($"SuspendRequested: {exportThreadPool.Count(t => t.ThreadState == System.Threading.ThreadState.SuspendRequested)}   ");
//  //      System.Console.WriteLine($"       Unstarted: {exportThreadPool.Count(t => t.ThreadState == System.Threading.ThreadState.Unstarted)}   ");
//  //      System.Console.WriteLine($"   WaitSleepJoin: {exportThreadPool.Count(t => t.ThreadState == System.Threading.ThreadState.WaitSleepJoin)}   ");
//  //      System.Console.SetCursorPosition(0, 12);
//  //      System.Console.WriteLine($"         Aborted: {importThreadPool.Count(t => t.ThreadState == System.Threading.ThreadState.Aborted)}   ");
//  //      System.Console.WriteLine($"  AbortRequested: {importThreadPool.Count(t => t.ThreadState == System.Threading.ThreadState.AbortRequested)}   ");
//  //      System.Console.WriteLine($"      Background: {importThreadPool.Count(t => t.ThreadState == System.Threading.ThreadState.Background)}   ");
//  //      System.Console.WriteLine($"         Running: {importThreadPool.Count(t => t.ThreadState == System.Threading.ThreadState.Running)}   ");
//  //      System.Console.WriteLine($"         Stopped: {importThreadPool.Count(t => t.ThreadState == System.Threading.ThreadState.Stopped)}   ");
//  //      System.Console.WriteLine($"   StopRequested: {importThreadPool.Count(t => t.ThreadState == System.Threading.ThreadState.StopRequested)}   ");
//  //      System.Console.WriteLine($"       Suspended: {importThreadPool.Count(t => t.ThreadState == System.Threading.ThreadState.Suspended)}   ");
//  //      System.Console.WriteLine($"SuspendRequested: {importThreadPool.Count(t => t.ThreadState == System.Threading.ThreadState.SuspendRequested)}   ");
//  //      System.Console.WriteLine($"       Unstarted: {importThreadPool.Count(t => t.ThreadState == System.Threading.ThreadState.Unstarted)}   ");
//  //      System.Console.WriteLine($"   WaitSleepJoin: {importThreadPool.Count(t => t.ThreadState == System.Threading.ThreadState.WaitSleepJoin)}   ");
//  //      var lap = System.DateTime.Now;
//  //      System.Console.WriteLine($"          Timing: {(lap - started).ToString()} ({lap.ToStringISO8601Full()})");
//  //      System.Threading.Thread.Sleep(300);
//  //    }

//  //    xeNodePool.Save(System.IO.Path.Combine(dataPath, $"#NodePool.xml"));
//  //  }

//  //  static void Main(string[] args)
//  //  {
//  //    LaunchSession(@"F:\Pipeline\Meta\", @"F:\Pipeline\Data\");
//  //  }
//  //}
//}
