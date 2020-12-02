namespace Flux
{
	public static partial class Xtensions
	{
		/// <summary>Creates a new data table from the sequence using the value selector and the column names.</summary>
		public static System.Data.DataTable ToDataTable<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, int, object[]> arraySelector, params string[] columnNames)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (arraySelector is null) throw new System.ArgumentNullException(nameof(arraySelector));

			var dt = new System.Data.DataTable();

			foreach (var columnName in columnNames)
				dt.Columns.Add(columnName);

			var index = 0;

			foreach (var element in source)
			{
				var dr = dt.NewRow();
				dr.ItemArray = arraySelector(element, index++);
				dt.Rows.Add(dr);
			}

			return dt;
		}
	}
}
