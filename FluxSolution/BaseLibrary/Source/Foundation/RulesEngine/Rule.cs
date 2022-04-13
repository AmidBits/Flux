using System.Linq;
using System.Reflection;

namespace Flux.RulesEngine
{
  public struct Rule
    : System.IEquatable<Rule>
  {
    public static readonly Rule Empty;

    private readonly string m_name { get; }
    private readonly string m_operator { get; }
    private readonly object m_value { get; }

    public Rule(string name, string @operator, string value)
    {
      m_name = name;
      m_operator = @operator;
      m_value = value;
    }
    [System.CLSCompliant(false)]
    public Rule(string name, string @operator, System.IConvertible value)
    {
      m_name = name;
      m_operator = @operator;
      m_value = value;
    }

    [System.Diagnostics.Contracts.Pure]
    public string Name
      => m_name;
    [System.Diagnostics.Contracts.Pure]
    public string Operator 
      => m_operator;
    [System.Diagnostics.Contracts.Pure]
    public object Value 
      => m_value;

    [System.Diagnostics.Contracts.Pure]
    public System.Func<T, bool> Compile<T>()
      => CompileRule<T>(this);

    #region Static methods
    [System.Diagnostics.Contracts.Pure]
    public static System.Linq.Expressions.Expression BuildExpression<T>(Rule rule, System.Linq.Expressions.ParameterExpression parameterExpression)
    {
      var meProperty = System.Linq.Expressions.MemberExpression.Property(parameterExpression, rule.m_name);
      var propertyType = typeof(T).GetProperty(rule.m_name)?.PropertyType ?? throw new System.ArgumentNullException(nameof(rule), $"There is no property named {rule.m_name} in the type.");

      if (System.Linq.Expressions.ExpressionType.TryParse(rule.m_operator, out System.Linq.Expressions.ExpressionType expressionType))
      {
        var ceRight = System.Linq.Expressions.Expression.Constant(System.Convert.ChangeType(rule.m_value, propertyType, null) ?? throw new System.ArgumentNullException(nameof(rule), $"The rule value cannot be converted to {propertyType.Name}."));

        return System.Linq.Expressions.Expression.MakeBinary(expressionType, meProperty, ceRight);
      }
      else
      {
        var methodInfo = propertyType.GetMethods().Where(mi => mi.Name == rule.m_operator && mi.GetParameters() is var pi && pi.Length == 1 && pi[0].ParameterType.GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo())).First();
        var parameterType = methodInfo.GetParameters()[0].ParameterType;

        var ceArgument = System.Linq.Expressions.Expression.Constant(System.Convert.ChangeType(rule.m_value, parameterType, null) ?? throw new System.ArgumentNullException(nameof(rule), $"The rule value cannot be converted to {propertyType.Name}."));

        return System.Linq.Expressions.Expression.Call(meProperty, methodInfo, ceArgument);
      }
    }

    [System.Diagnostics.Contracts.Pure]
    public static System.Func<T, bool> CompileRule<T>(Rule rule)
    {
      var parameterExpression = System.Linq.Expressions.Expression.Parameter(typeof(T));
      var expression = BuildExpression<T>(rule, parameterExpression);

      return System.Linq.Expressions.Expression.Lambda<System.Func<T, bool>>(expression, parameterExpression).Compile();
    }
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure]
    public static bool operator ==(Rule a, Rule b)
      => a.Equals(b);
    [System.Diagnostics.Contracts.Pure]
    public static bool operator !=(Rule a, Rule b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // System.IEquatable<Rule>
    [System.Diagnostics.Contracts.Pure]
    public bool Equals(Rule other)
      => m_name == other.m_name && m_operator == other.m_operator && m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure]
    public override bool Equals(object? obj)
      => obj is Rule o && Equals(o);
    [System.Diagnostics.Contracts.Pure]
    public override int GetHashCode()
      => System.HashCode.Combine(m_name, m_operator, m_value);
    [System.Diagnostics.Contracts.Pure]
    public override string? ToString()
      => $"{nameof(Rule)} {{ \"{m_name}\" {m_operator} '{m_value}' }}";
    #endregion Object overrides
  }
}
