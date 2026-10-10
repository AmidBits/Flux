namespace Flux
{
  public static partial class TypeExtensions
  {
    extension(System.Type)
    {
      public static System.Collections.Generic.IEnumerable<System.Type> GetDefinedTypesInAllLoadedAssemblies()
      {
        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
          foreach (var dt in asm.DefinedTypes)
          {
            yield return dt;
          }
        }
      }
    }

    extension(System.Type type)
    {
      /// <summary>
      /// <para>Indicates whether a <see cref="System.Type"/> is a reference type, as how the CLR defines it.</para>
      /// </summary>
      public bool IsReferenceType
        => !type.IsValueType && !type.IsByRefLike;

      /// <summary>
      /// <para>Indicates whether a <see cref="System.Type"/> is a static class, as how the .</para>
      /// </summary>
      public bool IsStaticClass
        => type is not null && type.IsClass && type.IsAbstract && type.IsSealed
        && type.GetConstructors(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic).Length == 0; // Static classes cannot have instance constructors, so this eliminates false positives.

      #region CreateDefaultValue

      /// <summary>
      /// <para>Uses the built-in <see cref="System.Activator.CreateInstance(System.Type)"/> to return the default value of <see cref="System.Type"/>.</para>
      /// </summary>
      public object? CreateDefaultValue(params object?[]? args)
        => (type?.IsValueType ?? false)
        ? System.Activator.CreateInstance(type, args)
        : null;

      #endregion

      #region ..GetArrayType functions

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

      //      public bool IsDotNetNumberBase()
      //        => type == typeof(System.Numerics.Complex)
      //        || IsDotNetNumber(type);

      //      public bool IsDotNetNumber()
      //        => IsDotNetFloatingPoint(type)
      //        || IsDotNetInteger(type);

      //      public bool IsDotNetFloatingPoint()
      //        => type == typeof(System.Decimal)
      //        || type == typeof(System.Double)
      //        || type == typeof(System.Single)
      //#if NET11_0_OR_GREATER
      //        || type == typeof(System.Numerics.BFloat16)
      //        || type == typeof(System.Numerics.Decimal128)
      //        || type == typeof(System.Numerics.Decimal32)
      //        || type == typeof(System.Numerics.Decimal64)
      //#endif
      //        || type == typeof(System.Runtime.InteropServices.NFloat);

      //      public bool IsDotNetInteger()
      //        => IsDotNetSignedInteger(type)
      //        || IsDotNetUnsignedInteger(type);

      //      public bool IsDotNetUnsignedInteger()
      //        => type == typeof(System.Byte)
      //        || type == typeof(System.UInt128)
      //        || type == typeof(System.UInt16)
      //        || type == typeof(System.UInt32)
      //        || type == typeof(System.UInt64)
      //        || type == typeof(System.UIntPtr);

      //public bool IsDotNetSignedInteger()
      //  => type == typeof(System.Int128)
      //  || type == typeof(System.Int16)
      //  || type == typeof(System.Int32)
      //  || type == typeof(System.Int64)
      //  || type == typeof(System.IntPtr)
      //  || type == typeof(System.Numerics.BigInteger)
      //  || type == typeof(System.SByte);

      //      #region IsNumerics.. functions + helper

      //      /// <summary>
      //      /// <para>Indicates whether a type inherits from <see cref="System.Numerics.IBinaryInteger{TSelf}"/> and optionally is a .NET primitive.</para>
      //      /// </summary>
      //      public bool IsNumericsIBinaryInteger() => IsAssignableToGenericAware(type, typeof(System.Numerics.IBinaryInteger<>));

      //      /// <summary>
      //      /// <para>Indicates whether a type inherits from <see cref="System.Numerics.IFloatingPoint{TSelf}"/> and optionally is a .NET primitive.</para>
      //      /// </summary>
      //      public bool IsNumericsIFloatingPoint() => IsAssignableToGenericAware(type, typeof(System.Numerics.IFloatingPoint<>));

      //      /// <summary>
      //      /// <para>Indicates whether a type inherits from <see cref="System.Numerics.IBinaryFloatingPointIeee754{TSelf}"/> and optionally is a .NET primitive.</para>
      //      /// </summary>
      //      public bool IsNumericsIBinaryFloatingPointIeee754() => IsAssignableToGenericAware(type, typeof(System.Numerics.IBinaryFloatingPointIeee754<>));

      //      /// <summary>
      //      /// <para>Indicates whether a type inherits from <see cref="System.Numerics.IMinMaxValue{TSelf}"/> and optionally is a .NET primitive.</para>
      //      /// </summary>
      //      public bool IsNumericsIMinMaxValue() => IsAssignableToGenericAware(type, typeof(System.Numerics.IMinMaxValue<>));

      //      /// <summary>
      //      /// <para>Indicates whether a type inherits from <see cref="System.Numerics.INumber{TSelf}"/> and optionally is a .NET primitive.</para>
      //      /// </summary>
      //      public bool IsNumericsINumber() => IsAssignableToGenericAware(type, typeof(System.Numerics.INumber<>));

      //      /// <summary>
      //      /// <para>Indicates whether a type inherits from <see cref="System.Numerics.ISignedNumber{TSelf}"/> and optionally is a .NET primitive.</para>
      //      /// </summary>
      //      public bool IsNumericsISignedNumber() => IsAssignableToGenericAware(type, typeof(System.Numerics.ISignedNumber<>));

      //      /// <summary>
      //      /// <para>Indicates whether a type inherits from <see cref="System.Numerics.IUnsignedNumber{TSelf}"/> and optionally is a .NET primitive.</para>
      //      /// </summary>
      //      public bool IsNumericsIUnsignedNumber() => IsAssignableToGenericAware(type, typeof(System.Numerics.IUnsignedNumber<>));

      //      public bool IsNumericsSignedInteger() => IsAssignableToGenericAware(type, typeof(System.Numerics.ISignedNumber<>)) && IsNumericsIBinaryInteger(type);

      //      public bool IsNumericsUnsignedInteger() => IsAssignableToGenericAware(type, typeof(System.Numerics.IUnsignedNumber<>)) && IsNumericsIBinaryInteger(type);

      //      #endregion

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
