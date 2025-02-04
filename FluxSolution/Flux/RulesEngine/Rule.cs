namespace Flux.RulesEngine
{
  public readonly record struct Rule
  {
    private readonly string m_name;
    private readonly string m_operator;
    private readonly object m_value;

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

    public string Name => m_name;
    public string Operator => m_operator;
    public object Value => m_value;

    public System.Func<T, bool> Compile<T>() => CompileRule<T>(this);

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

    #endregion // Static methods

    public override string? ToString()
      => $"{nameof(Rule)} {{ \"{m_name}\" {m_operator} '{m_value}' }}";
  }
}
