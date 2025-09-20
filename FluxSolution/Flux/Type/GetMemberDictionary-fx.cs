//namespace Flux
//{
//  public static partial class Fx
//  {
//    /// <summary>
//    /// <para></para>
//    /// </summary>
//    /// <param name="source"></param>
//    /// <param name="instanceOrStatic">Pass null for static values.</param>
//    /// <param name="bindingFlags"></param>
//    /// <returns></returns>
//    public static System.Collections.Generic.IDictionary<System.Reflection.MemberInfo, object?> GetMemberDictionary(this System.Type source, object? instanceOrStatic = null, System.Reflection.BindingFlags bindingFlags = System.Reflection.BindingFlags.FlattenHierarchy | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
//    {
//      var members = new DataStructures.OrderedDictionary<System.Reflection.MemberInfo, object?>();

//      foreach (var mi in source.GetMembers(bindingFlags))
//      {
//        object? value = null;

//        if (mi is System.Reflection.FieldInfo fi)
//        {
//          if (fi.IsStatic)
//            value = fi.GetValue(null); // Get the static field value.
//          else if (instanceOrStatic is not null)
//            value = fi.GetValue(instanceOrStatic); // Get the field instance value.
//        }

//        if (mi is System.Reflection.PropertyInfo pi)
//        {
//          if (pi.GetMethod?.IsStatic ?? false)
//            value = pi.GetValue(null); // Get the static property value.
//          else if (instanceOrStatic is not null)
//            value = pi.GetValue(instanceOrStatic); // Get the property instance value.
//        }

//        if (value is not null)
//          members.Add(mi, value); // Add only when there is a value.
//      }

//      return members;
//    }
//  }
//}
