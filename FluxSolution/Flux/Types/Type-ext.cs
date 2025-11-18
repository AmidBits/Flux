namespace Flux
{
  public static partial class TypeExtensions
  {
    #region NotSureAboutThese (clever way of finding out, but needed..)

    extension<TNumber>(TNumber value)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      #region IsNumericTypeSigned/UnsignedNumber

      /// <summary>
      /// <para>Determines whether the <paramref name="value"/> is signed.</para>
      /// <para><seealso href="https://stackoverflow.com/a/13609383/3178666"/></para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      public bool IsNumericTypeSigned()
        => typeof(TNumber) is var type
        && (
          type.IsNumericTypeSigned()
          // Or if 0.CompareTo(FIELD:MinValue) > 0, i.e. zero is greater than the field value. https://stackoverflow.com/a/13609383
          || TNumber.Zero.CompareTo(type.GetField("MinValue")?.GetValue(null) ?? TNumber.Zero) > 0
          // Or if 0.CompareTo(PROPERTY:NegativeOne) > 0, i.e. zero is greater than the property value.
          || TNumber.Zero.CompareTo(type.GetProperty("NegativeOne", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?.GetValue(null) ?? TNumber.Zero) > 0
          // Or if 0.CompareTo(PROPERTY:"System.Numerics.ISignedNumber<[FULLY_QUALIFIED_TYPE_NAME]>.NegativeOne") > 0, i.e. zero is greater than the property value.
          || TNumber.Zero.CompareTo(type.GetProperty($"System.Numerics.ISignedNumber<{type.FullName}>.NegativeOne", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?.GetValue(null) ?? TNumber.Zero) > 0
          // Or if type is-assignable-to System.Numerics.ISignedNumber<>.
          || type.IsAssignableToGenericType(typeof(System.Numerics.ISignedNumber<>))
        );

      /// <summary>
      /// <para>Determines whether the <paramref name="value"/> is unsigned.</para>
      /// <para><seealso href="https://stackoverflow.com/a/13609383/3178666"/></para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      public bool IsNumericTypeUnsigned()
        => typeof(TNumber) is var type
        && (
          type.IsNumericTypeUnsigned()
          // Or if 0.CompareTo(FIELD:MinValue) <= 0, i.e. zero is equal-or-less-than the number.
          || TNumber.Zero.CompareTo(type.GetField("MinValue")?.GetValue(null) ?? TNumber.One) <= 0
          // Or if type is-assignable-to System.Numerics.IUnsignedNumber<>.
          || type.IsAssignableToGenericType(typeof(System.Numerics.IUnsignedNumber<>))
        );

      #endregion
    }

    #endregion

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

      #region Try/GetCsharpKeyword

      /// <summary>
      /// <para>Get the C# keyword alias of a <see cref="System.Type"/>, if it exist.</para>
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
      /// <para>Attempt to get the C# keyword alias of a <see cref="System.Type"/>, if it exist.</para>
      /// <para>This was originally constructed for numeric types but expanded somewhat over time (for various reasons).</para>
      /// <para><see href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/"/></para>
      /// </summary>
      public bool TryGetCsharpKeyword(out string keyword)
      {
        keyword = type.GetCsharpKeyword();

        return !string.IsNullOrEmpty(keyword);
      }

      #endregion

      #region GetDerived

      /// <summary>
      /// <para>Creates a new sequence with the derived types of a <see cref="System.Type"/> selected from types within the type assembly.</para>
      /// </summary>
      public System.Collections.Generic.List<System.Type> GetDerived()
        => type.GetDerived(type.Assembly.DefinedTypes);

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
      public System.Collections.Generic.List<System.Type> GetImplements()
      {
        System.ArgumentNullException.ThrowIfNull(type);

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

        var list = new System.Collections.Generic.List<System.Type>();

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

      #endregion

      /// <summary>
      /// <para>Indicates whether a <see cref="System.Type"/> is assignable to the specified <paramref name="genericType"/>.</para>
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

      #region IsNumericType..

      /// <summary>
      /// <para>Indicates whether a <see cref="System.Type"/> is a floating-point numeric type.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="isPrimitive"></param>
      /// <returns></returns>
      public bool IsNumericTypeFloatingPoint(bool isPrimitive = false)
        => type.IsSystemNullable()
        ? IsNumericTypeFloatingPoint(System.Nullable.GetUnderlyingType(type)!, false) // Wrapped into System.Nullable<>, so call itself again with the underlying-type.
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
      /// <para>Indicates whether a <see cref="System.Type"/> is an integer numeric type.</para>
      /// </summary>
      /// <remarks>This method does not consider <see cref="System.Enum"/> to be an integer numeric type.</remarks>
      /// <param name="source"></param>
      /// <param name="isPrimitive">If true then the integer numeric type also has to be a .NET primitive, otherwise any integer numeric type is accepted.</param>
      /// <returns></returns>
      public bool IsNumericTypeInteger(bool isPrimitive = false)
        => type.IsSystemNullable()
        ? IsNumericTypeInteger(System.Nullable.GetUnderlyingType(type)!, false) // Wrapped into System.Nullable<>, so call itself again with the underlying-type.
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
      /// <para>Indicates whether a <see cref="System.Type"/> is a signed numeric type.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="isPrimitive">Whether the type must be a .NET primitive.</param>
      /// <returns></returns>
      public bool IsNumericTypeSigned(bool isPrimitive = false)
        => type.IsSystemNullable()
        ? IsNumericTypeSigned(System.Nullable.GetUnderlyingType(type)!, false)
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
      /// <para>Indicates whether a <see cref="System.Type"/> is an unsigned numeric type.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      public bool IsNumericTypeUnsigned(bool isPrimitive = false)
        => type.IsSystemNullable()
        ? IsNumericTypeUnsigned(System.Nullable.GetUnderlyingType(type)!, false)
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

      #endregion

      /// <summary>
      /// <para>Indicates whether a <see cref="System.Type"/> is a reference type.</para>
      /// </summary>
      public bool IsReferenceType()
        => type.CreateDefaultValue() is null && !IsSystemNullable(type);

      /// <summary>Indicates whether a <see cref="System.Type"/> is a static class, based on it being a class, abstract and sealed.</summary>
      public bool IsStaticClass()
        => type is null ? throw new System.ArgumentNullException(nameof(type)) : type.IsClass && type.IsAbstract && type.IsSealed;

      #region IsSub/SuperTypeOf

      /// <summary>
      /// <para>Indicates whether a <see cref="System.Type"/> is a subtype of the specified <paramref name="superType"/>.</para>
      /// </summary>
      /// <remarks>Similar functionality as the built-in <see cref="System.Type.IsSubclassOf(System.Type)"/> but can also handle generics. This is also the same as switching the two arguments for <see cref="IsSupertypeOf(System.Type, System.Type)"/>.</remarks>
      public bool IsSubtypeOf(System.Type superType)
      {
        if (type is null || superType is null || type.Equals(superType))
          return false;

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

      /// <summary>Indicates whether a <see cref="System.Type"/> is System.Nullable<T>.</summary>
      /// <remark>Should be able to alternatively use: (System.Nullable.GetUnderlyingType(typeof(T)) != null)</remark>
      public bool IsSystemNullable()
        => type is not null && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(System.Nullable<>);
    }
  }
}
