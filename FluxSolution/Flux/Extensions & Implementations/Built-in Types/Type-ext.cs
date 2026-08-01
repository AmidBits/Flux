namespace Flux
{
  public static partial class TypeExtensions
  {
    //public readonly struct JoinedInterfaces : System.Collections.Generic.IEnumerable<Type>
    //{
    //  private readonly Type[] _a;
    //  private readonly Type[] _b;

    //  public JoinedInterfaces(Type[] a, Type[] b)
    //  {
    //    _a = a;
    //    _b = b;
    //  }

    //  public Enumerator GetEnumerator() => new Enumerator(_a, _b);

    //  System.Collections.Generic.IEnumerator<Type> System.Collections.Generic.IEnumerable<Type>.GetEnumerator()
    //  {
    //    foreach (var t in _a) yield return t;
    //    foreach (var t in _b) yield return t;
    //  }

    //  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => ((System.Collections.Generic.IEnumerable<Type>)this).GetEnumerator();

    //  public struct Enumerator
    //  {
    //    private readonly Type[] _a;
    //    private readonly Type[] _b;
    //    private int _index;
    //    private bool _inA;

    //    public Enumerator(Type[] a, Type[] b)
    //    {
    //      _a = a;
    //      _b = b;
    //      _index = -1;
    //      _inA = true;
    //    }

    //    public Type Current => _inA ? _a[_index] : _b[_index];

    //    public bool MoveNext()
    //    {
    //      if (_inA)
    //      {
    //        _index++;
    //        if (_index < _a.Length)
    //          return true;

    //        _inA = false;
    //        _index = 0;
    //        return _b.Length > 0;
    //      }

    //      _index++;
    //      return _index < _b.Length;
    //    }
    //  }
    //}

    public readonly struct CombinedSequence<T>(params System.Collections.Generic.IList<T>[] sequences)
      : System.Collections.Generic.IEnumerable<T>
    {
      public Enumerator GetEnumerator() => new(sequences);

      System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
      {
        foreach (var sequence in sequences)
          foreach (var element in sequence)
            yield return element;
      }

      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        => ((IEnumerable<T>)this).GetEnumerator();

      public struct Enumerator
      {
        private readonly System.Collections.Generic.IList<T>[] m_sequences;
        private int m_outerIndex;
        private int m_innerIndex;

        internal Enumerator(System.Collections.Generic.IList<T>[] sequences)
        {
          m_sequences = sequences;

          Reset();
        }

        public readonly T Current
          => m_sequences[m_outerIndex][m_innerIndex];

        public bool MoveNext()
        {
          while (m_outerIndex < m_sequences.Length)
          {
            m_innerIndex++;

            if (m_innerIndex < m_sequences[m_outerIndex].Count)
              return true;

            m_outerIndex++;
            m_innerIndex = -1;
          }

          return false;
        }

        public void Reset()
        {
          m_outerIndex = 0;
          m_innerIndex = -1;
        }
      }
    }

    public static readonly System.Type NumericsIBinaryInteger = typeof(System.Numerics.IBinaryInteger<>);
    public static readonly System.Type NumericsIFloatingPoint = typeof(System.Numerics.IFloatingPoint<>);
    public static readonly System.Type NumericsIMinMaxValue = typeof(System.Numerics.IMinMaxValue<>);
    public static readonly System.Type NumericsINumber = typeof(System.Numerics.INumber<>);
    public static readonly System.Type NumericsISignedNumber = typeof(System.Numerics.ISignedNumber<>);
    public static readonly System.Type NumericsIUnsignedNumber = typeof(System.Numerics.IUnsignedNumber<>);

    private static readonly System.Collections.Concurrent.ConcurrentDictionary<Type, Type[]> m_getInterfacesCachedGeneric = new();

    private static readonly System.Collections.Concurrent.ConcurrentDictionary<Type, Type[]> m_getInterfacesCachedNonGeneric = new();

    private static void EnsureInterfaceCaches(Type type)
    {
      m_getInterfacesCachedGeneric.GetOrAdd(type, static t =>
      {
        var all = t.GetInterfaces();
        var list = new List<Type>(all.Length);

        foreach (var iface in all)
          if (iface.IsGenericType)
            list.Add(iface.GetGenericTypeDefinition());

        return [.. list];
      });

      m_getInterfacesCachedNonGeneric.GetOrAdd(type, static t =>
      {
        var all = t.GetInterfaces();
        var list = new List<Type>(all.Length);

        foreach (var iface in all)
          if (!iface.IsGenericType)
            list.Add(iface);

        return [.. list];
      });
    }

    extension(System.Type type)
    {
      /// <summary>
      /// <para>A constructed generic type is any generic type that is not a generic type definition.</para>
      /// </summary>
      public bool IsConstructedGenericType
        => type is not null && type.IsGenericType && !type.IsGenericTypeDefinition;

      /// <summary>
      /// <para>A constructed generic type with no unassigned generic parameters.</para>
      /// </summary>
      public bool IsClosedConstructedGenericType
        => type.IsConstructedGenericType && !type.ContainsGenericParameters;

      /// <summary>
      /// <para>A constructed generic type that still contains generic parameters.</para>
      /// </summary>
      public bool IsOpenConstructedGenericType
        => type.IsConstructedGenericType && type.ContainsGenericParameters;

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

      public System.Collections.Generic.IEnumerable<System.Type> GetInterfacesCached()
      {
        System.ArgumentNullException.ThrowIfNull(type);

        EnsureInterfaceCaches(type);

        var g = m_getInterfacesCachedGeneric[type];
        var ng = m_getInterfacesCachedNonGeneric[type];

        return new CombinedSequence<System.Type>(g, ng);
      }

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

      #region IsAssignableToGenericAware

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

        if (otherType.IsGenericType && !otherType.IsGenericTypeDefinition) // Normalize otherType if it's a constructed generic.
          otherType = otherType.GetGenericTypeDefinition();

        if (type == otherType || (type.IsGenericType && type.GetGenericTypeDefinition() == otherType)) // Direct match (including generic definition match), or if type is generic, check its definition.
          return true;

        foreach (var typeInterface in type.GetInterfaces()) // Check interfaces.
          if (typeInterface == otherType || (typeInterface.IsGenericType && typeInterface.GetGenericTypeDefinition() == otherType)) // Same logic as above, but for interfaces.
            return true;

        for (var current = type.BaseType; current != null; current = current.BaseType) // Walk base types.
          if (current == otherType || (current.IsGenericType && current.GetGenericTypeDefinition() == otherType)) // Same logic as above, but for base types.
            return true;

        return false;
      }

      #endregion

      #region IsNumerics.. functions + helper

      /// <summary>
      /// <para>Indicates whether a type inherits from <see cref="System.Numerics.IBinaryInteger{TSelf}"/> and optionally is a .NET primitive.</para>
      /// </summary>
      /// <param name="isPrimitive"></param>
      /// <returns></returns>
      public bool ImplementsIBinaryInteger() => IsAssignableToGenericAware(type, NumericsIBinaryInteger);

      /// <summary>
      /// <para>Indicates whether a type inherits from <see cref="System.Numerics.IFloatingPoint{TSelf}"/> and optionally is a .NET primitive.</para>
      /// </summary>
      /// <param name="isPrimitive"></param>
      /// <returns></returns>
      public bool ImplementsIFloatingPoint() => IsAssignableToGenericAware(type, NumericsIFloatingPoint);

      /// <summary>
      /// <para>Indicates whether a type inherits from <see cref="System.Numerics.IMinMaxValue{TSelf}"/> and optionally is a .NET primitive.</para>
      /// </summary>
      /// <param name="isPrimitive"></param>
      /// <returns></returns>
      public bool ImplementsIMinMaxValue() => IsAssignableToGenericAware(type, NumericsIMinMaxValue);

      /// <summary>
      /// <para>Indicates whether a type inherits from <see cref="System.Numerics.INumber{TSelf}"/> and optionally is a .NET primitive.</para>
      /// </summary>
      /// <param name="isPrimitive"></param>
      /// <returns></returns>
      public bool ImplementsINumber() => IsAssignableToGenericAware(type, NumericsINumber);

      /// <summary>
      /// <para>Indicates whether a type inherits from <see cref="System.Numerics.ISignedNumber{TSelf}"/> and optionally is a .NET primitive.</para>
      /// </summary>
      /// <param name="isPrimitive"></param>
      /// <returns></returns>
      public bool ImplementsISignedNumber() => IsAssignableToGenericAware(type, NumericsISignedNumber);

      /// <summary>
      /// <para>Indicates whether a type inherits from <see cref="System.Numerics.IUnsignedNumber{TSelf}"/> and optionally is a .NET primitive.</para>
      /// </summary>
      /// <param name="isPrimitive"></param>
      /// <returns></returns>
      public bool ImplementsIUnsignedNumber() => IsAssignableToGenericAware(type, NumericsIUnsignedNumber);

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
