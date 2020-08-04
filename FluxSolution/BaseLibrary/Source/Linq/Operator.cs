//namespace Flux
//{
//  public static class Operator<T>
//  {
//    /// <summary>Returns the zero value for value-types (even full Nullable<T>), or null for reference types.</summary>
//    public static T Zero { get; private set; }

//    public static System.Func<T, T, T> Add { get; private set; }
//    public static System.Func<T, T, T> Divide { get; private set; }
//    public static System.Func<T, T, T> Multiply { get; private set; }
//    public static System.Func<T, T, T> Subtract { get; private set; }

//    public static System.Func<T, T, T> And { get; private set; }
//    public static System.Func<T, T> Negate { get; private set; }
//    public static System.Func<T, T> Not { get; private set; }
//    public static System.Func<T, T, T> Or { get; private set; }
//    public static System.Func<T, T, T> Xor { get; private set; }

//    public static System.Func<T, T, bool> Equal { get; private set; }
//    public static System.Func<T, T, bool> GreaterThan { get; private set; }
//    public static System.Func<T, T, bool> GreaterThanOrEqual { get; private set; }
//    public static System.Func<T, T, bool> LessThan { get; private set; }
//    public static System.Func<T, T, bool> LessThanOrEqual { get; private set; }
//    public static System.Func<T, T, bool> NotEqual { get; private set; }

//    //public static System.Func<bool, T, T> Conditional { get; private set; }

//    static Operator()
//    {
//      Zero = (typeof(T) is var type && type.IsValueType && type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(System.Nullable<>))) ? (T)System.Activator.CreateInstance(type.GetGenericArguments()[0]) : default!;

//      Add = Linq.CreateExpressionBinary<T, T, T>(System.Linq.Expressions.Expression.Add);
//      Divide = Linq.CreateExpressionBinary<T, T, T>(System.Linq.Expressions.Expression.Divide);
//      Multiply = Linq.CreateExpressionBinary<T, T, T>(System.Linq.Expressions.Expression.Multiply);
//      Subtract = Linq.CreateExpressionBinary<T, T, T>(System.Linq.Expressions.Expression.Subtract);

//      And = Linq.CreateExpressionBinary<T, T, T>(System.Linq.Expressions.Expression.And);
//      Negate = Linq.CreateExpressionUnary<T, T>(System.Linq.Expressions.Expression.Negate);
//      Not = Linq.CreateExpressionUnary<T, T>(System.Linq.Expressions.Expression.Not);
//      Or = Linq.CreateExpressionBinary<T, T, T>(System.Linq.Expressions.Expression.Or);
//      Xor = Linq.CreateExpressionBinary<T, T, T>(System.Linq.Expressions.Expression.ExclusiveOr);

//      Equal = Linq.CreateExpressionBinary<T, T, bool>(System.Linq.Expressions.Expression.Equal);
//      GreaterThan = Linq.CreateExpressionBinary<T, T, bool>(System.Linq.Expressions.Expression.GreaterThan);
//      GreaterThanOrEqual = Linq.CreateExpressionBinary<T, T, bool>(System.Linq.Expressions.Expression.GreaterThanOrEqual);
//      LessThan = Linq.CreateExpressionBinary<T, T, bool>(System.Linq.Expressions.Expression.LessThan);
//      LessThanOrEqual = Linq.CreateExpressionBinary<T, T, bool>(System.Linq.Expressions.Expression.LessThanOrEqual);
//      NotEqual = Linq.CreateExpressionBinary<T, T, bool>(System.Linq.Expressions.Expression.NotEqual);

//      //Conditional = Linq.CreateExpressionBinary<bool, T, T>(System.Linq.Expressions.Expression.IfThenElse)
//    }
//  }
//}
