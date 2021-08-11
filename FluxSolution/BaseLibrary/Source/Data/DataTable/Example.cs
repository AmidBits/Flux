
/*
  var dt = new System.Data.DataTable(@"New Table");
  dt.Columns.Add("A");
  dt.Columns.Add("B");
  dt.Columns.Add("C");
  dt.Columns.Add("D");
  dt.Columns.Add("E");
  dt.Rows.Add(new object[] { 1, 2, 3, 4, 5 });
  dt.Rows.Add(new object[] { 6, 7, 8, 9, 10 });
  dt.Rows.Add(new object[] { 11, 12, 13, 14, 15 });
  dt.Rows.Add(new object[] { 16, 17, 18, 19, 20 });
  dt.Rows.Add(new object[] { 21, 22, 23, 24, 25 });
  System.Console.WriteLine(dt.ToConsoleString());

  System.Console.WriteLine("GetValuesInColumn(2)");
  System.Console.WriteLine(string.Join('|', dt.GetValuesInColumn(2)));

  System.Console.WriteLine("ToArray");
  var mda = dt.To2dArray(1, 3, 1, 3);
  System.Console.WriteLine(mda.ToConsoleString2d());

  System.Console.WriteLine("ReverseColumns");
  var reversedColumns = dt.ReverseColumns();
  System.Console.WriteLine(reversedColumns.ToConsoleString());

  System.Console.WriteLine("ReverseColumnsInline");
  reversedColumns.ReverseColumnsInline(1, 2);
  System.Console.WriteLine(reversedColumns.ToConsoleString());

  System.Console.WriteLine("ReverseRows");
  var reversedRows = dt.ReverseRows();
  System.Console.WriteLine(reversedRows.ToConsoleString());

  System.Console.WriteLine("ReverseRowsInline");
  reversedRows.ReverseRowsInline(1, 2);
  System.Console.WriteLine(reversedRows.ToConsoleString());

  System.Console.WriteLine("Transposed");
  var transposed = dt.Transpose(out var _, "X", "Y", "Z");
  System.Console.WriteLine(transposed.ToConsoleString());

  System.Console.WriteLine("RotateLeft");
  var rotatedLeft = dt.RotateLeft(out var _, "X", "Y", "Z");
  System.Console.WriteLine(rotatedLeft.ToConsoleString());

  System.Console.WriteLine("RotateRight");
  var rotatedRight = dt.RotateRight(out var _, "X", "Y", "Z");
  System.Console.WriteLine(rotatedRight.ToConsoleString());
*/
