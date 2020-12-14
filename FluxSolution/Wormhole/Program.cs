namespace Wormhole
{
	class Program
	{
    static void Main(string[] args)
    {
      System.AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

      if (System.Diagnostics.Debugger.IsAttached || System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName.Contains(".vshost"))
      {
        #region Testing and experimenting.
        //  var qname = Flux.Data.Sql.QName.Parse("[SPXRPTDB].[maximo].[dbo].[workorder]");
        //  var qname = Flux.Data.Sql.QName.Parse("[SDBIDB].[tools].[dbo].[ActivityLog]");
        //  var qname = Flux.Data.Sql.QName.Parse("[SDBIDB].[tools].[Misc].[AllDataTypes]");

        //if (0 == 0)
        //{
        //  using (var connection = new System.Data.SqlClient.SqlConnection(qname.TrustedConnectionStringSqlClient()))
        //  {
        //    connection.Open();

        //    var columns = connection.ExecuteRecords(Flux.Data.Sql.T.InformationSchema.SelectColumns(qname, @"COLUMN_NAME"), 1200, (idr, dt) => string.Concat(idr.GetStringsEx(string.Empty))).ToArray();
        //  }
        //}

        //  if (0 == 0)
        //  {
        //    using (var connection = new System.Data.SqlClient.SqlConnection(qname.TrustedConnectionStringSqlClient()))
        //    {
        //      connection.Open();

        //      var st0 = connection.ExecuteSchemaTable(qname, 120, true);
        //      var st1 = st0.PivotData(out var scn0);
        //      var st2 = st1.PivotData(out var scn1, scn0);
        //    }
        //  }

        //  if (1 == 0)
        //  {
        //    using (var connection = new System.Data.SqlClient.SqlConnection(qname.TrustedConnectionStringSqlClient()))
        //    {
        //      connection.Open();

        //      //var dt1 = System.DateTime.Parse("2013-08-20 18:19:45.1234567");
        //      //var dt2 = System.DateTime.Parse("2013-08-20 18:19:45.1230000");
        //      //var dt3 = System.DateTime.Parse("2013-08-20 18:19:45.0000000");

        //      //var s = connection.ExecuteStrings($"SELECT TOP 100 BatchID,Category,CreateTime,ChangeTime,Location,Message,Name,Provider FROM {qname.QualifiedNameQuoted(3)} (NOLOCK)", 10, null, null, string.Empty, ",", System.Environment.NewLine, System.Environment.NewLine).ToArray();

        //      //System.Console.WriteLine(string.Concat(s));

        //      foreach (var o in connection.ExecuteResults($"SELECT TOP 1 * FROM {qname.QualifiedNameQuoted(3)} (NOLOCK)", 120).Where(o => o is System.Data.DataTable))
        //      {
        //        if (o is System.Data.DataTable && o as System.Data.DataTable is var schemaTable) { }
        //        else if (o is System.Data.IDataRecord && o as System.Data.IDataRecord is var record) { }

        //        switch (o)
        //        {
        //          case System.Data.DataTable dt:
        //            var dt1 = dt;
        //            break;
        //          case System.Data.IDataRecord dr:
        //            var dr1 = dr;
        //            break;
        //        }
        //      }
        //    }
        //  }

        //  if (1 == 0)
        //  {
        //    using (var connection = new System.Data.OleDb.OleDbConnection(qname.TrustedConnectionStringOleDb()))
        //    {
        //      connection.Open();

        //      foreach (var o in connection.ExecuteResults($"SELECT TOP 1 * FROM {qname.QualifiedNameQuoted(3)} (NOLOCK)", 120).Where(o => o is System.Data.DataTable))
        //      {
        //        if (o is System.Data.DataTable && o as System.Data.DataTable is var schemaTable) { }
        //        else if (o is System.Data.IDataRecord && o as System.Data.IDataRecord is var record) { }

        //        switch (o)
        //        {
        //          case System.Data.DataTable dt:
        //            var dt1 = dt;
        //            break;
        //          case System.Data.IDataRecord dr:
        //            var dr1 = dr;
        //            break;
        //        }
        //      }
        //    }
        //  }

        //  if (1 == 0)
        //  {
        //    using (var connection = new System.Data.Odbc.OdbcConnection(qname.TrustedConnectionStringOdbc()))
        //    {
        //      connection.Open();

        //      foreach (var o in connection.ExecuteResults($"SELECT TOP 1 * FROM {qname.QualifiedNameQuoted(3)} (NOLOCK)", 120).Where(o => o is System.Data.DataTable))
        //      {
        //        if (o is System.Data.DataTable && o as System.Data.DataTable is var schemaTable) { }
        //        else if (o is System.Data.IDataRecord && o as System.Data.IDataRecord is var record) { }

        //        switch (o)
        //        {
        //          case System.Data.DataTable dt:
        //            var dt1 = dt;
        //            break;
        //          case System.Data.IDataRecord dr:
        //            var dr1 = dr;
        //            break;
        //        }
        //      }
        //    }
        //  }
        #endregion Testing and experimenting.

        //return;
      }

      var wormholeRoot = new System.IO.DirectoryInfo($"{System.AppDomain.CurrentDomain.BaseDirectory}..\\..\\..\\");

      if (System.Diagnostics.Debugger.IsAttached || System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName.Contains(".vshost"))
      {
        args = new string[] { $"{wormholeRoot.FullName}Test\\Meta", $"{wormholeRoot.FullName}Test\\Work" };
        //args = new string[] { @"F:\Wormhole\Meta", @"F:\Wormhole\Work" };
      }

      if (args.Length != 2)
      {
        throw new System.ArgumentException("Please provide two arguments, the XML, and the path to a directory.");
      }

      if (!Core.TryLoadOrParseXmlManifest(args[0], out var xElement, Core.FlowType.Serial))
      {
        throw new System.ArgumentException("The first argument has to be either a file or an XML document fragment.");
      }

      if (!Core.TryFindOrCreateDirectory(args[1], out var workDirectory, false))
      {
        throw new System.ArgumentException("The second argument has to be a valid directory.");
      }

      Core.Create(xElement, workDirectory.FullName);

      System.AppDomain.CurrentDomain.UnhandledException -= CurrentDomain_UnhandledException;
    }

    private static void CurrentDomain_UnhandledException(object sender, System.UnhandledExceptionEventArgs e)
    {
      var ex = e.ExceptionObject as System.Exception;

      System.Console.Error.WriteLine(ex.Message);

      if (!System.Diagnostics.Debugger.IsAttached)
      {
        System.Environment.Exit(System.Runtime.InteropServices.Marshal.GetHRForException(ex));
      }
    }
  }
}
