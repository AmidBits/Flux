//namespace Flux
//{
//  public static partial class Types
//  {
//    /// <summary>Creates a new sequence with the derived types of the <paramref name="source"/>.</summary>
//    public static System.Collections.Generic.IEnumerable<System.Type> GetDerived(this System.Type source)
//    {
//      var list = new System.Collections.Generic.List<System.Type>();

//      foreach (var inheritanceType in GetInheritance(source))
//      {
//        foreach (var implementsType in GetImplements(inheritanceType))
//          if (!list.Contains(implementsType))
//            list.Add(implementsType);

//        list.Add(inheritanceType);
//      }

//      return list;
//    }

//    /// <summary>Creates a new sequence with implemented interfaces of the <paramref name="source"/>.</summary>
//    public static System.Collections.Generic.IEnumerable<System.Type> GetImplements(this System.Type source)
//    {
//      if (source is null) throw new System.ArgumentNullException(nameof(source));

//      return new System.Collections.Generic.Stack<System.Type>(source.GetInterfaces());
//    }

//    /// <summary>Creates a new sequence with the inheritance type chain of the <paramref name="source"/>.</summary>
//    public static System.Collections.Generic.IEnumerable<System.Type> GetInheritance(this System.Type source)
//    {
//      var stack = new System.Collections.Generic.Stack<System.Type>();

//      for (var type = source; type != null; type = type.BaseType)
//        stack.Push(type);

//      return stack;
//    }
//  }
//}
