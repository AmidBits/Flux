using System.Linq;
using System.Reflection;

namespace Flux.RulesEngine
{
  public struct Rule
    : System.IEquatable<Rule>
  {
    public static readonly Rule Empty;

    public string Name { get; }
    public string Operator { get; }
    public object Value { get; }

    public Rule(string name, string @operator, string value)
    {
      Name = name;
      Operator = @operator;
      Value = value;
    }
    [System.CLSCompliant(false)]
    public Rule(string name, string @operator, System.IConvertible value)
    {
      Name = name;
      Operator = @operator;
      Value = value;
    }

    public System.Func<T, bool> Compile<T>()
      => CompileRule<T>(this);

    #region Static methods
    public static System.Linq.Expressions.Expression BuildExpression<T>(Rule rule, System.Linq.Expressions.ParameterExpression parameterExpression)
    {
      var meProperty = System.Linq.Expressions.MemberExpression.Property(parameterExpression, rule.Name);
      var propertyType = typeof(T).GetProperty(rule.Name)?.PropertyType ?? throw new System.ArgumentNullException(nameof(rule), $"There is no property named {rule.Name} in the type.");

      if (System.Linq.Expressions.ExpressionType.TryParse(rule.Operator, out System.Linq.Expressions.ExpressionType expressionType))
      {
        var ceRight = System.Linq.Expressions.Expression.Constant(System.Convert.ChangeType(rule.Value, propertyType, null) ?? throw new System.ArgumentNullException(nameof(rule), $"The rule value cannot be converted to {propertyType.Name}."));

        return System.Linq.Expressions.Expression.MakeBinary(expressionType, meProperty, ceRight);
      }
      else
      {
        var methodInfo = propertyType.GetMethods().Where(mi => mi.Name == rule.Operator && mi.GetParameters() is var pi && pi.Length == 1 && pi[0].ParameterType.GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo())).First();
        var parameterType = methodInfo.GetParameters()[0].ParameterType;

        var ceArgument = System.Linq.Expressions.Expression.Constant(System.Convert.ChangeType(rule.Value, parameterType, null) ?? throw new System.ArgumentNullException(nameof(rule), $"The rule value cannot be converted to {propertyType.Name}."));

        return System.Linq.Expressions.Expression.Call(meProperty, methodInfo, ceArgument);
      }
    }

    public static System.Func<T, bool> CompileRule<T>(Rule rule)
    {
      var parameterExpression = System.Linq.Expressions.Expression.Parameter(typeof(T));
      var expression = BuildExpression<T>(rule, parameterExpression);

      return System.Linq.Expressions.Expression.Lambda<System.Func<T, bool>>(expression, parameterExpression).Compile();
    }
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(Rule a, Rule b)
      => a.Equals(b);
    public static bool operator !=(Rule a, Rule b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // System.IEquatable<Rule>
    public bool Equals(Rule other)
      => Name == other.Name && Operator == other.Operator && Value == other.Value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Rule o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Name, Operator, Value);
    public override string? ToString()
      => $"{nameof(Rule)} {{ \"{Name}\" {Operator} '{Value}' }}";
    #endregion Object overrides
  }
}
