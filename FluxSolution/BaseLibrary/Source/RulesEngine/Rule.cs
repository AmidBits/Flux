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


    public string Name
      => m_name;

    public string Operator
      => m_operator;

    public object Value
      => m_value;


    public System.Func<T, bool> Compile<T>()
      => CompileRule<T>(this);

    #region Static methods

    public static System.Linq.Expressions.Expression BuildExpression<T>(Rule rule, System.Linq.Expressions.ParameterExpression parameterExpression)
    {
      var propertyType = typeof(T).GetProperty(rule.m_name)?.PropertyType ?? throw new System.ArgumentNullException(nameof(rule), $"There is no property named {rule.m_name} in the type.");
      var propertyExpression = System.Linq.Expressions.MemberExpression.Property(parameterExpression, rule.m_name);

      if (System.Linq.Expressions.ExpressionType.TryParse(rule.m_operator, out System.Linq.Expressions.ExpressionType expressionType))
      {
        var constantExpression = System.Linq.Expressions.Expression.Constant(System.Convert.ChangeType(rule.m_value, propertyType, null) ?? throw new System.ArgumentNullException(nameof(rule), $"The rule value cannot be converted to {propertyType.Name}."));

        return System.Linq.Expressions.Expression.MakeBinary(expressionType, propertyExpression, constantExpression);
      }
      else // Could not parse the operator, so we'll try to get 
      {
        var methodInfo = propertyType.GetMethods().Where(mi => mi.Name == rule.m_operator && mi.GetParameters() is var pi && pi.Length == 1 && pi[0].ParameterType.GetType().IsAssignableFrom(typeof(T).GetType())).First();
        var parameterType = methodInfo.GetParameters()[0].ParameterType;

        var constantExpression = System.Linq.Expressions.Expression.Constant(System.Convert.ChangeType(rule.m_value, parameterType, null) ?? throw new System.ArgumentNullException(nameof(rule), $"The rule value cannot be converted to {propertyType.Name}."));

        return System.Linq.Expressions.Expression.Call(propertyExpression, methodInfo, constantExpression);
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
      => m_name == other.m_name && m_operator == other.m_operator && m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides

    public override bool Equals(object? obj)
      => obj is Rule o && Equals(o);

    public override int GetHashCode()
      => System.HashCode.Combine(m_name, m_operator, m_value);

    public override string? ToString()
      => $"{nameof(Rule)} {{ \"{m_name}\" {m_operator} '{m_value}' }}";
    #endregion Object overrides
  }
}
