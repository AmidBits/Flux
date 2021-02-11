namespace Flux
{
	//public static class TabularDataAcquirer
	//{
	//	public static System.Data.IDataReader GetDataReader(this ITabularDataAcquirer source)
	//	{
	//	}
	//}

	public interface ITabularDataAcquirer
	{
		/// <summary>Acquire tabular data from the URI. The first array should be field names.</summary>
		System.Collections.Generic.IEnumerable<object[]> AcquireTabularData();
	}
}
