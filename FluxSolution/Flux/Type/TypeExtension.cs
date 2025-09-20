namespace Flux
{
  public static partial class TypeExtension
  {
    extension(System.Type type)
    {
      /// <summary>
      /// <para>Uses the built-in <see cref="System.Activator.CreateInstance(System.Type)"/> to return the default value of <see cref="System.Type"/>.</para>
      /// </summary>
      public object? CreateDefaultValue(params object?[]? args)
        => (type?.IsValueType ?? false)
        ? System.Activator.CreateInstance(type, args)
        : null;

      /// <summary>
      /// <para>Gets all <typeparamref name="TAttribute"/> attributes of <paramref name="source"/>.</para>
      /// </summary>
      /// <typeparam name="TAttribute"></typeparam>
      /// <param name="source"></param>
      /// <param name="inherit"></param>
      /// <returns></returns>
      public System.Collections.Generic.List<TAttribute> GetAttribute<TAttribute>(bool inherit = false)
        where TAttribute : System.Attribute
        => [.. type.GetCustomAttributes(typeof(TAttribute), inherit).OfType<TAttribute>()];

      /// <summary>
      /// <para>Attempts to get all <typeparamref name="TAttribute"/> attributes of <paramref name="source"/> and indicates whether successful.</para>
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

      /// <summary>
      /// <para>Get the C# keyword alias of basic .NET built-in types.</para>
      /// <para>This was originally constructed for numeric types but expanded somewhat over time (for various reasons).</para>
      /// <para><see href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/"/></para>
      /// </summary>
      public string GetCsharpKeyword()
        => type is null ? string.Empty
        : (type == typeof(System.Boolean)) ? "bool"
        : (type == typeof(System.Byte)) ? typeof(System.Byte).Name.ToLowerInvariant()
        : (type == typeof(System.Char)) ? typeof(System.Char).Name.ToLowerInvariant()
        : (type == typeof(System.Decimal)) ? typeof(System.Decimal).Name.ToLowerInvariant()
        : (type == typeof(System.Double)) ? typeof(System.Double).Name.ToLowerInvariant()
        : (type == typeof(System.Enum)) ? typeof(System.Enum).Name.ToLowerInvariant()
        : (type == typeof(System.Int16)) ? "short"
        : (type == typeof(System.Int32)) ? "int"
        : (type == typeof(System.Int64)) ? "long"
        : (type == typeof(System.IntPtr)) ? "nint"
        : (type == typeof(System.Object)) ? typeof(System.Object).Name.ToLowerInvariant() // Reference type.
        : (type == typeof(System.SByte)) ? "sbyte"
        : (type == typeof(System.Single)) ? "float"
        : (type == typeof(System.String)) ? typeof(System.String).Name.ToLowerInvariant() // Reference type.
        : (type == typeof(System.UInt16)) ? "ushort"
        : (type == typeof(System.UInt32)) ? "uint"
        : (type == typeof(System.UInt64)) ? "ulong"
        : (type == typeof(System.UIntPtr)) ? "nuint"
        : string.Empty;

      /// <summary>
      /// <para>Get the C# keyword alias of basic .NET built-in types.</para>
      /// <para>This was originally constructed for numeric types but expanded somewhat over time (for various reasons).</para>
      /// <para><see href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/"/></para>
      /// </summary>
      public bool TryGetCsharpKeyword(out string keyword)
      {
        keyword = type.GetCsharpKeyword();

        return !string.IsNullOrEmpty(keyword);
      }

      /// <summary>Creates a new sequence with the derived types of <paramref name="source"/> selected from types within the <paramref name="source"/> assembly.</summary>
      public System.Collections.Generic.List<System.Type> GetDerivedTypes()
        => type.GetDerivedTypes(type.Assembly.DefinedTypes);

      /// <summary>Creates a new sequence with the derived types of the <paramref name="source"/> from the specified <paramref name="typeCollection"/>.</summary>
      public System.Collections.Generic.List<System.Type> GetDerivedTypes(System.Collections.Generic.IEnumerable<System.Type> typeCollection)
        => [.. typeCollection.Where(type => type.IsSubtypeOf(type))];

      /// <summary>Creates a new sequence with the inheritance "chain" of base types of the <paramref name="source"/> type, excluding the <paramref name="source"/> itself. This does not include interfaces.</summary>
      public System.Collections.Generic.List<System.Type> GetInheritedBaseTypes(bool includeSource)
      {
        System.ArgumentNullException.ThrowIfNull(type);

        var list = new System.Collections.Generic.List<System.Type>();

        for (var baseType = includeSource ? type : type.BaseType; baseType != null; baseType = baseType.BaseType)
          list.Add(baseType);

        return list;
      }

      /// <summary>
      /// <para>Creates a new dictionary with field and property members.</para>
      /// </summary>
      /// <param name="instanceOrStatic">Pass null for static values.</param>
      /// <param name="bindingFlags"></param>
      /// <returns></returns>
      public System.Collections.Generic.IDictionary<System.Reflection.MemberInfo, object?> GetMemberDictionary(object? instanceOrStatic = null, System.Reflection.BindingFlags bindingFlags = System.Reflection.BindingFlags.FlattenHierarchy | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
      {
        var members = new DataStructures.OrderedDictionary<System.Reflection.MemberInfo, object?>();

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

      /// <summary>
      /// <para>Determines whether <paramref name="source"/> is assignable to the <paramref name="genericType"/>.</para>
      /// <para><see href="https://stackoverflow.com/a/1075059/3178666"/></para>
      /// <example>var isUnsignedNumber = typeof(int).IsAssignableToGenericType(typeof(System.Numerics.IUnsignedNumber&lt;>));</example>
      /// </summary>
      public bool IsAssignableToGenericType(System.Type genericType)
      {
        if (type.IsGenericType && type.GetGenericTypeDefinition() == genericType)
          return true;

        foreach (var it in type.GetInterfaces())
          if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
            return true;

        return type.BaseType?.IsAssignableToGenericType(genericType) ?? false;
      }

      /// <summary>
      /// <para>Indicates whether the source type is a floating-point numeric type.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="isPrimitive"></param>
      /// <returns></returns>
      public bool IsFloatingPointNumericType(bool isPrimitive = false)
        => type.IsSystemNullable()
        ? IsFloatingPointNumericType(System.Nullable.GetUnderlyingType(type)!) // Wrapped into System.Nullable<>, so call itself again with the underlying-type.
        : type is not null
        && (!isPrimitive || type.IsPrimitive) // Only verify primitive, if asked for it.
        && (
          System.Type.GetTypeCode(type)
          is System.TypeCode.Decimal
          or System.TypeCode.Double
          or System.TypeCode.Single
          || type == typeof(System.Half)
          || type == typeof(System.Runtime.InteropServices.NFloat)
          || type.IsAssignableToGenericType(typeof(System.Numerics.IFloatingPoint<>))
        );

      /// <summary>
      /// <para>Indicates whether the source type is an integer numeric type.</para>
      /// </summary>
      /// <remarks>This method does not consider <see cref="System.Enum"/> to be an integer numeric type.</remarks>
      /// <param name="source"></param>
      /// <param name="isPrimitive">If true then the integer numeric type also has to be a .NET primitive, otherwise any integer numeric type is accepted.</param>
      /// <returns></returns>
      public bool IsIntegerNumericType(bool isPrimitive = false)
        => type.IsSystemNullable()
        ? IsIntegerNumericType(System.Nullable.GetUnderlyingType(type)!) // Wrapped into System.Nullable<>, so call itself again with the underlying-type.
        : type is not null
        && !type.IsEnum // Enum is NOT considered a numeric type, even though it cannot represent anything but a primitive number. Too much ambiguity, so user has to handle it to their needs.
        && (!isPrimitive || type.IsPrimitive) // Only verify primitive, if asked for it.
        && (
          System.Type.GetTypeCode(type)
          is System.TypeCode.Byte
          or System.TypeCode.Char // Even though char is not a number, per se, it does derive from System.Numerics.[numeric]<> interfaces, so we check it here instead of deferring to the slower but inevitable match below by IsAssignableToGenericType(..).
          or System.TypeCode.Int16
          or System.TypeCode.Int32
          or System.TypeCode.Int64
          or System.TypeCode.SByte
          or System.TypeCode.UInt16
          or System.TypeCode.UInt32
          or System.TypeCode.UInt64
          || type == typeof(System.Int128)
          || type == typeof(System.IntPtr)
          || type == typeof(System.Numerics.BigInteger)
          || type == typeof(System.UInt128)
          || type == typeof(System.UIntPtr)
          || type.IsAssignableToGenericType(typeof(System.Numerics.IBinaryInteger<>))
        );

      /// <summary>
      /// <para>Determines whether the <paramref name="source"/> type is a reference type.</para>
      /// </summary>
      public bool IsReferenceType()
        => type.CreateDefaultValue() is null && !IsSystemNullable(type);

      /// <summary>Determines whether the type is a static class, based on it being a class, abstract and sealed.</summary>
      public bool IsStaticClass()
        => type is null ? throw new System.ArgumentNullException(nameof(type)) : type.IsClass && type.IsAbstract && type.IsSealed;

      /// <summary>
      /// <para>Determines whether the <paramref name="source"/> is a subtype of <paramref name="supertype"/>.</para>
      /// </summary>
      /// <remarks>Similar functionality as the built-in <see cref="System.Type.IsSubclassOf(System.Type)"/> but can also handle generics. This is also the same as switching the two arguments for <see cref="IsSupertypeOf(System.Type, System.Type)"/>.</remarks>
      public bool IsSubtypeOf(System.Type supertype)
      {
        if (type is null || supertype is null || type.Equals(supertype))
          return false;

        var interfaceTypes = type.GetInterfaces();

        for (var index = interfaceTypes.Length - 1; index >= 0; index--)
        {
          var interfaceType = interfaceTypes[index];

          if (supertype.IsGenericTypeDefinition && interfaceType.IsGenericType)
            interfaceType = interfaceType.GetGenericTypeDefinition();

          if (supertype.Equals(interfaceType))
            return true;
        }

        var baseTypes = GetInheritedBaseTypes(type, false);

        for (var index = baseTypes.Count - 1; index >= 0; index--)
        {
          var baseType = baseTypes[index];

          if (supertype.IsGenericTypeDefinition && baseType.IsGenericType)
            baseType = baseType.GetGenericTypeDefinition();

          if (supertype.Equals(baseType))
            return true;
        }

        return false;
      }

      /// <summary>
      /// <para>Determines whether the <paramref name="source"/> is a supertype of <paramref name="subtype"/>.</para>
      /// </summary>
      /// <remarks>This is (literally) the same as switching the two arguments for <see cref="IsSubtypeOf(System.Type, System.Type)"/>.</remarks>
      public bool IsSupertypeOf(System.Type subtype)
        => subtype.IsSubtypeOf(type);

      /// <summary>Determines whether the type is System.Nullable<T>.</summary>
      /// <remark>Should be able to alternatively use: (System.Nullable.GetUnderlyingType(typeof(T)) != null)</remark>
      public bool IsSystemNullable()
        => type is not null && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(System.Nullable<>);

      /// <summary>
      /// <para>Indicates whether the source type is a signed numeric type.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="isPrimitive">Whether the type must be a .NET primitive.</param>
      /// <returns></returns>
      public bool IsSignedNumericType(bool isPrimitive = false)
        => type.IsSystemNullable()
        ? IsSignedNumericType(System.Nullable.GetUnderlyingType(type)!)
        : type is not null
        && !type.IsEnum
        && (!isPrimitive || type.IsPrimitive)
        && (
          System.Type.GetTypeCode(type)
          is System.TypeCode.Decimal
          or System.TypeCode.Double
          or System.TypeCode.Int16
          or System.TypeCode.Int32
          or System.TypeCode.Int64
          or System.TypeCode.SByte
          or System.TypeCode.Single
          || type == typeof(System.Half)
          || type == typeof(System.Int128)
          || type == typeof(System.Runtime.InteropServices.NFloat)
          || type == typeof(System.IntPtr)
          || type == typeof(System.Numerics.BigInteger)
          || type == typeof(System.Numerics.Complex)
          || type.IsAssignableToGenericType(typeof(System.Numerics.ISignedNumber<>))
        );

      /// <summary>
      /// <para>Indicates whether the source type is an unsigned numeric type.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      public bool IsUnsignedNumericType(bool isPrimitive = false)
        => type.IsSystemNullable()
        ? IsUnsignedNumericType(System.Nullable.GetUnderlyingType(type)!)
        : type is not null
        && !type.IsEnum
        && (!isPrimitive || type.IsPrimitive)
        && (
          System.Type.GetTypeCode(type)
          is System.TypeCode.Byte
          or System.TypeCode.Char
          or System.TypeCode.UInt16
          or System.TypeCode.UInt32
          or System.TypeCode.UInt64
          || type == typeof(System.UInt128)
          || type == typeof(System.UIntPtr)
          || type.IsAssignableToGenericType(typeof(System.Numerics.IUnsignedNumber<>))
        );
    }
  }
}
