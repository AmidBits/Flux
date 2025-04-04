﻿//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Dynamic;
//using System.Linq;
//using System.Reflection;
//using System.Reflection.Emit;
//using System.Runtime.CompilerServices;
//using System.Threading;
//using Microsoft.CSharp.RuntimeBinder;

//namespace Flux
//{
//  #region CallSiteCache

//  internal static class CallSiteCache
//  {
//    private static readonly Hashtable getters = new Hashtable(), setters = new Hashtable();

//    internal static object GetValue(string name, object target)
//    {
//      CallSite<Func<CallSite, object, object>> callSite = (CallSite<Func<CallSite, object, object>>)getters[name];
//      if (callSite == null)
//      {
//        CallSite<Func<CallSite, object, object>> newSite = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, name, typeof(CallSiteCache), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
//        lock (getters)
//        {
//          callSite = (CallSite<Func<CallSite, object, object>>)getters[name];
//          if (callSite == null)
//          {
//            getters[name] = callSite = newSite;
//          }
//        }
//      }
//      return callSite.Target(callSite, target);
//    }
//    internal static void SetValue(string name, object target, object value)
//    {
//      CallSite<Func<CallSite, object, object, object>> callSite = (CallSite<Func<CallSite, object, object, object>>)setters[name];
//      if (callSite == null)
//      {
//        CallSite<Func<CallSite, object, object, object>> newSite = CallSite<Func<CallSite, object, object, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, name, typeof(CallSiteCache), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null) }));
//        lock (setters)
//        {
//          callSite = (CallSite<Func<CallSite, object, object, object>>)setters[name];
//          if (callSite == null)
//          {
//            setters[name] = callSite = newSite;
//          }
//        }
//      }
//      callSite.Target(callSite, target, value);
//    }

//  }

//  #endregion

//  #region MemberSet

//  /// <summary>
//  /// Represents an abstracted view of the members defined for a type
//  /// </summary>
//  public sealed class MemberSet : IEnumerable<Member>, IList<Member>
//  {
//    Member[] members;
//    internal MemberSet(Type type)
//    {
//      const BindingFlags PublicInstance = BindingFlags.Public | BindingFlags.Instance;
//      members = type.GetTypeAndInterfaceProperties(PublicInstance).Cast<MemberInfo>().Concat(type.GetFields(PublicInstance).Cast<MemberInfo>()).OrderBy(x => x.Name)
//          .Select(member => new Member(member)).ToArray();
//    }
//    /// <summary>
//    /// Return a sequence of all defined members
//    /// </summary>
//    public IEnumerator<Member> GetEnumerator()
//    {
//      foreach (var member in members) yield return member;
//    }
//    /// <summary>
//    /// Get a member by index
//    /// </summary>
//    public Member this[int index]
//    {
//      get { return members[index]; }
//    }
//    /// <summary>
//    /// The number of members defined for this type
//    /// </summary>
//    public int Count { get { return members.Length; } }
//    Member IList<Member>.this[int index]
//    {
//      get { return members[index]; }
//      set { throw new NotSupportedException(); }
//    }

//    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }
//    bool ICollection<Member>.Remove(Member item) { throw new NotSupportedException(); }
//    void ICollection<Member>.Add(Member item) { throw new NotSupportedException(); }
//    void ICollection<Member>.Clear() { throw new NotSupportedException(); }
//    void IList<Member>.RemoveAt(int index) { throw new NotSupportedException(); }
//    void IList<Member>.Insert(int index, Member item) { throw new NotSupportedException(); }

//    bool ICollection<Member>.Contains(Member item) => members.Contains(item);
//    void ICollection<Member>.CopyTo(Member[] array, int arrayIndex) { members.CopyTo(array, arrayIndex); }
//    bool ICollection<Member>.IsReadOnly { get { return true; } }
//    int IList<Member>.IndexOf(Member member) { return Array.IndexOf<Member>(members, member); }

//  }
//  /// <summary>
//  /// Represents an abstracted view of an individual member defined for a type
//  /// </summary>
//  public sealed class Member
//  {
//    private readonly MemberInfo member;
//    internal Member(MemberInfo member)
//    {
//      this.member = member;
//    }
//    /// <summary>
//    /// The ordinal of this member among other members.
//    /// Returns -1 in case the ordinal is not set.
//    /// </summary>
//    public int Ordinal
//    {
//      get
//      {
//        var ordinalAttr = member.CustomAttributes.FirstOrDefault(p => p.AttributeType == typeof(OrdinalAttribute));

//        if (ordinalAttr == null)
//        {
//          return -1;
//        }

//        // OrdinalAttribute class must have only one constructor with a single argument.
//        return Convert.ToInt32(ordinalAttr.ConstructorArguments.Single().Value);
//      }
//    }
//    /// <summary>
//    /// The name of this member
//    /// </summary>
//    public string Name { get { return member.Name; } }
//    /// <summary>
//    /// The type of value stored in this member
//    /// </summary>
//    public Type Type
//    {
//      get
//      {
//        if (member is FieldInfo) return ((FieldInfo)member).FieldType;
//        if (member is PropertyInfo) return ((PropertyInfo)member).PropertyType;
//        throw new NotSupportedException(member.GetType().Name);
//      }
//    }

//    /// <summary>
//    /// Is the attribute specified defined on this type
//    /// </summary>
//    public bool IsDefined(Type attributeType)
//    {
//      if (attributeType == null) throw new ArgumentNullException(nameof(attributeType));
//      return Attribute.IsDefined(member, attributeType);
//    }

//    /// <summary>
//    /// Getting Attribute Type
//    /// </summary>
//    public Attribute GetAttribute(Type attributeType, bool inherit)
//        => Attribute.GetCustomAttribute(member, attributeType, inherit);

//    /// <summary>
//    /// Property Can Write
//    /// </summary>
//    public bool CanWrite
//    {
//      get
//      {
//        switch (member.MemberType)
//        {
//          case MemberTypes.Property: return ((PropertyInfo)member).CanWrite;
//          default: throw new NotSupportedException(member.MemberType.ToString());
//        }
//      }
//    }

//    /// <summary>
//    /// Property Can Read
//    /// </summary>
//    public bool CanRead
//    {
//      get
//      {
//        switch (member.MemberType)
//        {
//          case MemberTypes.Property: return ((PropertyInfo)member).CanRead;
//          default: throw new NotSupportedException(member.MemberType.ToString());
//        }
//      }
//    }
//  }

//  #endregion

//  #region TypeAccessor

//  /// <summary>
//  /// Provides by-name member-access to objects of a given type
//  /// </summary>
//  public abstract class TypeAccessor
//  {
//    // hash-table has better read-without-locking semantics than dictionary
//    private static readonly Hashtable publicAccessorsOnly = new Hashtable(), nonPublicAccessors = new Hashtable();

//    /// <summary>
//    /// Does this type support new instances via a parameterless constructor?
//    /// </summary>
//    public virtual bool CreateNewSupported { get { return false; } }
//    /// <summary>
//    /// Create a new instance of this type
//    /// </summary>
//    public virtual object CreateNew() { throw new NotSupportedException(); }

//    /// <summary>
//    /// Can this type be queried for member availability?
//    /// </summary>
//    public virtual bool GetMembersSupported { get { return false; } }
//    /// <summary>
//    /// Query the members available for this type
//    /// </summary>
//    public virtual MemberSet GetMembers() { throw new NotSupportedException(); }

//    /// <summary>
//    /// Provides a type-specific accessor, allowing by-name access for all objects of that type
//    /// </summary>
//    /// <remarks>The accessor is cached internally; a pre-existing accessor may be returned</remarks>
//    public static TypeAccessor Create(Type type)
//    {
//      return Create(type, false);
//    }

//    /// <summary>
//    /// Provides a type-specific accessor, allowing by-name access for all objects of that type
//    /// </summary>
//    /// <remarks>The accessor is cached internally; a pre-existing accessor may be returned</remarks>
//    public static TypeAccessor Create(Type type, bool allowNonPublicAccessors)
//    {
//      if (type == null) throw new ArgumentNullException("type");
//      var lookup = allowNonPublicAccessors ? nonPublicAccessors : publicAccessorsOnly;
//      TypeAccessor obj = (TypeAccessor)lookup[type];
//      if (obj != null) return obj;

//      lock (lookup)
//      {
//        // double-check
//        obj = (TypeAccessor)lookup[type];
//        if (obj != null) return obj;

//        obj = CreateNew(type, allowNonPublicAccessors);

//        lookup[type] = obj;
//        return obj;
//      }
//    }
//    sealed class DynamicAccessor : TypeAccessor
//    {
//      public static readonly DynamicAccessor Singleton = new DynamicAccessor();
//      private DynamicAccessor() { }
//      public override object this[object target, string name]
//      {
//        get { return CallSiteCache.GetValue(name, target); }
//        set { CallSiteCache.SetValue(name, target, value); }
//      }
//    }

//    private static AssemblyBuilder assembly;
//    private static ModuleBuilder module;
//    private static int counter;

//    private static int GetNextCounterValue()
//    {
//      return Interlocked.Increment(ref counter);
//    }

//    static readonly MethodInfo tryGetValue = typeof(Dictionary<string, int>).GetMethod("TryGetValue");
//    private static void WriteMapImpl(ILGenerator il, Type type, List<MemberInfo> members, FieldBuilder mapField, bool allowNonPublicAccessors, bool isGet)
//    {
//      OpCode obj, index, value;

//      Label fail = il.DefineLabel();
//      if (mapField == null)
//      {
//        index = OpCodes.Ldarg_0;
//        obj = OpCodes.Ldarg_1;
//        value = OpCodes.Ldarg_2;
//      }
//      else
//      {
//        il.DeclareLocal(typeof(int));
//        index = OpCodes.Ldloc_0;
//        obj = OpCodes.Ldarg_1;
//        value = OpCodes.Ldarg_3;

//        il.Emit(OpCodes.Ldarg_0);
//        il.Emit(OpCodes.Ldfld, mapField);
//        il.Emit(OpCodes.Ldarg_2);
//        il.Emit(OpCodes.Ldloca_S, (byte)0);
//        il.EmitCall(OpCodes.Callvirt, tryGetValue, null);
//        il.Emit(OpCodes.Brfalse, fail);
//      }
//      Label[] labels = new Label[members.Count];
//      for (int i = 0; i < labels.Length; i++)
//      {
//        labels[i] = il.DefineLabel();
//      }
//      il.Emit(index);
//      il.Emit(OpCodes.Switch, labels);
//      il.MarkLabel(fail);
//      il.Emit(OpCodes.Ldstr, "name");
//      il.Emit(OpCodes.Newobj, typeof(ArgumentOutOfRangeException).GetConstructor(new Type[] { typeof(string) }));
//      il.Emit(OpCodes.Throw);
//      for (int i = 0; i < labels.Length; i++)
//      {
//        il.MarkLabel(labels[i]);
//        var member = members[i];
//        bool isFail = true;

//        void WriteField(FieldInfo fieldToWrite)
//        {
//          if (!fieldToWrite.FieldType.IsByRef)
//          {
//            il.Emit(obj);
//            Cast(il, type, true);
//            if (isGet)
//            {
//              il.Emit(OpCodes.Ldfld, fieldToWrite);
//              if (fieldToWrite.FieldType.IsValueType) il.Emit(OpCodes.Box, fieldToWrite.FieldType);
//            }
//            else
//            {
//              il.Emit(value);
//              Cast(il, fieldToWrite.FieldType, false);
//              il.Emit(OpCodes.Stfld, fieldToWrite);
//            }
//            il.Emit(OpCodes.Ret);
//            isFail = false;
//          }
//        }
//        if (member is FieldInfo field)
//        {
//          WriteField(field);
//        }
//        else if (member is PropertyInfo prop)
//        {
//          var propType = prop.PropertyType;
//          bool isByRef = propType.IsByRef, isValid = true;
//          if (isByRef)
//          {
//            if (!isGet && prop.CustomAttributes.Any(x => x.AttributeType.FullName == "System.Runtime.CompilerServices.IsReadOnlyAttribute"))
//            {
//              isValid = false; // can't assign indirectly to ref-readonly
//            }
//            propType = propType.GetElementType(); // from "ref Foo" to "Foo"
//          }

//          var accessor = (isGet | isByRef) ? prop.GetGetMethod(allowNonPublicAccessors) : prop.GetSetMethod(allowNonPublicAccessors);
//          if (accessor == null && allowNonPublicAccessors && !isByRef)
//          {
//            // No getter/setter, use backing field instead if it exists
//            var backingField = $"<{prop.Name}>k__BackingField";
//            field = prop.DeclaringType?.GetField(backingField, BindingFlags.Instance | BindingFlags.NonPublic);

//            if (field != null)
//            {
//              WriteField(field);
//            }
//          }
//          else if (isValid && prop.CanRead && accessor != null)
//          {
//            il.Emit(obj);
//            Cast(il, type, true); // cast the input object to the right target type

//            if (isGet)
//            {
//              il.EmitCall(type.IsValueType ? OpCodes.Call : OpCodes.Callvirt, accessor, null);
//              if (isByRef) il.Emit(OpCodes.Ldobj, propType); // defererence if needed
//              if (propType.IsValueType) il.Emit(OpCodes.Box, propType); // box the value if needed
//            }
//            else
//            {
//              // when by-ref, we get the target managed pointer *first*, i.e. put obj.TheRef on the stack
//              if (isByRef) il.EmitCall(type.IsValueType ? OpCodes.Call : OpCodes.Callvirt, accessor, null);

//              // load the new value, and type it
//              il.Emit(value);
//              Cast(il, propType, false);

//              if (isByRef)
//              {   // assign to the managed pointer
//                il.Emit(OpCodes.Stobj, propType);
//              }
//              else
//              {   // call the setter
//                il.EmitCall(type.IsValueType ? OpCodes.Call : OpCodes.Callvirt, accessor, null);
//              }
//            }
//            il.Emit(OpCodes.Ret);
//            isFail = false;
//          }
//        }
//        if (isFail) il.Emit(OpCodes.Br, fail);
//      }
//    }

//    private static readonly MethodInfo strinqEquals = typeof(string).GetMethod("op_Equality", new Type[] { typeof(string), typeof(string) });

//    /// <summary>
//    /// A TypeAccessor based on a Type implementation, with available member metadata
//    /// </summary>
//    protected abstract class RuntimeTypeAccessor : TypeAccessor
//    {
//      /// <summary>
//      /// Returns the Type represented by this accessor
//      /// </summary>
//      protected abstract Type Type { get; }

//      /// <summary>
//      /// Can this type be queried for member availability?
//      /// </summary>
//      public override bool GetMembersSupported { get { return true; } }
//      private MemberSet members;
//      /// <summary>
//      /// Query the members available for this type
//      /// </summary>
//      public override MemberSet GetMembers()
//      {
//        return members ?? (members = new MemberSet(Type));
//      }
//    }
//    sealed class DelegateAccessor : RuntimeTypeAccessor
//    {
//      private readonly Dictionary<string, int> map;
//      private readonly Func<int, object, object> getter;
//      private readonly Action<int, object, object> setter;
//      private readonly Func<object> ctor;
//      private readonly Type type;
//      protected override Type Type
//      {
//        get { return type; }
//      }
//      public DelegateAccessor(Dictionary<string, int> map, Func<int, object, object> getter, Action<int, object, object> setter, Func<object> ctor, Type type)
//      {
//        this.map = map;
//        this.getter = getter;
//        this.setter = setter;
//        this.ctor = ctor;
//        this.type = type;
//      }
//      public override bool CreateNewSupported { get { return ctor != null; } }
//      public override object CreateNew()
//      {
//        return ctor != null ? ctor() : base.CreateNew();
//      }
//      public override object this[object target, string name]
//      {
//        get
//        {
//          int index;
//          if (map.TryGetValue(name, out index)) return getter(index, target);
//          else throw new ArgumentOutOfRangeException("name");
//        }
//        set
//        {
//          int index;
//          if (map.TryGetValue(name, out index)) setter(index, target, value);
//          else throw new ArgumentOutOfRangeException("name");
//        }
//      }
//    }
//    private static bool IsFullyPublic(Type type, PropertyInfo[] props, bool allowNonPublicAccessors)
//    {
//      while (type.IsNestedPublic) type = type.DeclaringType;
//      if (!type.IsPublic) return false;

//      if (allowNonPublicAccessors)
//      {
//        for (int i = 0; i < props.Length; i++)
//        {
//          if (props[i].GetGetMethod(true) != null && props[i].GetGetMethod(false) == null) return false; // non-public getter
//          if (props[i].GetSetMethod(true) != null && props[i].GetSetMethod(false) == null) return false; // non-public setter
//        }
//      }

//      return true;
//    }
//    static TypeAccessor CreateNew(Type type, bool allowNonPublicAccessors)
//    {
//      if (typeof(IDynamicMetaObjectProvider).IsAssignableFrom(type))
//      {
//        return DynamicAccessor.Singleton;
//      }

//      PropertyInfo[] props = type.GetTypeAndInterfaceProperties(BindingFlags.Public | BindingFlags.Instance);
//      FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
//      Dictionary<string, int> map = new Dictionary<string, int>();
//      List<MemberInfo> members = new List<MemberInfo>(props.Length + fields.Length);
//      int i = 0;
//      foreach (var prop in props)
//      {
//        if (!map.ContainsKey(prop.Name) && prop.GetIndexParameters().Length == 0)
//        {
//          map.Add(prop.Name, i++);
//          members.Add(prop);
//        }
//      }
//      foreach (var field in fields) if (!map.ContainsKey(field.Name)) { map.Add(field.Name, i++); members.Add(field); }

//      ConstructorInfo ctor = null;
//      if (type.IsClass && !type.IsAbstract)
//      {
//        ctor = type.GetConstructor(Type.EmptyTypes);
//      }
//      ILGenerator il;
//      if (!IsFullyPublic(type, props, allowNonPublicAccessors))
//      {
//        DynamicMethod dynGetter = new DynamicMethod(type.FullName + "_get", typeof(object), new Type[] { typeof(int), typeof(object) }, type, true),
//                      dynSetter = new DynamicMethod(type.FullName + "_set", null, new Type[] { typeof(int), typeof(object), typeof(object) }, type, true);
//        WriteMapImpl(dynGetter.GetILGenerator(), type, members, null, allowNonPublicAccessors, true);
//        WriteMapImpl(dynSetter.GetILGenerator(), type, members, null, allowNonPublicAccessors, false);
//        DynamicMethod dynCtor = null;
//        if (ctor != null)
//        {
//          dynCtor = new DynamicMethod(type.FullName + "_ctor", typeof(object), Type.EmptyTypes, type, true);
//          il = dynCtor.GetILGenerator();
//          il.Emit(OpCodes.Newobj, ctor);
//          il.Emit(OpCodes.Ret);
//        }
//        return new DelegateAccessor(
//            map,
//            (Func<int, object, object>)dynGetter.CreateDelegate(typeof(Func<int, object, object>)),
//            (Action<int, object, object>)dynSetter.CreateDelegate(typeof(Action<int, object, object>)),
//            dynCtor == null ? null : (Func<object>)dynCtor.CreateDelegate(typeof(Func<object>)), type);
//      }

//      // note this region is synchronized; only one is being created at a time so we don't need to stress about the builders
//      if (assembly == null)
//      {
//        AssemblyName name = new AssemblyName("FastMember_dynamic");
//        assembly = AssemblyBuilder.DefineDynamicAssembly(name, AssemblyBuilderAccess.Run);
//        module = assembly.DefineDynamicModule(name.Name);
//      }
//      TypeAttributes attribs = typeof(TypeAccessor).Attributes;
//      TypeBuilder tb = module.DefineType("FastMember_dynamic." + type.Name + "_" + GetNextCounterValue(),
//          (attribs | TypeAttributes.Sealed | TypeAttributes.Public) & ~(TypeAttributes.Abstract | TypeAttributes.NotPublic), typeof(RuntimeTypeAccessor));

//      il = tb.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new[] {
//                typeof(Dictionary<string,int>)
//            }).GetILGenerator();
//      il.Emit(OpCodes.Ldarg_0);
//      il.Emit(OpCodes.Ldarg_1);
//      FieldBuilder mapField = tb.DefineField("_map", typeof(Dictionary<string, int>), FieldAttributes.InitOnly | FieldAttributes.Private);
//      il.Emit(OpCodes.Stfld, mapField);
//      il.Emit(OpCodes.Ret);


//      PropertyInfo indexer = typeof(TypeAccessor).GetProperty("Item");
//      MethodInfo baseGetter = indexer.GetGetMethod(), baseSetter = indexer.GetSetMethod();
//      MethodBuilder body = tb.DefineMethod(baseGetter.Name, baseGetter.Attributes & ~MethodAttributes.Abstract, typeof(object), new Type[] { typeof(object), typeof(string) });
//      il = body.GetILGenerator();
//      WriteMapImpl(il, type, members, mapField, allowNonPublicAccessors, true);
//      tb.DefineMethodOverride(body, baseGetter);

//      body = tb.DefineMethod(baseSetter.Name, baseSetter.Attributes & ~MethodAttributes.Abstract, null, new Type[] { typeof(object), typeof(string), typeof(object) });
//      il = body.GetILGenerator();
//      WriteMapImpl(il, type, members, mapField, allowNonPublicAccessors, false);
//      tb.DefineMethodOverride(body, baseSetter);

//      MethodInfo baseMethod;
//      if (ctor != null)
//      {
//        baseMethod = typeof(TypeAccessor).GetProperty("CreateNewSupported").GetGetMethod();
//        body = tb.DefineMethod(baseMethod.Name, baseMethod.Attributes, baseMethod.ReturnType, Type.EmptyTypes);
//        il = body.GetILGenerator();
//        il.Emit(OpCodes.Ldc_I4_1);
//        il.Emit(OpCodes.Ret);
//        tb.DefineMethodOverride(body, baseMethod);

//        baseMethod = typeof(TypeAccessor).GetMethod("CreateNew");
//        body = tb.DefineMethod(baseMethod.Name, baseMethod.Attributes, baseMethod.ReturnType, Type.EmptyTypes);
//        il = body.GetILGenerator();
//        il.Emit(OpCodes.Newobj, ctor);
//        il.Emit(OpCodes.Ret);
//        tb.DefineMethodOverride(body, baseMethod);
//      }

//      baseMethod = typeof(RuntimeTypeAccessor).GetProperty("Type", BindingFlags.NonPublic | BindingFlags.Instance).GetGetMethod(true);
//      body = tb.DefineMethod(baseMethod.Name, baseMethod.Attributes & ~MethodAttributes.Abstract, baseMethod.ReturnType, Type.EmptyTypes);
//      il = body.GetILGenerator();
//      il.Emit(OpCodes.Ldtoken, type);
//      il.Emit(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle"));
//      il.Emit(OpCodes.Ret);
//      tb.DefineMethodOverride(body, baseMethod);

//      var accessor = (TypeAccessor)Activator.CreateInstance(tb.CreateTypeInfo().AsType(), map);
//      return accessor;
//    }

//    private static void Cast(ILGenerator il, Type type, bool valueAsPointer)
//    {
//      if (type == typeof(object)) { }
//      else if (type.IsValueType)
//      {
//        if (valueAsPointer)
//        {
//          il.Emit(OpCodes.Unbox, type);
//        }
//        else
//        {
//          il.Emit(OpCodes.Unbox_Any, type);
//        }
//      }
//      else
//      {
//        il.Emit(OpCodes.Castclass, type);
//      }
//    }

//    /// <summary>
//    /// Get or set the value of a named member on the target instance
//    /// </summary>
//    public abstract object this[object target, string name]
//    {
//      get;
//      set;
//    }
//  }

//  #endregion

//  #region ObjectAccessor

//  /// <summary>
//  /// Represents an individual object, allowing access to members by-name
//  /// </summary>
//  public abstract class ObjectAccessor
//  {
//    /// <summary>
//    /// Get or Set the value of a named member for the underlying object
//    /// </summary>
//    public abstract object this[string name] { get; set; }
//    /// <summary>
//    /// The object represented by this instance
//    /// </summary>
//    public abstract object Target { get; }
//    /// <summary>
//    /// Use the target types definition of equality
//    /// </summary>
//    public override bool Equals(object obj)
//    {
//      return Target.Equals(obj);
//    }
//    /// <summary>
//    /// Obtain the hash of the target object
//    /// </summary>
//    public override int GetHashCode()
//    {
//      return Target.GetHashCode();
//    }
//    /// <summary>
//    /// Use the target's definition of a string representation
//    /// </summary>
//    public override string ToString()
//    {
//      return Target.ToString();
//    }

//    /// <summary>
//    /// Wraps an individual object, allowing by-name access to that instance
//    /// </summary>
//    public static ObjectAccessor Create(object target)
//    {
//      return Create(target, false);
//    }
//    /// <summary>
//    /// Wraps an individual object, allowing by-name access to that instance
//    /// </summary>
//    public static ObjectAccessor Create(object target, bool allowNonPublicAccessors)
//    {
//      if (target == null) throw new ArgumentNullException("target");
//      IDynamicMetaObjectProvider dlr = target as IDynamicMetaObjectProvider;
//      if (dlr != null) return new DynamicWrapper(dlr); // use the DLR
//      return new TypeAccessorWrapper(target, TypeAccessor.Create(target.GetType(), allowNonPublicAccessors));
//    }
//    sealed class TypeAccessorWrapper : ObjectAccessor
//    {
//      private readonly object target;
//      private readonly TypeAccessor accessor;
//      public TypeAccessorWrapper(object target, TypeAccessor accessor)
//      {
//        this.target = target;
//        this.accessor = accessor;
//      }
//      public override object this[string name]
//      {
//        get { return accessor[target, name]; }
//        set { accessor[target, name] = value; }
//      }
//      public override object Target
//      {
//        get { return target; }
//      }
//    }
//    sealed class DynamicWrapper : ObjectAccessor
//    {
//      private readonly IDynamicMetaObjectProvider target;
//      public override object Target
//      {
//        get { return target; }
//      }
//      public DynamicWrapper(IDynamicMetaObjectProvider target)
//      {
//        this.target = target;
//      }
//      public override object this[string name]
//      {
//        get { return CallSiteCache.GetValue(name, target); }
//        set { CallSiteCache.SetValue(name, target, value); }
//      }

//    }
//  }

//  #endregion

//  #region OrdinalAttribute

//  /// <summary>
//  /// Emphasizes column position used in <see cref="System.Data.IDataReader"/> instance.
//  /// </summary>
//  [System.AttributeUsage(System.AttributeTargets.Property | System.AttributeTargets.Field, AllowMultiple = false)]
//  public class OrdinalAttribute : System.Attribute
//  {
//    /// <summary>
//    /// Creates a new instance of <see cref="OrdinalAttribute"/> class.
//    /// </summary>
//    /// <param name="ordinal"></param>
//    public OrdinalAttribute(ushort ordinal)
//    {
//      Ordinal = ordinal;
//    }

//    /// <summary>
//    /// Column ordinal used in <see cref="System.Data.IDataReader"/> instance.
//    /// </summary>
//    public ushort Ordinal { get; private set; }
//  }

//  #endregion
//}
