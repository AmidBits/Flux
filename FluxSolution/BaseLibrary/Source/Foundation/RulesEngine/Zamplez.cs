#if ZAMPLEZ
namespace Flux
{
  public static partial class Zamplez
  {
    /// <summary>Run the rules engine zample.</summary>
    public static void RunRulesEngine()
    {
      System.Console.WriteLine();
      System.Console.WriteLine(nameof(RunRulesEngine));
      System.Console.WriteLine();

      User.ShowCase();

      System.Console.WriteLine();
    }

    private sealed class User
    {
      public int Age { get; set; }
      public string Name { get; set; }
      public string BirthCountry { get; set; }

      public User(int age, string name, string birthCountry)
      {
        Age = age;
        Name = name;
        BirthCountry = birthCountry;
      }

      public override string ToString()
        => $"{GetType().Name} {{ {Name}, {Age} ({BirthCountry}) }}";

      public static void ShowCase()
      {
        var rules = new RulesEngine.RulesDictionary
        {
          { "AgeLimit", new RulesEngine.Rule(nameof(Age), nameof(System.Linq.Expressions.BinaryExpression.GreaterThan), 20) },
          { "NameRequirement", new RulesEngine.Rule(nameof(Name), nameof(System.Linq.Expressions.BinaryExpression.Equal), "John") },
          { "CountryOfBirth", new RulesEngine.Rule(nameof(BirthCountry), nameof(System.Linq.Expressions.BinaryExpression.Equal), "Canada") }
        };

        foreach (var rule in rules)
          System.Console.WriteLine(rule);

        var user1 = new User(43, "Royi", "Australia");
        var user2 = new User(33, "John", "England");
        var user3 = new User(23, "John", "Canada");

        var rulesCompiled = rules.CompileRules<User>();
        System.Console.WriteLine($"{user1}, {rulesCompiled.EvaluateRules(user1).ToConsoleString(verticalSeparator: ", ")}");
        System.Console.WriteLine($"{user2}, {rulesCompiled.EvaluateRules(user2).ToConsoleString(verticalSeparator: ", ")}");
        System.Console.WriteLine($"{user3}, {rulesCompiled.EvaluateRules(user3).ToConsoleString(verticalSeparator: ", ")}");
      }
    }
  }
}
#endif
