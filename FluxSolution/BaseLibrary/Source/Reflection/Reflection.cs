using System.Linq;

namespace Flux
{
	public static class Reflect
	{
		public static object? GetDefaultValue(System.Type type)
			=> (type?.IsValueType ?? false) ? System.Activator.CreateInstance(type) : null;

		/// <summary>Returns all the fields matching the binding attributes.</summary>
		public static System.Collections.Generic.IDictionary<string, object?> GetFieldsFromInstanceObject<T>(T source)
			=> typeof(T).GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public).ToDictionary(fi => fi.Name, fi => fi.GetValue(source));
		public static System.Collections.Generic.IDictionary<string, object?> GetFieldsFromStaticType(System.Type source)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			return source.GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).ToDictionary(fi => fi.Name, fi => fi.GetValue(null));
		}

		/// <summary>Get the current method name without using reflection.</summary>
		/// <remarks>Using reflection System.Reflection.MethodInfo.GetCurrentMethod() also works.</remarks>
		public static string GetMethodName([System.Runtime.CompilerServices.CallerMemberName] string caller = null!)
			=> caller;

		/// <summary>Returns all the properties matching the binding attributes.</summary>
		public static System.Collections.Generic.IDictionary<string, object?> GetPropertiesFromInstanceObject<T>(T source)
			=> typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public).ToDictionary(pi => pi.Name, pi => pi.GetValue(source, System.Array.Empty<object>()));
		public static System.Collections.Generic.IDictionary<string, object?> GetPropertiesFromStaticType(System.Type source)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			return source.GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).ToDictionary(pi => pi.Name, pi => pi.GetValue(null, null));
		}

		/// <summary>Returns whether the source type is a reference type. Determined by typeof(T), not source.</summary>
		public static bool IsReferenceType<T>(T source)
			=> default(T) is null && !IsSystemNullable(source);

		public static bool IsStaticClass<T>(T source)
			=> (source?.GetType() ?? typeof(T)) is var t && t.IsClass && (t.IsAbstract && t.IsSealed);

		/// <summary>Returns whether the source type is System.Nullable<T>. Determined by typeof(T), not source.</summary>
		/// <remark>Should be able to alternatively use: (System.Nullable.GetUnderlyingType(typeof(T)) != null)</remark>
		public static bool IsSystemNullable<T>(T _)
			=> typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(System.Nullable<>);

		/// <summary>Returns whether the type is System.ValueType. Determined by typeof(T), not source.</summary>
		public static bool IsSystemValueType<T>(T _)
			=> typeof(T).IsValueType;

		/// <summary>Returns whether the source type is a fixed point numeric data type (i.e. decimal).</summary>
		public static bool IsTypeFixedPoint(object source)
			=> source is decimal;

		/// <summary>Returns whether the source type is a floating point numeric data type (i.e. float or double).</summary>
		public static bool IsTypeFloatingPoint(object source)
			=> source is double || source is float;

		/// <summary>Returns whether the source type is a floating point numeric data type (i.e. integer).</summary>
		public static bool IsTypeInteger(object source)
			=> source is byte || source is short || source is int || source is long || source is sbyte || source is ushort || source is uint || source is ulong || source is System.Numerics.BigInteger;

		/// <summary>Returns whether the source type is any kind of numeric data type.</summary>
		public static bool IsTypeNumeric(object source)
			=> IsTypeFixedPoint(source) || IsTypeFloatingPoint(source) || IsTypeInteger(source);
	}
}
