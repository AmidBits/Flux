namespace Flux.RulesEngine.Example
{
  public class User
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
      => $"<{nameof(User)}: {Name}, {Age} ({BirthCountry})>";

    public static void ShowCase()
    {
      var rules = new RulesDictionary
      {
        { "AgeLimit", new Rule("Age", "GreaterThan", 20) },
        { "NameRequirement", new Rule("Name", "Equal", "John") },
        { "CountryOfBirth", new Rule("BirthCountry", "Equal", "Canada") }
      };

      var rulesCompiled = rules.CompileRules<User>();
      foreach (var rule in rules)
        System.Console.WriteLine(rule);

      var user1 = new User(43, "Royi", "Australia");
      var user2 = new User(33, "John", "England");
      var user3 = new User(23, "John", "Canada");

      System.Console.WriteLine(rulesCompiled.EvaluateRules(user1).ToConsoleString());
      System.Console.WriteLine(rulesCompiled.EvaluateRules(user2).ToConsoleString());
      System.Console.WriteLine(rulesCompiled.EvaluateRules(user3).ToConsoleString());
    }
  }
}
