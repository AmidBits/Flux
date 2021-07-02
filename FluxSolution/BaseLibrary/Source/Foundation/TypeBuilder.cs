namespace Flux
{
  public static class TypeBuilder
  {
    private static void AddConstructor(System.Reflection.Emit.TypeBuilder typeBuilder)
    {
      typeBuilder.DefineDefaultConstructor(System.Reflection.MethodAttributes.Public | System.Reflection.MethodAttributes.SpecialName | System.Reflection.MethodAttributes.RTSpecialName);
    }

    public static void AddProperty(System.Reflection.Emit.TypeBuilder typeBuilder, string name, System.Type type, bool isNullable)
    {
      if (typeBuilder is null) throw new System.ArgumentNullException(nameof(typeBuilder));
      if (name is null) throw new System.ArgumentNullException(nameof(name));
      if (type is null) throw new System.ArgumentNullException(nameof(type));

      var propertyName = name;

      if (isNullable && (type.IsValueType || type.IsGenericType)) // This is not needed if the type is a reference type, since reference types can always be null.
      {
        type = typeof(System.Nullable<>).MakeGenericType(type);
      }

      var fieldBuilder = typeBuilder.DefineField(@"_" + propertyName, type, System.Reflection.FieldAttributes.Private);

      var propertyBuilder = typeBuilder.DefineProperty(propertyName, System.Reflection.PropertyAttributes.HasDefault, type, null);

      var getPropertyMethodBuilder = typeBuilder.DefineMethod(@"get_" + propertyName, System.Reflection.MethodAttributes.Public | System.Reflection.MethodAttributes.SpecialName | System.Reflection.MethodAttributes.HideBySig, type, System.Type.EmptyTypes);

      var gilg = getPropertyMethodBuilder.GetILGenerator();

      gilg.Emit(System.Reflection.Emit.OpCodes.Ldarg_0);
      gilg.Emit(System.Reflection.Emit.OpCodes.Ldfld, fieldBuilder);
      gilg.Emit(System.Reflection.Emit.OpCodes.Ret);

      var setPropertyMethodBuilder = typeBuilder.DefineMethod(@"set_" + propertyName, System.Reflection.MethodAttributes.Public | System.Reflection.MethodAttributes.SpecialName | System.Reflection.MethodAttributes.HideBySig, null, new[] { type });

      var silg = setPropertyMethodBuilder.GetILGenerator();

      var modifyProperty = silg.DefineLabel();
      var exitSet = silg.DefineLabel();

      silg.MarkLabel(modifyProperty);
      silg.Emit(System.Reflection.Emit.OpCodes.Ldarg_0);
      silg.Emit(System.Reflection.Emit.OpCodes.Ldarg_1);
      silg.Emit(System.Reflection.Emit.OpCodes.Stfld, fieldBuilder);

      silg.Emit(System.Reflection.Emit.OpCodes.Nop);
      silg.MarkLabel(exitSet);
      silg.Emit(System.Reflection.Emit.OpCodes.Ret);

      propertyBuilder.SetGetMethod(getPropertyMethodBuilder);
      propertyBuilder.SetSetMethod(setPropertyMethodBuilder);
    }

    public static object? GetPropertyValue(object instance, string propertyName)
    {
      if (instance is null) throw new System.ArgumentNullException(nameof(instance));
      if (propertyName is null) throw new System.ArgumentNullException(nameof(propertyName));

      return instance.GetType().GetProperty(propertyName)?.GetValue(instance);
    }

    public static object? GetPropertyValue(object instance, string propertyName, object[] index)
    {
      if (instance is null) throw new System.ArgumentNullException(nameof(instance));
      if (propertyName is null) throw new System.ArgumentNullException(nameof(propertyName));

      return instance.GetType().GetProperty(propertyName)?.GetValue(instance, index);
    }

    public static void SetPropertyValue(object instance, string propertyName, object propertyValue)
    {
      if (instance is null) throw new System.ArgumentNullException(nameof(instance));
      if (propertyName is null) throw new System.ArgumentNullException(nameof(propertyName));

      instance.GetType().GetProperty(propertyName)?.SetValue(instance, propertyValue);
    }

    public static System.Reflection.Emit.TypeBuilder Create(string className)
    {
      var an = new System.Reflection.AssemblyName(className);

      var ab = System.Reflection.Emit.AssemblyBuilder.DefineDynamicAssembly(an, System.Reflection.Emit.AssemblyBuilderAccess.Run);
      var mb = ab.DefineDynamicModule(an.Name ?? className);

      var typeBuilder = mb.DefineType(an.Name ?? className, System.Reflection.TypeAttributes.Public | System.Reflection.TypeAttributes.Class | System.Reflection.TypeAttributes.AutoClass | System.Reflection.TypeAttributes.AnsiClass | System.Reflection.TypeAttributes.BeforeFieldInit | System.Reflection.TypeAttributes.AutoLayout, null);

      typeBuilder.DefineDefaultConstructor(System.Reflection.MethodAttributes.Public | System.Reflection.MethodAttributes.SpecialName | System.Reflection.MethodAttributes.RTSpecialName);

      return typeBuilder;
    }
  }
}
