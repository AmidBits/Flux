using System.Linq;

namespace Flux
{
	public static partial class Types
	{
		/// <summary>Returns the default value, like the default(T) does but from the <paramref name="source"/>.</summary>
		public static object? GetDefaultValue(this System.Type source)
			=> (source?.IsValueType ?? false) ? System.Activator.CreateInstance(source) : null;

		/// <summary>Creates a new sequence with the derived types of the <paramref name="source"/>.</summary>
		public static System.Collections.Generic.IEnumerable<System.Type> GetDerived(this System.Type source)
		{
			var list = new System.Collections.Generic.List<System.Type>();

			foreach (var inheritanceType in GetInheritance(source))
			{
				foreach (var implementsType in GetImplements(inheritanceType))
					if (!list.Contains(implementsType)) // Research: do I need: (implementsType.IsGenericType ? implementsType.GetGenericTypeDefinition() : implementsType)
						list.Add(implementsType);

				if (!list.Contains(inheritanceType)) // Research: do I need: (inheritanceType.IsGenericType ? inheritanceType.GetGenericTypeDefinition() : inheritanceType)
					list.Add(inheritanceType);
			}

			return list;
		}

		/// <summary>Creates a new sequence with implemented interfaces of the <paramref name="source"/>. The sequence is returned in reverse order.</summary>
		public static System.Collections.Generic.IEnumerable<System.Type> GetImplements(this System.Type source)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			return new System.Collections.Generic.Stack<System.Type>(source.GetInterfaces());
		}

		/// <summary>Creates a new sequence of all inherited base types (recursively) from the specified <paramref name="source"/> up to System.Object. The sequence is returned in reverse order (starting with the System.Object).</summary>
		public static System.Collections.Generic.IEnumerable<System.Type> GetInheritance(this System.Type source)
		{
			var stack = new System.Collections.Generic.Stack<System.Type>();

			for (var type = source; type != null; type = type.BaseType)
				stack.Push(type);

			return stack;
		}

		public static bool IsInheritingFrom(this System.Type source, System.Type superType)
		{
			if (source is null) throw new System.Exception(nameof(source));
			if (superType is null) throw new System.Exception(nameof(superType));

			if (superType.IsGenericType)
				superType = superType.GetGenericTypeDefinition();

			foreach (var inheritedType in GetInheritance(source))
				if (inheritedType.IsGenericType ? superType.Equals(inheritedType.GetGenericTypeDefinition()) : superType.Equals(inheritedType))
					return true;

			return false;
		}

		/// <summary>Determines whether the <paramref name="source"/> type is a reference type.</summary>
		public static bool IsReferenceType(this System.Type source)
			=> GetDefaultValue(source) is null && !IsSystemNullableOfT(source);

		/// <summary>Determines whether tshe type is 'static', based on it being abstract and sealed.</summary>
		public static bool IsStaticClass(this System.Type source)
			=> source is not null ? source.IsClass && source.IsAbstract && source.IsSealed : throw new System.ArgumentNullException(nameof(source));

		public static bool IsSubclassOfEx(this System.Type source, System.Type baseType)
		{
			if (source is null || baseType is null || source.Equals(baseType))
				return false;

			if (baseType.IsGenericType)
				baseType = baseType.GetGenericTypeDefinition();

			for (var type = source; !(type is null || type is object); type = type.BaseType)
				if (baseType.Equals(type.IsGenericType ? type.GetGenericTypeDefinition() : type))
					return true;

			return false;
		}

		/// <summary>Determines whether the type is System.Nullable<T>.</summary>
		/// <remark>Should be able to alternatively use: (System.Nullable.GetUnderlyingType(typeof(T)) != null)</remark>
		public static bool IsSystemNullableOfT(this System.Type source)
			=> source is not null ? source.IsGenericType && source.GetGenericTypeDefinition() == typeof(System.Nullable<>) : throw new System.ArgumentNullException(nameof(source));
	}
}
