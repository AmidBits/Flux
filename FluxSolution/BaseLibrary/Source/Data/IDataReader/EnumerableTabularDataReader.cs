using System.Linq;

namespace Flux.Data
{
	/// <summary>An implementation of a <see cref="TabularDataReader"/> over a System.Collection.Generic.IEnumerable<T>.</summary>
	public class EnumerableTabularDataReader
		: TabularDataReader
	{
		private readonly System.Collections.Generic.IEnumerator<System.Collections.Generic.IEnumerable<object>> m_enumerator;

		public EnumerableTabularDataReader(System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<object>> source, System.Collections.Generic.IList<string> fieldNames, System.Collections.Generic.IList<System.Type> fieldTypes)
			: base(fieldNames, fieldTypes)
			=> m_enumerator = source?.GetEnumerator() ?? throw new System.ArgumentNullException(nameof(source));
		public EnumerableTabularDataReader(System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<object>> source, System.Collections.Generic.IList<string> fieldNames)
			: base(fieldNames)
			=> m_enumerator = source?.GetEnumerator() ?? throw new System.ArgumentNullException(nameof(source));
		public EnumerableTabularDataReader(System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<object>> source, int fieldCount)
			: base(fieldCount)
			=> m_enumerator = source?.GetEnumerator() ?? throw new System.ArgumentNullException(nameof(source));

		// Statics
		public static EnumerableTabularDataReader Create<TSource>(System.Collections.Generic.IEnumerable<TSource> collection, System.Collections.Generic.IList<string> fieldNames, System.Collections.Generic.IList<System.Type> fieldTypes, System.Func<TSource, System.Collections.Generic.IList<object>> valueSelector)
			=> new EnumerableTabularDataReader(collection.Select(ts => valueSelector(ts)).ToList(), fieldNames, fieldTypes);
		public static EnumerableTabularDataReader Create<TSource>(System.Collections.Generic.IEnumerable<TSource> collection, System.Collections.Generic.IList<string> fieldNames, System.Func<TSource, System.Collections.Generic.IList<object>> valueSelector)
			=> new EnumerableTabularDataReader(collection.Select(ts => valueSelector(ts)).ToList(), fieldNames);
		public static EnumerableTabularDataReader Create<TSource>(System.Collections.Generic.IEnumerable<TSource> collection, int fieldCount, System.Func<TSource, System.Collections.Generic.IList<object>> valueSelector)
			=> new EnumerableTabularDataReader(collection.Select(ts => valueSelector(ts)).ToList(), fieldCount);

		// IDataReader
		public override bool Read()
		{
			FieldValues.Clear();

			IsClosed = !(m_enumerator?.MoveNext() ?? true);

			if (!IsClosed)
			{
				FieldValues.AddRange(m_enumerator?.Current ?? throw new System.NullReferenceException(nameof(m_enumerator.Current)));

				if (FieldValues.Count != FieldCount) throw new System.Exception($"Mismatch between the count of field values ({(FieldValues is null ? @"<null>" : FieldValues.Count)}) and FieldCount ({FieldCount}).");
			}

			return !IsClosed;
		}

		// IDisposable
		protected override void DisposeManaged()
		{
			m_enumerator.Dispose();

			base.DisposeManaged();
		}
	}
}
