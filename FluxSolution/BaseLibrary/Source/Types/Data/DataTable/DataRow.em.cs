namespace Flux
{
	public static partial class DataTableEm
	{
		/// <summary>Returns a new array with the data from the specified column names.</summary>
		public static object[] GetValues(this System.Data.DataRow source, params string[] columnNames)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			var array = new object[columnNames.Length];
			for (var index = array.Length - 1; index >= 0; index--)
				array[index] = source[columnNames[index]];
			return array;
		}

		/// <summary>Returns a new array with the data from the specified column indices.</summary>
		public static object[] GetValues(this System.Data.DataRow source, params int[] columnIndices)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			var array = new object[columnIndices.Length];
			for (var index = array.Length - 1; index >= 0; index--)
				array[index] = source[columnIndices[index]];
			return array;
		}
	}
}
