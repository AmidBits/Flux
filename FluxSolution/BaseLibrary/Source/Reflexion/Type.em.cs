namespace Flux.Reflexion
{
  public static partial class Types
  {
    /// <summary>Determines whether the source equals the target, taking into account generics.</summary>
    public static bool EqualsEx(this System.Type source, System.Type target)
      => source is null || target is null
      ? source is null && target is null
      : (source.IsGenericType ? source.GetGenericTypeDefinition() : source).Equals(target.IsGenericType ? target.GetGenericTypeDefinition() : target);

    /// <summary>Returns the default value, like the default(T) does but from the <paramref name="source"/>.</summary>
    public static object? GetDefaultValue(this System.Type source)
      => source?.IsValueType ?? false
      ? System.Activator.CreateInstance(source)
      : null;

    /// <summary>Creates a new sequence with the derived types of the <paramref name="source"/> from the specified type collection.</summary>
    public static System.Collections.Generic.IEnumerable<System.Type> GetDerivedTypes(this System.Type source, params System.Type[] types)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      if (source.IsGenericType)
        source = source.GetGenericTypeDefinition();

      foreach (var type in types ?? throw new System.ArgumentNullException(nameof(types)))
        if ((type.IsGenericType ? type.GetGenericTypeDefinition() : type).IsSubtypeOf(source))
          yield return type;
    }
    /// <summary>Creates a new sequence with the derived types of the <paramref name="source"/> from the types from within the Flux library only.</summary>
    public static System.Collections.Generic.IEnumerable<System.Type> GetDerivedTypes(this System.Type source)
      => GetDerivedTypes(source, typeof(Types).Assembly.GetTypes());

    /// <summary>Creates a new sequence with implemented interfaces of the <paramref name="source"/>.</summary>
    public static System.Collections.Generic.IEnumerable<System.Type> GetTypeImplements(this System.Type source)
      => source is null
      ? throw new System.ArgumentNullException(nameof(source))
      : new System.Collections.Generic.Stack<System.Type>(source.GetInterfaces());

    /// <summary>Creates a new sequence with the inheritance type chain of the <paramref name="source"/>.</summary>
    public static System.Collections.Generic.IEnumerable<System.Type> GetTypeInheritance(this System.Type source)
    {
      var stack = new System.Collections.Generic.Stack<System.Type>();
      for (var type = source; type != null; type = type.BaseType)
        stack.Push(type);
      return stack;
    }

    /// <summary>Determines whether the <paramref name="source"/> type is a reference type.</summary>
    public static bool IsReferenceType(this System.Type source)
      => GetDefaultValue(source) is null && !IsSystemNullable(source);

    /// <summary>Determines whether the type is 'static', based on it being abstract and sealed.</summary>
    public static bool IsStaticClass(this System.Type source)
      => source is null
      ? throw new System.ArgumentNullException(nameof(source))
      : source.IsClass && source.IsAbstract && source.IsSealed;

    /// <summary>Perform the same functionality as IsSubclassOf but can also handle generics.</summary>
    public static bool IsSubtypeOf(this System.Type source, System.Type superType)
    {
      if (source is null || superType is null || source.Equals(superType))
        return false;

      if (superType.IsGenericType)
        superType = superType.GetGenericTypeDefinition();

      foreach (var type in GetTypeImplements(source))
        if (superType.Equals(type.IsGenericType ? type.GetGenericTypeDefinition() : type))
          return true;

      foreach (var type in GetTypeInheritance(source))
        if (superType.Equals(type.IsGenericType ? type.GetGenericTypeDefinition() : type))
          return true;

      return false;
    }

    /// <summary>Determines whether the <paramref name="source"/> is a super-type of <paramref name="subType"/>.</summary>
    public static bool IsSupertypeOf(this System.Type source, System.Type subType)
      => IsSubtypeOf(subType, source);

    /// <summary>Determines whether the type is System.Nullable<T>.</summary>
    /// <remark>Should be able to alternatively use: (System.Nullable.GetUnderlyingType(typeof(T)) != null)</remark>
    public static bool IsSystemNullable(this System.Type source)
      => source is null
      ? throw new System.ArgumentNullException(nameof(source))
      : source.IsGenericType && source.GetGenericTypeDefinition() == typeof(System.Nullable<>);
  }
}
