namespace Flux
{
	public static partial class SystemTypeEm
	{
		/// <summary>Returns the default value, like the default(T) does but from the <paramref name="source"/>.</summary>
		public static object? GetDefaultValue(this System.Type source)
			=> (source?.IsValueType ?? false) ? System.Activator.CreateInstance(source) : null;

		/// <summary>Determines whether the <paramref name="source"/> type is a reference type.</summary>
		public static bool IsReferenceType(this System.Type source)
			=> GetDefaultValue(source) is null && !IsSystemNullableOfT(source);

		/// <summary>Determines whether the type is 'static', based on it being abstract and sealed.</summary>
		public static bool IsStatic(this System.Type source)
			=> source is not null ? source.IsAbstract && source.IsSealed : throw new System.ArgumentNullException(nameof(source));

		/// <summary>Determines whether the type is System.Nullable<T>.</summary>
		/// <remark>Should be able to alternatively use: (System.Nullable.GetUnderlyingType(typeof(T)) != null)</remark>
		public static bool IsSystemNullableOfT(this System.Type source)
			=> source is not null ? source.IsGenericType && source.GetGenericTypeDefinition() == typeof(System.Nullable<>) : throw new System.ArgumentNullException(nameof(source));
	}
}
