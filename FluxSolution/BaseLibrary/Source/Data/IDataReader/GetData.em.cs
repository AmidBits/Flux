namespace Flux
{
	public static partial class ExtensionMethods
	{
		/// <summary>Creates a new sequence of <typeparamref name="TResult"/> from the IDataReader.</summary>
		public static System.Collections.Generic.IEnumerable<TResult> EnumerateData<TResult>(this System.Data.IDataReader source, System.Func<System.Data.IDataRecord, TResult> resultSelector)
		{
			if (source is null) throw new System.NullReferenceException(nameof(source));
			if (resultSelector is null) throw new System.NullReferenceException(nameof(resultSelector));

			using (source)
				while (source.Read())
					yield return resultSelector(source);
		}
	}
}
