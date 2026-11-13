namespace Flux
{
  public static partial class TypeExtensions
  {
    #region ConcurrentDictionary of Cached Interfaces

    private static readonly System.Collections.Concurrent.ConcurrentDictionary<System.Type, System.Type[]> m_cachedInterfaces = new();

    #endregion

    #region FrozenDictionary of C# Aliases

    private static readonly System.Collections.Frozen.FrozenDictionary<System.Type, string> m_csharpAliases
      = System.Collections.Frozen.FrozenDictionary.ToFrozenDictionary<System.Type, string>(new System.Collections.Generic.Dictionary<System.Type, string>()
      {
        [typeof(bool)] = "bool",
        [typeof(byte)] = "byte",
        [typeof(sbyte)] = "sbyte",
        [typeof(short)] = "short",
        [typeof(ushort)] = "ushort",
        [typeof(int)] = "int",
        [typeof(uint)] = "uint",
        [typeof(long)] = "long",
        [typeof(ulong)] = "ulong",
        [typeof(nint)] = "nint",
        [typeof(nuint)] = "nuint",
        [typeof(char)] = "char",
        [typeof(float)] = "float",
        [typeof(double)] = "double",
        [typeof(decimal)] = "decimal",
        [typeof(string)] = "string",
        [typeof(object)] = "object",
        [typeof(void)] = "void",
      });

    #endregion

    extension(System.Type)
    {
      #region ClearCachedInterfaces

      /// <summary>
      /// <para>Clears all cached interfaces.</para>
      /// </summary>
      public static void ClearCachedInterfaces()
        => m_cachedInterfaces.Clear();

      #endregion
    }

    extension(System.Type type)
    {
      #region CreateDefaultValue

      /// <summary>
      /// <para>Uses the built-in <see cref="System.Activator.CreateInstance(System.Type)"/> to return the default value of <see cref="System.Type"/>.</para>
      /// </summary>
      public object? CreateDefaultValue(params object?[]? args)
        => (type?.IsValueType ?? false)
        ? System.Activator.CreateInstance(type, args)
        : null;

      #endregion

      #region Try/GetArrayType

      /// <summary>
      /// <para>If a type is an array, determines its specific array type.</para>
      /// </summary>
      /// <returns></returns>
      public ArrayType GetArrayType()
      {
        if (type is not null && type.IsArray)
        {
          var arrayRank = type.GetArrayRank();

          if (arrayRank == 1)
            return (type.GetElementType()?.IsArray ?? false) ? ArrayType.JaggedArray : ArrayType.OneDimensionalArray;
          else if (arrayRank == 2)
            return ArrayType.TwoDimensionalArray;
          else if (arrayRank > 2)
            return ArrayType.MultiDimensionalArray;
        }

        return ArrayType.NotAnArray;
      }

      /// <summary>
      /// <para>Attempts to get the specific array type of a <see cref="System.Type"/> and indicates whether successful.</para>
      /// </summary>
      /// <param name="arrayType"></param>
      /// <returns></returns>
      public bool TryGetArrayType(out ArrayType arrayType)
      {
        arrayType = GetArrayType(type);

        return arrayType != ArrayType.NotAnArray;
      }

      #endregion

      #region Try/GetAttribute

      /// <summary>
      /// <para>Gets all <typeparamref name="TAttribute"/> attributes of a <see cref="System.Type"/>.</para>
      /// </summary>
      /// <typeparam name="TAttribute"></typeparam>
      /// <param name="source"></param>
      /// <param name="inherit"></param>
      /// <returns></returns>
      public System.Collections.Generic.List<TAttribute> GetAttribute<TAttribute>(bool inherit = false)
        where TAttribute : System.Attribute
        => [.. type.GetCustomAttributes(typeof(TAttribute), inherit).OfType<TAttribute>()];

      /// <summary>
      /// <para>Attempts to get all <typeparamref name="TAttribute"/> attributes of a <see cref="System.Type"/> and indicates whether successful.</para>
      /// <para><see href="https://stackoverflow.com/a/37803935/3178666"/></para>
      /// </summary>
      /// <typeparam name="TAttribute"></typeparam>
      /// <param name="source"></param>
      /// <param name="attributes"></param>
      /// <param name="inherit"></param>
      /// <returns></returns>
      public bool TryGetAttribute<TAttribute>(out System.Collections.Generic.List<TAttribute> attributes, bool inherit = false)
        where TAttribute : System.Attribute
      {
        try { attributes = type.GetAttribute<TAttribute>(inherit); }
        catch { attributes = []; }

        return attributes is not null && attributes.Count > 0;
      }

      #endregion

      #region GetCachedInterfaces

      /// <summary>
      /// <para>Gets the interfaces of a <paramref name="type"/> by either retrieving from reflection or from interface cache. Generic types are replaced with generic type definitions.</para>
      /// </summary>
      /// <param name="skipCaching"></param>
      /// <returns>All interfaces implemented by the <paramref name="type"/>.</returns>
      public System.Type[] GetCachedInterfaces(bool skipCaching = false)
      {
        if (m_cachedInterfaces.TryGetValue(type, out var interfaces))
          return interfaces;

        interfaces = [.. type.GetInterfaces().Select(ti => ti.IsGenericType ? ti.GetGenericTypeDefinition() : ti)];

        if (!skipCaching && interfaces.Length > 0)
          m_cachedInterfaces[type] = interfaces;

        return interfaces;
      }

      #endregion

      #region GetCsharpAlias

      public string? GetCsharpAlias()
        => m_csharpAliases.TryGetValue(type, out var value) ? value : null;

      #endregion

      #region GetCsharpName

      public string GetCsharpName()
        => m_csharpAliases.TryGetValue(type, out var value) ? value : type.Name;

      #endregion

      #region GetDerived

      /// <summary>
      /// <para>Creates a new sequence with the derived types of a <see cref="System.Type"/> selected from types within the type assembly.</para>
      /// </summary>
      public System.Collections.Generic.List<System.Type> GetDerived()
        => type.GetDerived(System.AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()));

      /// <summary>
      /// <para>Creates a new sequence with the derived types of a <see cref="System.Type"/> in the specified <paramref name="typeCollection"/>.</para>
      /// </summary>
      public System.Collections.Generic.List<System.Type> GetDerived(System.Collections.Generic.IEnumerable<System.Type> typeCollection)
        => [.. typeCollection.Where(t => t.IsSubtypeOf(type))];

      #endregion

      #region GetImplements

      /// <summary>
      /// <para>Creates a new list with all implemented interfaces through the inheritance chain of a <see cref="System.Type"/>.</para>
      /// </summary>
      /// <returns></returns>

      public System.Collections.Generic.IReadOnlyList<System.Type> GetImplements()
      {
        var list = new System.Collections.Generic.List<System.Type>();

        foreach (var baseType in GetInheritance(type, true))
          foreach (var interfaceType in baseType.GetInterfaces())
            list.Add(interfaceType);

        return list;
      }

      #endregion

      #region GetInheritedTypes

      /// <summary>
      /// <para>Creates a new sequence with the inheritance "chain" of base types of a <see cref="System.Type"/>, excluding the type itself. This also does not include interfaces, only baseTypes up the chain.</para>
      /// </summary>
      public System.Collections.Generic.List<System.Type> GetInheritance(bool includeSource)
      {
        System.ArgumentNullException.ThrowIfNull(type);

        var list = new System.Collections.Generic.List<System.Type>(4);

        for (var baseType = includeSource ? type : type.BaseType; baseType != null; baseType = baseType.BaseType)
          list.Add(baseType);

        return list;
      }

      #endregion

      #region GetMemberDictionary

      /// <summary>
      /// <para>Creates a new dictionary with field and property members of a <see cref="System.Type"/>.</para>
      /// </summary>
      /// <param name="instanceOrStatic">Pass null for static values.</param>
      /// <param name="bindingFlags"></param>
      /// <returns></returns>
      public System.Collections.Generic.IDictionary<System.Reflection.MemberInfo, object?> GetMemberDictionary(object? instanceOrStatic = null, System.Reflection.BindingFlags bindingFlags = System.Reflection.BindingFlags.FlattenHierarchy | System.Reflection.BindingFlags.Instance | /*System.Reflection.BindingFlags.NonPublic | */System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
      {
        var members = new System.Collections.Generic.OrderedDictionary<System.Reflection.MemberInfo, object?>();

        foreach (var mi in type.GetMembers(bindingFlags))
        {
          object? value = null;

          if (mi is System.Reflection.FieldInfo fi)
          {
            if (fi.IsStatic)
              value = fi.GetValue(null); // Get the static field value.
            else if (instanceOrStatic is not null)
              value = fi.GetValue(instanceOrStatic); // Get the field instance value.
          }

          if (mi is System.Reflection.PropertyInfo pi)
          {
            if (pi.GetMethod?.IsStatic ?? false)
              value = pi.GetValue(null); // Get the static property value.
            else if (instanceOrStatic is not null)
              value = pi.GetValue(instanceOrStatic); // Get the property instance value.
          }

          if (value is not null)
            members.Add(mi, value); // Add only when there is a value.
        }

        return members;
      }

      #endregion

      #region IsAssignable..GenericAware

      public bool IsAssignableFromGenericAware(System.Type otherType)
        => otherType.IsAssignableToGenericAware(type);

      /// <summary>
      /// <para>Indicates whether a type is assignable to the specified <paramref name="otherType"/>.</para>
      /// <para>If type is a generic type, it will check against the generic type definition.</para>
      /// <para><see href="https://stackoverflow.com/a/1075059/3178666"/></para>
      /// <example>
      /// <code>var isSignedNumber = typeof(int).IsAssignableToGenericAware(typeof(System.Numerics.ISignedNumber&lt;>)); // == false</code>
      /// <code>var isUnsignedNumber = typeof(int).IsAssignableToGenericAware(typeof(System.Numerics.IUnsignedNumber&lt;>)); // == true</code>
      /// </example>
      /// </summary>
      public bool IsAssignableToGenericAware(System.Type otherType)
      {
        if (type is null || otherType is null)
          return false;

        var ot = (otherType.IsGenericType && !otherType.IsGenericTypeDefinition) // Extract generic type definition, if needed.
          ? otherType.GetGenericTypeDefinition()
          : otherType;

        if (type == ot
          || (type.IsGenericType && type.GetGenericTypeDefinition() == ot)) // Direct match (including generic definition match), or if type is generic, check its definition.
          return true;

        foreach (var it in type.GetInterfaces()) // Check interfaces.
          if (it == ot
            || (it.IsGenericType && it.GetGenericTypeDefinition() == ot)) // Same logic as above, but for interfaces.
            return true;

        for (var bt = type.BaseType; bt is not null; bt = bt.BaseType) // Walk base types.
          if (bt.IsAssignableToGenericAware(ot))
            return true;

        return false;
      }

      #endregion

      #region IsReferenceType

      /// <summary>
      /// <para>Indicates whether a <see cref="System.Type"/> is a reference type, as how the CLR defines it.</para>
      /// </summary>
      public bool IsReferenceType
        => !type.IsValueType && !type.IsByRefLike;

      #endregion

      #region IsStaticClass

      /// <summary>
      /// <para>Indicates whether a <see cref="System.Type"/> is a static class, as how the .</para>
      /// </summary>
      public bool IsStaticClass
        => type is not null && type.IsClass && type.IsAbstract && type.IsSealed
        && type.GetConstructors(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic).Length == 0; // Static classes cannot have instance constructors, so this eliminates false positives.

      #endregion

      #region IsSub/SuperTypeOf

      /// <summary>
      /// <para>Indicates whether a <see cref="System.Type"/> is a subtype of the specified <paramref name="superType"/>.</para>
      /// </summary>
      /// <remarks>Similar functionality as the built-in <see cref="System.Type.IsSubclassOf(System.Type)"/> but can also handle generics. This is also the same as switching the two arguments for <see cref="IsSupertypeOf(System.Type, System.Type)"/>.</remarks>
      public bool IsSubtypeOf(System.Type superType)
      {
        if (type is null || superType is null || type.Equals(superType))
          return false;

        //var stigt = superType.IsGenericTypeDefinition;

        //if (type.GetInterfaces().Any(it => superType.Equals((stigt && it.IsGenericType) ? it.GetGenericTypeDefinition() : it)))
        //  return true;

        //if (GetInheritance(type, false).Any(bt => superType.Equals((stigt && bt.IsGenericType) ? bt.GetGenericTypeDefinition() : bt)))
        //  return true;

        var interfaceTypes = type.GetInterfaces();

        for (var index = interfaceTypes.Length - 1; index >= 0; index--)
        {
          var interfaceType = interfaceTypes[index];

          if (superType.IsGenericTypeDefinition && interfaceType.IsGenericType)
            interfaceType = interfaceType.GetGenericTypeDefinition();

          if (superType.Equals(interfaceType))
            return true;
        }

        var baseTypes = GetInheritance(type, false);

        for (var index = baseTypes.Count - 1; index >= 0; index--)
        {
          var baseType = baseTypes[index];

          if (superType.IsGenericTypeDefinition && baseType.IsGenericType)
            baseType = baseType.GetGenericTypeDefinition();

          if (superType.Equals(baseType))
            return true;
        }

        return false;
      }

      /// <summary>
      /// <para>Indicates whether a <see cref="System.Type"/> is a supertype of the specified <paramref name="subType"/>.</para>
      /// </summary>
      /// <remarks>This is (literally) the same as switching the two arguments for <see cref="IsSubtypeOf(System.Type, System.Type)"/>.</remarks>
      public bool IsSupertypeOf(System.Type subType)
        => subType.IsSubtypeOf(type);

      #endregion

      #region IsSystemNullable

      /// <summary>
      /// <para>Indicates whether a <see cref="System.Type"/> is <see cref="System.Nullable{T}"/>.</para>
      /// </summary>
      /// <remark>Should be able to alternatively use <c>(System.Nullable.GetUnderlyingType(typeof(T)) != null)</c>.</remark>
      /// <returns></returns>
      public bool IsSystemNullable()
        => type is not null && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(System.Nullable<>);

      /// <summary>
      /// <para>Indicates whether a type is <see cref="System.Nullable{T}"/>. If <see cref="System.Nullable{T}"/>, outputs the <paramref name="underlyingType"/> (the type of the generic argument), and if not, the type itself.</para>
      /// </summary>
      /// <remark>Should be able to alternatively use <c>(System.Nullable.GetUnderlyingType(typeof(T)) != null)</c>.</remark>
      /// <param name="underlyingType"></param>
      /// <returns></returns>
      public bool TryIsSystemNullable(out System.Type? underlyingType)
        => (underlyingType = System.Nullable.GetUnderlyingType(type)) is not null;

      #endregion
    }
  }
}
