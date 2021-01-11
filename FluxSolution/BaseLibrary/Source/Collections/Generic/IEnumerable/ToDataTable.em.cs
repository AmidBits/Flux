namespace Flux
{
	public static partial class SystemCollectionsGenericEm
	{
		public static System.Data.DataTable ToDataTable<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, string tableName, System.Func<TSource, object[]> arraySelector)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (arraySelector is null) throw new System.ArgumentNullException(nameof(arraySelector));

			var dt = new System.Data.DataTable(tableName);

			using var e = source.GetEnumerator();

			if (e.MoveNext())
			{
				var fieldNames = arraySelector(e.Current);
				
				for (var columnIndex = 0; columnIndex < fieldNames.Length; columnIndex++)
					dt.Columns.Add($"{fieldNames[columnIndex]}");

				while (e.MoveNext())
					dt.Rows.Add(arraySelector(e.Current));
			}

			return dt;
		}

		/// <summary>Returns a new data table with the data from the source via an array selector (each call with the ordinal index within the sequence) and the column names/types.</summary>
		public static System.Data.DataTable ToDataTable<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, string tableName, System.Func<TSource, int, object[]> arraySelector, System.Collections.Generic.IList<string> columnNames, System.Collections.Generic.IList<System.Type> columnTypes)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (arraySelector is null) throw new System.ArgumentNullException(nameof(arraySelector));
			if (columnNames is null) throw new System.ArgumentNullException(nameof(columnNames));
			if (columnTypes is null) throw new System.ArgumentNullException(nameof(columnTypes));

			var dt = new System.Data.DataTable(tableName);
			for (var columnIndex = 0; columnIndex < columnNames.Count; columnIndex++)
				dt.Columns.Add(columnNames[columnIndex], columnTypes[columnIndex]);
			var rowIndex = 0;
			foreach (var item in source)
				if (arraySelector(item, rowIndex++) is var itemArray && itemArray is not null)
					dt.Rows.Add(arraySelector(item, rowIndex++));
			return dt;
		}
		/// <summary>Returns a new data table with the data from the source via an array selector (each call with the ordinal index within the sequence) and the column names.</summary>
		public static System.Data.DataTable ToDataTable<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, string tableName, System.Func<TSource, int, object[]> arraySelector, System.Collections.Generic.IList<string> columnNames)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (arraySelector is null) throw new System.ArgumentNullException(nameof(arraySelector));
			if (columnNames is null) throw new System.ArgumentNullException(nameof(columnNames));

			var dt = new System.Data.DataTable(tableName);
			for (var columnIndex = 0; columnIndex < columnNames.Count; columnIndex++)
				dt.Columns.Add(columnNames[columnIndex]);
			var rowIndex = 0;
			foreach (var item in source)
				dt.Rows.Add(arraySelector(item, rowIndex++));
			return dt;
		}
	}
}
